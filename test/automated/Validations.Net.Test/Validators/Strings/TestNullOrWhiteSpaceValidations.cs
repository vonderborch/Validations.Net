using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Validations.Net.Validators.Strings;
using Validations.Net;

namespace Validations.Net.Test.Validators.Strings
{
    [TestFixture]
    public class TestNullOrWhiteSpaceValidations
    {
        [Test]
        public void CheckIsNullOrWhiteSpace_Null_ReturnsTrue()
        {
            string? value = null;
            Assert.That(value.CheckIsNullOrWhiteSpace(), Is.True);
        }

        [Test]
        public void CheckIsNullOrWhiteSpace_WhiteSpace_ReturnsTrue()
        {
            Assert.That("   ".CheckIsNullOrWhiteSpace(), Is.True);
        }

        [Test]
        public void CheckIsNullOrWhiteSpace_EmptyString_ReturnsTrue()
        {
            Assert.That(string.Empty.CheckIsNullOrWhiteSpace(), Is.True);
        }

        [Test]
        public void CheckIsNullOrWhiteSpace_NotWhiteSpace_ReturnsFalse()
        {
            Assert.That("abc".CheckIsNullOrWhiteSpace(), Is.False);
        }

        [Test]
        public void CheckIsNotNullOrWhiteSpace_Null_ReturnsFalse()
        {
            string? value = null;
            Assert.That(value.CheckIsNotNullOrWhiteSpace(), Is.False);
        }

        [Test]
        public void CheckIsNotNullOrWhiteSpace_WhiteSpace_ReturnsFalse()
        {
            Assert.That("   ".CheckIsNotNullOrWhiteSpace(), Is.False);
        }

        [Test]
        public void CheckIsNotNullOrWhiteSpace_EmptyString_ReturnsFalse()
        {
            Assert.That(string.Empty.CheckIsNotNullOrWhiteSpace(), Is.False);
        }

        [Test]
        public void CheckIsNotNullOrWhiteSpace_NotWhiteSpace_ReturnsTrue()
        {
            Assert.That("abc".CheckIsNotNullOrWhiteSpace(), Is.True);
        }

        [Test]
        public void ValidateIsNullOrWhiteSpace_Null_ReturnsNull()
        {
            string? value = null;
            var result = value.ValidateIsNullOrWhiteSpace("test");
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ValidateIsNullOrWhiteSpace_WhiteSpace_ReturnsWhiteSpace()
        {
            var result = "   ".ValidateIsNullOrWhiteSpace("test");
            Assert.That(result, Is.EqualTo("   "));
        }

        [Test]
        public void ValidateIsNullOrWhiteSpace_NotWhiteSpace_Throws()
        {
            var ex = Assert.Throws<ValidationException>(() => "abc".ValidateIsNullOrWhiteSpace("test"));
            Assert.That(ex.Message, Does.Contain("must be null, empty, or white space"));
        }

        [Test]
        public void ValidateIsNotNullOrWhiteSpace_NotWhiteSpace_ReturnsString()
        {
            var result = "abc".ValidateIsNotNullOrWhiteSpace("test");
            Assert.That(result, Is.EqualTo("abc"));
        }

        [Test]
        public void ValidateIsNotNullOrWhiteSpace_Null_Throws()
        {
            string? value = null;
            var ex = Assert.Throws<ValidationException>(() => value.ValidateIsNotNullOrWhiteSpace("test"));
            Assert.That(ex.Message, Does.Contain("must not be null, empty, or white space"));
        }

        [Test]
        public void ValidateIsNotNullOrWhiteSpace_WhiteSpace_Throws()
        {
            var ex = Assert.Throws<ValidationException>(() => "   ".ValidateIsNotNullOrWhiteSpace("test"));
            Assert.That(ex.Message, Does.Contain("must not be null, empty, or white space"));
        }
    }
} 
