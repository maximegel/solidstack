using System;
using System.Collections.Generic;
using System.Linq;
using SolidStack.Core.Equality.Testing;
using SolidStack.Core.Equality.Tests.Doubles;
using Xunit;

namespace SolidStack.Core.Equality.Tests
{
    public class EqualityComparerTests
    {
        [Fact]
        public void ByElement_CreatesComparerBasedOnAllElementsEquality()
        {
            var comparer = EqualityComparer.ByElements<IEnumerable<string>>();

            comparer.Should()
                .HandleBasicEqualitiesAndInequalites()
                .And.ValidateEqualityOf(
                    new[] {"a", "b", "c"},
                    new[] {"a", "b", "c"},
                    "sequences with equals elements should equals")
                .And.ValidateEqualityOf(
                    new[] {"a", "b", null},
                    new[] {"a", "b", null},
                    "sequences with equals elements should equals even if some elements are nulls")
                .And.ValidateEqualityOf(
                    new List<string> {"a", "b", "c"},
                    new HashSet<string> {"a", "b", "c"},
                    "sequences with equals elements should equals even if their types are different")
                .And.InvalidateEqualityOf(
                    new[] {"a", "b", "c"},
                    new[] {"d", "e", "f"},
                    "sequences with different elements shouldn't equals");
        }

        [Fact]
        public void ByFields_CreatesComparerBasedOnAllPropertiesEquality()
        {
            var comparer =
                EqualityComparer.ByFields<DummyByMembersEqualityComparable>();

            comparer.Should()
                .HandleBasicEqualitiesAndInequalites()
                .And.ValidateEqualityOf(
                    new DummyByMembersEqualityComparable(
                        5, new[] {'a', 'b'}, new DateTime(2018, 07, 30), new[] {true, true}),
                    new DummyByMembersEqualityComparable(
                        5, new[] {'a', 'b'}, new DateTime(2018, 06, 24), new[] {false, false}),
                    "properties should be ignored")
                .And.InvalidateEqualityOf(
                    new DummyByMembersEqualityComparable(
                        5, new[] {'a', 'b'}, new DateTime(2018, 07, 30), new[] {true, true}),
                    new DummyByMembersEqualityComparable(
                        5, new[] {'c', 'd'}, new DateTime(2018, 07, 30), new[] {true, true}),
                    "objects with different fields shouldn't equals");
        }

        [Fact]
        public void ByFields_WithPredicate_CreatesComparerBasedOnSelectedFieldsEquality()
        {
            var comparer =
                EqualityComparer.ByFields<DummyByMembersEqualityComparable>(
                    field => field.Name.EndsWith("A"));

            comparer.Should()
                .HandleBasicEqualitiesAndInequalites()
                .And.ValidateEqualityOf(
                    new DummyByMembersEqualityComparable(
                        5, new[] {'a', 'b'}, new DateTime(2018, 07, 30), new[] {true, true}),
                    new DummyByMembersEqualityComparable(
                        5, new[] {'c', 'd'}, new DateTime(2018, 06, 24), new[] {false, false}),
                    "unselected fields and every property should be ignored")
                .And.InvalidateEqualityOf(
                    new DummyByMembersEqualityComparable(
                        5, new[] {'a', 'b'}, new DateTime(2018, 07, 30), new[] {true, true}),
                    new DummyByMembersEqualityComparable(
                        3, new[] {'a', 'b'}, new DateTime(2018, 06, 24), new[] {true, true}),
                    "objects with different selected fields shouldn't equals");
        }

