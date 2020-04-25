// Copyright (c) 2020 Sergio Aquilini
// This code is licensed under MIT license (see LICENSE file for details)

using System.Diagnostics.CodeAnalysis;

namespace Silverback.Messaging.Messages
{
    /// <summary>
    ///     Represents a message that notifies an event.
    /// </summary>
    [SuppressMessage("ReSharper", "CA1040", Justification = "Intentionally a marker interface")]
    public interface IEvent : IMessage
    {
    }
}