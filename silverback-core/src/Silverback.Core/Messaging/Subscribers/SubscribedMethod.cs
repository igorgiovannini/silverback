﻿using System;
using System.Reflection;

namespace Silverback.Messaging.Subscribers
{
    internal class SubscribedMethod
    {
        public ISubscriber Instance { get; set; }
        public MethodInfo MethodInfo { get; set; }
        public ParameterInfo[] Parameters { get; set; }
        public Type SubscribedMessageType { get; set; }
    }
}