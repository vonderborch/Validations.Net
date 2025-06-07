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
    public class TestEmptyValidations
    {
        [Test]
        public void CheckIsEmpty_ICollection_Empty_ReturnsTrue()
        {
            var collection = new List<int>();
            Assert.That(collection.CheckIsEmpty(), Is.True);
        }

        [Test]
        public void CheckIsEmpty_ICollection_NotEmpty_ReturnsFalse()
        {
            var collection = new List<int> { 1 };
            Assert.That(collection.CheckIsEmpty(), Is.False);
        }

        [Test]
        public void CheckIsEmpty_IEnumerable_Empty_ReturnsTrue()
        {
            IEnumerable<int> collection = Enumerable.Empty<int>();
            Assert.That(collection.CheckIsEmpty(), Is.True);
        }

        [Test]
        public void CheckIsEmpty_IEnumerable_NotEmpty_ReturnsFalse()
        {
            IEnumerable<int> collection = new List<int> { 1 };
            Assert.That(collection.CheckIsEmpty(), Is.False);
        }

        [Test]
        public void CheckIsNotEmpty_ICollection_Empty_ReturnsFalse()
        {
            var collection = new List<int>();
            Assert.That(collection.CheckIsNotEmpty(), Is.False);
        }

        [Test]
        public void CheckIsNotEmpty_ICollection_NotEmpty_ReturnsTrue()
        {
            var collection = new List<int> { 1 };
            Assert.That(collection.CheckIsNotEmpty(), Is.True);
        }

        [Test]
        public void CheckIsNotEmpty_IEnumerable_Empty_ReturnsFalse()
        {
            IEnumerable<int> collection = Enumerable.Empty<int>();
            Assert.That(collection.CheckIsNotEmpty(), Is.False);
        }

        [Test]
        public void CheckIsNotEmpty_IEnumerable_NotEmpty_ReturnsTrue()
        {
            IEnumerable<int> collection = new List<int> { 1 };
            Assert.That(collection.CheckIsNotEmpty(), Is.True);
        }

        [Test]
        public void ValidateIsEmpty_ICollection_Empty_ReturnsCollection()
        {
            var collection = new List<int>();
            var result = collection.ValidateIsEmpty("test");
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateIsEmpty_ICollection_NotEmpty_Throws()
        {
            var collection = new List<int> { 1 };
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateIsEmpty("test"));
            Assert.That(ex.Message, Does.Contain("must be empty"));
        }

        [Test]
        public void ValidateIsEmpty_IEnumerable_Empty_ReturnsCollection()
        {
            IEnumerable<int> collection = Enumerable.Empty<int>();
            var result = collection.ValidateIsEmpty("test");
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateIsEmpty_IEnumerable_NotEmpty_Throws()
        {
            IEnumerable<int> collection = new List<int> { 1 };
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateIsEmpty("test"));
            Assert.That(ex.Message, Does.Contain("must be empty"));
        }

        [Test]
        public void ValidateIsNotEmpty_ICollection_NotEmpty_ReturnsCollection()
        {
            var collection = new List<int> { 1 };
            var result = collection.ValidateIsNotEmpty("test");
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateIsNotEmpty_ICollection_Empty_Throws()
        {
            var collection = new List<int>();
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateIsNotEmpty("test"));
            Assert.That(ex.Message, Does.Contain("must not be empty"));
        }
    }
} 
