using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestIsNull
{
    [Test]
    public void CheckIsNull_WithNullValue_ReturnsTrue()
    {
        // Arrange
        string? value = null;

        // Act
        var result = value.CheckIsNull();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNull_WithNonNullValue_ReturnsFalse()
    {
        // Arrange
        string value = "test";

        // Act
        var result = value.CheckIsNull();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateIsNull_WithNullValue_ReturnsValue()
    {
        // Arrange
        string? value = null;
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsNull(propertyName);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void ValidateIsNull_WithNonNullValue_ThrowsValidationException()
    {
        // Arrange
        string value = "test";
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNull(propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNull"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must be null."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
        });
    }

    [Test]
    public void ValidateIsNull_WithBlackboard_PreservesBlackboardInException()
    {
        // Arrange
        string value = "test";
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNull(propertyName, blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 