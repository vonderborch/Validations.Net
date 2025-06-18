using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestIsLessThanOrEquals
{
    [Test]
    public void CheckIsLessThanOrEquals_WithLesserValue_ReturnsTrue()
    {
        // Arrange
        var value = 1;
        var compareTo = 5;

        // Act
        var result = value.CheckIsLessThanOrEquals(compareTo);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsLessThanOrEquals_WithEqualValue_ReturnsTrue()
    {
        // Arrange
        var value = 5;
        var compareTo = 5;

        // Act
        var result = value.CheckIsLessThanOrEquals(compareTo);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsLessThanOrEquals_WithGreaterValue_ReturnsFalse()
    {
        // Arrange
        var value = 10;
        var compareTo = 5;

        // Act
        var result = value.CheckIsLessThanOrEquals(compareTo);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsLessThanOrEquals_WithDifferentComparableTypes_WorksCorrectly()
    {
        // Arrange
        var value = 1.5;
        var compareTo = 5;

        // Act
        var result = value.CheckIsLessThanOrEquals(compareTo);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsLessThanOrEquals_WithStringValues_WorksCorrectly()
    {
        // Arrange
        var value = "apple";
        var compareTo = "zebra";

        // Act
        var result = value.CheckIsLessThanOrEquals(compareTo);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateIsLessThanOrEquals_WithLesserValue_ReturnsValue()
    {
        // Arrange
        var value = 1;
        var compareTo = 5;
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsLessThanOrEquals(compareTo, propertyName);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsLessThanOrEquals_WithEqualValue_ReturnsValue()
    {
        // Arrange
        var value = 5;
        var compareTo = 5;
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsLessThanOrEquals(compareTo, propertyName);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsLessThanOrEquals_WithGreaterValue_ThrowsValidationException()
    {
        // Arrange
        var value = 10;
        var compareTo = 5;
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsLessThanOrEquals(compareTo, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("ValidateIsLessThanOrEqualsAttribute"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must be less than or equal to {compareTo}."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["compareTo"], Is.EqualTo(compareTo));
        });
    }

    [Test]
    public void ValidateIsLessThanOrEquals_WithBlackboard_PreservesBlackboardInException()
    {
        // Arrange
        var value = 10;
        var compareTo = 5;
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsLessThanOrEquals(compareTo, propertyName, blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 