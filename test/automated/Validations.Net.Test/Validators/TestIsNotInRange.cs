using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestIsNotInRange
{
    [Test]
    public void CheckIsNotInRange_WithValueInRange_ReturnsFalse()
    {
        // Arrange
        var value = 5;
        var min = 1;
        var max = 10;

        // Act
        var result = value.CheckIsNotInRange(min, max);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotInRange_WithValueAtMinExclusive_ReturnsTrue()
    {
        // Arrange
        var value = 1;
        var min = 1;
        var max = 10;

        // Act
        var result = value.CheckIsNotInRange(min, max, minIsInclusive: false);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotInRange_WithValueAtMinInclusive_ReturnsFalse()
    {
        // Arrange
        var value = 1;
        var min = 1;
        var max = 10;

        // Act
        var result = value.CheckIsNotInRange(min, max, minIsInclusive: true);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotInRange_WithValueAtMaxExclusive_ReturnsTrue()
    {
        // Arrange
        var value = 10;
        var min = 1;
        var max = 10;

        // Act
        var result = value.CheckIsNotInRange(min, max, maxIsInclusive: false);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotInRange_WithValueAtMaxInclusive_ReturnsFalse()
    {
        // Arrange
        var value = 10;
        var min = 1;
        var max = 10;

        // Act
        var result = value.CheckIsNotInRange(min, max, maxIsInclusive: true);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotInRange_WithValueBelowMin_ReturnsTrue()
    {
        // Arrange
        var value = 0;
        var min = 1;
        var max = 10;

        // Act
        var result = value.CheckIsNotInRange(min, max);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotInRange_WithValueAboveMax_ReturnsTrue()
    {
        // Arrange
        var value = 11;
        var min = 1;
        var max = 10;

        // Act
        var result = value.CheckIsNotInRange(min, max);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotInRange_WithMinGreaterThanMax_ThrowsValidationException()
    {
        // Arrange
        var value = 5;
        var min = 10;
        var max = 1;

        // Act & Assert
        Assert.Throws<ValidationException>(() => value.CheckIsNotInRange(min, max));
    }

    [Test]
    public void CheckIsNotInRange_WithDifferentNumericTypes_WorksCorrectly()
    {
        // Arrange
        double value = 5.5;
        var min = 1;
        var max = 10;

        // Act
        var result = value.CheckIsNotInRange(min, max);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateNotIsInRange_WithValueOutOfRange_ReturnsValue()
    {
        // Arrange
        var value = 11;
        var min = 1;
        var max = 10;
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateNotIsInRange(min, max, propertyName);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateNotIsInRange_WithValueInRange_ThrowsValidationException()
    {
        // Arrange
        var value = 5;
        var min = 1;
        var max = 10;
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateNotIsInRange(min, max, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNotInRange"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not be in the range [{min}, {max}]."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["min"], Is.EqualTo(min));
            Assert.That(exception.ExceptionContext.Context["max"], Is.EqualTo(max));
        });
    }

    [Test]
    public void ValidateNotIsInRange_WithBlackboard_PreservesBlackboardInException()
    {
        // Arrange
        var value = 5;
        var min = 1;
        var max = 10;
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateNotIsInRange(min, max, propertyName, blackboard: blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 
