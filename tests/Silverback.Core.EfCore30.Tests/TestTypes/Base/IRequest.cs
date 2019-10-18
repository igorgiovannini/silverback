// Copyright (c) 2018-2019 Sergio Aquilini
// This code is licensed under MIT license (see LICENSE file for details)

using Silverback.Messaging.Messages;

namespace Silverback.Tests.Core.EFCore30.TestTypes.Base
{
    public interface IRequest<out TResponse> : IMessage
    {
    }
}