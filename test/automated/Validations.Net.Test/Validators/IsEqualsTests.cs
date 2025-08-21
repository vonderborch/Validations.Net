using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsEqualsTests
{
    [TestFixture]
    public class CheckIsEqualsTests
    {
        [Test]
        public void CheckIsEquals_WithEqualIntegers_ReturnsTrue()
        {
            // Arrange
            int value = 42;
            int expected = 42;

            // Act
            bool result = value.CheckIsEquals(expected);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsEquals_WithDifferentIntegers_ReturnsFalse()
        {
            // Arrange
            int value = 42;
            int expected = 100;

            // Act
            bool result = value.CheckIsEquals(expected);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsEquals_WithEqualStrings_ReturnsTrue()
        {
            // Arrange
            string value = "Hello";
            string expected = "Hello";

            // Act
            bool result = value.CheckIsEquals(expected);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsEquals_WithDifferentStrings_ReturnsFalse()
        {
            // Arrange
            string value = "Hello";
            string expected = "World";

            // Act
            bool result = value.CheckIsEquals(expected);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsEquals_WithNullValues_ReturnsTrue()
        {
            // Arrange
            string? value = null;
            string? expected = null;

            // Act
            bool result = value.CheckIsEquals(expected);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsEquals_WithNullAndNonNull_ReturnsFalse()
        {
            // Arrange
            string? value = null;
            string expected = "Hello";

            // Act
            bool result = value.CheckIsEquals(expected);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsEquals_WithNonNullAndNull_ReturnsFalse()
        {
            // Arrange
            string value = "Hello";
            string? expected = null;

            // Act
            bool result = value.CheckIsEquals(expected);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsEquals_WithEqualDoubles_ReturnsTrue()
        {
            // Arrange
            double value = 3.14159;
            double expected = 3.14159;

            // Act
            bool result = value.CheckIsEquals(expected);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsEquals_WithDifferentDoubles_ReturnsFalse()
        {
            // Arrange
            double value = 3.14159;
            double expected = 2.71828;

            // Act
            bool result = value.CheckIsEquals(expected);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsEquals_WithDoublesWithinTolerance_ReturnsTrue()
        {
            // Arrange
            double value = 3.14159;
            double expected = 3.1416;
            double tolerance = 0.001;

            // Act
            bool result = value.CheckIsEquals(expected, tolerance);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsEquals_WithDoublesOutsideTolerance_ReturnsFalse()
        {
            // Arrange
            double value = 3.14159;
            double expected = 3.15;
            double tolerance = 0.001;

            // Act
            bool result = value.CheckIsEquals(expected, tolerance);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsEquals_WithEqualFloats_ReturnsTrue()
        {
            // Arrange
            float value = 3.14f;
            float expected = 3.14f;

            // Act
            bool result = value.CheckIsEquals(expected);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsEquals_WithFloatsWithinTolerance_ReturnsTrue()
        {
            // Arrange
            float value = 3.14f;
            float expected = 3.141f;
            float tolerance = 0.01f;

            // Act
            bool result = value.CheckIsEquals(expected, tolerance);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsEquals_WithEqualObjects_ReturnsTrue()
        {
            // Arrange
            object value = "Hello";
            object expected = "Hello";

            // Act
            bool result = value.CheckIsEquals(expected);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsEquals_WithDifferentObjects_ReturnsFalse()
        {
            // Arrange
            object value = "Hello";
            object expected = "World";

            // Act
            bool result = value.CheckIsEquals(expected);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsEquals_WithNullObjects_ReturnsTrue()
        {
            // Arrange
            object? value = null;
            object? expected = null;

            // Act
            bool result = value.CheckIsEquals(expected);

            // Assert
            Assert.That(result, Is.True);
        }
    }

    [TestFixture]
    public class EnsureIsEqualsTests
    {
        [Test]
        public void EnsureIsEquals_WithEqualIntegers_ReturnsValue()
        {
            // Arrange
            int value = 42;
            int expected = 42;

            // Act
            int? result = value.EnsureIsEquals(expected);

            // Assert
            Assert.That(result, Is.EqualTo(42));
        }

        [Test]
        public void EnsureIsEquals_WithDifferentIntegers_ThrowsValidationException()
        {
            // Arrange
            int value = 42;
            int expected = 100;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsEquals(expected));
            Assert.That(exception.Message, Does.Contain("Parameter must equal the expected value"));
        }

        [Test]
        public void EnsureIsEquals_WithEqualStrings_ReturnsValue()
        {
            // Arrange
            string value = "Hello";
            string expected = "Hello";

            // Act
            string? result = value.EnsureIsEquals(expected);

            // Assert
            Assert.That(result, Is.EqualTo("Hello"));
        }

        [Test]
        public void EnsureIsEquals_WithDifferentStrings_ThrowsValidationException()
        {
            // Arrange
            string value = "Hello";
            string expected = "World";

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsEquals(expected));
            Assert.That(exception.Message, Does.Contain("Parameter must equal the expected value"));
        }

        [Test]
        public void EnsureIsEquals_WithNullValues_ReturnsValue()
        {
            // Arrange
            string? value = null;
            string? expected = null;

            // Act
            string? result = value.EnsureIsEquals(expected);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void EnsureIsEquals_WithDoublesWithinTolerance_ReturnsValue()
        {
            // Arrange
            double value = 3.14159;
            double expected = 3.1416;
            double tolerance = 0.001;

            // Act
            double result = value.EnsureIsEquals(expected, tolerance);

            // Assert
            Assert.That(result, Is.EqualTo(3.14159));
        }

        [Test]
        public void EnsureIsEquals_WithDoublesOutsideTolerance_ThrowsValidationException()
        {
            // Arrange
            double value = 3.14159;
            double expected = 3.15;
            double tolerance = 0.001;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsEquals(expected, tolerance));
            Assert.That(exception.Message, Does.Contain("Parameter must equal the expected value"));
        }

        [Test]
        public void EnsureIsEquals_WithEqualObjects_ReturnsValue()
        {
            // Arrange
            object value = "Hello";
            object expected = "Hello";

            // Act
            object? result = value.EnsureIsEquals(expected);

            // Assert
            Assert.That(result, Is.EqualTo("Hello"));
        }

        [Test]
        public void EnsureIsEquals_WithDifferentObjects_ThrowsValidationException()
        {
            // Arrange
            object value = "Hello";
            object expected = "World";

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsEquals(expected));
            Assert.That(exception.Message, Does.Contain("Parameter must equal the expected value"));
        }

        [Test]
        public void EnsureIsEquals_WithParameterName_IncludesParameterNameInException()
        {
            // Arrange
            int value = 42;
            int expected = 100;
            string parameterName = "testParam";

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsEquals(expected, null, parameterName));
            Assert.That(exception.Message, Does.Contain(parameterName));
        }

        [Test]
        public void EnsureIsEquals_WithBlackboard_IncludesBlackboardInException()
        {
            // Arrange
            int value = 42;
            int expected = 100;
            var blackboard = new Blackboard();

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsEquals(expected, blackboard));
            Assert.That(exception.Message, Does.Contain("Parameter must equal the expected value"));
        }
    }

    [TestFixture]
    public class ValidateIsEqualsTests
    {
        [Test]
        public void ValidateIsEquals_WithEqualIntegers_ReturnsValidResult()
        {
            // Arrange
            int value = 42;
            int expected = 42;

            // Act
            ValidationResult result = value.ValidateIsEquals(expected);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsEquals_WithDifferentIntegers_ReturnsInvalidResult()
        {
            // Arrange
            int value = 42;
            int expected = 100;

            // Act
            ValidationResult result = value.ValidateIsEquals(expected);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must equal the expected value"));
        }

        [Test]
        public void ValidateIsEquals_WithEqualStrings_ReturnsValidResult()
        {
            // Arrange
            string value = "Hello";
            string expected = "Hello";

            // Act
            ValidationResult result = value.ValidateIsEquals(expected);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsEquals_WithDifferentStrings_ReturnsInvalidResult()
        {
            // Arrange
            string value = "Hello";
            string expected = "World";

            // Act
            ValidationResult result = value.ValidateIsEquals(expected);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must equal the expected value"));
        }

        [Test]
        public void ValidateIsEquals_WithNullValues_ReturnsValidResult()
        {
            // Arrange
            string? value = null;
            string? expected = null;

            // Act
            ValidationResult result = value.ValidateIsEquals(expected);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsEquals_WithDoublesWithinTolerance_ReturnsValidResult()
        {
            // Arrange
            double value = 3.14159;
            double expected = 3.1416;
            double tolerance = 0.001;

            // Act
            ValidationResult result = value.ValidateIsEquals(expected, tolerance);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsEquals_WithDoublesOutsideTolerance_ReturnsInvalidResult()
        {
            // Arrange
            double value = 3.14159;
            double expected = 3.15;
            double tolerance = 0.001;

            // Act
            ValidationResult result = value.ValidateIsEquals(expected, tolerance);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must equal the expected value"));
        }

        [Test]
        public void ValidateIsEquals_WithEqualObjects_ReturnsValidResult()
        {
            // Arrange
            object value = "Hello";
            object expected = "Hello";

            // Act
            ValidationResult result = value.ValidateIsEquals(expected);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsEquals_WithDifferentObjects_ReturnsInvalidResult()
        {
            // Arrange
            object value = "Hello";
            object expected = "World";

            // Act
            ValidationResult result = value.ValidateIsEquals(expected);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must equal the expected value"));
        }

        [Test]
        public void ValidateIsEquals_WithParameterName_IncludesParameterNameInResult()
        {
            // Arrange
            int value = 42;
            int expected = 100;
            string parameterName = "testParam";

            // Act
            ValidationResult result = value.ValidateIsEquals(expected, null, parameterName);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain(parameterName));
        }

        [Test]
        public void ValidateIsEquals_WithBlackboard_IncludesBlackboardInResult()
        {
            // Arrange
            int value = 42;
            int expected = 100;
            var blackboard = new Blackboard();

            // Act
            ValidationResult result = value.ValidateIsEquals(expected, blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must equal the expected value"));
        }
    }
}
