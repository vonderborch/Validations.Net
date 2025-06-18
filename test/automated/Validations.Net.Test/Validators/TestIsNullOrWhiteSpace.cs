using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestIsNullOrWhiteSpace
{
    [Test]
    public void CheckIsNullOrWhiteSpace_WithNullString_ReturnsTrue()
    {
        // Arrange
        string? value = null;

        // Act
        var result = value.CheckIsNullOrWhiteSpace();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNullOrWhiteSpace_WithEmptyString_ReturnsTrue()
    {
        // Arrange  
        string value = string.Empty;

        // Act
        var result = value.CheckIsNullOrWhiteSpace();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNullOrWhiteSpace_WithWhitespaceString_ReturnsTrue()
    {
        // Arrange
        string value = "   \t\n\r";

        // Act
        var result = value.CheckIsNullOrWhiteSpace();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNullOrWhiteSpace_WithNonWhitespaceString_ReturnsFalse()
    {
        // Arrange
        string value = "test";

        // Act
        var result = value.CheckIsNullOrWhiteSpace();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNullOrWhiteSpace_WithMixedString_ReturnsFalse()
    {
        // Arrange
        string value = "  test  ";

        // Act
        var result = value.CheckIsNullOrWhiteSpace();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateIsNullOrWhiteSpace_WithNullString_ReturnsString()
    {
        // Arrange
        string? value = null;
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsNullOrWhiteSpace(propertyName);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void ValidateIsNullOrWhiteSpace_WithEmptyString_ReturnsString()
    {
        // Arrange
        string value = string.Empty;
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsNullOrWhiteSpace(propertyName);

        // Assert
        Assert.That(result, Is.SameAs(value));
    }

    [Test]
    public void ValidateIsNullOrWhiteSpace_WithWhitespaceString_ReturnsString()
    {
        // Arrange
        string value = "   \t\n\r";
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsNullOrWhiteSpace(propertyName);

        // Assert
        Assert.That(result, Is.SameAs(value));
    }

    [Test]
    public void ValidateIsNullOrWhiteSpace_WithNonWhitespaceString_ThrowsValidationException()
    {
        // Arrange
        string value = "test";
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNullOrWhiteSpace(propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNullOrWhiteSpace"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must be null or whitespace."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
        });
    }

    [Test]
    public void ValidateIsNullOrWhiteSpace_WithMixedString_ThrowsValidationException()
    {
        // Arrange
        string value = "  test  ";
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNullOrWhiteSpace(propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNullOrWhiteSpace"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must be null or whitespace."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
        });
    }

    [Test]
    public void ValidateIsNullOrWhiteSpace_WithBlackboard_PreservesBlackboardInException()
    {
        // Arrange
        string value = "test";
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNullOrWhiteSpace(propertyName, blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 