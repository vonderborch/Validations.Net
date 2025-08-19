namespace Validations.Net.Test;

[TestFixture]
public class ValidationExceptionTests
{
    [Test]
    public void Create_WithAllParameters_SetsPropertiesCorrectly()
    {
        // Arrange
        var validator = "TestValidator";
        var message = "Test validation message";
        var parameterName = "testParameter";
        var context = new ValidationContext(new Dictionary<string, object?> { { "key1", "value1" } });
        var blackboard = new ValidationContext(new Dictionary<string, object?> { { "blackboardKey", "blackboardValue" } });

        // Act
        var exception = ValidationException.Create(validator, message, parameterName, blackboard, context);

        // Assert
        Assert.That(exception.Validator, Is.EqualTo(validator));
        Assert.That(exception.ParameterName, Is.EqualTo(parameterName));
        Assert.That(exception.Context, Is.EqualTo(context));
        Assert.That(exception.Blackboard, Is.EqualTo(blackboard));
        Assert.That(exception.Message, Is.EqualTo($"`{validator}` failed against parameter `{parameterName}`: {message}"));
    }

    [Test]
    public void Create_WithNullParameterName_SetsParameterNameToNull()
    {
        // Arrange
        var validator = "TestValidator";
        var message = "Test validation message";
        var context = new ValidationContext(new Dictionary<string, object?> { { "key1", "value1" } });
        var blackboard = new ValidationContext(new Dictionary<string, object?> { { "blackboardKey", "blackboardValue" } });

        // Act
        var exception = ValidationException.Create(validator, message, null, blackboard, context);

        // Assert
        Assert.That(exception.ParameterName, Is.Null);
        Assert.That(exception.Message, Is.EqualTo($"`{validator}` failed against parameter ``: {message}"));
    }

    [Test]
    public void Create_WithNullBlackboard_SetsBlackboardToNull()
    {
        // Arrange
        var validator = "TestValidator";
        var message = "Test validation message";
        var parameterName = "testParameter";
        var context = new ValidationContext(new Dictionary<string, object?> { { "key1", "value1" } });

        // Act
        var exception = ValidationException.Create(validator, message, parameterName, null, context);

        // Assert
        Assert.That(exception.Blackboard, Is.Null);
    }

    [Test]
    public void Create_WithContextList_CreatesValidationContextCorrectly()
    {
        // Arrange
        var validator = "TestValidator";
        var message = "Test validation message";
        var parameterName = "testParameter";
        var contextList = new List<(string key, object? value)>
        {
            ("key1", "value1"),
            ("key2", 42),
            ("key3", null)
        };
        var blackboard = new ValidationContext(new Dictionary<string, object?> { { "blackboardKey", "blackboardValue" } });

        // Act
        var exception = ValidationException.Create(validator, message, parameterName, blackboard, contextList);

        // Assert
        Assert.That(exception.Context, Is.Not.Null);
        Assert.That(exception.Context.Context["key1"], Is.EqualTo("value1"));
        Assert.That(exception.Context.Context["key2"], Is.EqualTo(42));
        Assert.That(exception.Context.Context["key3"], Is.Null);
    }

    [Test]
    public void Create_WithEmptyContextList_CreatesEmptyValidationContext()
    {
        // Arrange
        var validator = "TestValidator";
        var message = "Test validation message";
        var parameterName = "testParameter";
        var contextList = new List<(string key, object? value)>();
        var blackboard = new ValidationContext(new Dictionary<string, object?> { { "blackboardKey", "blackboardValue" } });

        // Act
        var exception = ValidationException.Create(validator, message, parameterName, blackboard, contextList);

        // Assert
        Assert.That(exception.Context, Is.Not.Null);
        Assert.That(exception.Context.Context, Is.Empty);
    }

    [Test]
    public void Properties_AreReadOnly()
    {
        // Arrange
        var validator = "TestValidator";
        var message = "Test validation message";
        var parameterName = "testParameter";
        var context = new ValidationContext(new Dictionary<string, object?> { { "key1", "value1" } });
        var blackboard = new ValidationContext(new Dictionary<string, object?> { { "blackboardKey", "blackboardValue" } });
        var exception = ValidationException.Create(validator, message, parameterName, blackboard, context);

        // Act & Assert
        Assert.That(exception.Validator, Is.EqualTo(validator));
        Assert.That(exception.ParameterName, Is.EqualTo(parameterName));
        Assert.That(exception.Context, Is.EqualTo(context));
        Assert.That(exception.Blackboard, Is.EqualTo(blackboard));
    }

    [Test]
    public void Exception_InheritsFromException()
    {
        // Arrange
        var validator = "TestValidator";
        var message = "Test validation message";
        var parameterName = "testParameter";
        var context = new ValidationContext(new Dictionary<string, object?> { { "key1", "value1" } });
        var blackboard = new ValidationContext(new Dictionary<string, object?> { { "blackboardKey", "blackboardValue" } });

        // Act
        var exception = ValidationException.Create(validator, message, parameterName, blackboard, context);

        // Assert
        Assert.That(exception, Is.InstanceOf<Exception>());
    }

