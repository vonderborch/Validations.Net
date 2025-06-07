using System;
using NUnit.Framework;
using Validations.Net.Validators.Comparables;
using Validations.Net;

namespace Validations.Net.Test.Validators.Comparables
{
    [TestFixture]
    public class TestGreaterThanOrEqualToValidations
    {
        [Test]
        public void CheckIsGreaterThanOrEqualTo_GreaterOrEqualValue_ReturnsTrue()
        {
            int value = 15;
            int other = 10;
            Assert.That(value.CheckIsGreaterThanOrEqualTo(other), Is.True);
            value = 10;
            Assert.That(value.CheckIsGreaterThanOrEqualTo(other), Is.True);
        }

        [Test]
        public void CheckIsGreaterThanOrEqualTo_LessValue_ReturnsFalse()
        {
            int value = 5;
            int other = 10;
            Assert.That(value.CheckIsGreaterThanOrEqualTo(other), Is.False);
        }

        [Test]
        public void ValidateIsGreaterThanOrEqualTo_GreaterOrEqualValue_ReturnsValue()
        {
            int value = 15;
            int other = 10;
            var result = value.ValidateIsGreaterThanOrEqualTo(other, "test");
            Assert.That(result, Is.EqualTo(value));
            value = 10;
            result = value.ValidateIsGreaterThanOrEqualTo(other, "test");
            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        public void ValidateIsGreaterThanOrEqualTo_LessValue_Throws()
        {
            int value = 5;
            int other = 10;
            var ex = Assert.Throws<ValidationException>(() => value.ValidateIsGreaterThanOrEqualTo(other, "test"));
            Assert.That(ex.Message, Does.Contain("must be greater than or equal to 10"));
        }
    }
} 