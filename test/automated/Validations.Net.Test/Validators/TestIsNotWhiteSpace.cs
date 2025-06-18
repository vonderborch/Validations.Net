using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestIsNotWhiteSpace
{
    [Test]
    public void CheckIsNotWhiteSpace_WithNullString_ReturnsFalse()
    {
        // Arrange
        string? value = null;

        // Act
        var result = value.CheckIsNotWhiteSpace();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotWhiteSpace_WithEmptyString_ReturnsFalse()
    {
        // Arrange
        string value = string.Empty;

        // Act
        var result = value.CheckIsNotWhiteSpace();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotWhiteSpace_WithWhitespaceString_ReturnsFalse()
    {
        // Arrange
        string value = "   \t\n\r";

        // Act
        var result = value.CheckIsNotWhiteSpace();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotWhiteSpace_WithNonWhitespaceString_ReturnsTrue()
    {
        // Arrange
        string value = "test";

        // Act
        var result = value.CheckIsNotWhiteSpace();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotWhiteSpace_WithMixedString_ReturnsTrue()
    {
        // Arrange
        string value = "  test  ";

        // Act
        var result = value.CheckIsNotWhiteSpace();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateIsNotWhiteSpace_WithNonWhitespaceString_ReturnsString()
    {
        // Arrange
        string value = "test";
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsNotWhiteSpace(propertyName);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsNotWhiteSpace_WithMixedString_ReturnsString()
    {
        // Arrange
        string value = "  test  ";
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsNotWhiteSpace(propertyName);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsNotWhiteSpace_WithNullString_ThrowsValidationException()
    {
        // Arrange
        string? value = null;
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotWhiteSpace(propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNotWhiteSpace"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not be whitespace."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.Null);
        });
    }

    [Test]
    public void ValidateIsNotWhiteSpace_WithEmptyString_ThrowsValidationException()
    {
        // Arrange
        string value = string.Empty;
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotWhiteSpace(propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNotWhiteSpace"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not be whitespace."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
        });
    }

    [Test]
    public void ValidateIsNotWhiteSpace_WithWhitespaceString_ThrowsValidationException()
    {
        // Arrange
        string value = "   \t\n\r";
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotWhiteSpace(propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNotWhiteSpace"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not be whitespace."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
        });
    }

    [Test]
    public void ValidateIsNotWhiteSpace_WithBlackboard_PreservesBlackboardInException()
    {
        // Arrange
        string value = "   \t\n\r";
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotWhiteSpace(propertyName, blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 