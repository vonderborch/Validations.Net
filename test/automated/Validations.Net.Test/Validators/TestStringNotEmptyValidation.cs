using System;
using NUnit.Framework;
using Validations.Net.Validators;
using Validations.Net;

namespace Validations.Net.Test.Validators
{
    [TestFixture]
    public class TestStringNotEmptyValidation
    {
        [Test]
        public void IsNotEmpty_NonEmptyString_ReturnsTrue()
        {
            Assert.That(StringNotEmptyValidation.IsNotEmpty("abc"), Is.True);
        }

        [Test]
        public void IsNotEmpty_EmptyString_ReturnsFalse()
        {
            Assert.That(StringNotEmptyValidation.IsNotEmpty(""), Is.False);
        }

        [Test]
        public void IsNotEmpty_NullString_ReturnsFalse()
        {
            Assert.That(StringNotEmptyValidation.IsNotEmpty(null), Is.False);
        }

        [Test]
        public void CheckIsNotEmpty_NonEmptyString_ReturnsTrue()
        {
            string value = "abc";
            Assert.That(value.CheckIsNotEmpty(), Is.True);
        }

        [Test]
        public void CheckIsNotEmpty_EmptyString_ReturnsFalse()
        {
            string value = "";
            Assert.That(value.CheckIsNotEmpty(), Is.False);
        }

        [Test]
        public void CheckIsNotEmpty_NullString_ReturnsFalse()
        {
            string? value = null;
            Assert.That(value.CheckIsNotEmpty(), Is.False);
        }

        [Test]
        public void IsNotEmptyValidation_NonEmptyString_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => StringNotEmptyValidation.IsNotEmptyValidation("abc", "value"));
        }

        [Test]
        public void IsNotEmptyValidation_EmptyString_Throws()
        {
            Assert.Throws<ValidationException>(() => StringNotEmptyValidation.IsNotEmptyValidation("", "value"));
        }

        [Test]
        public void IsNotEmptyValidation_NullString_Throws()
        {
            Assert.Throws<ValidationException>(() => StringNotEmptyValidation.IsNotEmptyValidation(null, "value"));
        }

        [Test]
        public void ValidateIsNotEmpty_NonEmptyString_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => "abc".ValidateIsNotEmpty("value"));
        }

        [Test]
        public void ValidateIsNotEmpty_EmptyString_Throws()
        {
            Assert.Throws<ValidationException>(() => "".ValidateIsNotEmpty("value"));
        }

        [Test]
        public void ValidateIsNotEmpty_NullString_Throws()
        {
            string? value = null;
            Assert.Throws<ValidationException>(() => value.ValidateIsNotEmpty("value"));
        }
    }
} 