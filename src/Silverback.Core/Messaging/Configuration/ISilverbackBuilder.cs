// Copyright (c) 2020 Sergio Aquilini
// This code is licensed under MIT license (see LICENSE file for details)

using System;
using Microsoft.Extensions.DependencyInjection;
using Silverback.Messaging.Publishing;
using Silverback.Messaging.Subscribers;

namespace Silverback.Messaging.Configuration
{
    /// <summary>
    ///     An interface for configuring Silverback services.
    /// </summary>
    public interface ISilverbackBuilder
    {
        IServiceCollection Services { get; }

        /// <summary>
        ///     Adds a scoped subscriber of the type specified in <paramref name="subscriberType" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <param name="baseType">
        ///     The subscribers base class or interface.
        /// </param>
        /// <param name="subscriberType">
        ///     The type of the subscriber to register and the implementation to use.
        /// </param>
        /// <returns>
        ///     This <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddTransientSubscriber(Type baseType, Type subscriberType);

        /// <summary>
        ///     Adds a scoped subscriber of the type specified in <paramref name="subscriberType" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <param name="subscriberType">
        ///     The type of the subscriber to register and the implementation to use.
        /// </param>
        /// <returns>
        ///     This <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddTransientSubscriber(Type subscriberType);

        /// <summary>
        ///     Adds a scoped subscriber of the type specified in <typeparamref name="TSubscriber" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <typeparam name="TBase">
        ///     The subscribers base class or interface.
        /// </typeparam>
        /// <typeparam name="TSubscriber"> The type of the subscriber to add. </typeparam>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddTransientSubscriber<TBase, TSubscriber>()
            where TSubscriber : class, ISubscriber;

        /// <summary>
        ///     Adds a scoped subscriber of the type specified in <typeparamref name="TSubscriber" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <typeparam name="TSubscriber"> The type of the subscriber to add. </typeparam>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddTransientSubscriber<TSubscriber>()
            where TSubscriber : class, ISubscriber;

        /// <summary>
        ///     Adds a scoped subscriber of the type specified in <paramref name="subscriberType" /> with a
        ///     factory specified in <paramref name="implementationFactory" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <param name="baseType">
        ///     The subscribers base class or interface.
        /// </param>
        /// <param name="subscriberType">
        ///     The type of the subscriber to register.
        /// </param>
        /// <param name="implementationFactory"> The factory that creates the service. </param>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddTransientSubscriber(
            Type baseType,
            Type subscriberType,
            Func<IServiceProvider, object> implementationFactory);

