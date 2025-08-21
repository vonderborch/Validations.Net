using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsGreaterThanTests
{
    [TestFixture]
    public class CheckIsGreaterThanTests
    {
        [Test]
        public void CheckIsGreaterThan_WithGreaterInteger_ReturnsTrue()
        {
            // Arrange
            int value = 100;
            int other = 42;

            // Act
            bool result = value.CheckIsGreaterThan(other);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsGreaterThan_WithEqualIntegers_ReturnsFalse()
        {
            // Arrange
            int value = 42;
            int other = 42;

            // Act
            bool result = value.CheckIsGreaterThan(other);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsGreaterThan_WithLesserInteger_ReturnsFalse()
        {
            // Arrange
            int value = 42;
            int other = 100;

            // Act
            bool result = value.CheckIsGreaterThan(other);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsGreaterThan_WithGreaterDouble_ReturnsTrue()
        {
            // Arrange
            double value = 3.15;
            double other = 3.14;

            // Act
            bool result = value.CheckIsGreaterThan(other);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsGreaterThan_WithEqualDoubles_ReturnsFalse()
        {
            // Arrange
            double value = 3.14;
            double other = 3.14;

            // Act
            bool result = value.CheckIsGreaterThan(other);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsGreaterThan_WithLesserDouble_ReturnsFalse()
        {
            // Arrange
            double value = 3.14;
            double other = 3.15;

            // Act
            bool result = value.CheckIsGreaterThan(other);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsGreaterThan_WithGreaterFloat_ReturnsTrue()
        {
            // Arrange
            float value = 3.15f;
            float other = 3.14f;

            // Act
            bool result = value.CheckIsGreaterThan(other);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsGreaterThan_WithEqualFloats_ReturnsFalse()
        {
            // Arrange
            float value = 3.14f;
            float other = 3.14f;

            // Act
            bool result = value.CheckIsGreaterThan(other);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsGreaterThan_WithLesserFloat_ReturnsFalse()
        {
            // Arrange
            float value = 3.14f;
            float other = 3.15f;

            // Act
            bool result = value.CheckIsGreaterThan(other);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsGreaterThan_WithGreaterString_ReturnsTrue()
        {
            // Arrange
            string value = "World";
            string other = "Hello";

            // Act
            bool result = value.CheckIsGreaterThan(other);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsGreaterThan_WithEqualStrings_ReturnsFalse()
        {
            // Arrange
            string value = "Hello";
            string other = "Hello";

            // Act
            bool result = value.CheckIsGreaterThan(other);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsGreaterThan_WithLesserString_ReturnsFalse()
        {
            // Arrange
            string value = "Hello";
            string other = "World";

            // Act
            bool result = value.CheckIsGreaterThan(other);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsGreaterThan_WithGreaterDecimal_ReturnsTrue()
        {
            // Arrange
            decimal value = 3.15m;
            decimal other = 3.14m;

            // Act
            bool result = value.CheckIsGreaterThan(other);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsGreaterThan_WithEqualDecimals_ReturnsFalse()
        {
            // Arrange
            decimal value = 3.14m;
            decimal other = 3.14m;

            // Act
            bool result = value.CheckIsGreaterThan(other);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsGreaterThan_WithLesserDecimal_ReturnsFalse()
        {
            // Arrange
            decimal value = 3.14m;
            decimal other = 3.15m;

            // Act
            bool result = value.CheckIsGreaterThan(other);

            // Assert
            Assert.That(result, Is.False);
        }
    }

    [TestFixture]
    public class EnsureIsGreaterThanTests
    {
        [Test]
        public void EnsureIsGreaterThan_WithGreaterInteger_ReturnsValue()
        {
            // Arrange
            int value = 100;
            int other = 42;

            // Act
            int result = value.EnsureIsGreaterThan(other);

            // Assert
            Assert.That(result, Is.EqualTo(100));
        }

        [Test]
        public void EnsureIsGreaterThan_WithEqualIntegers_ThrowsValidationException()
        {
            // Arrange
            int value = 42;
            int other = 42;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsGreaterThan(other));
            Assert.That(exception.Message, Does.Contain("Parameter must be greater than the specified value"));
        }

        [Test]
        public void EnsureIsGreaterThan_WithLesserInteger_ThrowsValidationException()
        {
            // Arrange
            int value = 42;
            int other = 100;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsGreaterThan(other));
            Assert.That(exception.Message, Does.Contain("Parameter must be greater than the specified value"));
        }

        [Test]
        public void EnsureIsGreaterThan_WithGreaterDouble_ReturnsValue()
        {
            // Arrange
            double value = 3.15;
            double other = 3.14;

            // Act
            double result = value.EnsureIsGreaterThan(other);

            // Assert
            Assert.That(result, Is.EqualTo(3.15));
        }

        [Test]
        public void EnsureIsGreaterThan_WithEqualDoubles_ThrowsValidationException()
        {
            // Arrange
            double value = 3.14;
            double other = 3.14;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsGreaterThan(other));
            Assert.That(exception.Message, Does.Contain("Parameter must be greater than the specified value"));
        }

        [Test]
        public void EnsureIsGreaterThan_WithGreaterString_ReturnsValue()
        {
            // Arrange
            string value = "World";
            string other = "Hello";

            // Act
            string result = value.EnsureIsGreaterThan(other);

            // Assert
            Assert.That(result, Is.EqualTo("World"));
        }

        [Test]
        public void EnsureIsGreaterThan_WithEqualStrings_ThrowsValidationException()
        {
            // Arrange
            string value = "Hello";
            string other = "Hello";

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsGreaterThan(other));
            Assert.That(exception.Message, Does.Contain("Parameter must be greater than the specified value"));
        }

        [Test]
        public void EnsureIsGreaterThan_WithParameterName_IncludesParameterNameInException()
        {
            // Arrange
            int value = 42;
            int other = 100;
            string parameterName = "testParam";

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsGreaterThan(other, null, parameterName));
            Assert.That(exception.Message, Does.Contain(parameterName));
        }

        [Test]
        public void EnsureIsGreaterThan_WithBlackboard_IncludesBlackboardInException()
        {
            // Arrange
            int value = 42;
            int other = 100;
            var blackboard = new Blackboard();

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsGreaterThan(other, blackboard));
            Assert.That(exception.Message, Does.Contain("Parameter must be greater than the specified value"));
        }
    }

    [TestFixture]
    public class ValidateIsGreaterThanTests
    {
        [Test]
        public void ValidateIsGreaterThan_WithGreaterInteger_ReturnsValidResult()
        {
            // Arrange
            int value = 100;
            int other = 42;

            // Act
            ValidationResult result = value.ValidateIsGreaterThan(other);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsGreaterThan_WithEqualIntegers_ReturnsInvalidResult()
        {
            // Arrange
            int value = 42;
            int other = 42;

            // Act
            ValidationResult result = value.ValidateIsGreaterThan(other);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be greater than the specified value"));
        }

        [Test]
        public void ValidateIsGreaterThan_WithLesserInteger_ReturnsInvalidResult()
        {
            // Arrange
            int value = 42;
            int other = 100;

            // Act
            ValidationResult result = value.ValidateIsGreaterThan(other);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be greater than the specified value"));
        }

        [Test]
        public void ValidateIsGreaterThan_WithGreaterDouble_ReturnsValidResult()
        {
            // Arrange
            double value = 3.15;
            double other = 3.14;

            // Act
            ValidationResult result = value.ValidateIsGreaterThan(other);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsGreaterThan_WithEqualDoubles_ReturnsInvalidResult()
        {
            // Arrange
            double value = 3.14;
            double other = 3.14;

            // Act
            ValidationResult result = value.ValidateIsGreaterThan(other);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be greater than the specified value"));
        }

        [Test]
        public void ValidateIsGreaterThan_WithGreaterString_ReturnsValidResult()
        {
            // Arrange
            string value = "World";
            string other = "Hello";

            // Act
            ValidationResult result = value.ValidateIsGreaterThan(other);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsGreaterThan_WithEqualStrings_ReturnsInvalidResult()
        {
            // Arrange
            string value = "Hello";
            string other = "Hello";

            // Act
            ValidationResult result = value.ValidateIsGreaterThan(other);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be greater than the specified value"));
        }

        [Test]
        public void ValidateIsGreaterThan_WithParameterName_IncludesParameterNameInResult()
        {
            // Arrange
            int value = 42;
            int other = 100;
            string parameterName = "testParam";

            // Act
            ValidationResult result = value.ValidateIsGreaterThan(other, null, parameterName);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain(parameterName));
        }

        [Test]
        public void ValidateIsGreaterThan_WithBlackboard_IncludesBlackboardInResult()
        {
            // Arrange
            int value = 42;
            int other = 100;
            var blackboard = new Blackboard();

            // Act
            ValidationResult result = value.ValidateIsGreaterThan(other, blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be greater than the specified value"));
        }
    }
}