        [Fact]
        public void ByFields_WithSelector_CreatesComparerBasedOnSelectedFieldsEquality()
        {
            var comparer =
                EqualityComparer.ByFields<DummyByMembersEqualityComparable>(
                    fields => fields.Where(field => field.Name.EndsWith("A")));

            comparer.Should()
                .HandleBasicEqualitiesAndInequalites()
                .And.ValidateEqualityOf(
                    new DummyByMembersEqualityComparable(
                        5, new[] {'a', 'b'}, new DateTime(2018, 07, 30), new[] {true, true}),
                    new DummyByMembersEqualityComparable(
                        5, new[] {'c', 'd'}, new DateTime(2018, 06, 24), new[] {false, false}),
                    "unselected fields and every property should be ignored")
                .And.InvalidateEqualityOf(
                    new DummyByMembersEqualityComparable(
                        5, new[] {'a', 'b'}, new DateTime(2018, 07, 30), new[] {true, true}),
                    new DummyByMembersEqualityComparable(
                        3, new[] {'a', 'b'}, new DateTime(2018, 06, 24), new[] {true, true}),
                    "objects with different selected fields shouldn't equals");
        }

        [Fact]
        public void ByKey_CreatesComparerBasedOnKeyEquality()
        {
            var comparer = EqualityComparer.ByKey<DummyByKeyEqualityComparable>(obj => obj.Id);

            comparer.Should()
                .HandleBasicEqualitiesAndInequalites()
                .And.ValidateEqualityOf(
                    new DummyByKeyEqualityComparable("a1b2"),
                    new DummyByKeyEqualityComparable("a1b2"),
                    "objects with same key should equals")
                .And.InvalidateEqualityOf(
                    new DummyByKeyEqualityComparable("a1b2"),
                    new DummyByKeyEqualityComparableChild("a1c3"),
                    "objects of different types shouldn't equals")
                .And.InvalidateEqualityOf(
                    new DummyByKeyEqualityComparable("a1b2"),
                    new DummyByKeyEqualityComparable("a1c3"),
                    "objects with different keys shouldn't equals");
        }

        [Fact]
        public void ByMembers_CreatesComparerBasedOnAllMembersEquality()
        {
            var comparer = EqualityComparer.ByMembers<DummyByMembersEqualityComparable>();

            comparer.Should()
                .HandleBasicEqualitiesAndInequalites()
                .And.ValidateEqualityOf(
                    new DummyByMembersEqualityComparable(
                        5, new[] {'a', 'b'}, new DateTime(2018, 07, 30), new[] {true, true}),
                    new DummyByMembersEqualityComparable(
                        5, new[] {'a', 'b'}, new DateTime(2018, 07, 30), new[] {true, true}),
                    "objects with equals members should equals")
                .And.ValidateEqualityOf(
                    new DummyByMembersEqualityComparable(
                        5, null, new DateTime(2018, 07, 30), null),
                    new DummyByMembersEqualityComparable(
                        5, null, new DateTime(2018, 07, 30), null),
                    "objects with equals members should equals even if some members are nulls")
                .And.InvalidateEqualityOf(
                    new DummyByMembersEqualityComparable(
                        5, new[] {'a', 'b'}, new DateTime(2018, 07, 30), new[] {true, true}),
                    new DummyByMembersEqualityComparableChild(
                        5, new[] {'a', 'b'}, "abc", new DateTime(2018, 07, 30), new[] {true, true}, false),
                    "objects of different types shouldn't equals")
                .And.InvalidateEqualityOf(
                    new DummyByMembersEqualityComparable(
                        5, new[] {'a', 'b'}, new DateTime(2018, 07, 30), new[] {true, true}),
                    new DummyByMembersEqualityComparable(
                        3, new[] {'a', 'b'}, new DateTime(2018, 06, 24), new[] {true, true}),
                    "objects with different members shouldn't equals")
                .And.InvalidateEqualityOf(
                    new DummyByMembersEqualityComparable(
                        5, new[] {'a', 'b'}, new DateTime(2018, 07, 30), new[] {true, true}),
                    new DummyByMembersEqualityComparable(
                        5, new[] {'a', 'c'}, new DateTime(2018, 07, 30), new[] {true, true}),
                    "objects with different members shouldn't equals even if the difference is a element contained in a sequence");
        }

