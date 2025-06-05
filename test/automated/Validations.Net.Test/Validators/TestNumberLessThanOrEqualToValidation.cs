using System;
using NUnit.Framework;
using Validations.Net.Validators;
using Validations.Net;

namespace Validations.Net.Test.Validators
{
    [TestFixture]
    public class TestNumberLessThanOrEqualToValidation
    {
        [Test]
        public void IsLessThanOrEqualTo_LesserValue_ReturnsTrue()
        {
            Assert.That(NumberLessThanOrEqualToValidation.IsLessThanOrEqualTo(5, 10), Is.True);
        }

        [Test]
        public void IsLessThanOrEqualTo_EqualValue_ReturnsTrue()
        {
            Assert.That(NumberLessThanOrEqualToValidation.IsLessThanOrEqualTo(7, 7), Is.True);
        }

        [Test]
        public void IsLessThanOrEqualTo_GreaterValue_ReturnsFalse()
        {
            Assert.That(NumberLessThanOrEqualToValidation.IsLessThanOrEqualTo(12, 8), Is.False);
        }

        [Test]
        public void CheckIsLessThanOrEqualTo_LesserValue_ReturnsTrue()
        {
            int value = 3;
            Assert.That(value.CheckIsLessThanOrEqualTo(5), Is.True);
        }

        [Test]
        public void CheckIsLessThanOrEqualTo_EqualValue_ReturnsTrue()
        {
            int value = 10;
            Assert.That(value.CheckIsLessThanOrEqualTo(10), Is.True);
        }

        [Test]
        public void CheckIsLessThanOrEqualTo_GreaterValue_ReturnsFalse()
        {
            int value = 15;
            Assert.That(value.CheckIsLessThanOrEqualTo(7), Is.False);
        }

        [Test]
        public void IsLessThanOrEqualToValidation_LesserValue_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => NumberLessThanOrEqualToValidation.IsLessThanOrEqualToValidation(4, 9, "value"));
        }

        [Test]
        public void IsLessThanOrEqualToValidation_EqualValue_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => NumberLessThanOrEqualToValidation.IsLessThanOrEqualToValidation(6, 6, "value"));
        }

        [Test]
        public void IsLessThanOrEqualToValidation_GreaterValue_Throws()
        {
            Assert.Throws<ValidationException>(() => NumberLessThanOrEqualToValidation.IsLessThanOrEqualToValidation(10, 5, "value"));
        }

        [Test]
        public void ValidateIsLessThanOrEqualTo_LesserValue_DoesNotThrow()
        {
            int value = 2;
            Assert.DoesNotThrow(() => value.ValidateIsLessThanOrEqualTo(8, "value"));
        }

        [Test]
        public void ValidateIsLessThanOrEqualTo_EqualValue_DoesNotThrow()
        {
            int value = 9;
            Assert.DoesNotThrow(() => value.ValidateIsLessThanOrEqualTo(9, "value"));
        }

        [Test]
        public void ValidateIsLessThanOrEqualTo_GreaterValue_Throws()
        {
            int value = 11;
            Assert.Throws<ValidationException>(() => value.ValidateIsLessThanOrEqualTo(7, "value"));
        }
    }
} 