using System;
using NUnit.Framework;
using Validations.Net.Validators;
using Validations.Net;

namespace Validations.Net.Test.Validators
{
    [TestFixture]
    public class TestStringNullOrWhiteSpaceValidation
    {
        [Test]
        public void IsIsNullOrWhiteSpace_EmptyString_ReturnsTrue()
        {
            Assert.That(StringIsNullOrWhiteSpaceValidation.IsIsNullOrWhiteSpace(""), Is.True);
        }

        [Test]
        public void IsIsNullOrWhiteSpace_NullString_ReturnsTrue()
        {
            Assert.That(StringIsNullOrWhiteSpaceValidation.IsIsNullOrWhiteSpace(null), Is.True);
        }

        [Test]
        public void IsIsNullOrWhiteSpace_WhitespaceString_ReturnsTrue()
        {
            Assert.That(StringIsNullOrWhiteSpaceValidation.IsIsNullOrWhiteSpace("   \t\n"), Is.True);
        }

        [Test]
        public void IsIsNullOrWhiteSpace_NonWhitespaceString_ReturnsFalse()
        {
            Assert.That(StringIsNullOrWhiteSpaceValidation.IsIsNullOrWhiteSpace("abc"), Is.False);
        }

        [Test]
        public void CheckIsIsNullOrWhiteSpace_EmptyString_ReturnsTrue()
        {
            string value = "";
            Assert.That(value.CheckIsIsNullOrWhiteSpace(), Is.True);
        }

        [Test]
        public void CheckIsIsNullOrWhiteSpace_NullString_ReturnsTrue()
        {
            string? value = null;
            Assert.That(value.CheckIsIsNullOrWhiteSpace(), Is.True);
        }

        [Test]
        public void CheckIsIsNullOrWhiteSpace_WhitespaceString_ReturnsTrue()
        {
            string value = "   \t\n";
            Assert.That(value.CheckIsIsNullOrWhiteSpace(), Is.True);
        }

        [Test]
        public void CheckIsIsNullOrWhiteSpace_NonWhitespaceString_ReturnsFalse()
        {
            string value = "abc";
            Assert.That(value.CheckIsIsNullOrWhiteSpace(), Is.False);
        }

        [Test]
        public void IsIsNullOrWhiteSpaceValidation_EmptyString_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => StringIsNullOrWhiteSpaceValidation.IsIsNullOrWhiteSpaceValidation("", "value"));
        }

        [Test]
        public void IsIsNullOrWhiteSpaceValidation_NullString_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => StringIsNullOrWhiteSpaceValidation.IsIsNullOrWhiteSpaceValidation(null, "value"));
        }

        [Test]
        public void IsIsNullOrWhiteSpaceValidation_WhitespaceString_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => StringIsNullOrWhiteSpaceValidation.IsIsNullOrWhiteSpaceValidation("   \t\n", "value"));
        }

        [Test]
        public void IsIsNullOrWhiteSpaceValidation_NonWhitespaceString_Throws()
        {
            Assert.Throws<ValidationException>(() => StringIsNullOrWhiteSpaceValidation.IsIsNullOrWhiteSpaceValidation("abc", "value"));
        }

        [Test]
        public void ValidateIsIsNullOrWhiteSpace_EmptyString_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => "".ValidateIsIsNullOrWhiteSpace("value"));
        }

        [Test]
        public void ValidateIsIsNullOrWhiteSpace_NullString_DoesNotThrow()
        {
            string? value = null;
            Assert.DoesNotThrow(() => value.ValidateIsIsNullOrWhiteSpace("value"));
        }

        [Test]
        public void ValidateIsIsNullOrWhiteSpace_WhitespaceString_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => "   \t\n".ValidateIsIsNullOrWhiteSpace("value"));
        }

        [Test]
        public void ValidateIsIsNullOrWhiteSpace_NonWhitespaceString_Throws()
        {
            Assert.Throws<ValidationException>(() => "abc".ValidateIsIsNullOrWhiteSpace("value"));
        }
    }
} 