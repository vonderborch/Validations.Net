using System;
using NUnit.Framework;
using Validations.Net.Validators;
using Validations.Net;

namespace Validations.Net.Test.Validators
{
    [TestFixture]
    public class TestNumberLessThanValidation
    {
        [Test]
        public void IsLessThan_LesserValue_ReturnsTrue()
        {
            Assert.That(NumberLessThanValidation.IsLessThan(5, 10), Is.True);
        }

        [Test]
        public void IsLessThan_EqualValue_ReturnsFalse()
        {
            Assert.That(NumberLessThanValidation.IsLessThan(7, 7), Is.False);
        }

        [Test]
        public void IsLessThan_GreaterValue_ReturnsFalse()
        {
            Assert.That(NumberLessThanValidation.IsLessThan(12, 8), Is.False);
        }

        [Test]
        public void CheckIsLessThan_LesserValue_ReturnsTrue()
        {
            int value = 3;
            Assert.That(value.CheckIsLessThan(5), Is.True);
        }

        [Test]
        public void CheckIsLessThan_EqualValue_ReturnsFalse()
        {
            int value = 10;
            Assert.That(value.CheckIsLessThan(10), Is.False);
        }

        [Test]
        public void CheckIsLessThan_GreaterValue_ReturnsFalse()
        {
            int value = 15;
            Assert.That(value.CheckIsLessThan(7), Is.False);
        }

        [Test]
        public void IsLessThanValidation_LesserValue_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => NumberLessThanValidation.IsLessThanValidation(4, 9, "value"));
        }

        [Test]
        public void IsLessThanValidation_EqualValue_Throws()
        {
            Assert.Throws<ValidationException>(() => NumberLessThanValidation.IsLessThanValidation(6, 6, "value"));
        }

        [Test]
        public void IsLessThanValidation_GreaterValue_Throws()
        {
            Assert.Throws<ValidationException>(() => NumberLessThanValidation.IsLessThanValidation(10, 5, "value"));
        }

        [Test]
        public void ValidateIsLessThan_LesserValue_DoesNotThrow()
        {
            int value = 2;
            Assert.DoesNotThrow(() => value.ValidateIsLessThan(8, "value"));
        }

        [Test]
        public void ValidateIsLessThan_EqualValue_Throws()
        {
            int value = 9;
            Assert.Throws<ValidationException>(() => value.ValidateIsLessThan(9, "value"));
        }

        [Test]
        public void ValidateIsLessThan_GreaterValue_Throws()
        {
            int value = 11;
            Assert.Throws<ValidationException>(() => value.ValidateIsLessThan(7, "value"));
        }
    }
} 