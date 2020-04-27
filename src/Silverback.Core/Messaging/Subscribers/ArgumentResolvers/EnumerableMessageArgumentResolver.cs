// Copyright (c) 2020 Sergio Aquilini
// This code is licensed under MIT license (see LICENSE file for details)

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Silverback.Util;

namespace Silverback.Messaging.Subscribers.ArgumentResolvers
{
    /// <summary>
    ///     Resolves the parameters declared as <see cref="IEnumerable{T}" /> where <c> TMessage </c> is
    ///     a type compatible with the type of the message being published.
    /// </summary>
    [SuppressMessage(
        "ReSharper",
        "CA1062",
        Justification = "These methods are called by Silverback internals and don't need to check for null.")]
    public class EnumerableMessageArgumentResolver : IEnumerableMessageArgumentResolver
    {
        /// <inheritdoc />
        public bool CanResolve(Type parameterType) =>
            parameterType.IsGenericType &&
            parameterType.GetGenericTypeDefinition() == typeof(IEnumerable<>);

        /// <inheritdoc />
        public Type GetMessageType(Type parameterType) =>
            parameterType.GetGenericArguments()[0];

        /// <inheritdoc />
        public object GetValue(IReadOnlyCollection<object> messages, Type targetMessageType) =>
            messages.OfType(targetMessageType).ToList(targetMessageType);
    }
}
