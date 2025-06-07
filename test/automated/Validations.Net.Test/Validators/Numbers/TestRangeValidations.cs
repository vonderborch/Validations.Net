using System;
using NUnit.Framework;
using Validations.Net.Validators.Numbers;
using Validations.Net;

namespace Validations.Net.Test.Validators.Numbers
{
    [TestFixture]
    public class TestRangeValidations
    {
        [Test]
        public void CheckIsInRange_ValueInRange_ReturnsTrue()
        {
            double value = 5.0;
            Assert.That(value.CheckIsInRange(1, 10), Is.True);
        }

        [Test]
        public void CheckIsInRange_ValueOutOfRange_ReturnsFalse()
        {
            double value = 15.0;
            Assert.That(value.CheckIsInRange(1, 10), Is.False);
        }

        [Test]
        public void CheckIsNotInRange_ValueOutOfRange_ReturnsTrue()
        {
            double value = 15.0;
            Assert.That(value.CheckIsNotInRange(1, 10), Is.True);
        }

        [Test]
        public void CheckIsNotInRange_ValueInRange_ReturnsFalse()
        {
            double value = 5.0;
            Assert.That(value.CheckIsNotInRange(1, 10), Is.False);
        }

        [Test]
        public void ValidateIsInRange_ValueInRange_ReturnsValue()
        {
            double value = 5.0;
            var result = value.ValidateIsInRange(1, 10, "test");
            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        public void ValidateIsInRange_ValueOutOfRange_Throws()
        {
            double value = 15.0;
            var ex = Assert.Throws<ValidationException>(() => value.ValidateIsInRange(1, 10, "test"));
            Assert.That(ex.Message, Does.Contain("must be in the range [1, 10]"));
        }

        [Test]
        public void ValidateIsNotInRange_ValueOutOfRange_ReturnsValue()
        {
            double value = 15.0;
            var result = value.ValidateIsNotInRange(1, 10, "test");
            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        public void ValidateIsNotInRange_ValueInRange_Throws()
        {
            double value = 5.0;
            var ex = Assert.Throws<ValidationException>(() => value.ValidateIsNotInRange(1, 10, "test"));
            Assert.That(ex.Message, Does.Contain("must not be in the range [1, 10]"));
        }
    }
} 
