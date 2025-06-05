using System;
using NUnit.Framework;
using Validations.Net.Validators;
using Validations.Net;

namespace Validations.Net.Test.Validators
{
    [TestFixture]
    public class TestStringNullOrEmptyValidation
    {
        [Test]
        public void IsNullOrEmpty_EmptyString_ReturnsTrue()
        {
            Assert.That(StringNullOrEmptyValidation.IsNullOrEmpty(""), Is.True);
        }

        [Test]
        public void IsNullOrEmpty_NullString_ReturnsTrue()
        {
            Assert.That(StringNullOrEmptyValidation.IsNullOrEmpty(null), Is.True);
        }

        [Test]
        public void IsNullOrEmpty_NonEmptyString_ReturnsFalse()
        {
            Assert.That(StringNullOrEmptyValidation.IsNullOrEmpty("abc"), Is.False);
        }

        [Test]
        public void CheckIsNullOrEmpty_EmptyString_ReturnsTrue()
        {
            string value = "";
            Assert.That(value.CheckIsNullOrEmpty(), Is.True);
        }

        [Test]
        public void CheckIsNullOrEmpty_NullString_ReturnsTrue()
        {
            string? value = null;
            Assert.That(value.CheckIsNullOrEmpty(), Is.True);
        }

        [Test]
        public void CheckIsNullOrEmpty_NonEmptyString_ReturnsFalse()
        {
            string value = "abc";
            Assert.That(value.CheckIsNullOrEmpty(), Is.False);
        }

        [Test]
        public void IsNullOrEmptyValidation_EmptyString_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => StringNullOrEmptyValidation.IsNullOrEmptyValidation("", "value"));
        }

        [Test]
        public void IsNullOrEmptyValidation_NullString_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => StringNullOrEmptyValidation.IsNullOrEmptyValidation(null, "value"));
        }

        [Test]
        public void IsNullOrEmptyValidation_NonEmptyString_Throws()
        {
            Assert.Throws<ValidationException>(() => StringNullOrEmptyValidation.IsNullOrEmptyValidation("abc", "value"));
        }

        [Test]
        public void ValidateIsNullOrEmpty_EmptyString_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => "".ValidateIsNullOrEmpty("value"));
        }

        [Test]
        public void ValidateIsNullOrEmpty_NullString_DoesNotThrow()
        {
            string? value = null;
            Assert.DoesNotThrow(() => value.ValidateIsNullOrEmpty("value"));
        }

        [Test]
        public void ValidateIsNullOrEmpty_NonEmptyString_Throws()
        {
            Assert.Throws<ValidationException>(() => "abc".ValidateIsNullOrEmpty("value"));
        }
    }
} 