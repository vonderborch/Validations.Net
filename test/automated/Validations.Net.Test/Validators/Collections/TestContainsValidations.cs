using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Validations.Net.Validators.Collections;
using Validations.Net;

namespace Validations.Net.Test.Validators.Collections
{
    [TestFixture]
    public class TestContainsValidations
    {
        [Test]
        public void CheckContains_IEnumerable_ContainsItem_ReturnsTrue()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            Assert.That(collection.CheckContains(2), Is.True);
        }

        [Test]
        public void CheckContains_IEnumerable_DoesNotContainItem_ReturnsFalse()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            Assert.That(collection.CheckContains(4), Is.False);
        }

        [Test]
        public void CheckContains_IEnumerable_ContainsItemWithComparer_ReturnsTrue()
        {
            IEnumerable<string> collection = new List<string> { "a", "b", "c" };
            Assert.That(collection.CheckContains("A", StringComparer.OrdinalIgnoreCase), Is.True);
        }

        [Test]
        public void CheckContains_IEnumerable_DoesNotContainItemWithComparer_ReturnsFalse()
        {
            IEnumerable<string> collection = new List<string> { "a", "b", "c" };
            Assert.That(collection.CheckContains("d", StringComparer.OrdinalIgnoreCase), Is.False);
        }

        [Test]
        public void ValidateContains_IEnumerable_ContainsItem_ReturnsCollection()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            var result = collection.ValidateContains(2, "test");
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateContains_IEnumerable_DoesNotContainItem_Throws()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateContains(4, "test"));
            Assert.That(ex.Message, Does.Contain("must contain 4"));
        }

        [Test]
        public void CheckContains_ICollection_ContainsItemWithComparer_ReturnsTrue()
        {
            ICollection<string> collection = new List<string> { "a", "b", "c" };
            Assert.That(collection.CheckContains("A", StringComparer.OrdinalIgnoreCase), Is.True);
        }

        [Test]
        public void CheckContains_ICollection_DoesNotContainItemWithComparer_ReturnsFalse()
        {
            ICollection<string> collection = new List<string> { "a", "b", "c" };
            Assert.That(collection.CheckContains("d", StringComparer.OrdinalIgnoreCase), Is.False);
        }

        [Test]
        public void ValidateContains_ICollection_ContainsItem_ReturnsCollection()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            var result = collection.ValidateContains(2, "test");
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateContains_ICollection_DoesNotContainItem_Throws()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateContains(4, "test"));
            Assert.That(ex.Message, Does.Contain("must contain 4"));
        }

        [Test]
        public void CheckContains_IEnumerable_PredicateMatch_ReturnsTrue()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            Assert.That(collection.CheckContains(x => x > 2), Is.True);
        }

        [Test]
        public void CheckContains_IEnumerable_PredicateNoMatch_ReturnsFalse()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            Assert.That(collection.CheckContains(x => x > 5), Is.False);
        }

        [Test]
        public void ValidateContains_IEnumerable_PredicateMatch_ReturnsCollection()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            var result = collection.ValidateContains(x => x > 2, "test");
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateContains_IEnumerable_PredicateNoMatch_Throws()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateContains(x => x > 5, "test"));
            Assert.That(ex.Message, Does.Contain("must contain an item that matches the predicate"));
        }

        [Test]
        public void CheckContains_ICollection_PredicateMatch_ReturnsTrue()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            Assert.That(collection.CheckContains(x => x > 2), Is.True);
        }

        [Test]
        public void CheckContains_ICollection_PredicateNoMatch_ReturnsFalse()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            Assert.That(collection.CheckContains(x => x > 5), Is.False);
        }

        [Test]
        public void ValidateContains_ICollection_PredicateMatch_ReturnsCollection()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            var result = collection.ValidateContains(x => x > 2, "test");
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateContains_ICollection_PredicateNoMatch_Throws()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateContains(x => x > 5, "test"));
            Assert.That(ex.Message, Does.Contain("must contain an item that matches the predicate"));
        }
    }
} 