        /// <summary>
        ///     Adds a scoped subscriber of the type specified in <paramref name="subscriberType" /> with a
        ///     factory specified in <paramref name="implementationFactory" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <param name="subscriberType">
        ///     The type of the subscriber to register.
        /// </param>
        /// <param name="implementationFactory"> The factory that creates the service. </param>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddTransientSubscriber(
            Type subscriberType,
            Func<IServiceProvider, ISubscriber> implementationFactory);

        /// <summary>
        ///     Adds a scoped subscriber of the type specified in <typeparamref name="TSubscriber" /> with a
        ///     factory specified in <paramref name="implementationFactory" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <typeparam name="TBase">
        ///     The subscribers base class or interface.
        /// </typeparam>
        /// <typeparam name="TSubscriber"> The type of the subscriber to add. </typeparam>
        /// <param name="implementationFactory"> The factory that creates the service. </param>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddTransientSubscriber<TBase, TSubscriber>(
            Func<IServiceProvider, TSubscriber> implementationFactory)
            where TSubscriber : class, ISubscriber;

        /// <summary>
        ///     Adds a scoped subscriber of the type specified in <typeparamref name="TSubscriber" /> with a
        ///     factory specified in <paramref name="implementationFactory" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <typeparam name="TSubscriber"> The type of the subscriber to add. </typeparam>
        /// <param name="implementationFactory"> The factory that creates the service. </param>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddTransientSubscriber<TSubscriber>(
            Func<IServiceProvider, TSubscriber> implementationFactory)
            where TSubscriber : class, ISubscriber;

        /// <summary>
        ///     Adds a scoped subscriber of the type specified in <paramref name="subscriberType" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <param name="baseType">
        ///     The subscribers base class or interface.
        /// </param>
        /// <param name="subscriberType">
        ///     The type of the subscriber to register and the implementation to use.
        /// </param>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddScopedSubscriber(Type baseType, Type subscriberType);

        /// <summary>
        ///     Adds a scoped subscriber of the type specified in <paramref name="subscriberType" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <param name="subscriberType">
        ///     The type of the subscriber to register and the implementation to use.
        /// </param>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddScopedSubscriber(Type subscriberType);

        /// <summary>
        ///     Adds a scoped subscriber of the type specified in <typeparamref name="TSubscriber" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <typeparam name="TBase">
        ///     The subscribers base class or interface.
        /// </typeparam>
        /// <typeparam name="TSubscriber"> The type of the subscriber to add. </typeparam>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddScopedSubscriber<TBase, TSubscriber>()
            where TSubscriber : class, ISubscriber;

        /// <summary>
        ///     Adds a scoped subscriber of the type specified in <typeparamref name="TSubscriber" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <typeparam name="TSubscriber"> The type of the subscriber to add. </typeparam>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddScopedSubscriber<TSubscriber>()
            where TSubscriber : class, ISubscriber;

        /// <summary>
        ///     Adds a scoped subscriber of the type specified in <paramref name="subscriberType" /> with a
        ///     factory specified in <paramref name="implementationFactory" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <param name="baseType">
        ///     The subscribers base class or interface.
        /// </param>
        /// <param name="subscriberType">
        ///     The type of the subscriber to register.
        /// </param>
        /// <param name="implementationFactory"> The factory that creates the service. </param>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddScopedSubscriber(
            Type baseType,
            Type subscriberType,
            Func<IServiceProvider, object> implementationFactory);

        /// <summary>
        ///     Adds a scoped subscriber of the type specified in <paramref name="subscriberType" /> with a
        ///     factory specified in <paramref name="implementationFactory" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <param name="subscriberType">
        ///     The type of the subscriber to register.
        /// </param>
        /// <param name="implementationFactory"> The factory that creates the service. </param>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddScopedSubscriber(
            Type subscriberType,
            Func<IServiceProvider, ISubscriber> implementationFactory);

        /// <summary>
        ///     Adds a scoped subscriber of the type specified in <typeparamref name="TSubscriber" /> with a
        ///     factory specified in <paramref name="implementationFactory" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <typeparam name="TBase">
        ///     The subscribers base class or interface.
        /// </typeparam>
        /// <typeparam name="TSubscriber"> The type of the subscriber to add. </typeparam>
        /// <param name="implementationFactory"> The factory that creates the service. </param>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddScopedSubscriber<TBase, TSubscriber>(
            Func<IServiceProvider, TSubscriber> implementationFactory)
            where TSubscriber : class, ISubscriber;

        /// <summary>
        ///     Adds a scoped subscriber of the type specified in <typeparamref name="TSubscriber" /> with a
        ///     factory specified in <paramref name="implementationFactory" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <typeparam name="TSubscriber"> The type of the subscriber to add. </typeparam>
        /// <param name="implementationFactory"> The factory that creates the service. </param>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddScopedSubscriber<TSubscriber>(Func<IServiceProvider, TSubscriber> implementationFactory)
            where TSubscriber : class, ISubscriber;

        /// <summary>
        ///     Adds a singleton subscriber of the type specified in <paramref name="subscriberType" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <param name="baseType">
        ///     The subscribers base class or interface.
        /// </param>
        /// <param name="subscriberType">
        ///     The type of the subscriber to register and the implementation to use.
        /// </param>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddSingletonSubscriber(Type baseType, Type subscriberType);

        /// <summary>
        ///     Adds a singleton subscriber of the type specified in <paramref name="subscriberType" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <param name="subscriberType">
        ///     The type of the subscriber to register and the implementation to use.
        /// </param>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddSingletonSubscriber(Type subscriberType);

        /// <summary>
        ///     Adds a singleton subscriber of the type specified in <typeparamref name="TSubscriber" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <typeparam name="TBase">
        ///     The subscribers base class or interface.
        /// </typeparam>
        /// <typeparam name="TSubscriber"> The type of the subscriber to add. </typeparam>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddSingletonSubscriber<TBase, TSubscriber>()
            where TSubscriber : class, ISubscriber;

        /// <summary>
        ///     Adds a singleton subscriber of the type specified in <typeparamref name="TSubscriber" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <typeparam name="TSubscriber"> The type of the subscriber to add. </typeparam>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddSingletonSubscriber<TSubscriber>()
            where TSubscriber : class, ISubscriber;

        /// <summary>
        ///     Adds a singleton subscriber of the type specified in <paramref name="subscriberType" /> with a
        ///     factory specified in <paramref name="implementationFactory" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <param name="baseType">
        ///     The subscribers base class or interface.
        /// </param>
        /// <param name="subscriberType">
        ///     The type of the subscriber to register.
        /// </param>
        /// <param name="implementationFactory"> The factory that creates the service. </param>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddSingletonSubscriber(
            Type baseType,
            Type subscriberType,
            Func<IServiceProvider, object> implementationFactory);

        /// <summary>
        ///     Adds a singleton subscriber of the type specified in <paramref name="subscriberType" /> with a
        ///     factory specified in <paramref name="implementationFactory" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <param name="subscriberType">
        ///     The type of the subscriber to register.
        /// </param>
        /// <param name="implementationFactory"> The factory that creates the service. </param>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddSingletonSubscriber(
            Type subscriberType,
            Func<IServiceProvider, ISubscriber> implementationFactory);

        /// <summary>
        ///     Adds a singleton subscriber of the type specified in <typeparamref name="TSubscriber" /> with a
        ///     factory specified in <paramref name="implementationFactory" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <typeparam name="TBase">
        ///     The subscribers base class or interface.
        /// </typeparam>
        /// <typeparam name="TSubscriber"> The type of the subscriber to add. </typeparam>
        /// <param name="implementationFactory"> The factory that creates the service. </param>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddSingletonSubscriber<TBase, TSubscriber>(
            Func<IServiceProvider, TSubscriber> implementationFactory)
            where TSubscriber : class, ISubscriber;

        /// <summary>
        ///     Adds a singleton subscriber of the type specified in <typeparamref name="TSubscriber" /> with a
        ///     factory specified in <paramref name="implementationFactory" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <typeparam name="TSubscriber"> The type of the subscriber to add. </typeparam>
        /// <param name="implementationFactory"> The factory that creates the service. </param>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddSingletonSubscriber<TSubscriber>(
            Func<IServiceProvider, TSubscriber> implementationFactory)
            where TSubscriber : class, ISubscriber;

        /// <summary>
        ///     Adds a singleton subscriber of the type specified in <paramref name="subscriberType" /> with an
        ///     instance specified in <paramref name="implementationInstance" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <param name="baseType">
        ///     The subscribers base class or interface.
        /// </param>
        /// <param name="subscriberType">
        ///     The type of the subscriber to register.
        /// </param>
        /// <param name="implementationInstance"> The instance of the service. </param>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddSingletonSubscriber(
            Type baseType,
            Type subscriberType,
            ISubscriber implementationInstance);

        /// <summary>
        ///     Adds a singleton subscriber of the type specified in <paramref name="subscriberType" /> with an
        ///     instance specified in <paramref name="implementationInstance" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <param name="subscriberType">
        ///     The type of the subscriber to register.
        /// </param>
        /// <param name="implementationInstance"> The instance of the service. </param>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddSingletonSubscriber(Type subscriberType, ISubscriber implementationInstance);

        /// <summary>
        ///     Adds a singleton subscriber of the type specified in <typeparamref name="TSubscriber" /> with
        ///     an
        ///     instance specified in <paramref name="implementationInstance" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <typeparam name="TBase">
        ///     The subscribers base class or interface.
        /// </typeparam>
        /// <typeparam name="TSubscriber">
        ///     The type of the subscriber to register.
        /// </typeparam>
        /// <param name="implementationInstance"> The instance of the service. </param>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddSingletonSubscriber<TBase, TSubscriber>(TSubscriber implementationInstance)
            where TSubscriber : class, ISubscriber;

        /// <summary>
        ///     Adds a singleton subscriber of the type specified in <typeparamref name="TSubscriber" /> with
        ///     an
        ///     instance specified in <paramref name="implementationInstance" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <param name="implementationInstance"> The instance of the service. </param>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddSingletonSubscriber<TSubscriber>(TSubscriber implementationInstance)
            where TSubscriber : class, ISubscriber;

        /// <summary>
        ///     Adds a scoped behavior of the type specified in <paramref name="behaviorType" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <param name="behaviorType">
        ///     The type of the behavior to register and the implementation to use.
        /// </param>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddTransientBehavior(Type behaviorType);

        /// <summary>
        ///     Adds a scoped behavior of the type specified in <typeparamref name="TBehavior" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <typeparam name="TBehavior"> The type of the behavior to add. </typeparam>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddTransientBehavior<TBehavior>()
            where TBehavior : class, IBehavior;

        /// <summary>
        ///     Adds a scoped behavior with a
        ///     factory specified in <paramref name="implementationFactory" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <param name="implementationFactory"> The factory that creates the service. </param>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddTransientBehavior(Func<IServiceProvider, IBehavior> implementationFactory);

        /// <summary>
        ///     Adds a scoped behavior of the type specified in <paramref name="behaviorType" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <param name="behaviorType">
        ///     The type of the behavior to register and the implementation to use.
        /// </param>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddScopedBehavior(Type behaviorType);

        /// <summary>
        ///     Adds a scoped behavior of the type specified in <typeparamref name="TBehavior" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <typeparam name="TBehavior"> The type of the behavior to add. </typeparam>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddScopedBehavior<TBehavior>()
            where TBehavior : class, IBehavior;

        /// <summary>
        ///     Adds a scoped behavior with a
        ///     factory specified in <paramref name="implementationFactory" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <param name="implementationFactory"> The factory that creates the service. </param>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddScopedBehavior(Func<IServiceProvider, IBehavior> implementationFactory);

        /// <summary>
        ///     Adds a singleton behavior of the type specified in <paramref name="behaviorType" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <param name="behaviorType">
        ///     The type of the behavior to register and the implementation to use.
        /// </param>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddSingletonBehavior(Type behaviorType);

        /// <summary>
        ///     Adds a singleton behavior of the type specified in <typeparamref name="TBehavior" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <typeparam name="TBehavior"> The type of the behavior to add. </typeparam>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddSingletonBehavior<TBehavior>()
            where TBehavior : class, IBehavior;

        /// <summary>
        ///     Adds a singleton behavior with a
        ///     factory specified in <paramref name="implementationFactory" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <param name="implementationFactory"> The factory that creates the service. </param>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddSingletonBehavior(Func<IServiceProvider, IBehavior> implementationFactory);

        /// <summary>
        ///     Adds a singleton behavior with an
        ///     instance specified in <paramref name="implementationInstance" /> to the
        ///     specified <see cref="T:Microsoft.Extensions.DependencyInjection.ISilverbackBuilder" />.
        /// </summary>
        /// <param name="implementationInstance"> The instance of the service. </param>
        /// <returns>
        ///     A reference to this <see cref="ISilverbackBuilder" /> instance so that additional calls can be chained.
        /// </returns>
        ISilverbackBuilder AddSingletonBehavior(IBehavior implementationInstance);
    }
}
