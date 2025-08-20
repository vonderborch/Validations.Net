using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsGreaterThanOrEqualsTests
{
    [TestFixture]
    public class CheckIsGreaterThanOrEqualsTests
    {
        [Test]
        public void CheckIsGreaterThanOrEquals_WithGreaterInteger_ReturnsTrue()
        {
            // Arrange
            int value = 100;
            int other = 42;

            // Act
            bool result = value.CheckIsGreaterThanOrEquals(other);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsGreaterThanOrEquals_WithEqualIntegers_ReturnsTrue()
        {
            // Arrange
            int value = 42;
            int other = 42;

            // Act
            bool result = value.CheckIsGreaterThanOrEquals(other);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsGreaterThanOrEquals_WithLesserInteger_ReturnsFalse()
        {
            // Arrange
            int value = 42;
            int other = 100;

            // Act
            bool result = value.CheckIsGreaterThanOrEquals(other);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsGreaterThanOrEquals_WithGreaterDouble_ReturnsTrue()
        {
            // Arrange
            double value = 3.15;
            double other = 3.14;

            // Act
            bool result = value.CheckIsGreaterThanOrEquals(other);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsGreaterThanOrEquals_WithEqualDoubles_ReturnsTrue()
        {
            // Arrange
            double value = 3.14;
            double other = 3.14;

            // Act
            bool result = value.CheckIsGreaterThanOrEquals(other);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsGreaterThanOrEquals_WithLesserDouble_ReturnsFalse()
        {
            // Arrange
            double value = 3.14;
            double other = 3.15;

            // Act
            bool result = value.CheckIsGreaterThanOrEquals(other);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsGreaterThanOrEquals_WithGreaterString_ReturnsTrue()
        {
            // Arrange
            string value = "World";
            string other = "Hello";

            // Act
            bool result = value.CheckIsGreaterThanOrEquals(other);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsGreaterThanOrEquals_WithEqualStrings_ReturnsTrue()
        {
            // Arrange
            string value = "Hello";
            string other = "Hello";

            // Act
            bool result = value.CheckIsGreaterThanOrEquals(other);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsGreaterThanOrEquals_WithLesserString_ReturnsFalse()
        {
            // Arrange
            string value = "Hello";
            string other = "World";

            // Act
            bool result = value.CheckIsGreaterThanOrEquals(other);

            // Assert
            Assert.That(result, Is.False);
        }
    }

    [TestFixture]
    public class EnsureIsGreaterThanOrEqualsTests
    {
        [Test]
        public void EnsureIsGreaterThanOrEquals_WithGreaterInteger_ReturnsValue()
        {
            // Arrange
            int value = 100;
            int other = 42;

            // Act
            int result = value.EnsureIsGreaterThanOrEquals(other);

            // Assert
            Assert.That(result, Is.EqualTo(100));
        }

        [Test]
        public void EnsureIsGreaterThanOrEquals_WithEqualIntegers_ReturnsValue()
        {
            // Arrange
            int value = 42;
            int other = 42;

            // Act
            int result = value.EnsureIsGreaterThanOrEquals(other);

            // Assert
            Assert.That(result, Is.EqualTo(42));
        }

        [Test]
        public void EnsureIsGreaterThanOrEquals_WithLesserInteger_ThrowsValidationException()
        {
            // Arrange
            int value = 42;
            int other = 100;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsGreaterThanOrEquals(other));
            Assert.That(exception.Message, Does.Contain("Parameter must be greater than or equal to the specified value"));
        }

        [Test]
        public void EnsureIsGreaterThanOrEquals_WithGreaterDouble_ReturnsValue()
        {
            // Arrange
            double value = 3.15;
            double other = 3.14;

            // Act
            double result = value.EnsureIsGreaterThanOrEquals(other);

            // Assert
            Assert.That(result, Is.EqualTo(3.15));
        }

        [Test]
        public void EnsureIsGreaterThanOrEquals_WithEqualDoubles_ReturnsValue()
        {
            // Arrange
            double value = 3.14;
            double other = 3.14;

            // Act
            double result = value.EnsureIsGreaterThanOrEquals(other);

            // Assert
            Assert.That(result, Is.EqualTo(3.14));
        }

        [Test]
        public void EnsureIsGreaterThanOrEquals_WithLesserDouble_ThrowsValidationException()
        {
            // Arrange
            double value = 3.14;
            double other = 3.15;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsGreaterThanOrEquals(other));
            Assert.That(exception.Message, Does.Contain("Parameter must be greater than or equal to the specified value"));
        }

        [Test]
        public void EnsureIsGreaterThanOrEquals_WithParameterName_IncludesParameterNameInException()
        {
            // Arrange
            int value = 42;
            int other = 100;
            string parameterName = "testParam";

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsGreaterThanOrEquals(other, parameterName));
            Assert.That(exception.Message, Does.Contain(parameterName));
        }
    }

    [TestFixture]
    public class ValidateIsGreaterThanOrEqualsTests
    {
        [Test]
        public void ValidateIsGreaterThanOrEquals_WithGreaterInteger_ReturnsValidResult()
        {
            // Arrange
            int value = 100;
            int other = 42;

            // Act
            ValidationResult result = value.ValidateIsGreaterThanOrEquals(other);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsGreaterThanOrEquals_WithEqualIntegers_ReturnsValidResult()
        {
            // Arrange
            int value = 42;
            int other = 42;

            // Act
            ValidationResult result = value.ValidateIsGreaterThanOrEquals(other);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsGreaterThanOrEquals_WithLesserInteger_ReturnsInvalidResult()
        {
            // Arrange
            int value = 42;
            int other = 100;

            // Act
            ValidationResult result = value.ValidateIsGreaterThanOrEquals(other);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be greater than or equal to the specified value"));
        }

        [Test]
        public void ValidateIsGreaterThanOrEquals_WithGreaterDouble_ReturnsValidResult()
        {
            // Arrange
            double value = 3.15;
            double other = 3.14;

            // Act
            ValidationResult result = value.ValidateIsGreaterThanOrEquals(other);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsGreaterThanOrEquals_WithEqualDoubles_ReturnsValidResult()
        {
            // Arrange
            double value = 3.14;
            double other = 3.14;

            // Act
            ValidationResult result = value.ValidateIsGreaterThanOrEquals(other);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsGreaterThanOrEquals_WithLesserDouble_ReturnsInvalidResult()
        {
            // Arrange
            double value = 3.14;
            double other = 3.15;

            // Act
            ValidationResult result = value.ValidateIsGreaterThanOrEquals(other);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be greater than or equal to the specified value"));
        }

        [Test]
        public void ValidateIsGreaterThanOrEquals_WithParameterName_IncludesParameterNameInResult()
        {
            // Arrange
            int value = 42;
            int other = 100;
            string parameterName = "testParam";

            // Act
            ValidationResult result = value.ValidateIsGreaterThanOrEquals(other, parameterName);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain(parameterName));
        }
    }
}
