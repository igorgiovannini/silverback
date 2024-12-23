﻿// Copyright (c) 2020 Sergio Aquilini
// This code is licensed under MIT license (see LICENSE file for details)

using System;
using FluentAssertions;
using Silverback.Tests.Core.TestTypes.Messages;
using Silverback.Util;
using Xunit;

namespace Silverback.Tests.Core.Util
{
    public partial class TypesCacheTests
    {
        [Fact]
        public void GetType_ExistingType_TypeReturned()
        {
            var typeName = typeof(TestEventOne).AssemblyQualifiedName!;

            var type = TypesCache.GetType(typeName);

            type.Should().Be(typeof(TestEventOne));
        }

        [Fact]
        public void GetType_WrongAssemblyVersion_TypeReturned()
        {
            var typeName =
                "Silverback.Tests.Core.TestTypes.Messages.TestEventOne, " +
                "Silverback.Core.Tests, Version=123.123.123.123";

            var type = TypesCache.GetType(typeName);

            type.AssemblyQualifiedName.Should().Be(typeof(TestEventOne).AssemblyQualifiedName);
        }

        [Fact]
        public void GetType_NonExistingType_ExceptionThrown()
        {
            var typeName = "Baaaad.Event, Silverback.Core.Tests";

            Action act = () => TypesCache.GetType(typeName);

            act.Should().Throw<TypeLoadException>();
        }

        [Fact]
        public void GetType_NonExistingTypeWithNoThrow_NullReturned()
        {
            var typeName = "Baaaad.Event, Silverback.Core.Tests";

            var type = TypesCache.GetType(typeName, false);

            type.Should().BeNull();
        }

        [Fact]
        public void GetType_IncompleteTypeName_TypeReturned()
        {
            var typeName = "Silverback.Tests.Core.TestTypes.Messages.TestEventOne, Silverback.Core.Tests";

            var type = TypesCache.GetType(typeName);

            type.Should().Be(typeof(TestEventOne));
        }

        [Theory]
        [MemberData(nameof(AssemblyQualifiedNameType_ReturnType))]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1825:Avoid zero-length array allocations", Justification = "Unit test member data.")]
        public void GetType_GenericType_TypeReturned(string typeAssemblyQualifiedName, AssemblyQualifiedGenericTypeResult expectedResult)
        {
            var type = TypesCache.GetType(typeAssemblyQualifiedName);

            type.Should().NotBeNull();
            expectedResult.MatchingType.IsAssignableFrom(type).Should().BeTrue();
        }
    }
}
