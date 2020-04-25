// Copyright (c) 2020 Sergio Aquilini
// This code is licensed under MIT license (see LICENSE file for details)

using System.Diagnostics.CodeAnalysis;

namespace Silverback.Messaging.Messages
{
    /// <summary>
    ///     Represents a message that queries a result of type <typeparamref name="TResult"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result being returned.</typeparam>
    [SuppressMessage("ReSharper", "CA1040", Justification = "Intentionally a marker interface")]
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}