using System;
using NUnit.Framework;
using Validations.Net.Validators.Comparables;
using Validations.Net;

namespace Validations.Net.Test.Validators.Comparables
{
    [TestFixture]
    public class TestLessThanValidations
    {
        [Test]
        public void CheckIsLessThan_LessValue_ReturnsTrue()
        {
            int value = 5;
            int other = 10;
            Assert.That(value.CheckIsLessThan(other), Is.True);
        }

        [Test]
        public void CheckIsLessThan_EqualOrGreaterValue_ReturnsFalse()
        {
            int value = 10;
            int other = 10;
            Assert.That(value.CheckIsLessThan(other), Is.False);
            value = 15;
            Assert.That(value.CheckIsLessThan(other), Is.False);
        }

        [Test]
        public void ValidateIsLessThan_LessValue_ReturnsValue()
        {
            int value = 5;
            int other = 10;
            var result = value.ValidateIsLessThan(other, "test");
            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        public void ValidateIsLessThan_EqualOrGreaterValue_Throws()
        {
            int value = 10;
            int other = 10;
            var ex = Assert.Throws<ValidationException>(() => value.ValidateIsLessThan(other, "test"));
            Assert.That(ex.Message, Does.Contain("must be less than 10"));
            value = 15;
            ex = Assert.Throws<ValidationException>(() => value.ValidateIsLessThan(other, "test"));
            Assert.That(ex.Message, Does.Contain("must be less than 10"));
        }
    }
} 