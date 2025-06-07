using System;
using NUnit.Framework;
using Validations.Net.Validators.Numbers;
using Validations.Net;

namespace Validations.Net.Test.Validators.Numbers
{
    [TestFixture]
    public class TestEqualToValidations
    {
        [Test]
        public void CheckIsEqualTo_ValuesWithinTolerance_ReturnsTrue()
        {
            double value = 5.0;
            double other = 5.01;
            double tolerance = 0.02;
            Assert.That(value.CheckIsEqualTo(other, tolerance), Is.True);
        }

        [Test]
        public void CheckIsEqualTo_ValuesOutsideTolerance_ReturnsFalse()
        {
            double value = 5.0;
            double other = 5.1;
            double tolerance = 0.05;
            Assert.That(value.CheckIsEqualTo(other, tolerance), Is.False);
        }

        [Test]
        public void CheckIsNotEqualTo_ValuesOutsideTolerance_ReturnsTrue()
        {
            double value = 5.0;
            double other = 5.1;
            double tolerance = 0.05;
            Assert.That(value.CheckIsNotEqualTo(other, tolerance), Is.True);
        }

        [Test]
        public void CheckIsNotEqualTo_ValuesWithinTolerance_ReturnsFalse()
        {
            double value = 5.0;
            double other = 5.01;
            double tolerance = 0.02;
            Assert.That(value.CheckIsNotEqualTo(other, tolerance), Is.False);
        }

        [Test]
        public void ValidateIsEqualTo_ValuesWithinTolerance_ReturnsValue()
        {
            double value = 5.0;
            double other = 5.01;
            double tolerance = 0.02;
            var result = value.ValidateIsEqualTo(other, tolerance, "test");
            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        public void ValidateIsEqualTo_ValuesOutsideTolerance_Throws()
        {
            double value = 5.0;
            double other = 5.1;
            double tolerance = 0.05;
            var ex = Assert.Throws<ValidationException>(() => value.ValidateIsEqualTo(other, tolerance, "test"));
            Assert.That(ex.Message, Does.Contain("must be equal to 5.1 within the tolerance of 0.05"));
        }

        [Test]
        public void ValidateIsNotEqualTo_ValuesOutsideTolerance_ReturnsValue()
        {
            double value = 5.0;
            double other = 5.1;
            double tolerance = 0.05;
            var result = value.ValidateIsNotEqualTo(other, tolerance, "test");
            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        public void ValidateIsNotEqualTo_ValuesWithinTolerance_Throws()
        {
            double value = 5.0;
            double other = 5.01;
            double tolerance = 0.02;
            var ex = Assert.Throws<ValidationException>(() => value.ValidateIsNotEqualTo(other, tolerance, "test"));
            Assert.That(ex.Message, Does.Contain("must not be equal to 5.01 within the tolerance of 0.02"));
        }

        [Test]
        public void CheckIsEqualTo_EqualValues_ReturnsTrue()
        {
            double value = 5.0;
            double other = 5.0;
            double tolerance = 0.0001;
            Assert.That(value.CheckIsEqualTo(other, tolerance), Is.True);
        }

        [Test]
        public void CheckIsEqualTo_NotEqualValues_ReturnsFalse()
        {
            double value = 5.0;
            double other = 6.0;
            double tolerance = 0.0001;
            Assert.That(value.CheckIsEqualTo(other, tolerance), Is.False);
        }

        [Test]
        public void CheckIsNotEqualTo_NotEqualValues_ReturnsTrue()
        {
            double value = 5.0;
            double other = 6.0;
            double tolerance = 0.0001;
            Assert.That(value.CheckIsNotEqualTo(other, tolerance), Is.True);
        }

        [Test]
        public void CheckIsNotEqualTo_EqualValues_ReturnsFalse()
        {
            double value = 5.0;
            double other = 5.0;
            double tolerance = 0.0001;
            Assert.That(value.CheckIsNotEqualTo(other, tolerance), Is.False);
        }

        [Test]
        public void ValidateIsEqualTo_EqualValues_ReturnsValue()
        {
            double value = 5.0;
            double other = 5.0;
            double tolerance = 0.0001;
            var result = value.ValidateIsEqualTo(other, tolerance, "test");
            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        public void ValidateIsEqualTo_NotEqualValues_Throws()
        {
            double value = 5.0;
            double other = 6.0;
            double tolerance = 0.0001;
            var ex = Assert.Throws<ValidationException>(() => value.ValidateIsEqualTo(other, tolerance, "test"));
            Assert.That(ex.Message, Does.Contain("must be equal to 6"));
        }

        [Test]
        public void ValidateIsNotEqualTo_NotEqualValues_ReturnsValue()
        {
            double value = 5.0;
            double other = 6.0;
            double tolerance = 0.0001;
            var result = value.ValidateIsNotEqualTo(other, tolerance, "test");
            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        public void ValidateIsNotEqualTo_EqualValues_Throws()
        {
            double value = 5.0;
            double other = 5.0;
            double tolerance = 0.0001;
            var ex = Assert.Throws<ValidationException>(() => value.ValidateIsNotEqualTo(other, tolerance, "test"));
            Assert.That(ex.Message, Does.Contain("must not be equal to 5"));
        }
    }
} 