using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestIsGreaterThan
{
    [Test]
    public void CheckIsGreaterThan_WithGreaterValue_ReturnsTrue()
    {
        // Arrange
        var value = 10;
        var compareTo = 5;

        // Act
        var result = value.CheckIsGreaterThan(compareTo);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsGreaterThan_WithEqualValue_ReturnsFalse()
    {
        // Arrange
        var value = 5;
        var compareTo = 5;

        // Act
        var result = value.CheckIsGreaterThan(compareTo);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsGreaterThan_WithLesserValue_ReturnsFalse()
    {
        // Arrange
        var value = 1;
        var compareTo = 5;

        // Act
        var result = value.CheckIsGreaterThan(compareTo);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsGreaterThan_WithDifferentComparableTypes_WorksCorrectly()
    {
        // Arrange
        var value = 10.5;
        var compareTo = 5;

        // Act
        var result = value.CheckIsGreaterThan(compareTo);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsGreaterThan_WithStringValues_WorksCorrectly()
    {
        // Arrange
        var value = "zebra";
        var compareTo = "apple";

        // Act
        var result = value.CheckIsGreaterThan(compareTo);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateIsGreaterThan_WithGreaterValue_ReturnsValue()
    {
        // Arrange
        var value = 10;
        var compareTo = 5;
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsGreaterThan(compareTo, propertyName);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsGreaterThan_WithEqualValue_ThrowsValidationException()
    {
        // Arrange
        var value = 5;
        var compareTo = 5;
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsGreaterThan(compareTo, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsGreaterThan"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must be greater than {compareTo}."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["compareTo"], Is.EqualTo(compareTo));
        });
    }

    [Test]
    public void ValidateIsGreaterThan_WithLesserValue_ThrowsValidationException()
    {
        // Arrange
        var value = 1;
        var compareTo = 5;
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsGreaterThan(compareTo, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsGreaterThan"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must be greater than {compareTo}."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["compareTo"], Is.EqualTo(compareTo));
        });
    }

    [Test]
    public void ValidateIsGreaterThan_WithBlackboard_PreservesBlackboardInException()
    {
        // Arrange
        var value = 1;
        var compareTo = 5;
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsGreaterThan(compareTo, propertyName, blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 
