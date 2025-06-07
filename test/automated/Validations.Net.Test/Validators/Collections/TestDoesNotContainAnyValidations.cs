using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Validations.Net.Validators.Collections;
using Validations.Net;

namespace Validations.Net.Test.Validators.Collections
{
    [TestFixture]
    public class TestDoesNotContainAnyValidations
    {
        [Test]
        public void CheckDoesNotContainAny_IEnumerable_ContainsNone_ReturnsTrue()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            var items = new List<int> { 4, 5 };
            Assert.That(collection.CheckDoesNotContainAny(items), Is.True);
        }

        [Test]
        public void CheckDoesNotContainAny_IEnumerable_ContainsAny_ReturnsFalse()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            var items = new List<int> { 2, 5 };
            Assert.That(collection.CheckDoesNotContainAny(items), Is.False);
        }

        [Test]
        public void CheckDoesNotContainAny_IEnumerable_ContainsNoneWithComparer_ReturnsTrue()
        {
            IEnumerable<string> collection = new List<string> { "a", "b", "c" };
            var items = new List<string> { "x", "z" };
            Assert.That(collection.CheckDoesNotContainAny(items, StringComparer.OrdinalIgnoreCase), Is.True);
        }

        [Test]
        public void CheckDoesNotContainAny_IEnumerable_ContainsAnyWithComparer_ReturnsFalse()
        {
            IEnumerable<string> collection = new List<string> { "a", "b", "c" };
            var items = new List<string> { "A", "z" };
            Assert.That(collection.CheckDoesNotContainAny(items, StringComparer.OrdinalIgnoreCase), Is.False);
        }

        [Test]
        public void ValidateDoesNotContainAny_ICollection_ContainsNone_ReturnsCollection()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            var items = new List<int> { 4, 5 };
            var result = collection.ValidateDoesNotContainAny(items, "test");
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateDoesNotContainAny_ICollection_ContainsAny_Throws()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            var items = new List<int> { 2, 5 };
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateDoesNotContainAny(items, "test"));
            Assert.That(ex.Message, Does.Contain("must not contain any of the specified items"));
        }

        [Test]
        public void CheckDoesNotContainAny_ICollection_ContainsNoneWithComparer_ReturnsTrue()
        {
            ICollection<string> collection = new List<string> { "a", "b", "c" };
            var items = new List<string> { "x", "z" };
            Assert.That(collection.CheckDoesNotContainAny(items, StringComparer.OrdinalIgnoreCase), Is.True);
        }

        [Test]
        public void CheckDoesNotContainAny_ICollection_ContainsAnyWithComparer_ReturnsFalse()
        {
            ICollection<string> collection = new List<string> { "a", "b", "c" };
            var items = new List<string> { "A", "z" };
            Assert.That(collection.CheckDoesNotContainAny(items, StringComparer.OrdinalIgnoreCase), Is.False);
        }

        [Test]
        public void ValidateDoesNotContainAny_ICollection_ContainsNoneWithComparer_ReturnsCollection()
        {
            ICollection<string> collection = new List<string> { "a", "b", "c" };
            var items = new List<string> { "x", "z" };
            var result = collection.ValidateDoesNotContainAny(items, "test", StringComparer.OrdinalIgnoreCase);
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateDoesNotContainAny_ICollection_ContainsAnyWithComparer_Throws()
        {
            ICollection<string> collection = new List<string> { "a", "b", "c" };
            var items = new List<string> { "A", "z" };
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateDoesNotContainAny(items, "test", StringComparer.OrdinalIgnoreCase));
            Assert.That(ex.Message, Does.Contain("must not contain any of the specified items"));
        }

        [Test]
        public void CheckDoesNotContainAny_ICollection_Params_ContainsNone_ReturnsTrue()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            Assert.That(collection.CheckDoesNotContainAny(null, 4, 5), Is.True);
        }

        [Test]
        public void CheckDoesNotContainAny_ICollection_Params_ContainsAny_ReturnsFalse()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            Assert.That(collection.CheckDoesNotContainAny(null, 2, 5), Is.False);
        }

        [Test]
        public void ValidateDoesNotContainAny_ICollection_Params_ContainsNone_ReturnsCollection()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            var result = collection.ValidateDoesNotContainAny("test", null, 4, 5);
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateDoesNotContainAny_ICollection_Params_ContainsAny_Throws()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateDoesNotContainAny("test", null, 2, 5));
            Assert.That(ex.Message, Does.Contain("must not contain any of the specified items"));
        }

        [Test]
        public void CheckDoesNotContainAny_IEnumerable_PredicateNoMatch_ReturnsTrue()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            Assert.That(collection.CheckDoesNotContainAny(x => x > 5), Is.True);
        }

        [Test]
        public void CheckDoesNotContainAny_IEnumerable_PredicateMatch_ReturnsFalse()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            Assert.That(collection.CheckDoesNotContainAny(x => x > 2), Is.False);
        }

        [Test]
        public void ValidateDoesNotContainAny_IEnumerable_PredicateNoMatch_ReturnsCollection()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            var result = collection.ValidateDoesNotContainAny(x => x > 5, "test");
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateDoesNotContainAny_IEnumerable_PredicateMatch_Throws()
        {
            IEnumerable<int> collection = new List<int> { 1, 2, 3 };
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateDoesNotContainAny(x => x > 2, "test"));
            Assert.That(ex.Message, Does.Contain("must not contain any items that match the specified predicate"));
        }

        [Test]
        public void CheckDoesNotContainAny_ICollection_PredicateNoMatch_ReturnsTrue()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            Assert.That(collection.CheckDoesNotContainAny(x => x > 5), Is.True);
        }

        [Test]
        public void CheckDoesNotContainAny_ICollection_PredicateMatch_ReturnsFalse()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            Assert.That(collection.CheckDoesNotContainAny(x => x > 2), Is.False);
        }

        [Test]
        public void ValidateDoesNotContainAny_ICollection_PredicateNoMatch_ReturnsCollection()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            var result = collection.ValidateDoesNotContainAny(x => x > 5, "test");
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateDoesNotContainAny_ICollection_PredicateMatch_Throws()
        {
            ICollection<int> collection = new List<int> { 1, 2, 3 };
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateDoesNotContainAny(x => x > 2, "test"));
            Assert.That(ex.Message, Does.Contain("must not contain any items that match the specified predicate"));
        }
    }
} 
