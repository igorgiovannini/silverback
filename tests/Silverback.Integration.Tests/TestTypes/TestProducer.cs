﻿// Copyright (c) 2020 Sergio Aquilini
// This code is licensed under MIT license (see LICENSE file for details)

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Silverback.Diagnostics;
using Silverback.Messaging.Broker;
using Silverback.Messaging.Broker.Behaviors;
using Silverback.Messaging.Messages;
using Silverback.Tests.Types;
using Silverback.Util;

namespace Silverback.Tests.Integration.TestTypes
{
    public class TestProducer : Producer<TestBroker, TestProducerEndpoint>
    {
        private int _produceCount;

        public TestProducer(
            TestBroker broker,
            TestProducerEndpoint endpoint,
            IBrokerBehaviorsProvider<IProducerBehavior> behaviorsProvider,
            IServiceProvider serviceProvider)
            : base(
                broker,
                endpoint,
                behaviorsProvider,
                serviceProvider,
                Substitute.For<IOutboundLogger<TestProducer>>())
        {
            ProducedMessages = broker.ProducedMessages;
        }

        public IList<ProducedMessage> ProducedMessages { get; }

        protected override IBrokerMessageIdentifier? ProduceCore(
            object? message,
            Stream? messageStream,
            IReadOnlyCollection<MessageHeader>? headers,
            string actualEndpointName) =>
            ProduceCore(
                message,
                messageStream.ReadAll(),
                headers,
                actualEndpointName);

        protected override IBrokerMessageIdentifier? ProduceCore(
            object? message,
            byte[]? messageBytes,
            IReadOnlyCollection<MessageHeader>? headers,
            string actualEndpointName)
        {
            PerformMockProduce(messageBytes, headers);
            return null;
        }

        protected override void ProduceCore(
            object? message,
            Stream? messageStream,
            IReadOnlyCollection<MessageHeader>? headers,
            string actualEndpointName,
            Action<IBrokerMessageIdentifier?> onSuccess,
            Action<Exception> onError)
        {
            ProduceCore(
                message,
                messageStream.ReadAll(),
                headers,
                actualEndpointName,
                onSuccess,
                onError);
        }

        protected override void ProduceCore(
            object? message,
            byte[]? messageBytes,
            IReadOnlyCollection<MessageHeader>? headers,
            string actualEndpointName,
            Action<IBrokerMessageIdentifier?> onSuccess,
            Action<Exception> onError)
        {
            PerformMockProduce(messageBytes, headers);
            onSuccess.Invoke(null);
        }

        protected override async Task<IBrokerMessageIdentifier?> ProduceCoreAsync(
            object? message,
            Stream? messageStream,
            IReadOnlyCollection<MessageHeader>? headers,
            string actualEndpointName,
            CancellationToken cancellationToken = default) =>
            await ProduceCoreAsync(
                message,
                await messageStream.ReadAllAsync(cancellationToken).ConfigureAwait(false),
                headers,
                actualEndpointName,
                cancellationToken);

        protected override Task<IBrokerMessageIdentifier?> ProduceCoreAsync(
            object? message,
            byte[]? messageBytes,
            IReadOnlyCollection<MessageHeader>? headers,
            string actualEndpointName,
            CancellationToken cancellationToken = default)
        {
            PerformMockProduce(messageBytes, headers);
            return Task.FromResult<IBrokerMessageIdentifier?>(null);
        }

        protected override async Task ProduceCoreAsync(
            object? message,
            Stream? messageStream,
            IReadOnlyCollection<MessageHeader>? headers,
            string actualEndpointName,
            Action<IBrokerMessageIdentifier?> onSuccess,
            Action<Exception> onError,
            CancellationToken cancellationToken = default) =>
            await ProduceCoreAsync(
                message,
                await messageStream.ReadAllAsync(cancellationToken).ConfigureAwait(false),
                headers,
                actualEndpointName,
                onSuccess,
                onError,
                cancellationToken);

        protected override Task ProduceCoreAsync(
            object? message,
            byte[]? messageBytes,
            IReadOnlyCollection<MessageHeader>? headers,
            string actualEndpointName,
            Action<IBrokerMessageIdentifier?> onSuccess,
            Action<Exception> onError,
            CancellationToken cancellationToken = default)
        {
            PerformMockProduce(messageBytes, headers);
            onSuccess.Invoke(null);
            return Task.CompletedTask;
        }

        private void PerformMockProduce(byte[]? messageBytes, IReadOnlyCollection<MessageHeader>? headers)
        {
            if (Broker.FailProduceNumber != null && Broker.FailProduceNumber.Contains(++_produceCount))
                throw new InvalidOperationException("Produce failed (mock).");

            ProducedMessages.Add(new ProducedMessage(messageBytes, headers, Endpoint));
        }
    }
}
