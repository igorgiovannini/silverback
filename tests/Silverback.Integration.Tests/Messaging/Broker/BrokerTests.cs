﻿// Copyright (c) 2020 Sergio Aquilini
// This code is licensed under MIT license (see LICENSE file for details)

using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Silverback.Tests.Integration.TestTypes;
using Xunit;

namespace Silverback.Tests.Integration.Messaging.Broker
{
    public class BrokerTests
    {
        private readonly TestBroker _broker;

        public BrokerTests()
        {
            var services = new ServiceCollection();

            services
                .AddNullLogger()
                .AddSilverback()
                .WithConnectionToMessageBroker(options => options
                    .AddBroker<TestBroker>());

            var serviceProvider = services.BuildServiceProvider();

            _broker = serviceProvider.GetRequiredService<TestBroker>();
        }

        [Fact]
        public void GetProducer_SomeEndpoint_ProducerIsReturned()
        {
            var producer = _broker.GetProducer(TestProducerEndpoint.GetDefault());

            producer.Should().NotBeNull();
        }

        [Fact]
        public void GetProducer_SameEndpoint_SameInstanceIsReturned()
        {
            var producer = _broker.GetProducer(TestProducerEndpoint.GetDefault());
            var producer2 = _broker.GetProducer(TestProducerEndpoint.GetDefault());

            producer2.Should().BeSameAs(producer);
        }

        [Fact]
        public void GetProducer_DifferentEndpoint_DifferentInstanceIsReturned()
        {
            var producer = _broker.GetProducer(TestProducerEndpoint.GetDefault());
            var producer2 = _broker.GetProducer(new TestProducerEndpoint("test2"));

            producer2.Should().NotBeSameAs(producer);
        }

        [Fact]
        public void GetConsumer_SomeEndpoint_ConsumerIsReturned()
        {
            var consumer = _broker.GetConsumer(TestConsumerEndpoint.GetDefault());

            consumer.Should().NotBeNull();
        }

        [Fact]
        public void GetConsumer_SameEndpoint_DifferentInstanceIsReturned()
        {
            var consumer = _broker.GetConsumer(TestConsumerEndpoint.GetDefault());
            var consumer2 = _broker.GetConsumer(new TestConsumerEndpoint("test2"));

            consumer2.Should().NotBeSameAs(consumer);
        }

        [Fact]
        public void GetConsumer_DifferentEndpoint_DifferentInstanceIsReturned()
        {
            var consumer = _broker.GetConsumer(TestConsumerEndpoint.GetDefault());
            var consumer2 = _broker.GetConsumer(new TestConsumerEndpoint("test2"));

            consumer2.Should().NotBeSameAs(consumer);
        }
    }
}