// Copyright (c) 2020 Sergio Aquilini
// This code is licensed under MIT license (see LICENSE file for details)

using System;
using System.Runtime.Serialization;

namespace Silverback.Domain
{
    /// <summary>
    ///     The exception that is thrown when the stored events cannot be applied back to the domain
    ///     entity.
    /// </summary>
    public class SilverbackEventSourcingException : SilverbackException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SilverbackEventSourcingException" /> class.
        /// </summary>
        public SilverbackEventSourcingException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SilverbackEventSourcingException" /> class with
        ///     the specified message.
        /// </summary>
        /// <param name="message"> The exception message. </param>
        public SilverbackEventSourcingException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SilverbackEventSourcingException" /> class with
        ///     the specified message and inner exception.
        /// </summary>
        /// <param name="message"> The exception message. </param>
        /// <param name="innerException"> The inner exception. </param>
        public SilverbackEventSourcingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SilverbackEventSourcingException" /> class with
        ///     the serialized data.
        /// </summary>
        /// <param name="info">
        ///     The <see cref="SerializationInfo" /> that holds the serialized object data about the exception
        ///     being thrown.
        /// </param>
        /// <param name="context">
        ///     The <see cref="StreamingContext" /> that contains contextual information about the source or
        ///     destination.
        /// </param>
        public SilverbackEventSourcingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
