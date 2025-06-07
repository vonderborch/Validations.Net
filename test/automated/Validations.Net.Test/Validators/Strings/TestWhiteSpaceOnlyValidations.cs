using System;
using NUnit.Framework;
using Validations.Net.Validators.Strings;
using Validations.Net;

namespace Validations.Net.Test.Validators.Strings
{
    [TestFixture]
    public class TestWhiteSpaceOnlyValidations
    {
        [Test]
        public void CheckIsWhiteSpaceOnly_WhiteSpace_ReturnsTrue()
        {
            Assert.That("   ".CheckIsWhiteSpaceOnly(), Is.True);
        }

        [Test]
        public void CheckIsWhiteSpaceOnly_Null_ReturnsFalse()
        {
            string? value = null;
            Assert.That(value.CheckIsWhiteSpaceOnly(), Is.False);
        }

        [Test]
        public void CheckIsWhiteSpaceOnly_NotWhiteSpace_ReturnsFalse()
        {
            Assert.That("abc".CheckIsWhiteSpaceOnly(), Is.False);
        }

        [Test]
        public void CheckIsNotWhiteSpaceOnly_WhiteSpace_ReturnsFalse()
        {
            Assert.That("   ".CheckIsNotWhiteSpaceOnly(), Is.False);
        }

        [Test]
        public void CheckIsNotWhiteSpaceOnly_Null_ReturnsTrue()
        {
            string? value = null;
            Assert.That(value.CheckIsNotWhiteSpaceOnly(), Is.True);
        }

        [Test]
        public void CheckIsNotWhiteSpaceOnly_NotWhiteSpace_ReturnsTrue()
        {
            Assert.That("abc".CheckIsNotWhiteSpaceOnly(), Is.True);
        }

        [Test]
        public void ValidateIsWhiteSpaceOnly_WhiteSpace_ReturnsWhiteSpace()
        {
            var result = "   ".ValidateIsWhiteSpaceOnly("test");
            Assert.That(result, Is.EqualTo("   "));
        }

        [Test]
        public void ValidateIsWhiteSpaceOnly_NotWhiteSpace_Throws()
        {
            var ex = Assert.Throws<ValidationException>(() => "abc".ValidateIsWhiteSpaceOnly("test"));
            Assert.That(ex.Message, Does.Contain("must be white space"));
        }

        [Test]
        public void ValidateIsNotWhiteSpaceOnly_NotWhiteSpace_ReturnsString()
        {
            var result = "abc".ValidateIsNotWhiteSpaceOnly("test");
            Assert.That(result, Is.EqualTo("abc"));
        }

        [Test]
        public void ValidateIsNotWhiteSpaceOnly_WhiteSpace_Throws()
        {
            var ex = Assert.Throws<ValidationException>(() => "   ".ValidateIsNotWhiteSpaceOnly("test"));
            Assert.That(ex.Message, Does.Contain("must not be white space"));
        }
    }
} 
