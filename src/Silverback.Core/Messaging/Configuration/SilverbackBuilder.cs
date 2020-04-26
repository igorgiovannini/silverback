// Copyright (c) 2020 Sergio Aquilini
// This code is licensed under MIT license (see LICENSE file for details)

using System;
using Microsoft.Extensions.DependencyInjection;
using Silverback.Messaging.Publishing;
using Silverback.Messaging.Subscribers;

namespace Silverback.Messaging.Configuration
{
    internal class SilverbackBuilder : ISilverbackBuilder
    {
        public SilverbackBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }

        public ISilverbackBuilder AddTransientSubscriber(Type baseType, Type subscriberType)
        {
            Services.AddTransientSubscriber(baseType, subscriberType);
            return this;
        }

        public ISilverbackBuilder AddTransientSubscriber(Type subscriberType)
        {
            Services.AddTransientSubscriber(subscriberType);
            return this;
        }

        public ISilverbackBuilder AddTransientSubscriber<TBase, TSubscriber>()
            where TSubscriber : class, ISubscriber
        {
            Services.AddTransientSubscriber<TBase, TSubscriber>();
            return this;
        }

        public ISilverbackBuilder AddTransientSubscriber<TSubscriber>()
            where TSubscriber : class, ISubscriber
        {
            Services.AddTransientSubscriber<TSubscriber>();
            return this;
        }

        public ISilverbackBuilder AddTransientSubscriber(
            Type baseType,
            Type subscriberType,
            Func<IServiceProvider, object> implementationFactory)
        {
            Services.AddTransientSubscriber(baseType, subscriberType, implementationFactory);
            return this;
        }

        public ISilverbackBuilder AddTransientSubscriber(
            Type subscriberType,
            Func<IServiceProvider, ISubscriber> implementationFactory)
        {
            Services.AddTransientSubscriber(subscriberType, implementationFactory);
            return this;
        }

        public ISilverbackBuilder AddTransientSubscriber<TBase, TSubscriber>(
            Func<IServiceProvider, TSubscriber> implementationFactory)
            where TSubscriber : class, ISubscriber
        {
            Services.AddTransientSubscriber<TBase, TSubscriber>(implementationFactory);
            return this;
        }

        public ISilverbackBuilder AddTransientSubscriber<TSubscriber>(
            Func<IServiceProvider, TSubscriber> implementationFactory)
            where TSubscriber : class, ISubscriber
        {
            Services.AddTransientSubscriber(implementationFactory);
            return this;
        }

        public ISilverbackBuilder AddScopedSubscriber(Type baseType, Type subscriberType)
        {
            Services.AddScopedSubscriber(baseType, subscriberType);
            return this;
        }

        public ISilverbackBuilder AddScopedSubscriber(Type subscriberType)
        {
            Services.AddScopedSubscriber(subscriberType);
            return this;
        }

        public ISilverbackBuilder AddScopedSubscriber<TBase, TSubscriber>()
            where TSubscriber : class, ISubscriber
        {
            Services.AddScopedSubscriber<TBase, TSubscriber>();
            return this;
        }

        public ISilverbackBuilder AddScopedSubscriber<TSubscriber>()
            where TSubscriber : class, ISubscriber
        {
            Services.AddScopedSubscriber<TSubscriber>();
            return this;
        }

        public ISilverbackBuilder AddScopedSubscriber(
            Type baseType,
            Type subscriberType,
            Func<IServiceProvider, object> implementationFactory)
        {
            Services.AddScopedSubscriber(baseType, subscriberType, implementationFactory);
            return this;
        }

        public ISilverbackBuilder AddScopedSubscriber(
            Type subscriberType,
            Func<IServiceProvider, ISubscriber> implementationFactory)
        {
            Services.AddScopedSubscriber(subscriberType, implementationFactory);
            return this;
        }

        public ISilverbackBuilder AddScopedSubscriber<TBase, TSubscriber>(
            Func<IServiceProvider, TSubscriber> implementationFactory)
            where TSubscriber : class, ISubscriber
        {
            Services.AddScopedSubscriber(implementationFactory);
            return this;
        }

        public ISilverbackBuilder AddScopedSubscriber<TSubscriber>(
            Func<IServiceProvider, TSubscriber> implementationFactory)
            where TSubscriber : class, ISubscriber
        {
            Services.AddScopedSubscriber(implementationFactory);
            return this;
        }

        public ISilverbackBuilder AddSingletonSubscriber(Type baseType, Type subscriberType)
        {
            Services.AddSingletonSubscriber(baseType, subscriberType);
            return this;
        }

