using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestIsGreaterThanOrEquals
{
    [Test]
    public void CheckIsGreaterThanOrEquals_WithGreaterValue_ReturnsTrue()
    {
        // Arrange
        var value = 10;
        var compareTo = 5;

        // Act
        var result = value.CheckIsGreaterThanOrEquals(compareTo);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsGreaterThanOrEquals_WithEqualValue_ReturnsTrue()
    {
        // Arrange
        var value = 5;
        var compareTo = 5;

        // Act
        var result = value.CheckIsGreaterThanOrEquals(compareTo);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsGreaterThanOrEquals_WithLesserValue_ReturnsFalse()
    {
        // Arrange
        var value = 1;
        var compareTo = 5;

        // Act
        var result = value.CheckIsGreaterThanOrEquals(compareTo);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsGreaterThanOrEquals_WithDifferentComparableTypes_WorksCorrectly()
    {
        // Arrange
        var value = 10.5;
        var compareTo = 5;

        // Act
        var result = value.CheckIsGreaterThanOrEquals(compareTo);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsGreaterThanOrEquals_WithStringValues_WorksCorrectly()
    {
        // Arrange
        var value = "zebra";
        var compareTo = "apple";

        // Act
        var result = value.CheckIsGreaterThanOrEquals(compareTo);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateIsGreaterThanOrEquals_WithGreaterValue_ReturnsValue()
    {
        // Arrange
        var value = 10;
        var compareTo = 5;
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsGreaterThanOrEquals(compareTo, propertyName);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsGreaterThanOrEquals_WithEqualValue_ReturnsValue()
    {
        // Arrange
        var value = 5;
        var compareTo = 5;
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsGreaterThanOrEquals(compareTo, propertyName);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsGreaterThanOrEquals_WithLesserValue_ThrowsValidationException()
    {
        // Arrange
        var value = 1;
        var compareTo = 5;
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsGreaterThanOrEquals(compareTo, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsGreaterThanOrEquals"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must be greater than or equal to {compareTo}."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["compareTo"], Is.EqualTo(compareTo));
        });
    }

    [Test]
    public void ValidateIsGreaterThanOrEquals_WithBlackboard_PreservesBlackboardInException()
    {
        // Arrange
        var value = 1;
        var compareTo = 5;
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsGreaterThanOrEquals(compareTo, propertyName, blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 