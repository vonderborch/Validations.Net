using System;
using NUnit.Framework;
using Validations.Net.Validators;
using Validations.Net;

namespace Validations.Net.Test.Validators
{
    [TestFixture]
    public class TestStringWhiteSpaceValidation
    {
        [Test]
        public void IsWhiteSpace_WhitespaceString_ReturnsTrue()
        {
            Assert.That(StringWhiteSpaceValidation.IsWhiteSpace("   \t\n"), Is.True);
        }

        [Test]
        public void IsWhiteSpace_EmptyString_ReturnsTrue()
        {
            Assert.That(StringWhiteSpaceValidation.IsWhiteSpace(""), Is.True);
        }

        [Test]
        public void IsWhiteSpace_NullString_ReturnsFalse()
        {
            Assert.That(StringWhiteSpaceValidation.IsWhiteSpace(null), Is.False);
        }

        [Test]
        public void IsWhiteSpace_NonWhitespaceString_ReturnsFalse()
        {
            Assert.That(StringWhiteSpaceValidation.IsWhiteSpace("abc"), Is.False);
        }

        [Test]
        public void CheckIsWhiteSpace_WhitespaceString_ReturnsTrue()
        {
            string value = "   \t\n";
            Assert.That(value.CheckIsWhiteSpace(), Is.True);
        }

        [Test]
        public void CheckIsWhiteSpace_EmptyString_ReturnsTrue()
        {
            string value = "";
            Assert.That(value.CheckIsWhiteSpace(), Is.True);
        }

        [Test]
        public void CheckIsWhiteSpace_NullString_ReturnsFalse()
        {
            string? value = null;
            Assert.That(value.CheckIsWhiteSpace(), Is.False);
        }

        [Test]
        public void CheckIsWhiteSpace_NonWhitespaceString_ReturnsFalse()
        {
            string value = "abc";
            Assert.That(value.CheckIsWhiteSpace(), Is.False);
        }

        [Test]
        public void IsWhiteSpaceValidation_WhitespaceString_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => StringWhiteSpaceValidation.IsWhiteSpaceValidation("   \t\n", "value"));
        }

        [Test]
        public void IsWhiteSpaceValidation_EmptyString_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => StringWhiteSpaceValidation.IsWhiteSpaceValidation("", "value"));
        }

        [Test]
        public void IsWhiteSpaceValidation_NullString_Throws()
        {
            Assert.Throws<ValidationException>(() => StringWhiteSpaceValidation.IsWhiteSpaceValidation(null, "value"));
        }

        [Test]
        public void IsWhiteSpaceValidation_NonWhitespaceString_Throws()
        {
            Assert.Throws<ValidationException>(() => StringWhiteSpaceValidation.IsWhiteSpaceValidation("abc", "value"));
        }

        [Test]
        public void ValidateIsWhiteSpace_WhitespaceString_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => "   \t\n".ValidateIsWhiteSpace("value"));
        }

        [Test]
        public void ValidateIsWhiteSpace_EmptyString_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => "".ValidateIsWhiteSpace("value"));
        }

        [Test]
        public void ValidateIsWhiteSpace_NullString_Throws()
        {
            string? value = null;
            Assert.Throws<ValidationException>(() => value.ValidateIsWhiteSpace("value"));
        }

        [Test]
        public void ValidateIsWhiteSpace_NonWhitespaceString_Throws()
        {
            Assert.Throws<ValidationException>(() => "abc".ValidateIsWhiteSpace("value"));
        }
    }
} 