using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsLessThanTests
{
    [TestFixture]
    public class CheckIsLessThanTests
    {
        [Test]
        public void CheckIsLessThan_WithLesserInteger_ReturnsTrue()
        {
            // Arrange
            int value = 42;
            int other = 100;

            // Act
            bool result = value.CheckIsLessThan(other);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsLessThan_WithEqualIntegers_ReturnsFalse()
        {
            // Arrange
            int value = 42;
            int other = 42;

            // Act
            bool result = value.CheckIsLessThan(other);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsLessThan_WithGreaterInteger_ReturnsFalse()
        {
            // Arrange
            int value = 100;
            int other = 42;

            // Act
            bool result = value.CheckIsLessThan(other);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsLessThan_WithLesserDouble_ReturnsTrue()
        {
            // Arrange
            double value = 3.14;
            double other = 3.15;

            // Act
            bool result = value.CheckIsLessThan(other);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsLessThan_WithEqualDoubles_ReturnsFalse()
        {
            // Arrange
            double value = 3.14;
            double other = 3.14;

            // Act
            bool result = value.CheckIsLessThan(other);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsLessThan_WithGreaterDouble_ReturnsFalse()
        {
            // Arrange
            double value = 3.15;
            double other = 3.14;

            // Act
            bool result = value.CheckIsLessThan(other);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsLessThan_WithLesserString_ReturnsTrue()
        {
            // Arrange
            string value = "Hello";
            string other = "World";

            // Act
            bool result = value.CheckIsLessThan(other);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsLessThan_WithEqualStrings_ReturnsFalse()
        {
            // Arrange
            string value = "Hello";
            string other = "Hello";

            // Act
            bool result = value.CheckIsLessThan(other);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsLessThan_WithGreaterString_ReturnsFalse()
        {
            // Arrange
            string value = "World";
            string other = "Hello";

            // Act
            bool result = value.CheckIsLessThan(other);

            // Assert
            Assert.That(result, Is.False);
        }
    }

    [TestFixture]
    public class EnsureIsLessThanTests
    {
        [Test]
        public void EnsureIsLessThan_WithLesserInteger_ReturnsValue()
        {
            // Arrange
            int value = 42;
            int other = 100;

            // Act
            int result = value.EnsureIsLessThan(other);

            // Assert
            Assert.That(result, Is.EqualTo(42));
        }

        [Test]
        public void EnsureIsLessThan_WithEqualIntegers_ThrowsValidationException()
        {
            // Arrange
            int value = 42;
            int other = 42;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsLessThan(other));
            Assert.That(exception.Message, Does.Contain("Parameter must be less than the specified value"));
        }

        [Test]
        public void EnsureIsLessThan_WithGreaterInteger_ThrowsValidationException()
        {
            // Arrange
            int value = 100;
            int other = 42;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsLessThan(other));
            Assert.That(exception.Message, Does.Contain("Parameter must be less than the specified value"));
        }

        [Test]
        public void EnsureIsLessThan_WithLesserDouble_ReturnsValue()
        {
            // Arrange
            double value = 3.14;
            double other = 3.15;

            // Act
            double result = value.EnsureIsLessThan(other);

            // Assert
            Assert.That(result, Is.EqualTo(3.14));
        }

        [Test]
        public void EnsureIsLessThan_WithEqualDoubles_ThrowsValidationException()
        {
            // Arrange
            double value = 3.14;
            double other = 3.14;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsLessThan(other));
            Assert.That(exception.Message, Does.Contain("Parameter must be less than the specified value"));
        }

        [Test]
        public void EnsureIsLessThan_WithGreaterDouble_ThrowsValidationException()
        {
            // Arrange
            double value = 3.15;
            double other = 3.14;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsLessThan(other));
            Assert.That(exception.Message, Does.Contain("Parameter must be less than the specified value"));
        }

        [Test]
        public void EnsureIsLessThan_WithLesserString_ReturnsValue()
        {
            // Arrange
            string value = "Hello";
            string other = "World";

            // Act
            string result = value.EnsureIsLessThan(other);

            // Assert
            Assert.That(result, Is.EqualTo("Hello"));
        }

        [Test]
        public void EnsureIsLessThan_WithEqualStrings_ThrowsValidationException()
        {
            // Arrange
            string value = "Hello";
            string other = "Hello";

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsLessThan(other));
            Assert.That(exception.Message, Does.Contain("Parameter must be less than the specified value"));
        }

        [Test]
        public void EnsureIsLessThan_WithGreaterString_ThrowsValidationException()
        {
            // Arrange
            string value = "World";
            string other = "Hello";

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsLessThan(other));
            Assert.That(exception.Message, Does.Contain("Parameter must be less than the specified value"));
        }

        [Test]
        public void EnsureIsLessThan_WithParameterName_IncludesParameterNameInException()
        {
            // Arrange
            int value = 100;
            int other = 42;
            string parameterName = "testParam";

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsLessThan(other, null, parameterName));
            Assert.That(exception.Message, Does.Contain(parameterName));
        }
    }

    [TestFixture]
    public class ValidateIsLessThanTests
    {
        [Test]
        public void ValidateIsLessThan_WithLesserInteger_ReturnsValidResult()
        {
            // Arrange
            int value = 42;
            int other = 100;

            // Act
            ValidationResult result = value.ValidateIsLessThan(other);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsLessThan_WithEqualIntegers_ReturnsInvalidResult()
        {
            // Arrange
            int value = 42;
            int other = 42;

            // Act
            ValidationResult result = value.ValidateIsLessThan(other);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be less than the specified value"));
        }

        [Test]
        public void ValidateIsLessThan_WithGreaterInteger_ReturnsInvalidResult()
        {
            // Arrange
            int value = 100;
            int other = 42;

            // Act
            ValidationResult result = value.ValidateIsLessThan(other);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be less than the specified value"));
        }

        [Test]
        public void ValidateIsLessThan_WithLesserDouble_ReturnsValidResult()
        {
            // Arrange
            double value = 3.14;
            double other = 3.15;

            // Act
            ValidationResult result = value.ValidateIsLessThan(other);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsLessThan_WithEqualDoubles_ReturnsInvalidResult()
        {
            // Arrange
            double value = 3.14;
            double other = 3.14;

            // Act
            ValidationResult result = value.ValidateIsLessThan(other);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be less than the specified value"));
        }

        [Test]
        public void ValidateIsLessThan_WithGreaterDouble_ReturnsInvalidResult()
        {
            // Arrange
            double value = 3.15;
            double other = 3.14;

            // Act
            ValidationResult result = value.ValidateIsLessThan(other);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be less than the specified value"));
        }

        [Test]
        public void ValidateIsLessThan_WithLesserString_ReturnsValidResult()
        {
            // Arrange
            string value = "Hello";
            string other = "World";

            // Act
            ValidationResult result = value.ValidateIsLessThan(other);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsLessThan_WithEqualStrings_ReturnsInvalidResult()
        {
            // Arrange
            string value = "Hello";
            string other = "Hello";

            // Act
            ValidationResult result = value.ValidateIsLessThan(other);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be less than the specified value"));
        }

        [Test]
        public void ValidateIsLessThan_WithGreaterString_ReturnsInvalidResult()
        {
            // Arrange
            string value = "World";
            string other = "Hello";

            // Act
            ValidationResult result = value.ValidateIsLessThan(other);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be less than the specified value"));
        }

        [Test]
        public void ValidateIsLessThan_WithParameterName_IncludesParameterNameInResult()
        {
            // Arrange
            int value = 100;
            int other = 42;
            string parameterName = "testParam";

            // Act
            ValidationResult result = value.ValidateIsLessThan(other, null, parameterName);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain(parameterName));
        }
    }
}
