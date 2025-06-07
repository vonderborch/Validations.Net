using System;
using NUnit.Framework;
using Validations.Net.Validators.Comparables;
using Validations.Net;

namespace Validations.Net.Test.Validators.Comparables
{
    [TestFixture]
    public class TestGreaterThanValidations
    {
        [Test]
        public void CheckIsGreaterThan_GreaterValue_ReturnsTrue()
        {
            int value = 15;
            int other = 10;
            Assert.That(value.CheckIsGreaterThan(other), Is.True);
        }

        [Test]
        public void CheckIsGreaterThan_EqualOrLessValue_ReturnsFalse()
        {
            int value = 10;
            int other = 10;
            Assert.That(value.CheckIsGreaterThan(other), Is.False);
            value = 5;
            Assert.That(value.CheckIsGreaterThan(other), Is.False);
        }

        [Test]
        public void ValidateIsGreaterThan_GreaterValue_ReturnsValue()
        {
            int value = 15;
            int other = 10;
            var result = value.ValidateIsGreaterThan(other, "test");
            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        public void ValidateIsGreaterThan_EqualOrLessValue_Throws()
        {
            int value = 10;
            int other = 10;
            var ex = Assert.Throws<ValidationException>(() => value.ValidateIsGreaterThan(other, "test"));
            Assert.That(ex.Message, Does.Contain("must be greater than 10"));
            value = 5;
            ex = Assert.Throws<ValidationException>(() => value.ValidateIsGreaterThan(other, "test"));
            Assert.That(ex.Message, Does.Contain("must be greater than 10"));
        }
    }
} 