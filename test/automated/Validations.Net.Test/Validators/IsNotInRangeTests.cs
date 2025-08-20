using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNotInRangeTests
{
    [TestFixture]
    public class CheckIsNotInRangeTests
    {
        [Test]
        public void CheckIsNotInRange_WithValueInRange_ReturnsFalse()
        {
            // Arrange
            var value = 5;
            var minimum = 1;
            var maximum = 10;

            // Act
            var result = value.CheckIsNotInRange(minimum, maximum);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotInRange_WithValueEqualToMinimum_ReturnsFalse()
        {
            // Arrange
            var value = 1;
            var minimum = 1;
            var maximum = 10;

            // Act
            var result = value.CheckIsNotInRange(minimum, maximum);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotInRange_WithValueEqualToMaximum_ReturnsFalse()
        {
            // Arrange
            var value = 10;
            var minimum = 1;
            var maximum = 10;

            // Act
            var result = value.CheckIsNotInRange(minimum, maximum);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotInRange_WithValueBelowMinimum_ReturnsTrue()
        {
            // Arrange
            var value = 0;
            var minimum = 1;
            var maximum = 10;

            // Act
            var result = value.CheckIsNotInRange(minimum, maximum);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotInRange_WithValueAboveMaximum_ReturnsTrue()
        {
            // Arrange
            var value = 11;
            var minimum = 1;
            var maximum = 10;

            // Act
            var result = value.CheckIsNotInRange(minimum, maximum);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotInRange_WithExclusiveMinimum_ValueEqualToMinimum_ReturnsTrue()
        {
            // Arrange
            var value = 1;
            var minimum = 1;
            var maximum = 10;

            // Act
            var result = value.CheckIsNotInRange(minimum, maximum, minimumInclusive: false);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotInRange_WithExclusiveMaximum_ValueEqualToMaximum_ReturnsTrue()
        {
            // Arrange
            var value = 10;
            var minimum = 1;
            var maximum = 10;

            // Act
            var result = value.CheckIsNotInRange(minimum, maximum, maximumInclusive: false);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotInRange_WithExclusiveBounds_ValueInRange_ReturnsFalse()
        {
            // Arrange
            var value = 5;
            var minimum = 1;
            var maximum = 10;

            // Act
            var result = value.CheckIsNotInRange(minimum, maximum, minimumInclusive: false, maximumInclusive: false);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotInRange_WithStringValues_ReturnsExpectedResult()
        {
            // Arrange
            var value = "zebra";
            var minimum = "alpha";
            var maximum = "middle";

            // Act
            var result = value.CheckIsNotInRange(minimum, maximum);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotInRange_WithDateTimeValues_ReturnsExpectedResult()
        {
            // Arrange
            var value = DateTime.Now.AddDays(2);
            var minimum = DateTime.Now.AddDays(-1);
            var maximum = DateTime.Now.AddDays(1);

            // Act
            var result = value.CheckIsNotInRange(minimum, maximum);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotInRange_WithDecimalValues_ReturnsExpectedResult()
        {
            // Arrange
            var value = 15.5m;
            var minimum = 1.0m;
            var maximum = 10.0m;

            // Act
            var result = value.CheckIsNotInRange(minimum, maximum);

            // Assert
            Assert.That(result, Is.True);
        }
    }

    [TestFixture]
    public class EnsureIsNotInRangeTests
    {
        [Test]
        public void EnsureIsNotInRange_WithValueNotInRange_DoesNotThrow()
        {
            // Arrange
            var value = 0;
            var minimum = 1;
            var maximum = 10;

            // Act & Assert
            Assert.DoesNotThrow(() => value.EnsureIsNotInRange(minimum, maximum));
        }

        [Test]
        public void EnsureIsNotInRange_WithValueInRange_ThrowsValidationException()
        {
            // Arrange
            var value = 5;
            var minimum = 1;
            var maximum = 10;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotInRange(minimum, maximum));
            Assert.That(exception.Message, Does.Contain("Value must not be >= 1 and <= 10"));
            Assert.That(exception.Message, Does.Contain("Actual value: 5"));
        }

        [Test]
        public void EnsureIsNotInRange_WithValueEqualToMinimum_ThrowsValidationException()
        {
            // Arrange
            var value = 1;
            var minimum = 1;
            var maximum = 10;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotInRange(minimum, maximum));
            Assert.That(exception.Message, Does.Contain("Value must not be >= 1 and <= 10"));
            Assert.That(exception.Message, Does.Contain("Actual value: 1"));
        }

        [Test]
        public void EnsureIsNotInRange_WithValueEqualToMaximum_ThrowsValidationException()
        {
            // Arrange
            var value = 10;
            var minimum = 1;
            var maximum = 10;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotInRange(minimum, maximum));
            Assert.That(exception.Message, Does.Contain("Value must not be >= 1 and <= 10"));
            Assert.That(exception.Message, Does.Contain("Actual value: 10"));
        }

        [Test]
        public void EnsureIsNotInRange_WithExclusiveMinimum_ValueEqualToMinimum_DoesNotThrow()
        {
            // Arrange
            var value = 1;
            var minimum = 1;
            var maximum = 10;

            // Act & Assert
            Assert.DoesNotThrow(() => value.EnsureIsNotInRange(minimum, maximum, minimumInclusive: false));
        }

        [Test]
        public void EnsureIsNotInRange_WithExclusiveMaximum_ValueEqualToMaximum_DoesNotThrow()
        {
            // Arrange
            var value = 10;
            var minimum = 1;
            var maximum = 10;

            // Act & Assert
            Assert.DoesNotThrow(() => value.EnsureIsNotInRange(minimum, maximum, maximumInclusive: false));
        }

        [Test]
        public void EnsureIsNotInRange_WithExclusiveBounds_ValueEqualToBounds_DoesNotThrow()
        {
            // Arrange
            var value = 5;
            var minimum = 5;
            var maximum = 5;

            // Act & Assert
            Assert.DoesNotThrow(() => value.EnsureIsNotInRange(minimum, maximum, minimumInclusive: false, maximumInclusive: false));
        }

        [Test]
        public void EnsureIsNotInRange_WithCustomParameterName_IncludesParameterNameInException()
        {
            // Arrange
            var value = 5;
            var minimum = 1;
            var maximum = 10;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotInRange(minimum, maximum, parameterName: "testValue"));
            Assert.That(exception.ParameterName, Is.EqualTo("testValue"));
        }
    }

    [TestFixture]
    public class ValidateIsNotInRangeTests
    {
        [Test]
        public void ValidateIsNotInRange_WithValueNotInRange_ReturnsSuccess()
        {
            // Arrange
            var value = 0;
            var minimum = 1;
            var maximum = 10;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsNotInRange(minimum, maximum, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNotInRange_WithValueInRange_ReturnsFailure()
        {
            // Arrange
            var value = 5;
            var minimum = 1;
            var maximum = 10;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsNotInRange(minimum, maximum, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain("Value must not be >= 1 and <= 10"));
            Assert.That(result.ValidationException!.Message, Does.Contain("Actual value: 5"));
        }

        [Test]
        public void ValidateIsNotInRange_WithValueEqualToMinimum_ReturnsFailure()
        {
            // Arrange
            var value = 1;
            var minimum = 1;
            var maximum = 10;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsNotInRange(minimum, maximum, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain("Value must not be >= 1 and <= 10"));
            Assert.That(result.ValidationException!.Message, Does.Contain("Actual value: 1"));
        }

        [Test]
        public void ValidateIsNotInRange_WithValueEqualToMaximum_ReturnsFailure()
        {
            // Arrange
            var value = 10;
            var minimum = 1;
            var maximum = 10;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsNotInRange(minimum, maximum, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain("Value must not be >= 1 and <= 10"));
            Assert.That(result.ValidationException!.Message, Does.Contain("Actual value: 10"));
        }

        [Test]
        public void ValidateIsNotInRange_WithExclusiveBounds_ValueEqualToBounds_ReturnsSuccess()
        {
            // Arrange
            var value = 5;
            var minimum = 5;
            var maximum = 5;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsNotInRange(minimum, maximum, minimumInclusive: false, maximumInclusive: false, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNotInRange_WithCustomParameterName_IncludesParameterNameInResult()
        {
            // Arrange
            var value = 5;
            var minimum = 1;
            var maximum = 10;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsNotInRange(minimum, maximum, blackboard: blackboard, parameterName: "testValue");

            // Assert
            Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("testValue"));
        }

        [Test]
        public void ValidateIsNotInRange_WithBlackboard_IncludesContextInResult()
        {
            // Arrange
            var value = 5;
            var minimum = 1;
            var maximum = 10;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsNotInRange(minimum, maximum, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        }

        [Test]
        public void ValidateIsNotInRange_WithFailure_IncludesValidationData()
        {
            // Arrange
            var value = 5;
            var minimum = 1;
            var maximum = 10;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsNotInRange(minimum, maximum, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Context, Is.Not.Null);
            Assert.That(result.ValidationException!.Context.Context.Count, Is.EqualTo(5));
            Assert.That(result.ValidationException!.Context.Context["value"], Is.EqualTo(5));
            Assert.That(result.ValidationException!.Context.Context["minimum"], Is.EqualTo(1));
            Assert.That(result.ValidationException!.Context.Context["maximum"], Is.EqualTo(10));
            Assert.That(result.ValidationException!.Context.Context["minimumInclusive"], Is.EqualTo(true));
            Assert.That(result.ValidationException!.Context.Context["maximumInclusive"], Is.EqualTo(true));
        }
    }
}