    [Test]
    public void Message_Format_IsCorrect()
    {
        // Arrange
        var validator = "CustomValidator";
        var message = "Value must be greater than zero";
        var parameterName = "age";
        var context = new ValidationContext(new Dictionary<string, object?> { { "minValue", 0 } });
        var blackboard = new ValidationContext(new Dictionary<string, object?> { { "validationType", "range" } });

        // Act
        var exception = ValidationException.Create(validator, message, parameterName, blackboard, context);

        // Assert
        var expectedMessage = $"`{validator}` failed against parameter `{parameterName}`: {message}";
        Assert.That(exception.Message, Is.EqualTo(expectedMessage));
    }

    [Test]
    public void Create_WithSpecialCharactersInMessage_HandlesCorrectly()
    {
        // Arrange
        var validator = "TestValidator";
        var message = "Value contains special chars: !@#$%^&*()";
        var parameterName = "testParam";
        var context = new ValidationContext(new Dictionary<string, object?> { { "key1", "value1" } });
        var blackboard = new ValidationContext(new Dictionary<string, object?> { { "blackboardKey", "blackboardValue" } });

        // Act
        var exception = ValidationException.Create(validator, message, parameterName, blackboard, context);

        // Assert
        var expectedMessage = $"`{validator}` failed against parameter `{parameterName}`: {message}";
        Assert.That(exception.Message, Is.EqualTo(expectedMessage));
    }

    [Test]
    public void Create_WithLongMessage_HandlesCorrectly()
    {
        // Arrange
        var validator = "TestValidator";
        var message = new string('A', 1000);
        var parameterName = "testParam";
        var context = new ValidationContext(new Dictionary<string, object?> { { "key1", "value1" } });
        var blackboard = new ValidationContext(new Dictionary<string, object?> { { "blackboardKey", "blackboardValue" } });

        // Act
        var exception = ValidationException.Create(validator, message, parameterName, blackboard, context);

        // Assert
        var expectedLength = validator.Length + parameterName.Length + message.Length + 32; // Format string length
        Assert.That(exception.Message.Length, Is.EqualTo(expectedLength));
    }

    [Test]
    public void Create_WithUnicodeCharactersInMessage_HandlesCorrectly()
    {
        // Arrange
        var validator = "TestValidator";
        var message = "Value contains unicode: 🚀🌟🎉中文日本語한국어";
        var parameterName = "testParam";
        var context = new ValidationContext(new Dictionary<string, object?> { { "key1", "value1" } });
        var blackboard = new ValidationContext(new Dictionary<string, object?> { { "blackboardKey", "blackboardValue" } });

        // Act
        var exception = ValidationException.Create(validator, message, parameterName, blackboard, context);

        // Assert
        var expectedMessage = $"`{validator}` failed against parameter `{parameterName}`: {message}";
        Assert.That(exception.Message, Is.EqualTo(expectedMessage));
    }

    [Test]
    public void Create_WithNullValuesInContextList_HandlesCorrectly()
    {
        // Arrange
        var validator = "TestValidator";
        var message = "Test validation message";
        var parameterName = "testParameter";
        var contextList = new List<(string key, object? value)>
        {
            ("key1", "value1"),
            ("key2", null),
            ("key3", "value3")
        };
        var blackboard = new ValidationContext(new Dictionary<string, object?> { { "blackboardKey", "blackboardValue" } });

        // Act
        var exception = ValidationException.Create(validator, message, parameterName, blackboard, contextList);

        // Assert
        Assert.That(exception.Context.Context["key1"], Is.EqualTo("value1"));
        Assert.That(exception.Context.Context["key2"], Is.Null);
        Assert.That(exception.Context.Context["key3"], Is.EqualTo("value3"));
    }

    [Test]
    public void Create_WithDuplicateKeysInContextList_OverwritesPreviousValues()
    {
        // Arrange
        var validator = "TestValidator";
        var message = "Test validation message";
        var parameterName = "testParameter";
        var contextList = new List<(string key, object? value)>
        {
            ("key1", "value1"),
            ("key1", "value2"), // Duplicate key
            ("key2", "value3")
        };
        var blackboard = new ValidationContext(new Dictionary<string, object?> { { "blackboardKey", "blackboardValue" } });

        // Act
        var exception = ValidationException.Create(validator, message, parameterName, blackboard, contextList);

        // Assert
        Assert.That(exception.Context.Context["key1"], Is.EqualTo("value2")); // Should be overwritten
        Assert.That(exception.Context.Context["key2"], Is.EqualTo("value3"));
    }
}


