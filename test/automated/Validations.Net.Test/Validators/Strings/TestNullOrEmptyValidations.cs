using System;
using NUnit.Framework;
using Validations.Net.Validators.Strings;
using Validations.Net;

namespace Validations.Net.Test.Validators.Strings
{
    [TestFixture]
    public class TestNullOrEmptyValidations
    {
        [Test]
        public void CheckIsNullOrEmpty_Null_ReturnsTrue()
        {
            string? value = null;
            Assert.That(value.CheckIsNullOrEmpty(), Is.True);
        }

        [Test]
        public void CheckIsNullOrEmpty_Empty_ReturnsTrue()
        {
            Assert.That(string.Empty.CheckIsNullOrEmpty(), Is.True);
        }

        [Test]
        public void CheckIsNullOrEmpty_NotEmpty_ReturnsFalse()
        {
            Assert.That("abc".CheckIsNullOrEmpty(), Is.False);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_Null_ReturnsFalse()
        {
            string? value = null;
            Assert.That(value.CheckIsNotNullOrEmpty(), Is.False);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_Empty_ReturnsFalse()
        {
            Assert.That(string.Empty.CheckIsNotNullOrEmpty(), Is.False);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_NotEmpty_ReturnsTrue()
        {
            Assert.That("abc".CheckIsNotNullOrEmpty(), Is.True);
        }

        [Test]
        public void ValidateIsNullOrEmpty_Null_ReturnsNull()
        {
            string? value = null;
            var result = value.ValidateIsNullOrEmpty("test");
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ValidateIsNullOrEmpty_Empty_ReturnsEmpty()
        {
            var result = string.Empty.ValidateIsNullOrEmpty("test");
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void ValidateIsNullOrEmpty_NotEmpty_Throws()
        {
            var ex = Assert.Throws<ValidationException>(() => "abc".ValidateIsNullOrEmpty("test"));
            Assert.That(ex.Message, Does.Contain("must be null or empty"));
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_NotEmpty_ReturnsString()
        {
            var result = "abc".ValidateIsNotNullOrEmpty("test");
            Assert.That(result, Is.EqualTo("abc"));
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_Null_Throws()
        {
            string? value = null;
            var ex = Assert.Throws<ValidationException>(() => value.ValidateIsNotNullOrEmpty("test"));
            Assert.That(ex.Message, Does.Contain("must not be null or empty"));
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_Empty_Throws()
        {
            var ex = Assert.Throws<ValidationException>(() => string.Empty.ValidateIsNotNullOrEmpty("test"));
            Assert.That(ex.Message, Does.Contain("must not be null or empty"));
        }
    }
} 