using System;
using NUnit.Framework;
using Validations.Net.Validators;
using Validations.Net;

namespace Validations.Net.Test.Validators
{
    [TestFixture]
    public class TestNumberEqualToValidation
    {
        [Test]
        public void IsEqualTo_EqualValues_ReturnsTrue()
        {
            Assert.That(NumberEqualToValidation.IsEqualTo(5, 5), Is.True);
        }

        [Test]
        public void IsEqualTo_NotEqualValues_ReturnsFalse()
        {
            Assert.That(NumberEqualToValidation.IsEqualTo(5, 10), Is.False);
        }

        [Test]
        public void CheckIsEqualTo_EqualValues_ReturnsTrue()
        {
            int value = 7;
            Assert.That(value.CheckIsEqualTo(7), Is.True);
        }

        [Test]
        public void CheckIsEqualTo_NotEqualValues_ReturnsFalse()
        {
            int value = 7;
            Assert.That(value.CheckIsEqualTo(3), Is.False);
        }

        [Test]
        public void IsEqualToValidation_EqualValues_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => NumberEqualToValidation.IsEqualToValidation(8, 8, nameof(IsEqualToValidation_EqualValues_DoesNotThrow)));
        }

        [Test]
        public void IsEqualToValidation_NotEqualValues_Throws()
        {
            Assert.Throws<ValidationException>(() => NumberEqualToValidation.IsEqualToValidation(8, 9, nameof(IsEqualToValidation_NotEqualValues_Throws)));
        }

        [Test]
        public void ValidateIsEqualTo_EqualValues_DoesNotThrow()
        {
            int value = 12;
            Assert.DoesNotThrow(() => value.ValidateIsEqualTo(12, nameof(ValidateIsEqualTo_EqualValues_DoesNotThrow)));
        }

        [Test]
        public void ValidateIsEqualTo_NotEqualValues_Throws()
        {
            int value = 12;
            Assert.Throws<ValidationException>(() => value.ValidateIsEqualTo(13, nameof(ValidateIsEqualTo_NotEqualValues_Throws)));
        }
    }
} 