        [Fact]
        public void ByMembers_WithDerivedType_CreatesComparerBasedOnAllMembersEquality()
        {
            var comparer = EqualityComparer.ByMembers<DummyByMembersEqualityComparableChild>();

            comparer.Should()
                .HandleBasicEqualitiesAndInequalites()
                .And.ValidateEqualityOf(
                    new DummyByMembersEqualityComparableChild(
                        5, new[] {'a', 'b'}, "abc", new DateTime(2018, 07, 30), new[] {true, true}, false),
                    new DummyByMembersEqualityComparableChild(
                        5, new[] {'a', 'b'}, "abc", new DateTime(2018, 07, 30), new[] {true, true}, false),
                    "objects with inherited members should equals if their members are equals")
                .And.ValidateEqualityOf(
                    new DummyByMembersEqualityComparableChild(
                        5, new[] {'a', 'b'}, null, new DateTime(2018, 07, 30), null, false),
                    new DummyByMembersEqualityComparableChild(
                        5, new[] {'a', 'b'}, null, new DateTime(2018, 07, 30), null, false),
                    "objects with inherited members should equals if their members are equals even if some members are nulls")
                .And.InvalidateEqualityOf(
                    new DummyByMembersEqualityComparableChild(
                        5, new[] {'a', 'b'}, "abc", new DateTime(2018, 07, 30), new[] {true, true}, false),
                    new DummyByMembersEqualityComparableChild(
                        5, new[] {'a', 'b'}, "def", new DateTime(2018, 07, 30), new[] {true, true}, false),
                    "objects with inherited members shouldn't equals if their members aren't equals")
                .And.InvalidateEqualityOf(
                    new DummyByMembersEqualityComparableChild(
                        5, new[] {'a', 'b'}, "abc", new DateTime(2018, 07, 30), new[] {true, true}, false),
                    new DummyByMembersEqualityComparableChild(
                        3, new[] {'a', 'b'}, "abc", new DateTime(2018, 07, 30), new[] {true, true}, false),
                    "objects with inherited members shouldn't equals if their members aren't equals even if the different member is an inherited one");
        }

        [Fact]
        public void ByMembers_WithDynamicMembers_CreatesComparerBasedOnAllMembersEquality()
        {
            var comparer = EqualityComparer.ByMembers<DummyByDynamicMembersEqualityComparable>();

            comparer.Should()
                .HandleBasicEqualitiesAndInequalites()
                .And.ValidateEqualityOf(
                    new DummyByDynamicMembersEqualityComparable("abc", true),
                    new DummyByDynamicMembersEqualityComparable("abc", true),
                    "objects with equals members should equals")
                .And.ValidateEqualityOf(
                    new DummyByDynamicMembersEqualityComparable(null, true),
                    new DummyByDynamicMembersEqualityComparable(null, true),
                    "objects with equals members should equals even if some members are nulls")
                .And.InvalidateEqualityOf(
                    new DummyByDynamicMembersEqualityComparable("abc", true),
                    new DummyByDynamicMembersEqualityComparable("def", true),
                    "objects with different members shouldn't equals")
                .And.InvalidateEqualityOf(
                    new DummyByDynamicMembersEqualityComparable("abc", true),
                    new DummyByDynamicMembersEqualityComparable("abc", 5),
                    "objects with members of different runtime types shouldn't equals");
        }

