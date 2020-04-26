// Copyright (c) 2020 Sergio Aquilini
// This code is licensed under MIT license (see LICENSE file for details)

using Microsoft.Extensions.DependencyInjection;

namespace Silverback.Messaging.Configuration
{
    /// <summary>
    ///     Used to add the services necessary to enable the Silverback features.
    /// </summary>
    public interface ISilverbackBuilder
    {
        /// <summary>
        ///     Gets the <see cref="IServiceCollection" /> that is being modified by this
        ///     <see cref="ISilverbackBuilder" />.
        /// </summary>
        IServiceCollection Services { get; }
    }
}
