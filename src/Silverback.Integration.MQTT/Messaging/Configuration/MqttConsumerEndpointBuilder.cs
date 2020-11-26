﻿// Copyright (c) 2020 Sergio Aquilini
// This code is licensed under MIT license (see LICENSE file for details)

using System.Linq;
using MQTTnet.Protocol;
using Silverback.Util;

namespace Silverback.Messaging.Configuration
{
    /// <inheritdoc cref="IMqttConsumerEndpointBuilder" />
    public class MqttConsumerEndpointBuilder
        : ConsumerEndpointBuilder<MqttConsumerEndpoint, IMqttConsumerEndpointBuilder>, IMqttConsumerEndpointBuilder
    {
        private readonly MqttClientConfig _clientConfig;

        private string[]? _topicNames;

        private MqttQualityOfServiceLevel? _qualityOfServiceLevel;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MqttConsumerEndpointBuilder" /> class.
        /// </summary>
        /// <param name="clientConfig">
        ///     The <see cref="MqttClientConfig" />.
        /// </param>
        /// <param name="endpointsConfigurationBuilder">
        ///     The optional reference to the <see cref="IEndpointsConfigurationBuilder" /> that instantiated the
        ///     builder.
        /// </param>
        public MqttConsumerEndpointBuilder(
            MqttClientConfig clientConfig,
            IEndpointsConfigurationBuilder? endpointsConfigurationBuilder = null)
            : base(endpointsConfigurationBuilder)
        {
            _clientConfig = clientConfig;
        }

        /// <inheritdoc cref="EndpointBuilder{TEndpoint,TBuilder}.This" />
        protected override IMqttConsumerEndpointBuilder This => this;

        /// <inheritdoc cref="IMqttConsumerEndpointBuilder.ConsumeFrom" />
        public IMqttConsumerEndpointBuilder ConsumeFrom(params string[] topics)
        {
            Check.HasNoEmpties(topics, nameof(topics));

            _topicNames = topics;

            return this;
        }

        /// <inheritdoc cref="IMqttConsumerEndpointBuilder.WithQualityOfServiceLevel" />
        public IMqttConsumerEndpointBuilder WithQualityOfServiceLevel(MqttQualityOfServiceLevel qosLevel)
        {
            _qualityOfServiceLevel = qosLevel;
            return this;
        }

        /// <inheritdoc cref="IMqttConsumerEndpointBuilder.WithAtMostOnceQoS" />
        public IMqttConsumerEndpointBuilder WithAtMostOnceQoS()
        {
            _qualityOfServiceLevel = MqttQualityOfServiceLevel.AtMostOnce;
            return this;
        }

        /// <inheritdoc cref="IMqttConsumerEndpointBuilder.WithAtLeastOnceQoS" />
        public IMqttConsumerEndpointBuilder WithAtLeastOnceQoS()
        {
            _qualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce;
            return this;
        }

        /// <inheritdoc cref="IMqttConsumerEndpointBuilder.WithExactlyOnceQoS" />
        public IMqttConsumerEndpointBuilder WithExactlyOnceQoS()
        {
            _qualityOfServiceLevel = MqttQualityOfServiceLevel.ExactlyOnce;
            return this;
        }

        /// <inheritdoc cref="EndpointBuilder{TEndpoint,TBuilder}.CreateEndpoint" />
        protected override MqttConsumerEndpoint CreateEndpoint()
        {
            if (_topicNames == null || _topicNames.Any(string.IsNullOrEmpty))
                throw new EndpointConfigurationException("Topic name not set.");

            var endpoint = new MqttConsumerEndpoint(_topicNames)
            {
                Configuration = _clientConfig
            };

            if (_qualityOfServiceLevel != null)
                endpoint.QualityOfServiceLevel = _qualityOfServiceLevel.Value;

            return endpoint;
        }
    }
}
