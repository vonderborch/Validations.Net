using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsLessThanOrEqualsTests
{
    [TestFixture]
    public class CheckIsLessThanOrEqualsTests
    {
        [Test]
        public void CheckIsLessThanOrEquals_WithLesserInteger_ReturnsTrue()
        {
            // Arrange
            int value = 42;
            int other = 100;

            // Act
            bool result = value.CheckIsLessThanOrEquals(other);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsLessThanOrEquals_WithEqualIntegers_ReturnsTrue()
        {
            // Arrange
            int value = 42;
            int other = 42;

            // Act
            bool result = value.CheckIsLessThanOrEquals(other);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsLessThanOrEquals_WithGreaterInteger_ReturnsFalse()
        {
            // Arrange
            int value = 100;
            int other = 42;

            // Act
            bool result = value.CheckIsLessThanOrEquals(other);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsLessThanOrEquals_WithLesserDouble_ReturnsTrue()
        {
            // Arrange
            double value = 3.14;
            double other = 3.15;

            // Act
            bool result = value.CheckIsLessThanOrEquals(other);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsLessThanOrEquals_WithEqualDoubles_ReturnsTrue()
        {
            // Arrange
            double value = 3.14;
            double other = 3.14;

            // Act
            bool result = value.CheckIsLessThanOrEquals(other);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsLessThanOrEquals_WithGreaterDouble_ReturnsFalse()
        {
            // Arrange
            double value = 3.15;
            double other = 3.14;

            // Act
            bool result = value.CheckIsLessThanOrEquals(other);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsLessThanOrEquals_WithLesserString_ReturnsTrue()
        {
            // Arrange
            string value = "Hello";
            string other = "World";

            // Act
            bool result = value.CheckIsLessThanOrEquals(other);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsLessThanOrEquals_WithEqualStrings_ReturnsTrue()
        {
            // Arrange
            string value = "Hello";
            string other = "Hello";

            // Act
            bool result = value.CheckIsLessThanOrEquals(other);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsLessThanOrEquals_WithGreaterString_ReturnsFalse()
        {
            // Arrange
            string value = "World";
            string other = "Hello";

            // Act
            bool result = value.CheckIsLessThanOrEquals(other);

            // Assert
            Assert.That(result, Is.False);
        }
    }

    [TestFixture]
    public class EnsureIsLessThanOrEqualsTests
    {
        [Test]
        public void EnsureIsLessThanOrEquals_WithLesserInteger_ReturnsValue()
        {
            // Arrange
            int value = 42;
            int other = 100;

            // Act
            int result = value.EnsureIsLessThanOrEquals(other);

            // Assert
            Assert.That(result, Is.EqualTo(42));
        }

        [Test]
        public void EnsureIsLessThanOrEquals_WithEqualIntegers_ReturnsValue()
        {
            // Arrange
            int value = 42;
            int other = 42;

            // Act
            int result = value.EnsureIsLessThanOrEquals(other);

            // Assert
            Assert.That(result, Is.EqualTo(42));
        }

        [Test]
        public void EnsureIsLessThanOrEquals_WithGreaterInteger_ThrowsValidationException()
        {
            // Arrange
            int value = 100;
            int other = 42;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsLessThanOrEquals(other));
            Assert.That(exception.Message, Does.Contain("Parameter must be less than or equal to the specified value"));
        }

        [Test]
        public void EnsureIsLessThanOrEquals_WithLesserDouble_ReturnsValue()
        {
            // Arrange
            double value = 3.14;
            double other = 3.15;

            // Act
            double result = value.EnsureIsLessThanOrEquals(other);

            // Assert
            Assert.That(result, Is.EqualTo(3.14));
        }

        [Test]
        public void EnsureIsLessThanOrEquals_WithEqualDoubles_ReturnsValue()
        {
            // Arrange
            double value = 3.14;
            double other = 3.14;

            // Act
            double result = value.EnsureIsLessThanOrEquals(other);

            // Assert
            Assert.That(result, Is.EqualTo(3.14));
        }

        [Test]
        public void EnsureIsLessThanOrEquals_WithGreaterDouble_ThrowsValidationException()
        {
            // Arrange
            double value = 3.15;
            double other = 3.14;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsLessThanOrEquals(other));
            Assert.That(exception.Message, Does.Contain("Parameter must be less than or equal to the specified value"));
        }

        [Test]
        public void EnsureIsLessThanOrEquals_WithLesserString_ReturnsValue()
        {
            // Arrange
            string value = "Hello";
            string other = "World";

            // Act
            string result = value.EnsureIsLessThanOrEquals(other);

            // Assert
            Assert.That(result, Is.EqualTo("Hello"));
        }

        [Test]
        public void EnsureIsLessThanOrEquals_WithEqualStrings_ReturnsValue()
        {
            // Arrange
            string value = "Hello";
            string other = "Hello";

            // Act
            string result = value.EnsureIsLessThanOrEquals(other);

            // Assert
            Assert.That(result, Is.EqualTo("Hello"));
        }

        [Test]
        public void EnsureIsLessThanOrEquals_WithGreaterString_ThrowsValidationException()
        {
            // Arrange
            string value = "World";
            string other = "Hello";

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsLessThanOrEquals(other));
            Assert.That(exception.Message, Does.Contain("Parameter must be less than or equal to the specified value"));
        }

        [Test]
        public void EnsureIsLessThanOrEquals_WithParameterName_IncludesParameterNameInException()
        {
            // Arrange
            int value = 100;
            int other = 42;
            string parameterName = "testParam";

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsLessThanOrEquals(other, parameterName));
            Assert.That(exception.Message, Does.Contain(parameterName));
        }
    }

    [TestFixture]
    public class ValidateIsLessThanOrEqualsTests
    {
        [Test]
        public void ValidateIsLessThanOrEquals_WithLesserInteger_ReturnsValidResult()
        {
            // Arrange
            int value = 42;
            int other = 100;

            // Act
            ValidationResult result = value.ValidateIsLessThanOrEquals(other);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsLessThanOrEquals_WithEqualIntegers_ReturnsValidResult()
        {
            // Arrange
            int value = 42;
            int other = 42;

            // Act
            ValidationResult result = value.ValidateIsLessThanOrEquals(other);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsLessThanOrEquals_WithGreaterInteger_ReturnsInvalidResult()
        {
            // Arrange
            int value = 100;
            int other = 42;

            // Act
            ValidationResult result = value.ValidateIsLessThanOrEquals(other);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be less than or equal to the specified value"));
        }

        [Test]
        public void ValidateIsLessThanOrEquals_WithLesserDouble_ReturnsValidResult()
        {
            // Arrange
            double value = 3.14;
            double other = 3.15;

            // Act
            ValidationResult result = value.ValidateIsLessThanOrEquals(other);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsLessThanOrEquals_WithEqualDoubles_ReturnsValidResult()
        {
            // Arrange
            double value = 3.14;
            double other = 3.14;

            // Act
            ValidationResult result = value.ValidateIsLessThanOrEquals(other);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsLessThanOrEquals_WithGreaterDouble_ReturnsInvalidResult()
        {
            // Arrange
            double value = 3.15;
            double other = 3.14;

            // Act
            ValidationResult result = value.ValidateIsLessThanOrEquals(other);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be less than or equal to the specified value"));
        }

        [Test]
        public void ValidateIsLessThanOrEquals_WithLesserString_ReturnsValidResult()
        {
            // Arrange
            string value = "Hello";
            string other = "World";

            // Act
            ValidationResult result = value.ValidateIsLessThanOrEquals(other);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsLessThanOrEquals_WithEqualStrings_ReturnsValidResult()
        {
            // Arrange
            string value = "Hello";
            string other = "Hello";

            // Act
            ValidationResult result = value.ValidateIsLessThanOrEquals(other);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsLessThanOrEquals_WithGreaterString_ReturnsInvalidResult()
        {
            // Arrange
            string value = "World";
            string other = "Hello";

            // Act
            ValidationResult result = value.ValidateIsLessThanOrEquals(other);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be less than or equal to the specified value"));
        }

        [Test]
        public void ValidateIsLessThanOrEquals_WithParameterName_IncludesParameterNameInResult()
        {
            // Arrange
            int value = 100;
            int other = 42;
            string parameterName = "testParam";

            // Act
            ValidationResult result = value.ValidateIsLessThanOrEquals(other, parameterName);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain(parameterName));
        }
    }
}
