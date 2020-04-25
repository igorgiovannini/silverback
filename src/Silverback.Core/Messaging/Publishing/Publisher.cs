﻿// Copyright (c) 2020 Sergio Aquilini
// This code is licensed under MIT license (see LICENSE file for details)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Silverback.Messaging.Subscribers;
using Silverback.Util;

namespace Silverback.Messaging.Publishing
{
    /// <inheritdoc cref="IPublisher" />
    public class Publisher : IPublisher
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        private IReadOnlyCollection<IBehavior>? _behaviors;
        private SubscribedMethodInvoker? _methodInvoker;
        private SubscribedMethodsLoader? _methodsLoader;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Publisher" /> class.
        /// </summary>
        /// <param name="serviceProvider">
        ///     The <see cref="IServiceProvider" /> instance to be used to resolve the subscribers.
        /// </param>
        /// <param name="logger">
        ///     The <see cref="ILogger{TCategoryName}" /> instance.
        /// </param>
        public Publisher(IServiceProvider serviceProvider, ILogger<Publisher> logger)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            _logger = logger;
        }

        /// <inheritdoc />
        public void Publish(object message) =>
            Publish(new[] { message });

        /// <inheritdoc />
        public Task PublishAsync(object message) =>
            PublishAsync(new[] { message });

        /// <inheritdoc />
        public IReadOnlyCollection<TResult> Publish<TResult>(object message) =>
            Publish<TResult>(new[] { message });

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<TResult>> PublishAsync<TResult>(object message) =>
            await PublishAsync<TResult>(new[] { message });

        /// <inheritdoc />
        public void Publish(IEnumerable<object> messages) =>
            Publish(messages.ToList(), false).Wait();

        /// <inheritdoc />
        public Task PublishAsync(IEnumerable<object> messages) =>
            Publish(messages.ToList(), true);

        /// <inheritdoc />
        public IReadOnlyCollection<TResult> Publish<TResult>(IEnumerable<object> messages) =>
            CastResults<TResult>(Publish(messages.ToList(), false).Result).ToList();

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<TResult>> PublishAsync<TResult>(IEnumerable<object> messages) =>
            CastResults<TResult>(await Publish(messages.ToList(), true)).ToList();

        private IEnumerable<TResult> CastResults<TResult>(IReadOnlyCollection<object> results)
        {
            foreach (var result in results)
            {
                if (result is TResult castResult)
                {
                    yield return castResult;
                }
                else
                {
                    _logger.LogDebug(
                        $"Discarding result of type {result.GetType().FullName} because it doesn't match " +
                        $"the expected return type {typeof(TResult).FullName}.");
                }
            }
        }

        private async Task<IReadOnlyCollection<object>> Publish(IReadOnlyCollection<object> messages, bool executeAsync)
        {
            var messagesList = messages?.ToList();

            if (messagesList == null || !messagesList.Any())
                return Array.Empty<object>();

            return await ExecutePipeline(
                GetBehaviors(),
                messagesList,
                async m =>
                    (await InvokeExclusiveMethods(m, executeAsync))
                    .Union(await InvokeNonExclusiveMethods(m, executeAsync))
                    .ToList());
        }

        private Task<IReadOnlyCollection<object>> ExecutePipeline(
            IReadOnlyCollection<IBehavior> behaviors,
            IReadOnlyCollection<object> messages,
            Func<IReadOnlyCollection<object>, Task<IReadOnlyCollection<object>>> finalAction)
        {
            if (behaviors != null && behaviors.Any())
            {
                return behaviors.First().Handle(
                    messages,
                    nextMessages => ExecutePipeline(behaviors.Skip(1).ToList(), nextMessages, finalAction));
            }

            return finalAction(messages);
        }

        private async Task<IReadOnlyCollection<object>> InvokeExclusiveMethods(
            IReadOnlyCollection<object> messages,
            bool executeAsync) =>
            (await GetMethodsLoader().GetSubscribedMethods()
                .Where(method => method.IsExclusive)
                .SelectManyAsync(
                    method =>
                        GetMethodInvoker().Invoke(method, messages, executeAsync)))
            .ToList();

        private async Task<IReadOnlyCollection<object>> InvokeNonExclusiveMethods(
            IReadOnlyCollection<object> messages,
            bool executeAsync) =>
            (await GetMethodsLoader().GetSubscribedMethods()
                .Where(method => !method.IsExclusive)
                .ParallelSelectManyAsync(
                    method =>
                        GetMethodInvoker().Invoke(method, messages, executeAsync)))
            .ToList();

        private IReadOnlyCollection<IBehavior> GetBehaviors() =>
            _behaviors ??= _serviceProvider.GetServices<IBehavior>().SortBySortIndex().ToList();

        private SubscribedMethodInvoker GetMethodInvoker() =>
            _methodInvoker ??= _serviceProvider.GetRequiredService<SubscribedMethodInvoker>();

        private SubscribedMethodsLoader GetMethodsLoader() =>
            _methodsLoader ??= _serviceProvider.GetRequiredService<SubscribedMethodsLoader>();
    }
}
