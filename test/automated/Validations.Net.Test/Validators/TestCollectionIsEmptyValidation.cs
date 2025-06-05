using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Validations.Net.Validators;
using Validations.Net;

namespace Validations.Net.Test.Validators
{
    [TestFixture]
    public class TestCollectionIsEmptyValidation
    {
        [Test]
        public void IsEmpty_IEnumerable_Empty_ReturnsTrue()
        {
            IEnumerable<int> empty = new List<int>();
            Assert.That(CollectionIsEmptyValidation.IsEmpty(empty), Is.True);
        }

        [Test]
        public void IsEmpty_IEnumerable_NotEmpty_ReturnsFalse()
        {
            IEnumerable<int> notEmpty = new List<int> { 1 };
            Assert.That(CollectionIsEmptyValidation.IsEmpty(notEmpty), Is.False);
        }

        [Test]
        public void IsEmpty_ICollection_Empty_ReturnsTrue()
        {
            ICollection<int> empty = new List<int>();
            Assert.That(CollectionIsEmptyValidation.IsEmpty(empty), Is.True);
        }

        [Test]
        public void IsEmpty_ICollection_NotEmpty_ReturnsFalse()
        {
            ICollection<int> notEmpty = new List<int> { 1 };
            Assert.That(CollectionIsEmptyValidation.IsEmpty(notEmpty), Is.False);
        }

        [Test]
        public void CheckIsEmpty_IEnumerable_Empty_ReturnsTrue()
        {
            IEnumerable<int> empty = new List<int>();
            Assert.That(empty.CheckIsEmpty(), Is.True);
        }

        [Test]
        public void CheckIsEmpty_IEnumerable_NotEmpty_ReturnsFalse()
        {
            IEnumerable<int> notEmpty = new List<int> { 1 };
            Assert.That(notEmpty.CheckIsEmpty(), Is.False);
        }

        [Test]
        public void CheckIsEmpty_ICollection_Empty_ReturnsTrue()
        {
            ICollection<int> empty = new List<int>();
            Assert.That(empty.CheckIsEmpty(), Is.True);
        }

        [Test]
        public void CheckIsEmpty_ICollection_NotEmpty_ReturnsFalse()
        {
            ICollection<int> notEmpty = new List<int> { 1 };
            Assert.That(notEmpty.CheckIsEmpty(), Is.False);
        }

        [Test]
        public void IsEmptyValidation_IEnumerable_Empty_DoesNotThrow()
        {
            IEnumerable<int> empty = new List<int>();
            Assert.DoesNotThrow(() => CollectionIsEmptyValidation.IsEmptyValidation(empty, nameof(empty)));
        }

        [Test]
        public void IsEmptyValidation_IEnumerable_NotEmpty_Throws()
        {
            IEnumerable<int> notEmpty = new List<int> { 1 };
            Assert.Throws<ValidationException>(() => CollectionIsEmptyValidation.IsEmptyValidation(notEmpty, nameof(notEmpty)));
        }

        [Test]
        public void IsEmptyValidation_ICollection_Empty_DoesNotThrow()
        {
            ICollection<int> empty = new List<int>();
            Assert.DoesNotThrow(() => CollectionIsEmptyValidation.IsEmptyValidation(empty, nameof(empty)));
        }

        [Test]
        public void IsEmptyValidation_ICollection_NotEmpty_Throws()
        {
            ICollection<int> notEmpty = new List<int> { 1 };
            Assert.Throws<ValidationException>(() => CollectionIsEmptyValidation.IsEmptyValidation(notEmpty, nameof(notEmpty)));
        }

        [Test]
        public void ValidateIsEmpty_IEnumerable_Empty_DoesNotThrow()
        {
            IEnumerable<int> empty = new List<int>();
            Assert.DoesNotThrow(() => empty.ValidateIsEmpty(nameof(empty)));
        }

        [Test]
        public void ValidateIsEmpty_IEnumerable_NotEmpty_Throws()
        {
            IEnumerable<int> notEmpty = new List<int> { 1 };
            Assert.Throws<ValidationException>(() => notEmpty.ValidateIsEmpty(nameof(notEmpty)));
        }

        [Test]
        public void ValidateIsEmpty_ICollection_Empty_DoesNotThrow()
        {
            ICollection<int> empty = new List<int>();
            Assert.DoesNotThrow(() => empty.ValidateIsEmpty(nameof(empty)));
        }

        [Test]
        public void ValidateIsEmpty_ICollection_NotEmpty_Throws()
        {
            ICollection<int> notEmpty = new List<int> { 1 };
            Assert.Throws<ValidationException>(() => notEmpty.ValidateIsEmpty(nameof(notEmpty)));
        }
    }
}
