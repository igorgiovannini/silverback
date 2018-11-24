using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Silverback.Messaging.Serialization;
using Silverback.Util;

namespace Silverback.Messaging.Broker
{
    public abstract class Broker : IBroker, IDisposable
    {
        private readonly ILogger<Broker> _logger;

        private ConcurrentDictionary<IEndpoint, Producer> _producers = new ConcurrentDictionary<IEndpoint, Producer>();
        private ConcurrentDictionary<IEndpoint, Consumer> _consumers = new ConcurrentDictionary<IEndpoint, Consumer>();

        protected readonly ILoggerFactory LoggerFactory;

        protected Broker(ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory;

            _logger = loggerFactory.CreateLogger<Broker>();
        }

        #region Producer / Consumer

        public virtual IProducer GetProducer(IEndpoint endpoint)
        {
            return _producers.GetOrAdd(endpoint, _ =>
            {
                _logger?.LogInformation($"Creating new producer for endpoint '{endpoint.Name}'");
                return InstantiateProducer(endpoint);
            });
        }

        protected abstract Producer InstantiateProducer(IEndpoint endpoint);

        public virtual IConsumer GetConsumer(IEndpoint endpoint)
        {
            if (!IsConnected)
            {
                return _consumers.GetOrAdd(endpoint, _ =>
                {
                    _logger.LogInformation($"Creating new consumer for endpoint '{endpoint.Name}'");
                    return InstantiateConsumer(endpoint);
                });
            }

            if (!_consumers.TryGetValue(endpoint, out var consumer))
                throw new InvalidOperationException("The broker is already connected. Disconnect it to get a new consumer.");

            return consumer;
        }

        protected abstract Consumer InstantiateConsumer(IEndpoint endpoint);

        #endregion

        #region Connect / Disconnect

        public bool IsConnected { get; private set; }

        public void Connect()
        {
            if (IsConnected)
                return;

            _logger.LogTrace("Connecting to message broker...");

            Connect(_consumers.Values);
            IsConnected = true;

            _logger.LogTrace("Connected to message broker!");
        }

        protected abstract void Connect(IEnumerable<IConsumer> consumers);

        public void Disconnect()
        {
            if (!IsConnected)
                return;

            _logger.LogTrace("Disconnecting from message broker...");

            Disconnect(_consumers.Values);
            IsConnected = false;

            _logger.LogTrace("Disconnected from message broker!");
        }

        protected abstract void Disconnect(IEnumerable<IConsumer> consumers);

        #endregion

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            _consumers?.Values.OfType<IDisposable>().ForEach(o => o.Dispose());
            _consumers = null;

            _producers?.Values.OfType<IDisposable>().ForEach(o => o.Dispose());
            _producers = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public abstract class Broker<TEndpoint> : Broker
        where TEndpoint : class, IEndpoint
    {
        protected Broker(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
        }

        public override IProducer GetProducer(IEndpoint endpoint)
        {
            ThrowIfWrongEndpointType(endpoint);

            return base.GetProducer(endpoint);
        }

        public override IConsumer GetConsumer(IEndpoint endpoint)
        {
            ThrowIfWrongEndpointType(endpoint);

            return base.GetConsumer(endpoint);
        }

        public static void ThrowIfWrongEndpointType(IEndpoint endpoint)
        {
            if (!(endpoint is TEndpoint))
                throw new ArgumentException($"An endpoint of type {typeof(TEndpoint).Name} is expected.");
        }

    }
}