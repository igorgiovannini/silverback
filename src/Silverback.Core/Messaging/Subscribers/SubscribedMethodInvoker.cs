﻿// Copyright (c) 2020 Sergio Aquilini
// This code is licensed under MIT license (see LICENSE file for details)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Silverback.Messaging.Messages;
using Silverback.Messaging.Subscribers.ArgumentResolvers;
using Silverback.Messaging.Subscribers.ReturnValueHandlers;
using Silverback.Util;

namespace Silverback.Messaging.Subscribers
{
    internal class SubscribedMethodInvoker
    {
        private readonly ArgumentsResolver _argumentsResolver;
        private readonly ReturnValueHandler _returnValueHandler;
        private readonly IServiceProvider _serviceProvider;

        public SubscribedMethodInvoker(
            ArgumentsResolver argumentsResolver,
            ReturnValueHandler returnValueHandler,
            IServiceProvider serviceProvider)
        {
            _argumentsResolver = argumentsResolver ?? throw new ArgumentNullException(nameof(argumentsResolver));
            _returnValueHandler = returnValueHandler ?? throw new ArgumentNullException(nameof(returnValueHandler));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public async Task<IEnumerable<object>> Invoke(
            SubscribedMethod subscribedMethod,
            IReadOnlyCollection<object> messages,
            bool executeAsync)
        {
            var (messageArgumentResolver, targetMessageType) =
                _argumentsResolver.GetMessageArgumentResolver(subscribedMethod);

            if (messageArgumentResolver == null)
                return Array.Empty<object>();

            messages = UnwrapEnvelopesAndFilterMessages(messages, targetMessageType, subscribedMethod);

            if (!messages.Any())
                return Array.Empty<object>();

            var target = subscribedMethod.ResolveTargetType(_serviceProvider);
            var parameterValues = GetShiftedParameterValuesArray(subscribedMethod);

            IReadOnlyCollection<object> returnValues;

            switch (messageArgumentResolver)
            {
                case ISingleMessageArgumentResolver singleResolver:
                    returnValues = (await messages
                            .OfType(targetMessageType)
                            .SelectAsync(
                                message =>
                                {
                                    parameterValues[0] = singleResolver.GetValue(message);
                                    return Invoke(target, subscribedMethod, parameterValues, executeAsync);
                                },
                                subscribedMethod.IsParallel,
                                subscribedMethod.MaxDegreeOfParallelism))
                        .ToList();
                    break;
                case IEnumerableMessageArgumentResolver enumerableResolver:
                    parameterValues[0] = enumerableResolver.GetValue(messages, targetMessageType);

                    returnValues = new[] { await Invoke(target, subscribedMethod, parameterValues, executeAsync) };
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return await _returnValueHandler.HandleReturnValues(returnValues, executeAsync);
        }

        private static IReadOnlyCollection<object> UnwrapEnvelopesAndFilterMessages(
            IEnumerable<object> messages,
            Type targetMessageType,
            SubscribedMethod subscribedMethod) =>
            UnwrapEnvelopesIfNeeded(
                    ApplyFilters(subscribedMethod.Filters, messages),
                    targetMessageType)
                .Where(message => message != null)
                .OfType(targetMessageType)
                .ToList();

        private static IEnumerable<object> UnwrapEnvelopesIfNeeded(
            IEnumerable<object> messages,
            Type targetMessageType) =>
            typeof(IEnvelope).IsAssignableFrom(targetMessageType)
                ? messages
                : messages.Select(message => message is IEnvelope envelope
                    ? envelope.AutoUnwrap ? envelope.Message : null
                    : message);

        private static IEnumerable<object> ApplyFilters(
            IReadOnlyCollection<IMessageFilter> filters,
            IEnumerable<object> messages) =>
            messages.Where(message => filters.All(filter => filter.MustProcess(message)));

        private object[] GetShiftedParameterValuesArray(SubscribedMethod methodInfo) =>
            new object[1].Concat(
                    _argumentsResolver.GetAdditionalParameterValues(methodInfo))
                .ToArray();

        private Task<object> Invoke(object target, SubscribedMethod method, object[] parameters, bool executeAsync) =>
            executeAsync
                ? InvokeAsync(target, method, parameters)
                : Task.FromResult(InvokeSync(target, method, parameters));

        private object InvokeSync(object target, SubscribedMethod method, object[] parameters)
        {
            if (!method.MethodInfo.ReturnsTask())
                return method.MethodInfo.Invoke(target, parameters);

            return AsyncHelper.RunSynchronously(() =>
            {
                var result = (Task) method.MethodInfo.Invoke(target, parameters);
                return result.GetReturnValue();
            });
        }

        private Task<object> InvokeAsync(object target, SubscribedMethod method, object[] parameters)
        {
            if (!method.MethodInfo.ReturnsTask())
                return Task.Run(() => method.MethodInfo.Invoke(target, parameters));

            var result = method.MethodInfo.Invoke(target, parameters);
            return ((Task) result).GetReturnValue();
        }
    }
}