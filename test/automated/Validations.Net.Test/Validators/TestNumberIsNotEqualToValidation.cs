using System;
using NUnit.Framework;
using Validations.Net.Validators;
using Validations.Net;

namespace Validations.Net.Test.Validators
{
    [TestFixture]
    public class TestNumberIsNotEqualToValidation
    {
        [Test]
        public void IsNotEqualTo_NotEqualValues_ReturnsTrue()
        {
            Assert.That(NumberIsNotEqualToValidation.IsNotEqualTo(5, 10), Is.True);
        }

        [Test]
        public void IsNotEqualTo_EqualValues_ReturnsFalse()
        {
            Assert.That(NumberIsNotEqualToValidation.IsNotEqualTo(7, 7), Is.False);
        }

        [Test]
        public void CheckIsNotEqualTo_NotEqualValues_ReturnsTrue()
        {
            int value = 7;
            Assert.That(value.CheckIsNotEqualTo(3), Is.True);
        }

        [Test]
        public void CheckIsNotEqualTo_EqualValues_ReturnsFalse()
        {
            int value = 7;
            Assert.That(value.CheckIsNotEqualTo(7), Is.False);
        }

        [Test]
        public void IsNotEqualToValidation_NotEqualValues_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => NumberIsNotEqualToValidation.IsNotEqualToValidation(8, 9, "value"));
        }

        [Test]
        public void IsNotEqualToValidation_EqualValues_Throws()
        {
            Assert.Throws<ValidationException>(() => NumberIsNotEqualToValidation.IsNotEqualToValidation(8, 8, "value"));
        }

        [Test]
        public void ValidateIsNotEqualTo_NotEqualValues_DoesNotThrow()
        {
            int value = 12;
            Assert.DoesNotThrow(() => value.ValidateIsNotEqualTo(13, "value"));
        }

        [Test]
        public void ValidateIsNotEqualTo_EqualValues_Throws()
        {
            int value = 12;
            Assert.Throws<ValidationException>(() => value.ValidateIsNotEqualTo(12, "value"));
        }
    }
} 