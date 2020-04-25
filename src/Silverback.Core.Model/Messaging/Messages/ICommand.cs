// Copyright (c) 2020 Sergio Aquilini
// This code is licensed under MIT license (see LICENSE file for details)

using System.Diagnostics.CodeAnalysis;

namespace Silverback.Messaging.Messages
{
    /// <summary>
    ///     Represents a message that triggers an action.
    /// </summary>
    [SuppressMessage("ReSharper", "CA1040", Justification = "Intentionally a marker interface")]
    public interface ICommand : IMessage
    {
    }

    /// <summary>
    ///     Represents a message that triggers an action with a result <typeparamref name="TResult"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result being returned.</typeparam>
    [SuppressMessage("ReSharper", "CA1040", Justification = "Intentionally a marker interface")]
    public interface ICommand<out TResult> : ICommand, IRequest<TResult>
    {
    }
}