        [Fact]
        public void ByMembers_WithPredicate_CreatesComparerBasedOnSelectedMembersEquality()
        {
            var comparer =
                EqualityComparer.ByMembers<DummyByMembersEqualityComparable>(
                    member => member.Name.EndsWith("A"));

            comparer.Should()
                .HandleBasicEqualitiesAndInequalites()
                .And.ValidateEqualityOf(
                    new DummyByMembersEqualityComparable(
                        5, new[] {'a', 'b'}, new DateTime(2018, 07, 30), new[] {true, true}),
                    new DummyByMembersEqualityComparable(
                        5, new[] {'c', 'd'}, new DateTime(2018, 07, 30), new[] {false, false}),
                    "unselected members should be ignored")
                .And.InvalidateEqualityOf(
                    new DummyByMembersEqualityComparable(
                        5, new[] {'a', 'b'}, new DateTime(2018, 07, 30), new[] {true, true}),
                    new DummyByMembersEqualityComparable(
                        3, new[] {'a', 'b'}, new DateTime(2018, 07, 30), new[] {true, true}),
                    "objects with different selected members shouldn't equals");
        }

        [Fact]
        public void ByMembers_WithSelector_CreatesComparerBasedOnSelectedMembersEquality()
        {
            var comparer =
                EqualityComparer.ByMembers<DummyByMembersEqualityComparable>(
                    members => members.Where(member => member.Name.EndsWith("A")));

            comparer.Should()
                .HandleBasicEqualitiesAndInequalites()
                .And.ValidateEqualityOf(
                    new DummyByMembersEqualityComparable(
                        5, new[] {'a', 'b'}, new DateTime(2018, 07, 30), new[] {true, true}),
                    new DummyByMembersEqualityComparable(
                        5, new[] {'c', 'd'}, new DateTime(2018, 07, 30), new[] {false, false}),
                    "unselected members should be ignored")
                .And.InvalidateEqualityOf(
                    new DummyByMembersEqualityComparable(
                        5, new[] {'a', 'b'}, new DateTime(2018, 07, 30), new[] {true, true}),
                    new DummyByMembersEqualityComparable(
                        3, new[] {'a', 'b'}, new DateTime(2018, 07, 30), new[] {true, true}),
                    "objects with different selected members shouldn't equals");
        }

        [Fact]
        public void ByProperties_CreatesComparerBasedOnAllPropertiesEquality()
        {
            var comparer =
                EqualityComparer.ByProperties<DummyByMembersEqualityComparable>();

            comparer.Should()
                .HandleBasicEqualitiesAndInequalites()
                .And.ValidateEqualityOf(
                    new DummyByMembersEqualityComparable(
                        5, new[] {'a', 'b'}, new DateTime(2018, 07, 30), new[] {true, true}),
                    new DummyByMembersEqualityComparable(
                        3, new[] {'c', 'd'}, new DateTime(2018, 07, 30), new[] {true, true}),
                    "fields should be ignored")
                .And.InvalidateEqualityOf(
                    new DummyByMembersEqualityComparable(
                        5, new[] {'a', 'b'}, new DateTime(2018, 07, 30), new[] {true, true}),
                    new DummyByMembersEqualityComparable(
                        5, new[] {'a', 'b'}, new DateTime(2018, 06, 24), new[] {true, true}),
                    "objects with different properties shouldn't equals");
        }

        [Fact]
        public void ByProperties_WithPredicate_CreatesComparerBasedOnSelectedPropertiesEquality()
        {
            var comparer =
                EqualityComparer.ByProperties<DummyByMembersEqualityComparable>(
                    property => property.Name.EndsWith("A"));

            comparer.Should()
                .HandleBasicEqualitiesAndInequalites()
                .And.ValidateEqualityOf(
                    new DummyByMembersEqualityComparable(
                        5, new[] {'a', 'b'}, new DateTime(2018, 07, 30), new[] {true, true}),
                    new DummyByMembersEqualityComparable(
                        3, new[] {'c', 'd'}, new DateTime(2018, 07, 30), new[] {false, false}),
                    "unselected properties and every field should be ignored")
                .And.InvalidateEqualityOf(
                    new DummyByMembersEqualityComparable(
                        5, new[] {'a', 'b'}, new DateTime(2018, 07, 30), new[] {true, true}),
                    new DummyByMembersEqualityComparable(
                        5, new[] {'a', 'b'}, new DateTime(2018, 06, 24), new[] {true, true}),
                    "objects with different selected properties shouldn't equals");
        }

