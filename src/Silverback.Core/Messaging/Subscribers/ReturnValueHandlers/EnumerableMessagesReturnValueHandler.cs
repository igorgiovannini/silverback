﻿// Copyright (c) 2020 Sergio Aquilini
// This code is licensed under MIT license (see LICENSE file for details)

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Silverback.Messaging.Configuration;
using Silverback.Messaging.Publishing;
using Silverback.Util;

namespace Silverback.Messaging.Subscribers.ReturnValueHandlers
{
    // TODO: Test
    internal class EnumerableMessagesReturnValueHandler : IReturnValueHandler
    {
        private readonly IPublisher _publisher;
        private readonly BusOptions _publisherOptions;

        public EnumerableMessagesReturnValueHandler(IPublisher publisher, BusOptions publisherOptions)
        {
            _publisher = publisher;
            _publisherOptions = publisherOptions;
        }

        public bool CanHandle(object returnValue) =>
            returnValue != null &&
            returnValue.GetType().GetInterfaces().Any(
                i => i.IsGenericType &&
                     i.GetGenericTypeDefinition() == typeof(IEnumerable<>) &&
                     _publisherOptions.MessageTypes.Any(messageType =>
                         messageType.IsAssignableFrom(i.GenericTypeArguments[0])));

        public void Handle(object returnValue) =>
            _publisher.Publish<object?>(((IEnumerable<object?>)returnValue).WhereNotNull());

        public Task HandleAsync(object returnValue) =>
            _publisher.PublishAsync<object>(((IEnumerable<object?>)returnValue).WhereNotNull());
    }
}