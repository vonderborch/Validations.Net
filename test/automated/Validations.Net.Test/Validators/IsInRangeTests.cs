using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsInRangeTests
{
    [TestFixture]
    public class CheckIsInRangeTests
    {
        [Test]
        public void CheckIsInRange_WithValueInRange_ReturnsTrue()
        {
            // Arrange
            var value = 5;
            var minimum = 1;
            var maximum = 10;

            // Act
            var result = value.CheckIsInRange(minimum, maximum);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsInRange_WithValueEqualToMinimum_ReturnsTrue()
        {
            // Arrange
            var value = 1;
            var minimum = 1;
            var maximum = 10;

            // Act
            var result = value.CheckIsInRange(minimum, maximum);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsInRange_WithValueEqualToMaximum_ReturnsTrue()
        {
            // Arrange
            var value = 10;
            var minimum = 1;
            var maximum = 10;

            // Act
            var result = value.CheckIsInRange(minimum, maximum);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsInRange_WithValueBelowMinimum_ReturnsFalse()
        {
            // Arrange
            var value = 0;
            var minimum = 1;
            var maximum = 10;

            // Act
            var result = value.CheckIsInRange(minimum, maximum);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsInRange_WithValueAboveMaximum_ReturnsFalse()
        {
            // Arrange
            var value = 11;
            var minimum = 1;
            var maximum = 10;

            // Act
            var result = value.CheckIsInRange(minimum, maximum);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsInRange_WithExclusiveMinimum_ValueEqualToMinimum_ReturnsFalse()
        {
            // Arrange
            var value = 1;
            var minimum = 1;
            var maximum = 10;

            // Act
            var result = value.CheckIsInRange(minimum, maximum, minimumInclusive: false);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsInRange_WithExclusiveMaximum_ValueEqualToMaximum_ReturnsFalse()
        {
            // Arrange
            var value = 10;
            var minimum = 1;
            var maximum = 10;

            // Act
            var result = value.CheckIsInRange(minimum, maximum, maximumInclusive: false);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsInRange_WithExclusiveBounds_ValueInRange_ReturnsTrue()
        {
            // Arrange
            var value = 5;
            var minimum = 1;
            var maximum = 10;

            // Act
            var result = value.CheckIsInRange(minimum, maximum, minimumInclusive: false, maximumInclusive: false);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsInRange_WithStringValues_ReturnsExpectedResult()
        {
            // Arrange
            var value = "middle";
            var minimum = "alpha";
            var maximum = "zebra";

            // Act
            var result = value.CheckIsInRange(minimum, maximum);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsInRange_WithDateTimeValues_ReturnsExpectedResult()
        {
            // Arrange
            var value = DateTime.Now;
            var minimum = DateTime.Now.AddDays(-1);
            var maximum = DateTime.Now.AddDays(1);

            // Act
            var result = value.CheckIsInRange(minimum, maximum);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsInRange_WithDecimalValues_ReturnsExpectedResult()
        {
            // Arrange
            var value = 5.5m;
            var minimum = 1.0m;
            var maximum = 10.0m;

            // Act
            var result = value.CheckIsInRange(minimum, maximum);

            // Assert
            Assert.That(result, Is.True);
        }
    }

    [TestFixture]
    public class EnsureIsInRangeTests
    {
        [Test]
        public void EnsureIsInRange_WithValueInRange_DoesNotThrow()
        {
            // Arrange
            var value = 5;
            var minimum = 1;
            var maximum = 10;

            // Act & Assert
            Assert.DoesNotThrow(() => value.EnsureIsInRange(minimum, maximum));
        }

        [Test]
        public void EnsureIsInRange_WithValueBelowMinimum_ThrowsValidationException()
        {
            // Arrange
            var value = 0;
            var minimum = 1;
            var maximum = 10;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsInRange(minimum, maximum));
            Assert.That(exception.Message, Does.Contain("Value must be >= 1 and <= 10"));
            Assert.That(exception.Message, Does.Contain("Actual value: 0"));
        }

        [Test]
        public void EnsureIsInRange_WithValueAboveMaximum_ThrowsValidationException()
        {
            // Arrange
            var value = 11;
            var minimum = 1;
            var maximum = 10;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsInRange(minimum, maximum));
            Assert.That(exception.Message, Does.Contain("Value must be >= 1 and <= 10"));
            Assert.That(exception.Message, Does.Contain("Actual value: 11"));
        }

        [Test]
        public void EnsureIsInRange_WithExclusiveMinimum_ValueEqualToMinimum_ThrowsValidationException()
        {
            // Arrange
            var value = 1;
            var minimum = 1;
            var maximum = 10;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsInRange(minimum, maximum, minimumInclusive: false));
            Assert.That(exception.Message, Does.Contain("Value must be > 1 and <= 10"));
        }

        [Test]
        public void EnsureIsInRange_WithExclusiveMaximum_ValueEqualToMaximum_ThrowsValidationException()
        {
            // Arrange
            var value = 10;
            var minimum = 1;
            var maximum = 10;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsInRange(minimum, maximum, maximumInclusive: false));
            Assert.That(exception.Message, Does.Contain("Value must be >= 1 and < 10"));
        }

        [Test]
        public void EnsureIsInRange_WithExclusiveBounds_ValueEqualToBounds_ThrowsValidationException()
        {
            // Arrange
            var value = 5;
            var minimum = 5;
            var maximum = 5;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsInRange(minimum, maximum, minimumInclusive: false, maximumInclusive: false));
            Assert.That(exception.Message, Does.Contain("Value must be > 5 and < 5"));
        }

        [Test]
        public void EnsureIsInRange_WithCustomParameterName_IncludesParameterNameInException()
        {
            // Arrange
            var value = 0;
            var minimum = 1;
            var maximum = 10;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsInRange(minimum, maximum, parameterName: "testValue"));
            Assert.That(exception.ParameterName, Is.EqualTo("testValue"));
        }
    }

    [TestFixture]
    public class ValidateIsInRangeTests
    {
        [Test]
        public void ValidateIsInRange_WithValueInRange_ReturnsSuccess()
        {
            // Arrange
            var value = 5;
            var minimum = 1;
            var maximum = 10;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsInRange(minimum, maximum, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsInRange_WithValueBelowMinimum_ReturnsFailure()
        {
            // Arrange
            var value = 0;
            var minimum = 1;
            var maximum = 10;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsInRange(minimum, maximum, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain("Value must be >= 1 and <= 10"));
            Assert.That(result.ValidationException!.Message, Does.Contain("Actual value: 0"));
        }

        [Test]
        public void ValidateIsInRange_WithValueAboveMaximum_ReturnsFailure()
        {
            // Arrange
            var value = 11;
            var minimum = 1;
            var maximum = 10;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsInRange(minimum, maximum, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain("Value must be >= 1 and <= 10"));
            Assert.That(result.ValidationException!.Message, Does.Contain("Actual value: 11"));
        }

        [Test]
        public void ValidateIsInRange_WithExclusiveBounds_ValueEqualToBounds_ReturnsFailure()
        {
            // Arrange
            var value = 5;
            var minimum = 5;
            var maximum = 5;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsInRange(minimum, maximum, minimumInclusive: false, maximumInclusive: false, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain("Value must be > 5 and < 5"));
        }

        [Test]
        public void ValidateIsInRange_WithCustomParameterName_IncludesParameterNameInResult()
        {
            // Arrange
            var value = 0;
            var minimum = 1;
            var maximum = 10;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsInRange(minimum, maximum, blackboard: blackboard, parameterName: "testValue");

            // Assert
            Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("testValue"));
        }

        [Test]
        public void ValidateIsInRange_WithBlackboard_IncludesContextInResult()
        {
            // Arrange
            var value = 0;
            var minimum = 1;
            var maximum = 10;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsInRange(minimum, maximum, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        }

        [Test]
        public void ValidateIsInRange_WithFailure_IncludesValidationData()
        {
            // Arrange
            var value = 0;
            var minimum = 1;
            var maximum = 10;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsInRange(minimum, maximum, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Context, Is.Not.Null);
            Assert.That(result.ValidationException!.Context.Context.Count, Is.EqualTo(5));
            Assert.That(result.ValidationException!.Context.Context["value"], Is.EqualTo(0));
            Assert.That(result.ValidationException!.Context.Context["minimum"], Is.EqualTo(1));
            Assert.That(result.ValidationException!.Context.Context["maximum"], Is.EqualTo(10));
            Assert.That(result.ValidationException!.Context.Context["minimumInclusive"], Is.EqualTo(true));
            Assert.That(result.ValidationException!.Context.Context["maximumInclusive"], Is.EqualTo(true));
        }
    }
}
