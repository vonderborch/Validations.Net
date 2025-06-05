using System;
using NUnit.Framework;
using Validations.Net.Validators;
using Validations.Net;

namespace Validations.Net.Test.Validators
{
    [TestFixture]
    public class TestNumberGreaterThanOrEqualToValidation
    {
        [Test]
        public void IsGreaterThanOrEqualTo_GreaterValue_ReturnsTrue()
        {
            Assert.That(NumberGreaterThanOrEqualToValidation.IsGreaterThanOrEqualTo(10, 5), Is.True);
        }

        [Test]
        public void IsGreaterThanOrEqualTo_EqualValue_ReturnsTrue()
        {
            Assert.That(NumberGreaterThanOrEqualToValidation.IsGreaterThanOrEqualTo(7, 7), Is.True);
        }

        [Test]
        public void IsGreaterThanOrEqualTo_LesserValue_ReturnsFalse()
        {
            Assert.That(NumberGreaterThanOrEqualToValidation.IsGreaterThanOrEqualTo(3, 8), Is.False);
        }

        [Test]
        public void CheckIsGreaterThanOrEqualTo_GreaterValue_ReturnsTrue()
        {
            int value = 15;
            Assert.That(value.CheckIsGreaterThanOrEqualTo(10), Is.True);
        }

        [Test]
        public void CheckIsGreaterThanOrEqualTo_EqualValue_ReturnsTrue()
        {
            int value = 20;
            Assert.That(value.CheckIsGreaterThanOrEqualTo(20), Is.True);
        }

        [Test]
        public void CheckIsGreaterThanOrEqualTo_LesserValue_ReturnsFalse()
        {
            int value = 2;
            Assert.That(value.CheckIsGreaterThanOrEqualTo(5), Is.False);
        }

        [Test]
        public void IsGreaterThanOrEqualToValidation_GreaterValue_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => NumberGreaterThanOrEqualToValidation.IsGreaterThanOrEqualToValidation(9, 4, "value"));
        }

        [Test]
        public void IsGreaterThanOrEqualToValidation_EqualValue_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => NumberGreaterThanOrEqualToValidation.IsGreaterThanOrEqualToValidation(6, 6, "value"));
        }

        [Test]
        public void IsGreaterThanOrEqualToValidation_LesserValue_Throws()
        {
            Assert.Throws<ValidationException>(() => NumberGreaterThanOrEqualToValidation.IsGreaterThanOrEqualToValidation(1, 2, "value"));
        }

        [Test]
        public void ValidateIsGreaterThanOrEqualTo_GreaterValue_DoesNotThrow()
        {
            int value = 11;
            Assert.DoesNotThrow(() => value.ValidateIsGreaterThanOrEqualTo(7, "value"));
        }

        [Test]
        public void ValidateIsGreaterThanOrEqualTo_EqualValue_DoesNotThrow()
        {
            int value = 13;
            Assert.DoesNotThrow(() => value.ValidateIsGreaterThanOrEqualTo(13, "value"));
        }

        [Test]
        public void ValidateIsGreaterThanOrEqualTo_LesserValue_Throws()
        {
            int value = 4;
            Assert.Throws<ValidationException>(() => value.ValidateIsGreaterThanOrEqualTo(9, "value"));
        }
    }
} 