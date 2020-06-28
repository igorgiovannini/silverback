﻿// Copyright (c) 2020 Sergio Aquilini
// This code is licensed under MIT license (see LICENSE file for details)

using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace Silverback.Util
{
    internal static class TypesCache
    {
        private static readonly ConcurrentDictionary<string, Type> Cache = new ConcurrentDictionary<string, Type>();

        public static Type GetType(string typeName)
        {
            Check.NotNull(typeName, nameof(typeName));

            return Cache.GetOrAdd(typeName, ResolveType);
        }

        [SuppressMessage("", "CA1031", Justification = "Can catch all, the operation is retried")]
        private static Type ResolveType(string typeName)
        {
            Type? type = null;

            try
            {
                type = Type.GetType(typeName);
            }
            catch
            {
                // Ignore
            }

            type ??= Type.GetType(CleanAssemblyQualifiedName(typeName), true);

            return type;
        }

        private static string CleanAssemblyQualifiedName(string typeAssemblyQualifiedName)
        {
            if (string.IsNullOrEmpty(typeAssemblyQualifiedName))
                return typeAssemblyQualifiedName;

            var split = typeAssemblyQualifiedName.Split(',');

            return split.Length >= 2 ? $"{split[0]}, {split[1]}" : typeAssemblyQualifiedName;
        }
    }
}