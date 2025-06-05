using System;
using NUnit.Framework;
using Validations.Net.Validators;
using Validations.Net;

namespace Validations.Net.Test.Validators
{
    [TestFixture]
    public class TestStringNotWhiteSpaceValidation
    {
        [Test]
        public void IsNotWhiteSpace_NonWhitespaceString_ReturnsTrue()
        {
            Assert.That(StringNotWhiteSpaceValidation.IsNotWhiteSpace("abc"), Is.True);
        }

        [Test]
        public void IsNotWhiteSpace_EmptyString_ReturnsFalse()
        {
            Assert.That(StringNotWhiteSpaceValidation.IsNotWhiteSpace(""), Is.False);
        }

        [Test]
        public void IsNotWhiteSpace_NullString_ReturnsFalse()
        {
            Assert.That(StringNotWhiteSpaceValidation.IsNotWhiteSpace(null), Is.False);
        }

        [Test]
        public void IsNotWhiteSpace_WhitespaceString_ReturnsFalse()
        {
            Assert.That(StringNotWhiteSpaceValidation.IsNotWhiteSpace("   \t\n"), Is.False);
        }

        [Test]
        public void CheckIsNotWhiteSpace_NonWhitespaceString_ReturnsTrue()
        {
            string value = "abc";
            Assert.That(value.CheckIsNotWhiteSpace(), Is.True);
        }

        [Test]
        public void CheckIsNotWhiteSpace_EmptyString_ReturnsFalse()
        {
            string value = "";
            Assert.That(value.CheckIsNotWhiteSpace(), Is.False);
        }

        [Test]
        public void CheckIsNotWhiteSpace_NullString_ReturnsFalse()
        {
            string? value = null;
            Assert.That(value.CheckIsNotWhiteSpace(), Is.False);
        }

        [Test]
        public void CheckIsNotWhiteSpace_WhitespaceString_ReturnsFalse()
        {
            string value = "   \t\n";
            Assert.That(value.CheckIsNotWhiteSpace(), Is.False);
        }

        [Test]
        public void IsNotWhiteSpaceValidation_NonWhitespaceString_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => StringNotWhiteSpaceValidation.IsNotWhiteSpaceValidation("abc", "value"));
        }

        [Test]
        public void IsNotWhiteSpaceValidation_EmptyString_Throws()
        {
            Assert.Throws<ValidationException>(() => StringNotWhiteSpaceValidation.IsNotWhiteSpaceValidation("", "value"));
        }

        [Test]
        public void IsNotWhiteSpaceValidation_NullString_Throws()
        {
            Assert.Throws<ValidationException>(() => StringNotWhiteSpaceValidation.IsNotWhiteSpaceValidation(null, "value"));
        }

        [Test]
        public void IsNotWhiteSpaceValidation_WhitespaceString_Throws()
        {
            Assert.Throws<ValidationException>(() => StringNotWhiteSpaceValidation.IsNotWhiteSpaceValidation("   \t\n", "value"));
        }

        [Test]
        public void ValidateIsNotWhiteSpace_NonWhitespaceString_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => "abc".ValidateIsNotWhiteSpace("value"));
        }

        [Test]
        public void ValidateIsNotWhiteSpace_EmptyString_Throws()
        {
            Assert.Throws<ValidationException>(() => "".ValidateIsNotWhiteSpace("value"));
        }

        [Test]
        public void ValidateIsNotWhiteSpace_NullString_Throws()
        {
            string? value = null;
            Assert.Throws<ValidationException>(() => value.ValidateIsNotWhiteSpace("value"));
        }

        [Test]
        public void ValidateIsNotWhiteSpace_WhitespaceString_Throws()
        {
            Assert.Throws<ValidationException>(() => "   \t\n".ValidateIsNotWhiteSpace("value"));
        }
    }
} 