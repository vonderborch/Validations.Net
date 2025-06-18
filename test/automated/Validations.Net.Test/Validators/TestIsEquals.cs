using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestIsEquals
{
    [Test]
    public void CheckIsEquals_WithEqualValues_ReturnsTrue()
    {
        // Arrange
        var value = 5;
        var compareTo = 5;

        // Act
        var result = value.CheckIsEquals(compareTo);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsEquals_WithDifferentValues_ReturnsFalse()
    {
        // Arrange
        var value = 5;
        var compareTo = 10;

        // Act
        var result = value.CheckIsEquals(compareTo);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsEquals_WithBothNull_ReturnsTrue()
    {
        // Arrange
        string? value = null;
        string? compareTo = null;

        // Act
        var result = value.CheckIsEquals(compareTo);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsEquals_WithOneNull_ReturnsFalse()
    {
        // Arrange
        string? value = "test";
        string? compareTo = null;

        // Act
        var result = value.CheckIsEquals(compareTo);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsEquals_WithStringValues_WorksCorrectly()
    {
        // Arrange
        var value = "test";
        var compareTo = "test";

        // Act
        var result = value.CheckIsEquals(compareTo);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsEquals_WithCustomComparer_WorksCorrectly()
    {
        // Arrange
        var value = "TEST";
        var compareTo = "test";
        var comparer = StringComparer.OrdinalIgnoreCase;

        // Act
        var result = value.CheckIsEquals(compareTo, comparer);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateIsEquals_WithEqualValues_ReturnsValue()
    {
        // Arrange
        var value = 5;
        var compareTo = 5;
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsEquals(compareTo, propertyName);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsEquals_WithDifferentValues_ThrowsValidationException()
    {
        // Arrange
        var value = 5;
        var compareTo = 10;
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsEquals(compareTo, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsEquals"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must be equal to {compareTo}."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["compareTo"], Is.EqualTo(compareTo));
        });
    }

    [Test]
    public void ValidateIsEquals_WithOneNull_ThrowsValidationException()
    {
        // Arrange
        string? value = "test";
        string? compareTo = null;
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsEquals(compareTo, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsEquals"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must be equal to {compareTo}."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["compareTo"], Is.Null);
        });
    }

    [Test]
    public void ValidateIsEquals_WithCustomComparer_WorksCorrectly()
    {
        // Arrange
        var value = "TEST";
        var compareTo = "test";
        var comparer = StringComparer.OrdinalIgnoreCase;
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsEquals(compareTo, comparer, propertyName);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsEquals_WithCustomComparerAndDifferentValues_ThrowsValidationException()
    {
        // Arrange
        var value = "TEST";
        var compareTo = "different";
        var comparer = StringComparer.OrdinalIgnoreCase;
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsEquals(compareTo, comparer, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsEquals"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must be equal to {compareTo}."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["compareTo"], Is.EqualTo(compareTo));
        });
    }

    [Test]
    public void ValidateIsEquals_WithBlackboard_PreservesBlackboardInException()
    {
        // Arrange
        var value = 5;
        var compareTo = 10;
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsEquals(compareTo, propertyName, blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 