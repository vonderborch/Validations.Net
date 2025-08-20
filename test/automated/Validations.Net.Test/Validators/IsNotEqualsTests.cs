using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNotEqualsTests
{
    [TestFixture]
    public class CheckIsNotEqualsTests
    {
        [Test]
        public void CheckIsNotEquals_WithDifferentIntegers_ReturnsTrue()
        {
            // Arrange
            int value = 42;
            int notExpected = 100;

            // Act
            bool result = value.CheckIsNotEquals(notExpected);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotEquals_WithEqualIntegers_ReturnsFalse()
        {
            // Arrange
            int value = 42;
            int notExpected = 42;

            // Act
            bool result = value.CheckIsNotEquals(notExpected);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotEquals_WithDifferentStrings_ReturnsTrue()
        {
            // Arrange
            string value = "Hello";
            string notExpected = "World";

            // Act
            bool result = value.CheckIsNotEquals(notExpected);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotEquals_WithEqualStrings_ReturnsFalse()
        {
            // Arrange
            string value = "Hello";
            string notExpected = "Hello";

            // Act
            bool result = value.CheckIsNotEquals(notExpected);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotEquals_WithNullValues_ReturnsFalse()
        {
            // Arrange
            string? value = null;
            string? notExpected = null;

            // Act
            bool result = value.CheckIsNotEquals(notExpected);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotEquals_WithNullAndNonNull_ReturnsTrue()
        {
            // Arrange
            string? value = null;
            string notExpected = "Hello";

            // Act
            bool result = value.CheckIsNotEquals(notExpected);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotEquals_WithNonNullAndNull_ReturnsTrue()
        {
            // Arrange
            string value = "Hello";
            string? notExpected = null;

            // Act
            bool result = value.CheckIsNotEquals(notExpected);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotEquals_WithDifferentDoubles_ReturnsTrue()
        {
            // Arrange
            double value = 3.14159;
            double notExpected = 2.71828;

            // Act
            bool result = value.CheckIsNotEquals(notExpected);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotEquals_WithEqualDoubles_ReturnsFalse()
        {
            // Arrange
            double value = 3.14159;
            double notExpected = 3.14159;

            // Act
            bool result = value.CheckIsNotEquals(notExpected);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotEquals_WithDoublesOutsideTolerance_ReturnsTrue()
        {
            // Arrange
            double value = 3.14159;
            double notExpected = 3.15;
            double tolerance = 0.001;

            // Act
            bool result = value.CheckIsNotEquals(notExpected, tolerance);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotEquals_WithDoublesWithinTolerance_ReturnsFalse()
        {
            // Arrange
            double value = 3.14159;
            double notExpected = 3.1416;
            double tolerance = 0.001;

            // Act
            bool result = value.CheckIsNotEquals(notExpected, tolerance);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotEquals_WithDifferentFloats_ReturnsTrue()
        {
            // Arrange
            float value = 3.14f;
            float notExpected = 2.71f;

            // Act
            bool result = value.CheckIsNotEquals(notExpected);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotEquals_WithFloatsOutsideTolerance_ReturnsTrue()
        {
            // Arrange
            float value = 3.14f;
            float notExpected = 3.16f;
            float tolerance = 0.01f;

            // Act
            bool result = value.CheckIsNotEquals(notExpected, tolerance);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotEquals_WithFloatsWithinTolerance_ReturnsFalse()
        {
            // Arrange
            float value = 3.14f;
            float notExpected = 3.141f;
            float tolerance = 0.01f;

            // Act
            bool result = value.CheckIsNotEquals(notExpected, tolerance);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotEquals_WithDifferentObjects_ReturnsTrue()
        {
            // Arrange
            object value = "Hello";
            object notExpected = "World";

            // Act
            bool result = value.CheckIsNotEquals(notExpected);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotEquals_WithEqualObjects_ReturnsFalse()
        {
            // Arrange
            object value = "Hello";
            object notExpected = "Hello";

            // Act
            bool result = value.CheckIsNotEquals(notExpected);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotEquals_WithNullObjects_ReturnsFalse()
        {
            // Arrange
            object? value = null;
            object? notExpected = null;

            // Act
            bool result = value.CheckIsNotEquals(notExpected);

            // Assert
            Assert.That(result, Is.False);
        }
    }

    [TestFixture]
    public class EnsureIsNotEqualsTests
    {
        [Test]
        public void EnsureIsNotEquals_WithDifferentIntegers_ReturnsValue()
        {
            // Arrange
            int value = 42;
            int notExpected = 100;

            // Act
            int? result = value.EnsureIsNotEquals(notExpected);

            // Assert
            Assert.That(result, Is.EqualTo(42));
        }

        [Test]
        public void EnsureIsNotEquals_WithEqualIntegers_ThrowsValidationException()
        {
            // Arrange
            int value = 42;
            int notExpected = 42;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotEquals(notExpected));
            Assert.That(exception.Message, Does.Contain("Parameter must not equal the specified value"));
        }

        [Test]
        public void EnsureIsNotEquals_WithDifferentStrings_ReturnsValue()
        {
            // Arrange
            string value = "Hello";
            string notExpected = "World";

            // Act
            string? result = value.EnsureIsNotEquals(notExpected);

            // Assert
            Assert.That(result, Is.EqualTo("Hello"));
        }

        [Test]
        public void EnsureIsNotEquals_WithEqualStrings_ThrowsValidationException()
        {
            // Arrange
            string value = "Hello";
            string notExpected = "Hello";

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotEquals(notExpected));
            Assert.That(exception.Message, Does.Contain("Parameter must not equal the specified value"));
        }

        [Test]
        public void EnsureIsNotEquals_WithNullValues_ThrowsValidationException()
        {
            // Arrange
            string? value = null;
            string? notExpected = null;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotEquals(notExpected));
            Assert.That(exception.Message, Does.Contain("Parameter must not equal the specified value"));
        }

        [Test]
        public void EnsureIsNotEquals_WithDoublesOutsideTolerance_ReturnsValue()
        {
            // Arrange
            double value = 3.14159;
            double notExpected = 3.15;
            double tolerance = 0.001;

            // Act
            double result = value.EnsureIsNotEquals(notExpected, tolerance);

            // Assert
            Assert.That(result, Is.EqualTo(3.14159));
        }

        [Test]
        public void EnsureIsNotEquals_WithDoublesWithinTolerance_ThrowsValidationException()
        {
            // Arrange
            double value = 3.14159;
            double notExpected = 3.1416;
            double tolerance = 0.001;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotEquals(notExpected, tolerance));
            Assert.That(exception.Message, Does.Contain("Parameter must not equal the specified value"));
        }

        [Test]
        public void EnsureIsNotEquals_WithDifferentObjects_ReturnsValue()
        {
            // Arrange
            object value = "Hello";
            object notExpected = "World";

            // Act
            object? result = value.EnsureIsNotEquals(notExpected);

            // Assert
            Assert.That(result, Is.EqualTo("Hello"));
        }

        [Test]
        public void EnsureIsNotEquals_WithEqualObjects_ThrowsValidationException()
        {
            // Arrange
            object value = "Hello";
            object notExpected = "Hello";

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotEquals(notExpected));
            Assert.That(exception.Message, Does.Contain("Parameter must not equal the specified value"));
        }

        [Test]
        public void EnsureIsNotEquals_WithParameterName_IncludesParameterNameInException()
        {
            // Arrange
            int value = 42;
            int notExpected = 42;
            string parameterName = "testParam";

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotEquals(notExpected, parameterName));
            Assert.That(exception.Message, Does.Contain(parameterName));
        }

        [Test]
        public void EnsureIsNotEquals_WithBlackboard_IncludesBlackboardInException()
        {
            // Arrange
            int value = 42;
            int notExpected = 42;
            var blackboard = new Blackboard();

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotEquals(notExpected, null, blackboard));
            Assert.That(exception.Message, Does.Contain("Parameter must not equal the specified value"));
        }
    }

    [TestFixture]
    public class ValidateIsNotEqualsTests
    {
        [Test]
        public void ValidateIsNotEquals_WithDifferentIntegers_ReturnsValidResult()
        {
            // Arrange
            int value = 42;
            int notExpected = 100;

            // Act
            ValidationResult result = value.ValidateIsNotEquals(notExpected);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNotEquals_WithEqualIntegers_ReturnsInvalidResult()
        {
            // Arrange
            int value = 42;
            int notExpected = 42;

            // Act
            ValidationResult result = value.ValidateIsNotEquals(notExpected);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not equal the specified value"));
        }

        [Test]
        public void ValidateIsNotEquals_WithDifferentStrings_ReturnsValidResult()
        {
            // Arrange
            string value = "Hello";
            string notExpected = "World";

            // Act
            ValidationResult result = value.ValidateIsNotEquals(notExpected);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNotEquals_WithEqualStrings_ReturnsInvalidResult()
        {
            // Arrange
            string value = "Hello";
            string notExpected = "Hello";

            // Act
            ValidationResult result = value.ValidateIsNotEquals(notExpected);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not equal the specified value"));
        }

        [Test]
        public void ValidateIsNotEquals_WithNullValues_ReturnsInvalidResult()
        {
            // Arrange
            string? value = null;
            string? notExpected = null;

            // Act
            ValidationResult result = value.ValidateIsNotEquals(notExpected);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not equal the specified value"));
        }

        [Test]
        public void ValidateIsNotEquals_WithDoublesOutsideTolerance_ReturnsValidResult()
        {
            // Arrange
            double value = 3.14159;
            double notExpected = 3.15;
            double tolerance = 0.001;

            // Act
            ValidationResult result = value.ValidateIsNotEquals(notExpected, tolerance);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNotEquals_WithDoublesWithinTolerance_ReturnsInvalidResult()
        {
            // Arrange
            double value = 3.14159;
            double notExpected = 3.1416;
            double tolerance = 0.001;

            // Act
            ValidationResult result = value.ValidateIsNotEquals(notExpected, tolerance);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not equal the specified value"));
        }

        [Test]
        public void ValidateIsNotEquals_WithDifferentObjects_ReturnsValidResult()
        {
            // Arrange
            object value = "Hello";
            object notExpected = "World";

            // Act
            ValidationResult result = value.ValidateIsNotEquals(notExpected);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNotEquals_WithEqualObjects_ReturnsInvalidResult()
        {
            // Arrange
            object value = "Hello";
            object notExpected = "Hello";

            // Act
            ValidationResult result = value.ValidateIsNotEquals(notExpected);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not equal the specified value"));
        }

        [Test]
        public void ValidateIsNotEquals_WithParameterName_IncludesParameterNameInResult()
        {
            // Arrange
            int value = 42;
            int notExpected = 42;
            string parameterName = "testParam";

            // Act
            ValidationResult result = value.ValidateIsNotEquals(notExpected, parameterName);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain(parameterName));
        }

        [Test]
        public void ValidateIsNotEquals_WithBlackboard_IncludesBlackboardInResult()
        {
            // Arrange
            int value = 42;
            int notExpected = 42;
            var blackboard = new Blackboard();

            // Act
            ValidationResult result = value.ValidateIsNotEquals(notExpected, null, blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not equal the specified value"));
        }
    }
}
