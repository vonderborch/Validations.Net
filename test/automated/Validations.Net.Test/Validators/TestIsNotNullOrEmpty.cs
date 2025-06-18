using System.Collections.Generic;
using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestIsNotNullOrEmpty
{
    [Test]
    public void CheckIsNotNullOrEmpty_WithNullCollection_ReturnsFalse()
    {
        // Arrange
        ICollection<string>? value = null;

        // Act
        var result = value.CheckIsNotNullOrEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotNullOrEmpty_WithEmptyCollection_ReturnsFalse()
    {
        // Arrange
        var value = new List<string>();

        // Act
        var result = value.CheckIsNotNullOrEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotNullOrEmpty_WithNonEmptyCollection_ReturnsTrue()
    {
        // Arrange
        var value = new List<string> { "test" };

        // Act
        var result = value.CheckIsNotNullOrEmpty();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotNullOrEmpty_WithNullString_ReturnsFalse()
    {
        // Arrange
        string? value = null;

        // Act
        var result = value.CheckIsNotNullOrEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotNullOrEmpty_WithEmptyString_ReturnsFalse()
    {
        // Arrange
        string value = string.Empty;

        // Act
        var result = value.CheckIsNotNullOrEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotNullOrEmpty_WithNonEmptyString_ReturnsTrue()
    {
        // Arrange
        string value = "test";

        // Act
        var result = value.CheckIsNotNullOrEmpty();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateIsNotNullOrEmpty_WithNonEmptyCollection_ReturnsCollection()
    {
        // Arrange
        var value = new List<string> { "test" };
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsNotNullOrEmpty(propertyName);

        // Assert
        Assert.That(result, Is.SameAs(value));
    }

    [Test]
    public void ValidateIsNotNullOrEmpty_WithNullCollection_ThrowsValidationException()
    {
        // Arrange
        ICollection<string>? value = null;
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotNullOrEmpty(propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNotNullOrEmpty"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not be null or empty."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.Null);
        });
    }

    [Test]
    public void ValidateIsNotNullOrEmpty_WithEmptyCollection_ThrowsValidationException()
    {
        // Arrange
        var value = new List<string>();
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotNullOrEmpty(propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNotNullOrEmpty"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not be null or empty."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.SameAs(value));
        });
    }

    [Test]
    public void ValidateIsNotNullOrEmpty_WithNonEmptyString_ReturnsString()
    {
        // Arrange
        string value = "test";
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsNotNullOrEmpty(propertyName);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsNotNullOrEmpty_WithNullString_ThrowsValidationException()
    {
        // Arrange
        string? value = null;
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotNullOrEmpty(propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNotNullOrEmpty"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not be null or empty."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.Null);
        });
    }

    [Test]
    public void ValidateIsNotNullOrEmpty_WithEmptyString_ThrowsValidationException()
    {
        // Arrange
        string value = string.Empty;
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotNullOrEmpty(propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNotNullOrEmpty"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not be null or empty."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
        });
    }

    [Test]
    public void ValidateIsNotNullOrEmpty_WithBlackboard_PreservesBlackboardInException()
    {
        // Arrange
        var value = new List<string>();
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotNullOrEmpty(propertyName, blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 