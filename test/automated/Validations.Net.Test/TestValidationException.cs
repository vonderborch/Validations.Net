using System.Collections.Generic;
using NUnit.Framework;

namespace Validations.Net.Test;

[TestFixture]
public class TestValidationException
{
    [Test]
    public void Constructor_SetsAllProperties()
    {
        // Arrange
        string paramName = "testParam";
        string message = "Test validation message";
        string validation = "IsValid";
        var blackboard = new object();
        var exceptionContext = new Dictionary<string, object> { { "key", "value" } };

        // Act
        var exception = new ValidationException(paramName, message, validation, blackboard, exceptionContext);

        // Assert
        Assert.That(exception.ParameterName, Is.EqualTo(paramName), "ParameterName should be set correctly");
        Assert.That(exception.Message, Is.EqualTo(message), "Message should be set correctly");
        Assert.That(exception.Validation, Is.EqualTo(validation), "Validation should be set correctly");
        Assert.That(exception.Blackboard, Is.SameAs(blackboard), "Blackboard should be set correctly");
        Assert.That(exception.ExceptionContext, Is.SameAs(exceptionContext),
            "ExceptionContext should be set correctly");
    }

    [Test]
    public void Constructor_WithNullBlackboard_SetsNullBlackboard()
    {
        // Arrange
        string paramName = "testParam";
        string message = "Test validation message";
        string validation = "IsValid";
        object? blackboard = null;
        var exceptionContext = new Dictionary<string, object>();

        // Act
        var exception = new ValidationException(paramName, message, validation, blackboard, exceptionContext);

        // Assert
        Assert.That(exception.Blackboard, Is.Null, "Blackboard should be null");
    }

    [Test]
    public void Constructor_WithNullExceptionContext_SetsNullExceptionContext()
    {
        // Arrange
        string paramName = "testParam";
        string message = "Test validation message";
        string validation = "IsValid";
        var blackboard = new object();
        object? exceptionContext = null;

        // Act
        var exception = new ValidationException(paramName, message, validation, blackboard, exceptionContext);

        // Assert
        Assert.That(exception.ExceptionContext, Is.Null, "ExceptionContext should be null");
    }

    [Test]
    public void Constructor_WithoutExceptionContext_SetsDefaultExceptionContext()
    {
        // Arrange
        string paramName = "testParam";
        string message = "Test validation message";
        string validation = "IsValid";
        var blackboard = new object();

        // Act
        var exception = new ValidationException(paramName, message, validation, blackboard);

        // Assert
        Assert.That(exception.ExceptionContext, Is.Null, "ExceptionContext should be null when not provided");
    }

    [TestCase("param1", "Message 1", "IsGreaterThan")]
    [TestCase("param2", "Message 2", "IsLessThan")]
    [TestCase("param3", "Message 3", "IsInRange")]
    public void Constructor_WithDifferentValidations_SetsCorrectValidation(string paramName, string message, string validation)
    {
        // Act
        var exception = new ValidationException(paramName, message, validation, null);

        // Assert
        Assert.That(exception.Validation, Is.EqualTo(validation), "Validation should match the provided value");
    }

    [Test]
    public void InheritanceFromException_IsCorrect()
    {
        // Arrange & Act
        var exception = new ValidationException("param", "message", "validation", null);

        // Assert
        Assert.That(exception, Is.InstanceOf<Exception>(), "ValidationException should inherit from Exception");
    }

    [Test]
    public void ExceptionMessage_IsSetCorrectly()
    {
        // Arrange
        string message = "Custom validation error message";

        // Act
        var exception = new ValidationException("param", message, "validation", null);

        // Assert
        Assert.That(exception.Message, Is.EqualTo(message), "Exception message should match the provided message");
    }
}
