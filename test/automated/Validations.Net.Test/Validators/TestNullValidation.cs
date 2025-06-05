using System;
using NUnit.Framework;
using Validations.Net.Validators;
using Validations.Net;

namespace Validations.Net.Test.Validators
{
    [TestFixture]
    public class TestNullValidation
    {
        [Test]
        public void IsNull_Null_ReturnsTrue()
        {
            object? value = null;
            Assert.That(NullValidation.IsNull(value), Is.True);
        }

        [Test]
        public void IsNull_NonNull_ReturnsFalse()
        {
            object value = new object();
            Assert.That(NullValidation.IsNull(value), Is.False);
        }

        [Test]
        public void CheckIsNull_Null_ReturnsTrue()
        {
            object? value = null;
            Assert.That(value.CheckIsNull(), Is.True);
        }

        [Test]
        public void CheckIsNull_NonNull_ReturnsFalse()
        {
            object value = new object();
            Assert.That(value.CheckIsNull(), Is.False);
        }

        [Test]
        public void IsNullValidation_Null_DoesNotThrow()
        {
            object? value = null;
            Assert.DoesNotThrow(() => NullValidation.IsNullValidation(value, nameof(value)));
        }

        [Test]
        public void IsNullValidation_NonNull_Throws()
        {
            object value = new object();
            Assert.Throws<ValidationException>(() => NullValidation.IsNullValidation(value, nameof(value)));
        }

        [Test]
        public void ValidateIsNull_Null_DoesNotThrow()
        {
            object? value = null;
            Assert.DoesNotThrow(() => value.ValidateIsNull(nameof(value)));
        }

        [Test]
        public void ValidateIsNull_NonNull_Throws()
        {
            object value = new object();
            Assert.Throws<ValidationException>(() => value.ValidateIsNull(nameof(value)));
        }
    }
} 