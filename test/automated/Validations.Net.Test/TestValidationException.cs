using System.Collections.Generic;
using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test;

[TestFixture]
public class TestValidationException
{
    [Test]
    public void Constructor_WithValidParameters_SetsPropertiesCorrectly()
    {
        List<int> lst = new();
        List<int> lst2 = lst.EnsureIsLength(1, "");
        
        // Arrange
        var validator = "TestValidator";
        var parameterName = "TestParameter";
        var message = "Test message";
        var blackboard = new ValidationExceptionContext();
        var context = new Dictionary<string, object?> { { "key", "value" } };

        // Act
        var exception = new ValidationException(validator, parameterName, message, blackboard, context);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(exception.Validator, Is.EqualTo(validator));
            Assert.That(exception.ParameterName, Is.EqualTo(parameterName));
            Assert.That(exception.Message, Is.EqualTo(message));
            Assert.That(exception.Blackboard, Is.SameAs(blackboard));
            Assert.That(exception.ExceptionContext.Context["key"], Is.EqualTo("value"));
        });
    }

    [Test]
    public void Constructor_WithNullContext_CreatesEmptyContext()
    {
        // Arrange
        var validator = "TestValidator";
        var parameterName = "TestParameter";
        var message = "Test message";
        var blackboard = new ValidationExceptionContext();

        // Act
        var exception = new ValidationException(validator, parameterName, message, blackboard);

        // Assert
        Assert.That(exception.ExceptionContext.Context, Is.Empty);
    }

    [Test]
    public void Constructor_WithNullBlackboard_SetsBlackboardToNull()
    {
        // Arrange
        var validator = "TestValidator";
        var parameterName = "TestParameter";
        var message = "Test message";
        var context = new Dictionary<string, object?> { { "key", "value" } };

        // Act
        var exception = new ValidationException(validator, parameterName, message, null, context);

        // Assert
        Assert.That(exception.Blackboard, Is.Null);
    }

    [Test]
    public void CreateFromTypeMisMatch_WithValidParameters_CreatesExceptionWithCorrectProperties()
    {
        // Arrange
        var validator = "TestValidator";
        var parameterName = "TestParameter";
        var value = "test";
        var blackboard = new ValidationExceptionContext();

        // Act
        var exception = ValidationException.CreateFromTypeMisMatch<int>(validator, parameterName, value, blackboard);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(exception.Validator, Is.EqualTo($"{validator}->TypeMismatch"));
            Assert.That(exception.ParameterName, Is.EqualTo(parameterName));
            Assert.That(exception.Message, Is.EqualTo($"{parameterName} must be of type Int32."));
            Assert.That(exception.Blackboard, Is.SameAs(blackboard));
            Assert.That(exception.ExceptionContext.Context["expectedType"], Is.EqualTo("Int32"));
            Assert.That(exception.ExceptionContext.Context["actualType"], Is.EqualTo("String"));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
        });
    }

    [Test]
    public void CreateFromTypeMisMatch_WithNullValue_SetsActualTypeToNull()
    {
        // Arrange
        var validator = "TestValidator";
        var parameterName = "TestParameter";
        object? value = null;
        var blackboard = new ValidationExceptionContext();

        // Act
        var exception = ValidationException.CreateFromTypeMisMatch<int>(validator, parameterName, value, blackboard);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(exception.ExceptionContext.Context["expectedType"], Is.EqualTo("Int32"));
            Assert.That(exception.ExceptionContext.Context["actualType"], Is.EqualTo("null"));
            Assert.That(exception.ExceptionContext.Context["value"], Is.Null);
        });
    }

    [Test]
    public void CreateFromTypeMisMatch_WithNullBlackboard_SetsBlackboardToNull()
    {
        // Arrange
        var validator = "TestValidator";
        var parameterName = "TestParameter";
        var value = "test";

        // Act
        var exception = ValidationException.CreateFromTypeMisMatch<int>(validator, parameterName, value, null);

        // Assert
        Assert.That(exception.Blackboard, Is.Null);
    }
}
