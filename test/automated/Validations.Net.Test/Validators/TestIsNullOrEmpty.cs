using System.Collections.Generic;
using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestIsNullOrEmpty
{
    [Test]
    public void CheckIsNullOrEmpty_WithNullCollection_ReturnsTrue()
    {
        // Arrange
        ICollection<string>? value = null;

        // Act
        var result = value.CheckIsNullOrEmpty();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNullOrEmpty_WithEmptyCollection_ReturnsTrue()
    {
        // Arrange
        var value = new List<string>();

        // Act
        var result = value.CheckIsNullOrEmpty();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNullOrEmpty_WithNonEmptyCollection_ReturnsFalse()
    {
        // Arrange
        var value = new List<string> { "test" };

        // Act
        var result = value.CheckIsNullOrEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNullOrEmpty_WithNullString_ReturnsTrue()
    {
        // Arrange
        string? value = null;

        // Act
        var result = value.CheckIsNullOrEmpty();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNullOrEmpty_WithEmptyString_ReturnsTrue()
    {
        // Arrange
        string value = string.Empty;

        // Act
        var result = value.CheckIsNullOrEmpty();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNullOrEmpty_WithNonEmptyString_ReturnsFalse()
    {
        // Arrange
        string value = "test";

        // Act
        var result = value.CheckIsNullOrEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateIsNullOrEmpty_WithNullCollection_ReturnsCollection()
    {
        // Arrange
        ICollection<string>? value = null;
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsNullOrEmpty(propertyName);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void ValidateIsNullOrEmpty_WithEmptyCollection_ReturnsCollection()
    {
        // Arrange
        var value = new List<string>();
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsNullOrEmpty(propertyName);

        // Assert
        Assert.That(result, Is.SameAs(value));
    }

    [Test]
    public void ValidateIsNullOrEmpty_WithNonEmptyCollection_ThrowsValidationException()
    {
        // Arrange
        var value = new List<string> { "test" };
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNullOrEmpty(propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNullOrEmpty"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must be null or empty."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.SameAs(value));
        });
    }

    [Test]
    public void ValidateIsNullOrEmpty_WithNullString_ReturnsString()
    {
        // Arrange
        string? value = null;
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsNullOrEmpty(propertyName);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void ValidateIsNullOrEmpty_WithEmptyString_ReturnsString()
    {
        // Arrange
        string value = string.Empty;
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsNullOrEmpty(propertyName);

        // Assert
        Assert.That(result, Is.SameAs(value));
    }

    [Test]
    public void ValidateIsNullOrEmpty_WithNonEmptyString_ThrowsValidationException()
    {
        // Arrange
        string value = "test";
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNullOrEmpty(propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNullOrEmpty"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must be null or empty."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
        });
    }

    [Test]
    public void ValidateIsNullOrEmpty_WithBlackboard_PreservesBlackboardInException()
    {
        // Arrange
        var value = new List<string> { "test" };
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNullOrEmpty(propertyName, blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 