using System;
using NUnit.Framework;
using Validations.Net.Validators;
using Validations.Net;

namespace Validations.Net.Test.Validators
{
    [TestFixture]
    public class TestStringEmptyValidation
    {
        [Test]
        public void IsEmpty_EmptyString_ReturnsTrue()
        {
            Assert.That(StringEmptyValidation.IsEmpty(""), Is.True);
        }

        [Test]
        public void IsEmpty_NonEmptyString_ReturnsFalse()
        {
            Assert.That(StringEmptyValidation.IsEmpty("abc"), Is.False);
        }

        [Test]
        public void IsEmpty_NullString_ReturnsFalse()
        {
            Assert.That(StringEmptyValidation.IsEmpty(null), Is.False);
        }

        [Test]
        public void CheckIsEmpty_EmptyString_ReturnsTrue()
        {
            string value = "";
            Assert.That(value.CheckIsEmpty(), Is.True);
        }

        [Test]
        public void CheckIsEmpty_NonEmptyString_ReturnsFalse()
        {
            string value = "abc";
            Assert.That(value.CheckIsEmpty(), Is.False);
        }

        [Test]
        public void CheckIsEmpty_NullString_ReturnsFalse()
        {
            string? value = null;
            Assert.That(value.CheckIsEmpty(), Is.False);
        }

        [Test]
        public void IsEmptyValidation_EmptyString_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => StringEmptyValidation.IsEmptyValidation("", "value"));
        }

        [Test]
        public void IsEmptyValidation_NonEmptyString_Throws()
        {
            Assert.Throws<ValidationException>(() => StringEmptyValidation.IsEmptyValidation("abc", "value"));
        }

        [Test]
        public void IsEmptyValidation_NullString_Throws()
        {
            Assert.Throws<ValidationException>(() => StringEmptyValidation.IsEmptyValidation(null, "value"));
        }

        [Test]
        public void ValidateIsEmpty_EmptyString_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => "".ValidateIsEmpty("value"));
        }

        [Test]
        public void ValidateIsEmpty_NonEmptyString_Throws()
        {
            Assert.Throws<ValidationException>(() => "abc".ValidateIsEmpty("value"));
        }

        [Test]
        public void ValidateIsEmpty_NullString_Throws()
        {
            string? value = null;
            Assert.Throws<ValidationException>(() => value.ValidateIsEmpty("value"));
        }
    }
} 