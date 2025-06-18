using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestIsNotNullOrWhiteSpace
{
    [Test]
    public void CheckIsNotNullOrWhiteSpace_WithNullString_ReturnsFalse()
    {
        // Arrange
        string? value = null;

        // Act
        var result = value.CheckIsNotNullOrWhiteSpace();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotNullOrWhiteSpace_WithEmptyString_ReturnsFalse()
    {
        // Arrange
        string value = string.Empty;

        // Act
        var result = value.CheckIsNotNullOrWhiteSpace();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotNullOrWhiteSpace_WithWhitespaceString_ReturnsFalse()
    {
        // Arrange
        string value = "   \t\n\r";

        // Act
        var result = value.CheckIsNotNullOrWhiteSpace();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotNullOrWhiteSpace_WithNonWhitespaceString_ReturnsTrue()
    {
        // Arrange
        string value = "test";

        // Act
        var result = value.CheckIsNotNullOrWhiteSpace();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotNullOrWhiteSpace_WithMixedString_ReturnsTrue()
    {
        // Arrange
        string value = "  test  ";

        // Act
        var result = value.CheckIsNotNullOrWhiteSpace();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateIsNotNullOrWhiteSpace_WithNonWhitespaceString_ReturnsString()
    {
        // Arrange
        string value = "test";
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsNotNullOrWhiteSpace(propertyName);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsNotNullOrWhiteSpace_WithMixedString_ReturnsString()
    {
        // Arrange
        string value = "  test  ";
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsNotNullOrWhiteSpace(propertyName);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsNotNullOrWhiteSpace_WithNullString_ThrowsValidationException()
    {
        // Arrange
        string? value = null;
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotNullOrWhiteSpace(propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNotNullOrWhiteSpace"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not be null or whitespace."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.Null);
        });
    }

    [Test]
    public void ValidateIsNotNullOrWhiteSpace_WithEmptyString_ThrowsValidationException()
    {
        // Arrange
        string value = string.Empty;
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotNullOrWhiteSpace(propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNotNullOrWhiteSpace"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not be null or whitespace."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
        });
    }

    [Test]
    public void ValidateIsNotNullOrWhiteSpace_WithWhitespaceString_ThrowsValidationException()
    {
        // Arrange
        string value = "   \t\n\r";
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotNullOrWhiteSpace(propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNotNullOrWhiteSpace"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not be null or whitespace."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
        });
    }

    [Test]
    public void ValidateIsNotNullOrWhiteSpace_WithBlackboard_PreservesBlackboardInException()
    {
        // Arrange
        string value = "   \t\n\r";
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotNullOrWhiteSpace(propertyName, blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 