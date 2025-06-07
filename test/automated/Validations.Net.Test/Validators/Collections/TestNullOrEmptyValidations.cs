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
    public class TestNullOrEmptyValidations
    {
        [Test]
        public void CheckIsNullOrEmpty_ICollection_Null_ReturnsTrue()
        {
            List<int>? collection = null;
            Assert.That(collection.CheckIsNullOrEmpty(), Is.True);
        }

        [Test]
        public void CheckIsNullOrEmpty_ICollection_Empty_ReturnsTrue()
        {
            var collection = new List<int>();
            Assert.That(collection.CheckIsNullOrEmpty(), Is.True);
        }

        [Test]
        public void CheckIsNullOrEmpty_ICollection_NotEmpty_ReturnsFalse()
        {
            var collection = new List<int> { 1 };
            Assert.That(collection.CheckIsNullOrEmpty(), Is.False);
        }

        [Test]
        public void CheckIsNullOrEmpty_IEnumerable_Null_ReturnsTrue()
        {
            IEnumerable<int>? collection = null;
            Assert.That(collection.CheckIsNullOrEmpty(), Is.True);
        }

        [Test]
        public void CheckIsNullOrEmpty_IEnumerable_Empty_ReturnsTrue()
        {
            IEnumerable<int> collection = Enumerable.Empty<int>();
            Assert.That(collection.CheckIsNullOrEmpty(), Is.True);
        }

        [Test]
        public void CheckIsNullOrEmpty_IEnumerable_NotEmpty_ReturnsFalse()
        {
            IEnumerable<int> collection = new List<int> { 1 };
            Assert.That(collection.CheckIsNullOrEmpty(), Is.False);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_ICollection_Null_ReturnsFalse()
        {
            List<int>? collection = null;
            Assert.That(collection.CheckIsNotNullOrEmpty(), Is.False);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_ICollection_Empty_ReturnsFalse()
        {
            var collection = new List<int>();
            Assert.That(collection.CheckIsNotNullOrEmpty(), Is.False);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_ICollection_NotEmpty_ReturnsTrue()
        {
            var collection = new List<int> { 1 };
            Assert.That(collection.CheckIsNotNullOrEmpty(), Is.True);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_IEnumerable_Null_ReturnsFalse()
        {
            IEnumerable<int>? collection = null;
            Assert.That(collection.CheckIsNotNullOrEmpty(), Is.False);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_IEnumerable_Empty_ReturnsFalse()
        {
            IEnumerable<int> collection = Enumerable.Empty<int>();
            Assert.That(collection.CheckIsNotNullOrEmpty(), Is.False);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_IEnumerable_NotEmpty_ReturnsTrue()
        {
            IEnumerable<int> collection = new List<int> { 1 };
            Assert.That(collection.CheckIsNotNullOrEmpty(), Is.True);
        }

        [Test]
        public void ValidateIsNullOrEmpty_ICollection_Null_ReturnsNull()
        {
            List<int>? collection = null;
            var result = collection.ValidateIsNullOrEmpty("test");
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ValidateIsNullOrEmpty_ICollection_Empty_ReturnsCollection()
        {
            var collection = new List<int>();
            var result = collection.ValidateIsNullOrEmpty("test");
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateIsNullOrEmpty_ICollection_NotEmpty_Throws()
        {
            var collection = new List<int> { 1 };
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateIsNullOrEmpty("test"));
            Assert.That(ex.Message, Does.Contain("must be null or empty"));
        }

        [Test]
        public void ValidateIsNullOrEmpty_IEnumerable_Null_ReturnsNull()
        {
            IEnumerable<int>? collection = null;
            var result = collection.ValidateIsNullOrEmpty("test");
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ValidateIsNullOrEmpty_IEnumerable_Empty_ReturnsCollection()
        {
            IEnumerable<int> collection = Enumerable.Empty<int>();
            var result = collection.ValidateIsNullOrEmpty("test");
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateIsNullOrEmpty_IEnumerable_NotEmpty_Throws()
        {
            IEnumerable<int> collection = new List<int> { 1 };
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateIsNullOrEmpty("test"));
            Assert.That(ex.Message, Does.Contain("must be null or empty"));
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_ICollection_NotEmpty_ReturnsCollection()
        {
            var collection = new List<int> { 1 };
            var result = collection.ValidateIsNotNullOrEmpty("test");
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_ICollection_Null_Throws()
        {
            List<int>? collection = null;
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateIsNotNullOrEmpty("test"));
            Assert.That(ex.Message, Does.Contain("must not be null or empty"));
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_ICollection_Empty_Throws()
        {
            var collection = new List<int>();
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateIsNotNullOrEmpty("test"));
            Assert.That(ex.Message, Does.Contain("must not be null or empty"));
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_IEnumerable_NotEmpty_ReturnsCollection()
        {
            IEnumerable<int> collection = new List<int> { 1 };
            var result = collection.ValidateIsNotNullOrEmpty("test");
            Assert.That(result, Is.EqualTo(collection));
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_IEnumerable_Null_Throws()
        {
            IEnumerable<int>? collection = null;
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateIsNotNullOrEmpty("test"));
            Assert.That(ex.Message, Does.Contain("must not be null or empty"));
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_IEnumerable_Empty_Throws()
        {
            IEnumerable<int> collection = Enumerable.Empty<int>();
            var ex = Assert.Throws<ValidationException>(() => collection.ValidateIsNotNullOrEmpty("test"));
            Assert.That(ex.Message, Does.Contain("must not be null or empty"));
        }
    }
} 
