using System.Collections.Generic;
using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestIsEmpty
{
    [Test]
    public void CheckIsEmpty_WithNullCollection_ReturnsFalse()
    {
        // Arrange
        ICollection<int>? value = null;

        // Act
        var result = value.CheckIsEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsEmpty_WithEmptyCollection_ReturnsTrue()
    {
        // Arrange
        var value = new List<int>();

        // Act
        var result = value.CheckIsEmpty();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsEmpty_WithNonEmptyCollection_ReturnsFalse()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3 };

        // Act
        var result = value.CheckIsEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsEmpty_WithNullString_ReturnsFalse()
    {
        // Arrange
        string? value = null;

        // Act
        var result = value.CheckIsEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsEmpty_WithEmptyString_ReturnsTrue()
    {
        // Arrange
        string value = string.Empty;

        // Act
        var result = value.CheckIsEmpty();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsEmpty_WithNonEmptyString_ReturnsFalse()
    {
        // Arrange
        string value = "test";

        // Act
        var result = value.CheckIsEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateIsEmpty_WithEmptyCollection_ReturnsCollection()
    {
        // Arrange
        var value = new List<int>();
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsEmpty(propertyName);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsEmpty_WithNonEmptyCollection_ThrowsValidationException()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3 };
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsEmpty(propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsEmpty"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must be empty."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
        });
    }

    [Test]
    public void ValidateIsEmpty_WithEmptyString_ReturnsString()
    {
        // Arrange
        string value = string.Empty;
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsEmpty(propertyName);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsEmpty_WithNonEmptyString_ThrowsValidationException()
    {
        // Arrange
        string value = "test";
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsEmpty(propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsEmpty"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must be empty."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
        });
    }

    [Test]
    public void ValidateIsEmpty_WithBlackboard_PreservesBlackboardInException()
    {
        // Arrange
        string value = "test";
        var propertyName = "TestProperty";
        
        var blackboard = new ValidationExceptionContext();

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsEmpty(propertyName, blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 
