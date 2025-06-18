using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestIsLessThan
{
    [Test]
    public void CheckIsLessThan_WithLesserValue_ReturnsTrue()
    {
        // Arrange
        var value = 1;
        var compareTo = 5;

        // Act
        var result = value.CheckIsLessThan(compareTo);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsLessThan_WithEqualValue_ReturnsFalse()
    {
        // Arrange
        var value = 5;
        var compareTo = 5;

        // Act
        var result = value.CheckIsLessThan(compareTo);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsLessThan_WithGreaterValue_ReturnsFalse()
    {
        // Arrange
        var value = 10;
        var compareTo = 5;

        // Act
        var result = value.CheckIsLessThan(compareTo);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsLessThan_WithDifferentComparableTypes_WorksCorrectly()
    {
        // Arrange
        var value = 1.5;
        var compareTo = 5;

        // Act
        var result = value.CheckIsLessThan(compareTo);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsLessThan_WithStringValues_WorksCorrectly()
    {
        // Arrange
        var value = "apple";
        var compareTo = "zebra";

        // Act
        var result = value.CheckIsLessThan(compareTo);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateIsLessThan_WithLesserValue_ReturnsValue()
    {
        // Arrange
        var value = 1;
        var compareTo = 5;
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsLessThan(compareTo, propertyName);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsLessThan_WithEqualValue_ThrowsValidationException()
    {
        // Arrange
        var value = 5;
        var compareTo = 5;
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsLessThan(compareTo, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsLessThan"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must be less than {compareTo}."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["compareTo"], Is.EqualTo(compareTo));
        });
    }

    [Test]
    public void ValidateIsLessThan_WithGreaterValue_ThrowsValidationException()
    {
        // Arrange
        var value = 10;
        var compareTo = 5;
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsLessThan(compareTo, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsLessThan"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must be less than {compareTo}."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["compareTo"], Is.EqualTo(compareTo));
        });
    }

    [Test]
    public void ValidateIsLessThan_WithBlackboard_PreservesBlackboardInException()
    {
        // Arrange
        var value = 10;
        var compareTo = 5;
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsLessThan(compareTo, propertyName, blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 