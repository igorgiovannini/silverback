﻿// Copyright (c) 2020 Sergio Aquilini
// This code is licensed under MIT license (see LICENSE file for details)

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Silverback.Examples.Main.Menu;

namespace Silverback.Examples.Main.UseCases.Producing.Rabbit.Basic
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class _CategoryInfo : ICategory
    {
        public string Title => "Basics";

        public string Description => "The simplest configurations to get started using " +
                                     "Silverback with RabbitMQ.";

        public IEnumerable<Type> Children => new List<Type>
        {
            typeof(SimpleQueuePublishUseCase),
            typeof(FanoutPublishUseCase),
            typeof(TopicPublishUseCase)
        };
    }
}