// Copyright (c) 2020 Sergio Aquilini
// This code is licensed under MIT license (see LICENSE file for details)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Silverback.Messaging.Subscribers.ArgumentResolvers
{
    internal class ArgumentsResolver
    {
        private readonly IEnumerable<IArgumentResolver> _argumentResolvers;

        public ArgumentsResolver(IEnumerable<IArgumentResolver> argumentResolvers)
        {
            // Revert the resolvers order, to give priority to the ones added after the
            // default ones.
            _argumentResolvers = argumentResolvers.Reverse();
        }

        private MessageFilterAttribute GetRat()
        {
            return null!;
        }

        public (IMessageArgumentResolver resolver, Type messageType) GetMessageArgumentResolver(
            SubscribedMethod methodInfo)
        {
            IMessageArgumentResolver resolver = GetMessageArgumentResolver(
                methodInfo.Parameters[0],
                methodInfo.MethodInfo);

            return (resolver, resolver.GetMessageType(methodInfo.Parameters[0].ParameterType));
        }

        public IEnumerable<object?> GetAdditionalParameterValues(SubscribedMethod methodInfo)
        {
            return methodInfo.Parameters
                .Skip(1)
                .Select(
                    parameterInfo =>
                        GetAdditionalArgumentResolver(parameterInfo, methodInfo.MethodInfo)
                            .GetValue(parameterInfo.ParameterType))
                .ToArray();
        }

        private IMessageArgumentResolver
            GetMessageArgumentResolver(ParameterInfo parameterInfo, MethodInfo methodInfo)
        {
            return GetArgumentResolver<IMessageArgumentResolver>(parameterInfo, methodInfo);
        }

        private IAdditionalArgumentResolver GetAdditionalArgumentResolver(ParameterInfo parameterInfo,
            MethodInfo methodInfo)
        {
            return GetArgumentResolver<IAdditionalArgumentResolver>(parameterInfo, methodInfo);
        }

        private TResolver GetArgumentResolver<TResolver>(ParameterInfo parameterInfo, MethodInfo methodInfo)
            where TResolver : IArgumentResolver
        {
            TResolver resolver = _argumentResolvers
                .OfType<TResolver>()
                .FirstOrDefault(r => r.CanResolve(parameterInfo.ParameterType));

            if (resolver == null)
            {
                throw new SubscribedMethodInvocationException(
                    methodInfo,
                    $"No resolver could be found for argument '{parameterInfo.Name}' of type {parameterInfo.ParameterType.FullName}.");
            }

            return resolver;
        }
    }
}
