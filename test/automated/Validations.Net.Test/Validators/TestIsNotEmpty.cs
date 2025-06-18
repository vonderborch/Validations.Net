using System.Collections.Generic;
using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestIsNotEmpty
{
    [Test]
    public void CheckIsNotEmpty_WithNullCollection_ReturnsFalse()
    {
        // Arrange
        ICollection<int>? value = null;

        // Act
        var result = value.CheckIsNotEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotEmpty_WithEmptyCollection_ReturnsFalse()
    {
        // Arrange
        var value = new List<int>();

        // Act
        var result = value.CheckIsNotEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotEmpty_WithNonEmptyCollection_ReturnsTrue()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3 };

        // Act
        var result = value.CheckIsNotEmpty();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotEmpty_WithNullString_ReturnsFalse()
    {
        // Arrange
        string? value = null;

        // Act
        var result = value.CheckIsNotEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotEmpty_WithEmptyString_ReturnsFalse()
    {
        // Arrange
        string value = string.Empty;

        // Act
        var result = value.CheckIsNotEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotEmpty_WithNonEmptyString_ReturnsTrue()
    {
        // Arrange
        string value = "test";

        // Act
        var result = value.CheckIsNotEmpty();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateIsNotEmpty_WithNonEmptyCollection_ReturnsCollection()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3 };
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsNotEmpty(propertyName);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsNotEmpty_WithEmptyCollection_ThrowsValidationException()
    {
        // Arrange
        var value = new List<int>();
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotEmpty(propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNotEmpty"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not be empty."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
        });
    }

    [Test]
    public void ValidateIsNotEmpty_WithNonEmptyString_ReturnsString()
    {
        // Arrange
        string value = "test";
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsNotEmpty(propertyName);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsNotEmpty_WithEmptyString_ThrowsValidationException()
    {
        // Arrange
        string value = string.Empty;
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotEmpty(propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNotEmpty"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not be empty."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
        });
    }

    [Test]
    public void ValidateIsNotEmpty_WithBlackboard_PreservesBlackboardInException()
    {
        // Arrange
        var value = string.Empty;
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotEmpty(propertyName, blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 