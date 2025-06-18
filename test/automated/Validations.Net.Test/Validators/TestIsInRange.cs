using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestIsInRange
{
    [Test]
    public void CheckIsInRange_WithValueInRange_ReturnsTrue()
    {
        // Arrange
        var value = 5;
        var min = 1;
        var max = 10;

        // Act
        var result = value.CheckIsInRange(min, max);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsInRange_WithValueAtMinExclusive_ReturnsFalse()
    {
        // Arrange
        var value = 1;
        var min = 1;
        var max = 10;

        // Act
        var result = value.CheckIsInRange(min, max, minIsInclusive: false);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsInRange_WithValueAtMinInclusive_ReturnsTrue()
    {
        // Arrange
        var value = 1;
        var min = 1;
        var max = 10;

        // Act
        var result = value.CheckIsInRange(min, max, minIsInclusive: true);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsInRange_WithValueAtMaxExclusive_ReturnsFalse()
    {
        // Arrange
        var value = 10;
        var min = 1;
        var max = 10;

        // Act
        var result = value.CheckIsInRange(min, max, maxIsInclusive: false);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsInRange_WithValueAtMaxInclusive_ReturnsTrue()
    {
        // Arrange
        var value = 10;
        var min = 1;
        var max = 10;

        // Act
        var result = value.CheckIsInRange(min, max, maxIsInclusive: true);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsInRange_WithValueBelowMin_ReturnsFalse()
    {
        // Arrange
        var value = 0;
        var min = 1;
        var max = 10;

        // Act
        var result = value.CheckIsInRange(min, max);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsInRange_WithValueAboveMax_ReturnsFalse()
    {
        // Arrange
        var value = 11;
        var min = 1;
        var max = 10;

        // Act
        var result = value.CheckIsInRange(min, max);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsInRange_WithMinGreaterThanMax_ThrowsValidationException()
    {
        // Arrange
        var value = 5;
        var min = 10;
        var max = 1;

        // Act & Assert
        Assert.Throws<ValidationException>(() => value.CheckIsInRange(min, max));
    }

    [Test]
    public void CheckIsInRange_WithDifferentNumericTypes_WorksCorrectly()
    {
        // Arrange
        double value = 5.5;
        var min = 1;
        var max = 10;

        // Act
        var result = value.CheckIsInRange(min, max);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateIsInRange_WithValueInRange_ReturnsValue()
    {
        // Arrange
        var value = 5;
        var min = 1;
        var max = 10;
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsInRange(min, max, propertyName);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsInRange_WithValueOutOfRange_ThrowsValidationException()
    {
        // Arrange
        var value = 11;
        var min = 1;
        var max = 10;
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsInRange(min, max, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsInRange"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must be in the range [{min}, {max}]."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["min"], Is.EqualTo(min));
            Assert.That(exception.ExceptionContext.Context["max"], Is.EqualTo(max));
        });
    }

    [Test]
    public void ValidateIsInRange_WithBlackboard_PreservesBlackboardInException()
    {
        // Arrange
        var value = 11;
        var min = 1;
        var max = 10;
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsInRange(min, max, propertyName, blackboard: blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 
