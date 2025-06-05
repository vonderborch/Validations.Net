using System;
using NUnit.Framework;
using Validations.Net.Validators;
using Validations.Net;

namespace Validations.Net.Test.Validators
{
    [TestFixture]
    public class TestNumberIsOutOfRangeValidation
    {
        [Test]
        public void IsOutOfRange_ValueBelowMinInclusive_ReturnsTrue()
        {
            Assert.That(NumberIsOutOfRangeValidation.IsOutOfRange(0, 1, 10), Is.True);
        }

        [Test]
        public void IsOutOfRange_ValueAboveMaxInclusive_ReturnsTrue()
        {
            Assert.That(NumberIsOutOfRangeValidation.IsOutOfRange(11, 1, 10), Is.True);
        }

        [Test]
        public void IsOutOfRange_ValueWithinInclusiveBounds_ReturnsFalse()
        {
            Assert.That(NumberIsOutOfRangeValidation.IsOutOfRange(5, 1, 10), Is.False);
        }

        [Test]
        public void IsOutOfRange_ValueEqualsMinInclusive_ReturnsFalse()
        {
            Assert.That(NumberIsOutOfRangeValidation.IsOutOfRange(1, 1, 10), Is.False);
        }

        [Test]
        public void IsOutOfRange_ValueEqualsMaxInclusive_ReturnsFalse()
        {
            Assert.That(NumberIsOutOfRangeValidation.IsOutOfRange(10, 1, 10), Is.False);
        }

        [Test]
        public void IsOutOfRange_ValueEqualsMinExclusive_ReturnsTrue()
        {
            Assert.That(NumberIsOutOfRangeValidation.IsOutOfRange(1, 1, 10, minInclusive: false), Is.True);
        }

        [Test]
        public void IsOutOfRange_ValueEqualsMaxExclusive_ReturnsTrue()
        {
            Assert.That(NumberIsOutOfRangeValidation.IsOutOfRange(10, 1, 10, maxInclusive: false), Is.True);
        }

        [Test]
        public void CheckIsOutOfRange_ValueOutOfBounds_ReturnsTrue()
        {
            int value = 12;
            Assert.That(value.CheckIsOutOfRange(5, 10), Is.True);
        }

        [Test]
        public void CheckIsOutOfRange_ValueWithinBounds_ReturnsFalse()
        {
            int value = 7;
            Assert.That(value.CheckIsOutOfRange(5, 10), Is.False);
        }

        [Test]
        public void IsOutOfRangeValidation_ValueOutOfBounds_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => NumberIsOutOfRangeValidation.IsOutOfRangeValidation(0, 1, 10, "value"));
        }

        [Test]
        public void IsOutOfRangeValidation_ValueWithinBounds_Throws()
        {
            Assert.Throws<ValidationException>(() => NumberIsOutOfRangeValidation.IsOutOfRangeValidation(5, 1, 10, "value"));
        }

        [Test]
        public void ValidateIsOutOfRange_ValueOutOfBounds_DoesNotThrow()
        {
            int value = 15;
            Assert.DoesNotThrow(() => value.ValidateIsOutOfRange(5, 10, "value"));
        }

        [Test]
        public void ValidateIsOutOfRange_ValueWithinBounds_Throws()
        {
            int value = 8;
            Assert.Throws<ValidationException>(() => value.ValidateIsOutOfRange(5, 10, "value"));
        }

        [Test]
        public void IsOutOfRangeValidation_ValueEqualsMinExclusive_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => NumberIsOutOfRangeValidation.IsOutOfRangeValidation(1, 1, 10, "value", minInclusive: false));
        }

        [Test]
        public void IsOutOfRangeValidation_ValueEqualsMaxExclusive_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => NumberIsOutOfRangeValidation.IsOutOfRangeValidation(10, 1, 10, "value", maxInclusive: false));
        }
    }
} 