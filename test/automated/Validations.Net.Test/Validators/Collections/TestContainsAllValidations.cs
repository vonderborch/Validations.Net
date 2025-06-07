using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Validations.Net.Validators.Collections;
using Validations.Net;

namespace Validations.Net.Test.Validators.Collections
{
    [TestFixture]
    public class TestContainsAllValidations
    {
        [Test]
        public void CheckContainsAll_IEnumerable_ContainsAll_ReturnsTrue()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3, 4 };
            var items = new List<int> { 2, 3 };
            Assert.That(collection.CheckContainsAll(items), Is.True);
        }

        [Test]
        public void CheckContainsAll_IEnumerable_DoesNotContainAll_ReturnsFalse()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            var items = new List<int> { 2, 4 };
            Assert.That(collection.CheckContainsAll(items), Is.False);
        }

        [Test]
        public void CheckContainsAll_IEnumerable_ContainsAllWithComparer_ReturnsTrue()
        {
            IEnumerable<string> collection = new List<string> { "a", "b", "c" };
            var items = new List<string> { "A", "B" };
            Assert.That(collection.CheckContainsAll(items, StringComparer.OrdinalIgnoreCase), Is.True);
        }

        [Test]
        public void CheckContainsAll_IEnumerable_DoesNotContainAllWithComparer_ReturnsFalse()
        {
            IEnumerable<string> collection = new List<string> { "a", "b", "c" };
            var items = new List<string> { "A", "Z" };
            Assert.That(collection.CheckContainsAll(items, StringComparer.OrdinalIgnoreCase), Is.False);
        }

        [Test]
        public void ValidateContainsAll_IEnumerable_ContainsAll_ReturnsCollection()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3, 4 };
            var items = new List<int> { 2, 3 };
            var result = collection.ValidateContainsAll(items, "test");
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateContainsAll_IEnumerable_DoesNotContainAll_Throws()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            var items = new List<int> { 2, 4 };
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateContainsAll(items, "test"));
            Assert.That(ex.Message, Does.Contain("must contain all items in the specified collection."));
        }

        [Test]
        public void ValidateContainsAll_IEnumerable_ContainsAllWithComparer_ReturnsCollection()
        {
            IEnumerable<string> collection = new List<string> { "a", "b", "c" };
            var items = new List<string> { "A", "B" };
            var result = collection.ValidateContainsAll(items, "test", StringComparer.OrdinalIgnoreCase);
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateContainsAll_IEnumerable_DoesNotContainAllWithComparer_Throws()
        {
            IEnumerable<string> collection = new List<string> { "a", "b", "c" };
            var items = new List<string> { "A", "Z" };
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateContainsAll(items, "test", StringComparer.OrdinalIgnoreCase));
            Assert.That(ex.Message, Does.Contain("must contain all items in the specified collection."));
        }
    }
} 
