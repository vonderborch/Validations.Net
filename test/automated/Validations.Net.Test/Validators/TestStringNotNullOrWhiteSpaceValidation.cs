using System;
using NUnit.Framework;
using Validations.Net.Validators;
using Validations.Net;

namespace Validations.Net.Test.Validators
{
    [TestFixture]
    public class TestStringNotNullOrWhiteSpaceValidation
    {
        [Test]
        public void IsNotNullOrWhiteSpace_NonWhitespaceString_ReturnsTrue()
        {
            Assert.That(StringNotNullOrWhiteSpaceValidation.IsNotNullOrWhiteSpace("abc"), Is.True);
        }

        [Test]
        public void IsNotNullOrWhiteSpace_EmptyString_ReturnsFalse()
        {
            Assert.That(StringNotNullOrWhiteSpaceValidation.IsNotNullOrWhiteSpace(""), Is.False);
        }

        [Test]
        public void IsNotNullOrWhiteSpace_NullString_ReturnsFalse()
        {
            Assert.That(StringNotNullOrWhiteSpaceValidation.IsNotNullOrWhiteSpace(null), Is.False);
        }

        [Test]
        public void IsNotNullOrWhiteSpace_WhitespaceString_ReturnsFalse()
        {
            Assert.That(StringNotNullOrWhiteSpaceValidation.IsNotNullOrWhiteSpace("   \t\n"), Is.False);
        }

        [Test]
        public void CheckIsNotNullOrWhiteSpace_NonWhitespaceString_ReturnsTrue()
        {
            string value = "abc";
            Assert.That(value.CheckIsNotNullOrWhiteSpace(), Is.True);
        }

        [Test]
        public void CheckIsNotNullOrWhiteSpace_EmptyString_ReturnsFalse()
        {
            string value = "";
            Assert.That(value.CheckIsNotNullOrWhiteSpace(), Is.False);
        }

        [Test]
        public void CheckIsNotNullOrWhiteSpace_NullString_ReturnsFalse()
        {
            string? value = null;
            Assert.That(value.CheckIsNotNullOrWhiteSpace(), Is.False);
        }

        [Test]
        public void CheckIsNotNullOrWhiteSpace_WhitespaceString_ReturnsFalse()
        {
            string value = "   \t\n";
            Assert.That(value.CheckIsNotNullOrWhiteSpace(), Is.False);
        }

        [Test]
        public void IsNotNullOrWhiteSpaceValidation_NonWhitespaceString_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => StringNotNullOrWhiteSpaceValidation.IsNotNullOrWhiteSpaceValidation("abc", "value"));
        }

        [Test]
        public void IsNotNullOrWhiteSpaceValidation_EmptyString_Throws()
        {
            Assert.Throws<ValidationException>(() => StringNotNullOrWhiteSpaceValidation.IsNotNullOrWhiteSpaceValidation("", "value"));
        }

        [Test]
        public void IsNotNullOrWhiteSpaceValidation_NullString_Throws()
        {
            Assert.Throws<ValidationException>(() => StringNotNullOrWhiteSpaceValidation.IsNotNullOrWhiteSpaceValidation(null, "value"));
        }

        [Test]
        public void IsNotNullOrWhiteSpaceValidation_WhitespaceString_Throws()
        {
            Assert.Throws<ValidationException>(() => StringNotNullOrWhiteSpaceValidation.IsNotNullOrWhiteSpaceValidation("   \t\n", "value"));
        }

        [Test]
        public void ValidateIsNotNullOrWhiteSpace_NonWhitespaceString_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => "abc".ValidateIsNotNullOrWhiteSpace("value"));
        }

        [Test]
        public void ValidateIsNotNullOrWhiteSpace_EmptyString_Throws()
        {
            Assert.Throws<ValidationException>(() => "".ValidateIsNotNullOrWhiteSpace("value"));
        }

        [Test]
        public void ValidateIsNotNullOrWhiteSpace_NullString_Throws()
        {
            string? value = null;
            Assert.Throws<ValidationException>(() => value.ValidateIsNotNullOrWhiteSpace("value"));
        }

        [Test]
        public void ValidateIsNotNullOrWhiteSpace_WhitespaceString_Throws()
        {
            Assert.Throws<ValidationException>(() => "   \t\n".ValidateIsNotNullOrWhiteSpace("value"));
        }
    }
} 