        public ISilverbackBuilder AddSingletonSubscriber(Type subscriberType)
        {
            Services.AddSingletonSubscriber(subscriberType);
            return this;
        }

        public ISilverbackBuilder AddSingletonSubscriber<TBase, TSubscriber>()
            where TSubscriber : class, ISubscriber
        {
            Services.AddSingletonSubscriber<TBase, TSubscriber>();
            return this;
        }

        public ISilverbackBuilder AddSingletonSubscriber<TSubscriber>()
            where TSubscriber : class, ISubscriber
        {
            Services.AddSingletonSubscriber<TSubscriber>();
            return this;
        }

        public ISilverbackBuilder AddSingletonSubscriber(
            Type baseType,
            Type subscriberType,
            Func<IServiceProvider, object> implementationFactory)
        {
            Services.AddSingletonSubscriber(baseType, subscriberType, implementationFactory);
            return this;
        }

        public ISilverbackBuilder AddSingletonSubscriber(
            Type subscriberType,
            Func<IServiceProvider, ISubscriber> implementationFactory)
        {
            Services.AddSingletonSubscriber(subscriberType, implementationFactory);
            return this;
        }

        public ISilverbackBuilder AddSingletonSubscriber<TBase, TSubscriber>(
            Func<IServiceProvider, TSubscriber> implementationFactory)
            where TSubscriber : class, ISubscriber
        {
            Services.AddSingletonSubscriber<TBase, TSubscriber>(implementationFactory);
            return this;
        }

        public ISilverbackBuilder AddSingletonSubscriber<TSubscriber>(
            Func<IServiceProvider, TSubscriber> implementationFactory)
            where TSubscriber : class, ISubscriber
        {
            Services.AddSingletonSubscriber(implementationFactory);
            return this;
        }

        public ISilverbackBuilder AddSingletonSubscriber(
            Type baseType,
            Type subscriberType,
            ISubscriber implementationInstance)
        {
            Services.AddSingletonSubscriber(baseType, subscriberType, implementationInstance);
            return this;
        }


        public ISilverbackBuilder AddSingletonSubscriber(Type subscriberType, ISubscriber implementationInstance)
        {
            Services.AddSingletonSubscriber(subscriberType, implementationInstance);
            return this;
        }


        public ISilverbackBuilder AddSingletonSubscriber<TBase, TSubscriber>(TSubscriber implementationInstance)
            where TSubscriber : class, ISubscriber
        {
            Services.AddSingletonSubscriber(implementationInstance);
            return this;
        }

        public ISilverbackBuilder AddSingletonSubscriber<TSubscriber>(TSubscriber implementationInstance)
            where TSubscriber : class, ISubscriber
        {
            Services.AddSingletonSubscriber(implementationInstance);
            return this;
        }

        public ISilverbackBuilder AddTransientBehavior(Type behaviorType)
        {
            Services.AddTransientBehavior(behaviorType);
            return this;
        }

        public ISilverbackBuilder AddTransientBehavior<TBehavior>()
            where TBehavior : class, IBehavior
        {
            Services.AddTransientBehavior<TBehavior>();
            return this;
        }

        public ISilverbackBuilder AddTransientBehavior(Func<IServiceProvider, IBehavior> implementationFactory)
        {
            Services.AddTransientBehavior(implementationFactory);
            return this;
        }

        public ISilverbackBuilder AddScopedBehavior(Type behaviorType)
        {
            Services.AddScopedBehavior(behaviorType);
            return this;
        }

        public ISilverbackBuilder AddScopedBehavior<TBehavior>()
            where TBehavior : class, IBehavior
        {
            Services.AddScopedBehavior<TBehavior>();
            return this;
        }

        public ISilverbackBuilder AddScopedBehavior(Func<IServiceProvider, IBehavior> implementationFactory)
        {
            Services.AddScopedBehavior(implementationFactory);
            return this;
        }

        public ISilverbackBuilder AddSingletonBehavior(Type behaviorType)
        {
            Services.AddSingletonBehavior(behaviorType);
            return this;
        }

        public ISilverbackBuilder AddSingletonBehavior<TBehavior>()
            where TBehavior : class, IBehavior
        {
            Services.AddSingletonBehavior<TBehavior>();
            return this;
        }

        public ISilverbackBuilder AddSingletonBehavior(Func<IServiceProvider, IBehavior> implementationFactory)
        {
            Services.AddSingletonBehavior(implementationFactory);
            return this;
        }

        public ISilverbackBuilder AddSingletonBehavior(IBehavior implementationInstance)
        {
            Services.AddSingletonBehavior(implementationInstance);
            return this;
        }
    }
}
