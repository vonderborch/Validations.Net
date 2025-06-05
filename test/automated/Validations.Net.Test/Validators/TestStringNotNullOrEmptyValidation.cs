using System;
using NUnit.Framework;
using Validations.Net.Validators;
using Validations.Net;

namespace Validations.Net.Test.Validators
{
    [TestFixture]
    public class TestStringNotNullOrEmptyValidation
    {
        [Test]
        public void IsNotNullOrEmpty_NonEmptyString_ReturnsTrue()
        {
            Assert.That(StringNotNullOrEmptyValidation.IsNotNullOrEmpty("abc"), Is.True);
        }

        [Test]
        public void IsNotNullOrEmpty_EmptyString_ReturnsFalse()
        {
            Assert.That(StringNotNullOrEmptyValidation.IsNotNullOrEmpty(""), Is.False);
        }

        [Test]
        public void IsNotNullOrEmpty_NullString_ReturnsFalse()
        {
            Assert.That(StringNotNullOrEmptyValidation.IsNotNullOrEmpty(null), Is.False);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_NonEmptyString_ReturnsTrue()
        {
            string value = "abc";
            Assert.That(value.CheckIsNotNullOrEmpty(), Is.True);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_EmptyString_ReturnsFalse()
        {
            string value = "";
            Assert.That(value.CheckIsNotNullOrEmpty(), Is.False);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_NullString_ReturnsFalse()
        {
            string? value = null;
            Assert.That(value.CheckIsNotNullOrEmpty(), Is.False);
        }

        [Test]
        public void IsNotNullOrEmptyValidation_NonEmptyString_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => StringNotNullOrEmptyValidation.IsNotNullOrEmptyValidation("abc", "value"));
        }

        [Test]
        public void IsNotNullOrEmptyValidation_EmptyString_Throws()
        {
            Assert.Throws<ValidationException>(() => StringNotNullOrEmptyValidation.IsNotNullOrEmptyValidation("", "value"));
        }

        [Test]
        public void IsNotNullOrEmptyValidation_NullString_Throws()
        {
            Assert.Throws<ValidationException>(() => StringNotNullOrEmptyValidation.IsNotNullOrEmptyValidation(null, "value"));
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_NonEmptyString_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => "abc".ValidateIsNotNullOrEmpty("value"));
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_EmptyString_Throws()
        {
            Assert.Throws<ValidationException>(() => "".ValidateIsNotNullOrEmpty("value"));
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_NullString_Throws()
        {
            string? value = null;
            Assert.Throws<ValidationException>(() => value.ValidateIsNotNullOrEmpty("value"));
        }
    }
} 