        [Fact]
        public void ByProperties_WithSelector_CreatesComparerBasedOnSelectedPropertiesEquality()
        {
            var comparer =
                EqualityComparer.ByProperties<DummyByMembersEqualityComparable>(
                    properties => properties.Where(property => property.Name.EndsWith("A")));

            comparer.Should()
                .HandleBasicEqualitiesAndInequalites()
                .And.ValidateEqualityOf(
                    new DummyByMembersEqualityComparable(
                        5, new[] {'a', 'b'}, new DateTime(2018, 07, 30), new[] {true, true}),
                    new DummyByMembersEqualityComparable(
                        3, new[] {'c', 'd'}, new DateTime(2018, 07, 30), new[] {false, false}),
                    "unselected properties and every field should be ignored")
                .And.InvalidateEqualityOf(
                    new DummyByMembersEqualityComparable(
                        5, new[] {'a', 'b'}, new DateTime(2018, 07, 30), new[] {true, true}),
                    new DummyByMembersEqualityComparable(
                        5, new[] {'a', 'b'}, new DateTime(2018, 06, 24), new[] {true, true}),
                    "objects with different selected properties shouldn't equals");
        }

        [Fact]
        public void ByTaggedFields_CreatesComparerBasedOnTaggedFieldsEquality()
        {
            var comparer =
                EqualityComparer.ByTaggedFields<DummyByTaggedMembersEqualityComparable>();

            comparer.Should()
                .HandleBasicEqualitiesAndInequalites()
                .And.ValidateEqualityOf(
                    new DummyByTaggedMembersEqualityComparable(5, "abc", true, '!'),
                    new DummyByTaggedMembersEqualityComparable(5, "def", false, '*'),
                    "not tagged properties and every field should be ignored")
                .And.InvalidateEqualityOf(
                    new DummyByTaggedMembersEqualityComparable(5, "abc", true, '!'),
                    new DummyByTaggedMembersEqualityComparable(3, "abc", true, '!'),
                    "objects with different tagged properties shouldn't equals");
        }

        [Fact]
        public void ByTaggedMembers_CreatesComparerBasedOnTaggedMembersEquality()
        {
            var comparer =
                EqualityComparer.ByTaggedMembers<DummyByTaggedMembersEqualityComparable>();

            comparer.Should()
                .HandleBasicEqualitiesAndInequalites()
                .And.ValidateEqualityOf(
                    new DummyByTaggedMembersEqualityComparable(5, "abc", true, '!'),
                    new DummyByTaggedMembersEqualityComparable(5, "def", false, '!'),
                    "not tagged members should be ignored")
                .And.InvalidateEqualityOf(
                    new DummyByTaggedMembersEqualityComparable(5, "abc", true, '!'),
                    new DummyByTaggedMembersEqualityComparable(3, "def", false, '!'),
                    "objects with different tagged members shouldn't equals");
        }

        [Fact]
        public void ByTaggedProperties_CreatesComparerBasedOnTaggedPropertiesEquality()
        {
            var comparer =
                EqualityComparer.ByTaggedProperties<DummyByTaggedMembersEqualityComparable>();

            comparer.Should()
                .HandleBasicEqualitiesAndInequalites()
                .And.ValidateEqualityOf(
                    new DummyByTaggedMembersEqualityComparable(5, "abc", true, '!'),
                    new DummyByTaggedMembersEqualityComparable(3, "def", false, '!'),
                    "not tagged properties and every field should be ignored")
                .And.InvalidateEqualityOf(
                    new DummyByTaggedMembersEqualityComparable(5, "abc", true, '!'),
                    new DummyByTaggedMembersEqualityComparable(5, "abc", true, '*'),
                    "objects with different tagged properties shouldn't equals");
        }
    }
}