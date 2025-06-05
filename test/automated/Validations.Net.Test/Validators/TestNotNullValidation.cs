using System;
using NUnit.Framework;
using Validations.Net.Validators;
using Validations.Net;

namespace Validations.Net.Test.Validators
{
    [TestFixture]
    public class TestNotNullValidation
    {
        [Test]
        public void IsNotNull_NonNull_ReturnsTrue()
        {
            object value = new object();
            Assert.That(NotNullValidation.IsNotNull(value), Is.True);
        }

        [Test]
        public void IsNotNull_Null_ReturnsFalse()
        {
            object? value = null;
            Assert.That(NotNullValidation.IsNotNull(value), Is.False);
        }

        [Test]
        public void CheckIsNotNull_NonNull_ReturnsTrue()
        {
            object value = new object();
            Assert.That(value.CheckIsNotNull(), Is.True);
        }

        [Test]
        public void CheckIsNotNull_Null_ReturnsFalse()
        {
            object? value = null;
            Assert.That(value.CheckIsNotNull(), Is.False);
        }

        [Test]
        public void IsNotNullValidation_NonNull_DoesNotThrow()
        {
            object value = new object();
            Assert.DoesNotThrow(() => NotNullValidation.IsNotNullValidation(value, nameof(value)));
        }

        [Test]
        public void IsNotNullValidation_Null_Throws()
        {
            object? value = null;
            Assert.Throws<ValidationException>(() => NotNullValidation.IsNotNullValidation(value, nameof(value)));
        }

        [Test]
        public void ValidateIsNotNull_NonNull_DoesNotThrow()
        {
            object value = new object();
            Assert.DoesNotThrow(() => value.ValidateIsNotNull(nameof(value)));
        }

        [Test]
        public void ValidateIsNotNull_Null_Throws()
        {
            object? value = null;
            Assert.Throws<ValidationException>(() => value.ValidateIsNotNull(nameof(value)));
        }
    }
} 