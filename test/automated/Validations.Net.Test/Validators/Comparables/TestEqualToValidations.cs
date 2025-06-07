using System;
using NUnit.Framework;
using Validations.Net.Validators.Comparables;
using Validations.Net;

namespace Validations.Net.Test.Validators.Comparables
{
    [TestFixture]
    public class TestEqualToValidations
    {
        [Test]
        public void CheckIsEqualTo_EqualValues_ReturnsTrue()
        {
            int value = 5;
            int other = 5;
            Assert.That(value.CheckIsEqualTo(other), Is.True);
        }

        [Test]
        public void CheckIsEqualTo_NotEqualValues_ReturnsFalse()
        {
            int value = 5;
            int other = 6;
            Assert.That(value.CheckIsEqualTo(other), Is.False);
        }

        [Test]
        public void CheckIsNotEqualTo_EqualValues_ReturnsFalse()
        {
            int value = 5;
            int other = 5;
            Assert.That(value.CheckIsNotEqualTo(other), Is.False);
        }

        [Test]
        public void CheckIsNotEqualTo_NotEqualValues_ReturnsTrue()
        {
            int value = 5;
            int other = 6;
            Assert.That(value.CheckIsNotEqualTo(other), Is.True);
        }

        [Test]
        public void ValidateIsEqualTo_EqualValues_ReturnsValue()
        {
            int value = 5;
            int other = 5;
            var result = value.ValidateIsEqualTo(other, "test");
            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        public void ValidateIsEqualTo_NotEqualValues_Throws()
        {
            int value = 5;
            int other = 6;
            var ex = Assert.Throws<ValidationException>(() => value.ValidateIsEqualTo(other, "test"));
            Assert.That(ex.Message, Does.Contain("must be equal to 6"));
        }

        [Test]
        public void ValidateIsNotEqualTo_NotEqualValues_ReturnsValue()
        {
            int value = 5;
            int other = 6;
            var result = value.ValidateIsNotEqualTo(other, "test");
            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        public void ValidateIsNotEqualTo_EqualValues_Throws()
        {
            int value = 5;
            int other = 5;
            var ex = Assert.Throws<ValidationException>(() => value.ValidateIsNotEqualTo(other, "test"));
            Assert.That(ex.Message, Does.Contain("must not be equal to 5"));
        }
    }
} 