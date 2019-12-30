﻿// Copyright (c) 2019 Sergio Aquilini
// This code is licensed under MIT license (see LICENSE file for details)

using System.Diagnostics;
using Silverback.Examples.Main.Menu;

namespace Silverback.Examples.Main.UseCases
{
    public abstract class ExternalUseCase : IUseCase
    {
        public string Title { get; protected set; }
        public string Description { get; protected set; }

        public abstract void Run();
    }
}