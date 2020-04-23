// Copyright (c) 2020 Sergio Aquilini
// This code is licensed under MIT license (see LICENSE file for details)

namespace Silverback.Messaging.Messages
{
    /// <summary>
    ///     Represe
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}