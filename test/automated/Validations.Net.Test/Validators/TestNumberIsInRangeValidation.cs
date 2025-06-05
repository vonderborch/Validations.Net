using System;
using NUnit.Framework;
using Validations.Net.Validators;
using Validations.Net;

namespace Validations.Net.Test.Validators
{
    [TestFixture]
    public class TestNumberIsInRangeValidation
    {
        [Test]
        public void IsInRange_ValueWithinInclusiveBounds_ReturnsTrue()
        {
            Assert.That(NumberIsInRangeValidation.IsInRange(5, 1, 10), Is.True);
        }

        [Test]
        public void IsInRange_ValueEqualsMinInclusive_ReturnsTrue()
        {
            Assert.That(NumberIsInRangeValidation.IsInRange(1, 1, 10), Is.True);
        }

        [Test]
        public void IsInRange_ValueEqualsMaxInclusive_ReturnsTrue()
        {
            Assert.That(NumberIsInRangeValidation.IsInRange(10, 1, 10), Is.True);
        }

        [Test]
        public void IsInRange_ValueBelowMin_ReturnsFalse()
        {
            Assert.That(NumberIsInRangeValidation.IsInRange(0, 1, 10), Is.False);
        }

        [Test]
        public void IsInRange_ValueAboveMax_ReturnsFalse()
        {
            Assert.That(NumberIsInRangeValidation.IsInRange(11, 1, 10), Is.False);
        }

        [Test]
        public void IsInRange_ValueEqualsMinExclusive_ReturnsFalse()
        {
            Assert.That(NumberIsInRangeValidation.IsInRange(1, 1, 10, minInclusive: false), Is.False);
        }

        [Test]
        public void IsInRange_ValueEqualsMaxExclusive_ReturnsFalse()
        {
            Assert.That(NumberIsInRangeValidation.IsInRange(10, 1, 10, maxInclusive: false), Is.False);
        }

        [Test]
        public void CheckIsInRange_ValueWithinBounds_ReturnsTrue()
        {
            int value = 7;
            Assert.That(value.CheckIsInRange(5, 10), Is.True);
        }

        [Test]
        public void CheckIsInRange_ValueOutOfBounds_ReturnsFalse()
        {
            int value = 12;
            Assert.That(value.CheckIsInRange(5, 10), Is.False);
        }

        [Test]
        public void IsInRangeValidation_ValueWithinBounds_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => NumberIsInRangeValidation.IsInRangeValidation(6, 1, 10, "value"));
        }

        [Test]
        public void IsInRangeValidation_ValueOutOfBounds_Throws()
        {
            Assert.Throws<ValidationException>(() => NumberIsInRangeValidation.IsInRangeValidation(0, 1, 10, "value"));
        }

        [Test]
        public void ValidateIsInRange_ValueWithinBounds_DoesNotThrow()
        {
            int value = 8;
            Assert.DoesNotThrow(() => value.ValidateIsInRange(5, 10, "value"));
        }

        [Test]
        public void ValidateIsInRange_ValueOutOfBounds_Throws()
        {
            int value = 15;
            Assert.Throws<ValidationException>(() => value.ValidateIsInRange(5, 10, "value"));
        }

        [Test]
        public void IsInRangeValidation_ValueWithinExclusiveBounds_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => NumberIsInRangeValidation.IsInRangeValidation(5, 1, 10, "value", minInclusive: false, maxInclusive: false));
        }

        [Test]
        public void IsInRangeValidation_ValueEqualsMinExclusive_Throws()
        {
            Assert.Throws<ValidationException>(() => NumberIsInRangeValidation.IsInRangeValidation(1, 1, 10, "value", minInclusive: false));
        }

        [Test]
        public void IsInRangeValidation_ValueEqualsMaxExclusive_Throws()
        {
            Assert.Throws<ValidationException>(() => NumberIsInRangeValidation.IsInRangeValidation(10, 1, 10, "value", maxInclusive: false));
        }
    }
} 