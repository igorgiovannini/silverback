﻿// Copyright (c) 2020 Sergio Aquilini
// This code is licensed under MIT license (see LICENSE file for details)

using System;
using Silverback.Messaging.Configuration;
using Silverback.Util;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     Adds the <c>AddMqttEndpoints</c> method to the <see cref="ISilverbackBuilder" />.
    /// </summary>
    public static class SilverbackBuilderAddMqttEndpointsExtensions
    {
        /// <summary>
        ///     Adds an <see cref="IEndpointsConfigurator" /> to be used to setup the broker endpoints.
        /// </summary>
        /// <param name="silverbackBuilder">
        ///     The <see cref="ISilverbackBuilder" /> that references the <see cref="IServiceCollection" /> to add
        ///     the services to.
        /// </param>
        /// <param name="configureAction">
        ///     An <see cref="Action{T}" /> that takes the <see cref="IMqttEndpointsConfigurationBuilder" /> and adds
        ///     the outbound and inbound endpoints.
        /// </param>
        /// <returns>
        ///     The <see cref="ISilverbackBuilder" /> so that additional calls can be chained.
        /// </returns>
        public static ISilverbackBuilder AddMqttEndpoints(
            this ISilverbackBuilder silverbackBuilder,
            Action<IMqttEndpointsConfigurationBuilder> configureAction)
        {
            Check.NotNull(silverbackBuilder, nameof(silverbackBuilder));
            Check.NotNull(configureAction, nameof(configureAction));

            silverbackBuilder.AddEndpointsConfigurator(_ => new MqttEndpointsConfigurator(configureAction));

            return silverbackBuilder;
        }
    }
}
