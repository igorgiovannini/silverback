// Copyright (c) 2020 Sergio Aquilini
// This code is licensed under MIT license (see LICENSE file for details)

using System.Diagnostics.CodeAnalysis;

namespace Silverback.Messaging.Messages
{
    /// <summary>
    ///     Represents a message with a response. It is further specialized as <see cref="ICommand{TResult}" /> and
    ///     <see cref="IQuery{TResult}" />.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response received when this message is processed.</typeparam>
    [SuppressMessage("ReSharper", "CA1040", Justification = "Intentionally a marker interface")]
    [SuppressMessage(
        "ReSharper",
        "UnusedTypeParameter",
        Justification = "The parameter is used by the Publisher to know the return type")]
    public interface IRequest<out TResponse> : IMessage
    {
    }
}
