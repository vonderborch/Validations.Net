using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestIsNotEquals
{
    [Test]
    public void CheckIsNotEquals_WithEqualValues_ReturnsFalse()
    {
        // Arrange
        var value = 5;
        var compareTo = 5;

        // Act
        var result = value.CheckIsNotEquals(compareTo);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotEquals_WithDifferentValues_ReturnsTrue()
    {
        // Arrange
        var value = 5;
        var compareTo = 10;

        // Act
        var result = value.CheckIsNotEquals(compareTo);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotEquals_WithBothNull_ReturnsFalse()
    {
        // Arrange
        string? value = null;
        string? compareTo = null;

        // Act
        var result = value.CheckIsNotEquals(compareTo);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotEquals_WithOneNull_ReturnsTrue()
    {
        // Arrange
        string? value = "test";
        string? compareTo = null;

        // Act
        var result = value.CheckIsNotEquals(compareTo);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotEquals_WithStringValues_WorksCorrectly()
    {
        // Arrange
        var value = "test";
        var compareTo = "different";

        // Act
        var result = value.CheckIsNotEquals(compareTo);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotEquals_WithCustomComparer_WorksCorrectly()
    {
        // Arrange
        var value = "TEST";
        var compareTo = "test";
        var comparer = StringComparer.OrdinalIgnoreCase;

        // Act
        var result = value.CheckIsNotEquals(compareTo, comparer);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateIsNotEquals_WithDifferentValues_ReturnsValue()
    {
        // Arrange
        var value = 5;
        var compareTo = 10;
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsNotEquals(compareTo, propertyName);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsNotEquals_WithEqualValues_ThrowsValidationException()
    {
        // Arrange
        var value = 5;
        var compareTo = 5;
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotEquals(compareTo, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNotEquals"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not be equal to {compareTo}."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["compareTo"], Is.EqualTo(compareTo));
        });
    }

    [Test]
    public void ValidateIsNotEquals_WithBothNull_ThrowsValidationException()
    {
        // Arrange
        string? value = null;
        string? compareTo = null;
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotEquals(compareTo, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNotEquals"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not be equal to {compareTo}."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.Null);
            Assert.That(exception.ExceptionContext.Context["compareTo"], Is.Null);
        });
    }

    [Test]
    public void ValidateIsNotEquals_WithCustomComparer_WorksCorrectly()
    {
        // Arrange
        var value = "TEST";
        var compareTo = "different";
        var comparer = StringComparer.OrdinalIgnoreCase;
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsNotEquals(compareTo, comparer, propertyName);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsNotEquals_WithCustomComparerAndEqualValues_ThrowsValidationException()
    {
        // Arrange
        var value = "TEST";
        var compareTo = "test";
        var comparer = StringComparer.OrdinalIgnoreCase;
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotEquals(compareTo, comparer, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNotEquals"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not be equal to {compareTo}."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["compareTo"], Is.EqualTo(compareTo));
        });
    }

    [Test]
    public void ValidateIsNotEquals_WithBlackboard_PreservesBlackboardInException()
    {
        // Arrange
        var value = 5;
        var compareTo = 5;
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotEquals(compareTo, propertyName, blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 
