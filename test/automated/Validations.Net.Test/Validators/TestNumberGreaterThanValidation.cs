using System;
using NUnit.Framework;
using Validations.Net.Validators;
using Validations.Net;

namespace Validations.Net.Test.Validators
{
    [TestFixture]
    public class TestNumberGreaterThanValidation
    {
        [Test]
        public void IsGreaterThan_GreaterValue_ReturnsTrue()
        {
            Assert.That(NumberGreaterThanValidation.IsGreaterThan(10, 5), Is.True);
        }

        [Test]
        public void IsGreaterThan_EqualValue_ReturnsFalse()
        {
            Assert.That(NumberGreaterThanValidation.IsGreaterThan(7, 7), Is.False);
        }

        [Test]
        public void IsGreaterThan_LesserValue_ReturnsFalse()
        {
            Assert.That(NumberGreaterThanValidation.IsGreaterThan(3, 8), Is.False);
        }

        [Test]
        public void CheckIsGreaterThan_GreaterValue_ReturnsTrue()
        {
            int value = 15;
            Assert.That(value.CheckIsGreaterThan(10), Is.True);
        }

        [Test]
        public void CheckIsGreaterThan_EqualValue_ReturnsFalse()
        {
            int value = 20;
            Assert.That(value.CheckIsGreaterThan(20), Is.False);
        }

        [Test]
        public void CheckIsGreaterThan_LesserValue_ReturnsFalse()
        {
            int value = 2;
            Assert.That(value.CheckIsGreaterThan(5), Is.False);
        }

        [Test]
        public void IsGreaterThanValidation_GreaterValue_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => NumberGreaterThanValidation.IsGreaterThanValidation(9, 4, "value"));
        }

        [Test]
        public void IsGreaterThanValidation_EqualValue_Throws()
        {
            Assert.Throws<ValidationException>(() => NumberGreaterThanValidation.IsGreaterThanValidation(6, 6, "value"));
        }

        [Test]
        public void IsGreaterThanValidation_LesserValue_Throws()
        {
            Assert.Throws<ValidationException>(() => NumberGreaterThanValidation.IsGreaterThanValidation(1, 2, "value"));
        }

        [Test]
        public void ValidateIsGreaterThan_GreaterValue_DoesNotThrow()
        {
            int value = 11;
            Assert.DoesNotThrow(() => value.ValidateIsGreaterThan(7, "value"));
        }

        [Test]
        public void ValidateIsGreaterThan_EqualValue_Throws()
        {
            int value = 13;
            Assert.Throws<ValidationException>(() => value.ValidateIsGreaterThan(13, "value"));
        }

        [Test]
        public void ValidateIsGreaterThan_LesserValue_Throws()
        {
            int value = 4;
            Assert.Throws<ValidationException>(() => value.ValidateIsGreaterThan(9, "value"));
        }
    }
} 