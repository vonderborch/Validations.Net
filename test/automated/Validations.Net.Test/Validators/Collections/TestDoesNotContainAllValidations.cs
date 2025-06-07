using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Validations.Net.Validators.Collections;
using Validations.Net;

namespace Validations.Net.Test.Validators.Collections
{
    [TestFixture]
    public class TestDoesNotContainAllValidations
    {
        [Test]
        public void CheckDoesNotContainAll_IEnumerable_DoesNotContainAll_ReturnsTrue()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            var items = new List<int> { 2, 4 };
            Assert.That(collection.CheckDoesNotContainAll(items), Is.True);
        }

        [Test]
        public void CheckDoesNotContainAll_IEnumerable_ContainsAll_ReturnsFalse()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3, 4 };
            var items = new List<int> { 2, 3 };
            Assert.That(collection.CheckDoesNotContainAll(items), Is.False);
        }

        [Test]
        public void CheckDoesNotContainAll_IEnumerable_DoesNotContainAllWithComparer_ReturnsTrue()
        {
            IEnumerable<string> collection = new List<string> { "a", "b", "c" };
            var items = new List<string> { "A", "Z" };
            Assert.That(collection.CheckDoesNotContainAll(items, StringComparer.OrdinalIgnoreCase), Is.True);
        }

        [Test]
        public void CheckDoesNotContainAll_IEnumerable_ContainsAllWithComparer_ReturnsFalse()
        {
            IEnumerable<string> collection = new List<string> { "a", "b", "c" };
            var items = new List<string> { "A", "B" };
            Assert.That(collection.CheckDoesNotContainAll(items, StringComparer.OrdinalIgnoreCase), Is.False);
        }

        [Test]
        public void ValidateDoesNotContainAll_IEnumerable_DoesNotContainAll_ReturnsCollection()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            var items = new List<int> { 2, 4 };
            var result = collection.ValidateDoesNotContainAll(items, "test");
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateDoesNotContainAll_IEnumerable_ContainsAll_Throws()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3, 4 };
            var items = new List<int> { 2, 3 };
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateDoesNotContainAll(items, "test"));
            Assert.That(ex.Message, Does.Contain("must not contain all items in the specified collection"));
        }

        [Test]
        public void ValidateDoesNotContainAll_IEnumerable_DoesNotContainAllWithComparer_ReturnsCollection()
        {
            IEnumerable<string> collection = new List<string> { "a", "b", "c" };
            var items = new List<string> { "A", "Z" };
            var result = collection.ValidateDoesNotContainAll(items, "test", StringComparer.OrdinalIgnoreCase);
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateDoesNotContainAll_IEnumerable_ContainsAllWithComparer_Throws()
        {
            IEnumerable<string> collection = new List<string> { "a", "b", "c" };
            var items = new List<string> { "A", "B" };
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateDoesNotContainAll(items, "test", StringComparer.OrdinalIgnoreCase));
            Assert.That(ex.Message, Does.Contain("must not contain all items in the specified collection"));
        }

        [Test]
        public void CheckDoesNotContainAll_IEnumerable_PredicateNoMatch_ReturnsTrue()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            Assert.That(collection.CheckDoesNotContainAll(x => x > 5), Is.True);
        }

        [Test]
        public void CheckDoesNotContainAll_IEnumerable_PredicateAllMatch_ReturnsFalse()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            Assert.That(collection.CheckDoesNotContainAll(x => x > 0), Is.False);
        }

        [Test]
        public void ValidateDoesNotContainAll_IEnumerable_PredicateNoMatch_ReturnsCollection()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            var result = collection.ValidateDoesNotContainAll(x => x > 5, "test");
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateDoesNotContainAll_IEnumerable_PredicateAllMatch_Throws()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateDoesNotContainAll(x => x > 0, "test"));
            Assert.That(ex.Message, Does.Contain("must not contain any items that match the specified predicate"));
        }
    }
} 
