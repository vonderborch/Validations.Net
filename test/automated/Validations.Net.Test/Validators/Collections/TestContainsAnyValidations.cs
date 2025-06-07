using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Validations.Net.Validators.Collections;
using Validations.Net;

namespace Validations.Net.Test.Validators.Collections
{
    [TestFixture]
    public class TestContainsAnyValidations
    {
        [Test]
        public void CheckContainsAny_IEnumerable_ContainsAny_ReturnsTrue()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            var items = new List<int> { 4, 2 };
            Assert.That(collection.CheckContainsAny(items), Is.True);
        }

        [Test]
        public void CheckContainsAny_IEnumerable_ContainsNone_ReturnsFalse()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            var items = new List<int> { 4, 5 };
            Assert.That(collection.CheckContainsAny(items), Is.False);
        }

        [Test]
        public void CheckContainsAny_IEnumerable_ContainsAnyWithComparer_ReturnsTrue()
        {
            IEnumerable<string> collection = new List<string> { "a", "b", "c" };
            var items = new List<string> { "A", "z" };
            Assert.That(collection.CheckContainsAny(items, StringComparer.OrdinalIgnoreCase), Is.True);
        }

        [Test]
        public void CheckContainsAny_IEnumerable_ContainsNoneWithComparer_ReturnsFalse()
        {
            IEnumerable<string> collection = new List<string> { "a", "b", "c" };
            var items = new List<string> { "x", "z" };
            Assert.That(collection.CheckContainsAny(items, StringComparer.OrdinalIgnoreCase), Is.False);
        }

        [Test]
        public void ValidateContainsAny_ICollection_ContainsAny_ReturnsCollection()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            var items = new List<int> { 4, 2 };
            var result = collection.ValidateContainsAny(items, "test");
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateContainsAny_ICollection_ContainsNone_Throws()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            var items = new List<int> { 4, 5 };
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateContainsAny(items, "test"));
            Assert.That(ex.Message, Does.Contain("must contain at least one of the specified items"));
        }

        [Test]
        public void CheckContainsAny_ICollection_ContainsAnyWithComparer_ReturnsTrue()
        {
            ICollection<string> collection = new List<string> { "a", "b", "c" };
            var items = new List<string> { "A", "z" };
            Assert.That(collection.CheckContainsAny(items, StringComparer.OrdinalIgnoreCase), Is.True);
        }

        [Test]
        public void CheckContainsAny_ICollection_ContainsNoneWithComparer_ReturnsFalse()
        {
            ICollection<string> collection = new List<string> { "a", "b", "c" };
            var items = new List<string> { "x", "z" };
            Assert.That(collection.CheckContainsAny(items, StringComparer.OrdinalIgnoreCase), Is.False);
        }

        [Test]
        public void ValidateContainsAny_ICollection_ContainsAnyWithComparer_ReturnsCollection()
        {
            ICollection<string> collection = new List<string> { "a", "b", "c" };
            var items = new List<string> { "A", "z" };
            var result = collection.ValidateContainsAny(items, "test", StringComparer.OrdinalIgnoreCase);
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateContainsAny_ICollection_ContainsNoneWithComparer_Throws()
        {
            ICollection<string> collection = new List<string> { "a", "b", "c" };
            var items = new List<string> { "x", "z" };
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateContainsAny(items, "test", StringComparer.OrdinalIgnoreCase));
            Assert.That(ex.Message, Does.Contain("must contain at least one of the specified items"));
        }

        [Test]
        public void CheckContainsAny_ICollection_Params_ContainsAny_ReturnsTrue()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            Assert.That(collection.CheckContainsAny(null, 4, 2), Is.True);
        }

        [Test]
        public void CheckContainsAny_ICollection_Params_ContainsNone_ReturnsFalse()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            Assert.That(collection.CheckContainsAny(null, 4, 5), Is.False);
        }

        [Test]
        public void ValidateContainsAny_ICollection_Params_ContainsAny_ReturnsCollection()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            var result = collection.ValidateContainsAny("test", null, 4, 2);
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateContainsAny_ICollection_Params_ContainsNone_Throws()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateContainsAny("test", null, 4, 5));
            Assert.That(ex.Message, Does.Contain("must contain at least one of the specified items"));
        }

        [Test]
        public void CheckContainsAny_IEnumerable_PredicateMatch_ReturnsTrue()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            Assert.That(collection.CheckContainsAny(x => x > 2), Is.True);
        }

        [Test]
        public void CheckContainsAny_IEnumerable_PredicateNoMatch_ReturnsFalse()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            Assert.That(collection.CheckContainsAny(x => x > 5), Is.False);
        }

        [Test]
        public void ValidateContainsAny_IEnumerable_PredicateMatch_ReturnsCollection()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            var result = collection.ValidateContainsAny(x => x > 2, "test");
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void CheckContainsAny_ICollection_PredicateMatch_ReturnsTrue()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            Assert.That(collection.CheckContainsAny(x => x > 2), Is.True);
        }

        [Test]
        public void CheckContainsAny_ICollection_PredicateNoMatch_ReturnsFalse()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            Assert.That(collection.CheckContainsAny(x => x > 5), Is.False);
        }

        [Test]
        public void ValidateContainsAny_ICollection_PredicateMatch_ReturnsCollection()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            var result = collection.ValidateContainsAny(x => x > 2, "test");
            Assert.That(result, Is.EqualTo(collection));
        }
    }
} 