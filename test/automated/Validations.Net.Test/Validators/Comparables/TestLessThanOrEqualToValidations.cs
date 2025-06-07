using System;
using NUnit.Framework;
using Validations.Net.Validators.Comparables;
using Validations.Net;

namespace Validations.Net.Test.Validators.Comparables
{
    [TestFixture]
    public class TestLessThanOrEqualToValidations
    {
        [Test]
        public void CheckIsLessThanOrEqualTo_LessOrEqualValue_ReturnsTrue()
        {
            int value = 5;
            int other = 10;
            Assert.That(value.CheckIsLessThanOrEqualTo(other), Is.True);
            value = 10;
            Assert.That(value.CheckIsLessThanOrEqualTo(other), Is.True);
        }

        [Test]
        public void CheckIsLessThanOrEqualTo_GreaterValue_ReturnsFalse()
        {
            int value = 15;
            int other = 10;
            Assert.That(value.CheckIsLessThanOrEqualTo(other), Is.False);
        }

        [Test]
        public void ValidateIsLessThanOrEqualTo_LessOrEqualValue_ReturnsValue()
        {
            int value = 5;
            int other = 10;
            var result = value.ValidateIsLessThanOrEqualTo(other, "test");
            Assert.That(result, Is.EqualTo(value));
            value = 10;
            result = value.ValidateIsLessThanOrEqualTo(other, "test");
            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        public void ValidateIsLessThanOrEqualTo_GreaterValue_Throws()
        {
            int value = 15;
            int other = 10;
            var ex = Assert.Throws<ValidationException>(() => value.ValidateIsLessThanOrEqualTo(other, "test"));
            Assert.That(ex.Message, Does.Contain("must be less than or equal to 10"));
        }
    }
} 