﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Silverback.Examples.Common;
using Silverback.Examples.Common.Messages;
using Silverback.Examples.Common.Serialization;
using Silverback.Messaging;
using Silverback.Messaging.Broker;
using Silverback.Messaging.Configuration;

namespace Silverback.Examples.Main.UseCases.Advanced
{
    public class InteroperableMessageUseCase : UseCase
    {
        public InteroperableMessageUseCase() : base("Interoperable incoming message (free schema, not published by Silberback)", 10)
        {
        }

        protected override void ConfigureServices(IServiceCollection services) => services
            .AddBus()
            .AddBroker<KafkaBroker>();

        protected override void Configure(IBrokerEndpointsConfigurationBuilder endpoints) { }

        protected override async Task Execute(IServiceProvider serviceProvider)
        {
            var broker = serviceProvider.GetRequiredService<IBroker>();
            await broker.GetProducer(CreateEndpoint("silverback-examples-legacy-messages"))
                .ProduceAsync(new LegacyMessage { Content = "LEGACY - " + DateTime.Now.ToString("HH:mm:ss.fff") });
        }

        private IEndpoint CreateEndpoint(string name) =>
            new KafkaEndpoint(name)
            {
                Serializer = new LegacyMessageSerializer(),
                Configuration = new KafkaConfigurationDictionary
                {
                    {"bootstrap.servers", "PLAINTEXT://kafka:9092"},
                    {"client.id", GetType().FullName}
                }
            };
    }
}