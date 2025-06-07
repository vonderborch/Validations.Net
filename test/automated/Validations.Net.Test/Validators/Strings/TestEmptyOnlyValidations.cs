using System;
using NUnit.Framework;
using Validations.Net.Validators.Strings;
using Validations.Net;

namespace Validations.Net.Test.Validators.Strings
{
    [TestFixture]
    public class TestEmptyOnlyValidations
    {
        [Test]
        public void CheckIsEmptyOnly_Empty_ReturnsTrue()
        {
            Assert.That(string.Empty.CheckIsEmptyOnly(), Is.True);
        }

        [Test]
        public void CheckIsEmptyOnly_Null_ReturnsFalse()
        {
            string? value = null;
            Assert.That(value.CheckIsEmptyOnly(), Is.False);
        }

        [Test]
        public void CheckIsEmptyOnly_NotEmpty_ReturnsFalse()
        {
            Assert.That("abc".CheckIsEmptyOnly(), Is.False);
        }

        [Test]
        public void CheckIsNotEmptyOnly_Empty_ReturnsFalse()
        {
            Assert.That(string.Empty.CheckIsNotEmptyOnly(), Is.False);
        }

        [Test]
        public void CheckIsNotEmptyOnly_Null_ReturnsTrue()
        {
            string? value = null;
            Assert.That(value.CheckIsNotEmptyOnly(), Is.True);
        }

        [Test]
        public void CheckIsNotEmptyOnly_NotEmpty_ReturnsTrue()
        {
            Assert.That("abc".CheckIsNotEmptyOnly(), Is.True);
        }

        [Test]
        public void ValidateIsEmptyOnly_Empty_ReturnsEmpty()
        {
            var result = string.Empty.ValidateIsEmptyOnly("test");
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void ValidateIsEmptyOnly_NotEmpty_Throws()
        {
            var ex = Assert.Throws<ValidationException>(() => "abc".ValidateIsEmptyOnly("test"));
            Assert.That(ex.Message, Does.Contain("must be empty"));
        }

        [Test]
        public void ValidateIsNotEmptyOnly_NotEmpty_ReturnsString()
        {
            var result = "abc".ValidateIsNotEmptyOnly("test");
            Assert.That(result, Is.EqualTo("abc"));
        }

        [Test]
        public void ValidateIsNotEmptyOnly_Empty_Throws()
        {
            var ex = Assert.Throws<ValidationException>(() => string.Empty.ValidateIsNotEmptyOnly("test"));
            Assert.That(ex.Message, Does.Contain("must not be empty"));
        }
    }
} 
