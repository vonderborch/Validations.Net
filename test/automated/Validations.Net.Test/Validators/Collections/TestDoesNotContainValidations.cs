using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Validations.Net.Validators.Collections;
using Validations.Net;

namespace Validations.Net.Test.Validators.Collections
{
    [TestFixture]
    public class TestDoesNotContainValidations
    {
        [Test]
        public void CheckDoesNotContain_IEnumerable_DoesNotContainItem_ReturnsTrue()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            Assert.That(collection.CheckDoesNotContain(4), Is.True);
        }

        [Test]
        public void CheckDoesNotContain_IEnumerable_ContainsItem_ReturnsFalse()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            Assert.That(collection.CheckDoesNotContain(2), Is.False);
        }

        [Test]
        public void CheckDoesNotContain_IEnumerable_DoesNotContainItemWithComparer_ReturnsTrue()
        {
            IEnumerable<string> collection = new List<string> { "a", "b", "c" };
            Assert.That(collection.CheckDoesNotContain("D", StringComparer.OrdinalIgnoreCase), Is.True);
        }

        [Test]
        public void CheckDoesNotContain_IEnumerable_ContainsItemWithComparer_ReturnsFalse()
        {
            IEnumerable<string> collection = new List<string> { "a", "b", "c" };
            Assert.That(collection.CheckDoesNotContain("A", StringComparer.OrdinalIgnoreCase), Is.False);
        }

        [Test]
        public void ValidateDoesNotContain_IEnumerable_DoesNotContainItem_ReturnsCollection()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            var result = collection.ValidateDoesNotContain(4, "test");
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateDoesNotContain_IEnumerable_ContainsItem_Throws()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateDoesNotContain(2, "test"));
            Assert.That(ex.Message, Does.Contain("must not contain 2"));
        }

        [Test]
        public void CheckDoesNotContain_ICollection_DoesNotContainItemWithComparer_ReturnsTrue()
        {
            ICollection<string> collection = new List<string> { "a", "b", "c" };
            Assert.That(collection.CheckDoesNotContain("D", StringComparer.OrdinalIgnoreCase), Is.True);
        }

        [Test]
        public void CheckDoesNotContain_ICollection_ContainsItemWithComparer_ReturnsFalse()
        {
            ICollection<string> collection = new List<string> { "a", "b", "c" };
            Assert.That(collection.CheckDoesNotContain("A", StringComparer.OrdinalIgnoreCase), Is.False);
        }

        [Test]
        public void ValidateDoesNotContain_ICollection_DoesNotContainItem_ReturnsCollection()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            var result = collection.ValidateDoesNotContain(4, "test");
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateDoesNotContain_ICollection_ContainsItem_Throws()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateDoesNotContain(2, "test"));
            Assert.That(ex.Message, Does.Contain("must not contain 2"));
        }

        [Test]
        public void CheckDoesNotContain_IEnumerable_PredicateNoMatch_ReturnsTrue()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            Assert.That(collection.CheckDoesNotContain(x => x > 5), Is.True);
        }

        [Test]
        public void CheckDoesNotContain_IEnumerable_PredicateMatch_ReturnsFalse()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            Assert.That(collection.CheckDoesNotContain(x => x > 2), Is.False);
        }

        [Test]
        public void ValidateDoesNotContain_IEnumerable_PredicateNoMatch_ReturnsCollection()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            var result = collection.ValidateDoesNotContain(x => x > 5, "test");
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateDoesNotContain_IEnumerable_PredicateMatch_Throws()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateDoesNotContain(x => x > 2, "test"));
            Assert.That(ex.Message, Does.Contain("must not contain any item that matches the predicate"));
        }

        [Test]
        public void CheckDoesNotContain_ICollection_PredicateNoMatch_ReturnsTrue()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            Assert.That(collection.CheckDoesNotContain(x => x > 5), Is.True);
        }

        [Test]
        public void CheckDoesNotContain_ICollection_PredicateMatch_ReturnsFalse()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            Assert.That(collection.CheckDoesNotContain(x => x > 2), Is.False);
        }

        [Test]
        public void ValidateDoesNotContain_ICollection_PredicateNoMatch_ReturnsCollection()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            var result = collection.ValidateDoesNotContain(x => x > 5, "test");
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateDoesNotContain_ICollection_PredicateMatch_Throws()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateDoesNotContain(x => x > 2, "test"));
            Assert.That(ex.Message, Does.Contain("must not contain any item that matches the predicate"));
        }
    }
} 