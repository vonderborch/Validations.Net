using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestIsWhiteSpace
{
    [Test]
    public void CheckIsWhiteSpace_WithNullString_ReturnsFalse()
    {
        // Arrange
        string? value = null;

        // Act
        var result = value.CheckIsWhiteSpace();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsWhiteSpace_WithEmptyString_ReturnsFalse()
    {
        // Arrange
        string value = string.Empty;

        // Act
        var result = value.CheckIsWhiteSpace();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsWhiteSpace_WithWhitespaceString_ReturnsTrue()
    {
        // Arrange
        string value = "   \t\n\r";

        // Act
        var result = value.CheckIsWhiteSpace();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsWhiteSpace_WithNonWhitespaceString_ReturnsFalse()
    {
        // Arrange
        string value = "test";

        // Act
        var result = value.CheckIsWhiteSpace();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsWhiteSpace_WithMixedString_ReturnsFalse()
    {
        // Arrange
        string value = "  test  ";

        // Act
        var result = value.CheckIsWhiteSpace();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateIsWhiteSpace_WithWhitespaceString_ReturnsString()
    {
        // Arrange
        string value = "   \t\n\r";
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsWhiteSpace(propertyName);

        // Assert
        Assert.That(result, Is.SameAs(value));
    }

    [Test]
    public void ValidateIsWhiteSpace_WithNullString_ThrowsValidationException()
    {
        // Arrange
        string? value = null;
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsWhiteSpace(propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("ValidateIsWhiteSpace"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must be whitespace."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.Null);
        });
    }

    [Test]
    public void ValidateIsWhiteSpace_WithEmptyString_ThrowsValidationException()
    {
        // Arrange
        string value = string.Empty;
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsWhiteSpace(propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("ValidateIsWhiteSpace"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must be whitespace."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
        });
    }

    [Test]
    public void ValidateIsWhiteSpace_WithNonWhitespaceString_ThrowsValidationException()
    {
        // Arrange
        string value = "test";
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsWhiteSpace(propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("ValidateIsWhiteSpace"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must be whitespace."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
        });
    }

    [Test]
    public void ValidateIsWhiteSpace_WithMixedString_ThrowsValidationException()
    {
        // Arrange
        string value = "  test  ";
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsWhiteSpace(propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("ValidateIsWhiteSpace"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must be whitespace."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
        });
    }

    [Test]
    public void ValidateIsWhiteSpace_WithBlackboard_PreservesBlackboardInException()
    {
        // Arrange
        string value = "test";
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsWhiteSpace(propertyName, blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 