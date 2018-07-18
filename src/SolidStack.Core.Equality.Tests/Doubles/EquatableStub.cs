using System;
using System.Collections.Generic;

namespace SolidStack.Core.Equality.Tests.Doubles
{
    public sealed class EquatableStub : Equatable<EquatableStub>
    {
        public EquatableStub(Func<IEqualityComparer<EquatableStub>> equalityComparerAccessor) =>
            EqualityComparerAccessor = equalityComparerAccessor;

        private Func<IEqualityComparer<EquatableStub>> EqualityComparerAccessor { get; }

        protected override IEqualityComparer<EquatableStub> GetEqualityComparer() =>
            EqualityComparerAccessor();
    }
}