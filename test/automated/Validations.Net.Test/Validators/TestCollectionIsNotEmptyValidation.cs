using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Validations.Net.Validators;
using Validations.Net;

namespace Validations.Net.Test.Validators
{
    [TestFixture]
    public class TestCollectionIsNotEmptyValidation
    {
        [Test]
        public void IsNotEmpty_IEnumerable_NotEmpty_ReturnsTrue()
        {
            IEnumerable<int> notEmpty = new List<int> { 1 };
            Assert.That(CollectionIsNotEmptyValidation.IsNotEmpty(notEmpty), Is.True);
        }

        [Test]
        public void IsNotEmpty_IEnumerable_Empty_ReturnsFalse()
        {
            IEnumerable<int> empty = new List<int>();
            Assert.That(CollectionIsNotEmptyValidation.IsNotEmpty(empty), Is.False);
        }

        [Test]
        public void IsNotEmpty_ICollection_NotEmpty_ReturnsTrue()
        {
            ICollection<int> notEmpty = new List<int> { 1 };
            Assert.That(CollectionIsNotEmptyValidation.IsNotEmpty(notEmpty), Is.True);
        }

        [Test]
        public void IsNotEmpty_ICollection_Empty_ReturnsFalse()
        {
            ICollection<int> empty = new List<int>();
            Assert.That(CollectionIsNotEmptyValidation.IsNotEmpty(empty), Is.False);
        }

        [Test]
        public void CheckIsNotEmpty_IEnumerable_NotEmpty_ReturnsTrue()
        {
            IEnumerable<int> notEmpty = new List<int> { 1 };
            Assert.That(notEmpty.CheckIsNotEmpty(), Is.True);
        }

        [Test]
        public void CheckIsNotEmpty_IEnumerable_Empty_ReturnsFalse()
        {
            IEnumerable<int> empty = new List<int>();
            Assert.That(empty.CheckIsNotEmpty(), Is.False);
        }

        [Test]
        public void CheckIsNotEmpty_ICollection_NotEmpty_ReturnsTrue()
        {
            ICollection<int> notEmpty = new List<int> { 1 };
            Assert.That(notEmpty.CheckIsNotEmpty(), Is.True);
        }

        [Test]
        public void CheckIsNotEmpty_ICollection_Empty_ReturnsFalse()
        {
            ICollection<int> empty = new List<int>();
            Assert.That(empty.CheckIsNotEmpty(), Is.False);
        }

        [Test]
        public void IsNotEmptyValidation_IEnumerable_NotEmpty_DoesNotThrow()
        {
            IEnumerable<int> notEmpty = new List<int> { 1 };
            Assert.DoesNotThrow(() => CollectionIsNotEmptyValidation.IsNotEmptyValidation(notEmpty, nameof(notEmpty)));
        }

        [Test]
        public void IsNotEmptyValidation_IEnumerable_Empty_Throws()
        {
            IEnumerable<int> empty = new List<int>();
            Assert.Throws<ValidationException>(() => CollectionIsNotEmptyValidation.IsNotEmptyValidation(empty, nameof(empty)));
        }

        [Test]
        public void IsNotEmptyValidation_ICollection_NotEmpty_DoesNotThrow()
        {
            ICollection<int> notEmpty = new List<int> { 1 };
            Assert.DoesNotThrow(() => CollectionIsNotEmptyValidation.IsNotEmptyValidation(notEmpty, nameof(notEmpty)));
        }

        [Test]
        public void IsNotEmptyValidation_ICollection_Empty_Throws()
        {
            ICollection<int> empty = new List<int>();
            Assert.Throws<ValidationException>(() => CollectionIsNotEmptyValidation.IsNotEmptyValidation(empty, nameof(empty)));
        }

        [Test]
        public void ValidateIsNotEmpty_IEnumerable_NotEmpty_DoesNotThrow()
        {
            IEnumerable<int> notEmpty = new List<int> { 1 };
            Assert.DoesNotThrow(() => notEmpty.ValidateIsNotEmpty(nameof(notEmpty)));
        }

        [Test]
        public void ValidateIsNotEmpty_IEnumerable_Empty_Throws()
        {
            IEnumerable<int> empty = new List<int>();
            Assert.Throws<ValidationException>(() => empty.ValidateIsNotEmpty(nameof(empty)));
        }

        [Test]
        public void ValidateIsNotEmpty_ICollection_NotEmpty_DoesNotThrow()
        {
            ICollection<int> notEmpty = new List<int> { 1 };
            Assert.DoesNotThrow(() => notEmpty.ValidateIsNotEmpty(nameof(notEmpty)));
        }

        [Test]
        public void ValidateIsNotEmpty_ICollection_Empty_Throws()
        {
            ICollection<int> empty = new List<int>();
            Assert.Throws<ValidationException>(() => empty.ValidateIsNotEmpty(nameof(empty)));
        }
    }
} 