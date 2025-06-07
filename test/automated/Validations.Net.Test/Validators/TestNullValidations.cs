using System;
using NUnit.Framework;
using Validations.Net.Validators;
using Validations.Net;

namespace Validations.Net.Test.Validators
{
    [TestFixture]
    public class TestNullValidations
    {
        [Test]
        public void CheckIsNull_NullValue_ReturnsTrue()
        {
            object? value = null;
            Assert.That(value.CheckIsNull(), Is.True);
        }

        [Test]
        public void CheckIsNull_NotNullValue_ReturnsFalse()
        {
            object value = new object();
            Assert.That(value.CheckIsNull(), Is.False);
        }

        [Test]
        public void CheckIsNotNull_NullValue_ReturnsFalse()
        {
            object? value = null;
            Assert.That(value.CheckIsNotNull(), Is.False);
        }

        [Test]
        public void CheckIsNotNull_NotNullValue_ReturnsTrue()
        {
            object value = new object();
            Assert.That(value.CheckIsNotNull(), Is.True);
        }

        [Test]
        public void ValidateIsNull_NullValue_ReturnsNull()
        {
            object? value = null;
            var result = value.ValidateIsNull("test");
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ValidateIsNull_NotNullValue_Throws()
        {
            object value = new object();
            var ex = Assert.Throws<ValidationException>(() => value.ValidateIsNull("test"));
            Assert.That(ex.Message, Does.Contain("must be null"));
        }

        [Test]
        public void ValidateIsNotNull_NotNullValue_ReturnsValue()
        {
            object value = new object();
            var result = value.ValidateIsNotNull("test");
            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        public void ValidateIsNotNull_NullValue_Throws()
        {
            object? value = null;
            var ex = Assert.Throws<ValidationException>(() => value.ValidateIsNotNull("test"));
            Assert.That(ex.Message, Does.Contain("can not be null"));
        }
    }
} 