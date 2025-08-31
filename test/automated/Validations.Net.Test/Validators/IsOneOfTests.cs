using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsOneOfTests
{
    [Test]
    public void ValidatorName_IsCorrect()
    {
        // Assert
        Assert.That(IsOneOf.ValidatorName, Is.EqualTo("IsOneOf"));
    }

    [Test]
    public void ValidationFailureMessage_IsCorrect()
    {
        // Assert
        Assert.That(IsOneOf.ValidationFailureMessage, Is.EqualTo("Parameter must be one of the specified values"));
    }

    [Test]
    public void CheckIsOneOf_WithValueInOptions_ReturnsTrue()
    {
        // Arrange
        string value = "test";
        string[] options = { "test", "other", "another" };

        // Act
        var result = value.CheckIsOneOf(options);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsOneOf_WithValueNotInOptions_ReturnsFalse()
    {
        // Arrange
        string value = "notfound";
        string[] options = { "test", "other", "another" };

        // Act
        var result = value.CheckIsOneOf(options);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsOneOf_WithNullValue_ReturnsFalse()
    {
        // Arrange
        string? value = null;
        string[] options = { "test", "other", "another" };

        // Act
        var result = value.CheckIsOneOf(options);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsOneOf_WithEmptyOptions_ReturnsFalse()
    {
        // Arrange
        string value = "test";
        string[] options = { };

        // Act
        var result = value.CheckIsOneOf(options);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsOneOf_WithNullValueAndEmptyOptions_ReturnsFalse()
    {
        // Arrange
        string? value = null;
        string[] options = { };

        // Act
        var result = value.CheckIsOneOf(options);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsOneOf_WithValueType_ReturnsTrue()
    {
        // Arrange
        int value = 42;
        int[] options = { 10, 42, 100 };

        // Act
        var result = value.CheckIsOneOf(options);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsOneOf_WithCollectionOverload_ReturnsTrue()
    {
        // Arrange
        string value = "test";
        var options = new List<string> { "test", "other", "another" };

        // Act
        var result = value.CheckIsOneOf(options);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsOneOf_WithCollectionOverload_ReturnsFalse()
    {
        // Arrange
        string value = "notfound";
        var options = new List<string> { "test", "other", "another" };

        // Act
        var result = value.CheckIsOneOf(options);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsOneOf_WithEmptyCollection_ReturnsFalse()
    {
        // Arrange
        string value = "test";
        var options = new List<string>();

        // Act
        var result = value.CheckIsOneOf(options);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void EnsureIsOneOf_WithValueInOptions_ReturnsValue()
    {
        // Arrange
        string value = "test";
        string propertyName = "testProperty";
        string[] options = { "test", "other", "another" };

        // Act
        var result = value.EnsureIsOneOf(null, propertyName, options);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void EnsureIsOneOf_WithValueNotInOptions_ThrowsValidationException()
    {
        // Arrange
        string value = "notfound";
        string propertyName = "testProperty";
        string[] options = { "test", "other", "another" };

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsOneOf(null, propertyName, options));
        Assert.That(exception.Message, Does.Contain("Parameter must be one of the specified values"));
    }

    [Test]
    public void EnsureIsOneOf_WithCollectionOverload_ReturnsValue()
    {
        // Arrange
        string value = "test";
        string propertyName = "testProperty";
        var options = new List<string> { "test", "other", "another" };

        // Act
        var result = value.EnsureIsOneOf(options, null, propertyName);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void EnsureIsOneOf_WithCollectionOverload_ThrowsValidationException()
    {
        // Arrange
        string value = "notfound";
        string propertyName = "testProperty";
        var options = new List<string> { "test", "other", "another" };

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsOneOf(options, null, propertyName));
        Assert.That(exception.Message, Does.Contain("Parameter must be one of the specified values"));
    }

    [Test]
    public void ValidateIsOneOf_WithValueInOptions_ReturnsValidResult()
    {
        // Arrange
        string value = "test";
        string variableName = "testVariable";
        string[] options = { "test", "other", "another" };

        // Act
        var result = value.ValidateIsOneOf(null, variableName, options);

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsOneOf_WithValueNotInOptions_ReturnsInvalidResult()
    {
        // Arrange
        string value = "notfound";
        string variableName = "testVariable";
        string[] options = { "test", "other", "another" };

        // Act
        var result = value.ValidateIsOneOf(null, variableName, options);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be one of the specified values"));
    }

    [Test]
    public void ValidateIsOneOf_WithCollectionOverload_ReturnsValidResult()
    {
        // Arrange
        string value = "test";
        string variableName = "testVariable";
        var options = new List<string> { "test", "other", "another" };

        // Act
        var result = value.ValidateIsOneOf(options, null, variableName);

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsOneOf_WithCollectionOverload_ReturnsInvalidResult()
    {
        // Arrange
        string value = "notfound";
        string variableName = "testVariable";
        var options = new List<string> { "test", "other", "another" };

        // Act
        var result = value.ValidateIsOneOf(options, null, variableName);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be one of the specified values"));
    }

    [Test]
    public void ValidateIsOneOf_WithNullValue_ReturnsInvalidResult()
    {
        // Arrange
        string? value = null;
        string variableName = "testVariable";
        string[] options = { "test", "other", "another" };

        // Act
        var result = value.ValidateIsOneOf(null, variableName, options);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be one of the specified values"));
    }

    [Test]
    public void ValidateIsOneOf_WithEmptyOptions_ReturnsInvalidResult()
    {
        // Arrange
        string value = "test";
        string variableName = "testVariable";
        string[] options = { };

        // Act
        var result = value.ValidateIsOneOf(null, variableName, options);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be one of the specified values"));
    }

    [Test]
    public void ValidateIsOneOf_WithBlackboard_ReturnsValidResult()
    {
        // Arrange
        string value = "test";
        string variableName = "testVariable";
        var blackboard = new Blackboard();
        string[] options = { "test", "other", "another" };

        // Act
        var result = value.ValidateIsOneOf(blackboard, variableName, options);

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsOneOf_WithBlackboard_ReturnsInvalidResult()
    {
        // Arrange
        string value = "notfound";
        string variableName = "testVariable";
        var blackboard = new Blackboard();
        string[] options = { "test", "other", "another" };

        // Act
        var result = value.ValidateIsOneOf(blackboard, variableName, options);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be one of the specified values"));
    }

    [Test]
    public void ValidateIsOneOf_WithValueType_ReturnsValidResult()
    {
        // Arrange
        int value = 42;
        string variableName = "testVariable";
        int[] options = { 10, 42, 100 };

        // Act
        var result = value.ValidateIsOneOf(null, variableName, options);

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsOneOf_WithValueType_ReturnsInvalidResult()
    {
        // Arrange
        int value = 999;
        string variableName = "testVariable";
        int[] options = { 10, 42, 100 };

        // Act
        var result = value.ValidateIsOneOf(null, variableName, options);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be one of the specified values"));
    }

    [Test]
    public void ValidateIsOneOf_WithComplexObject_ReturnsValidResult()
    {
        // Arrange
        var value = new { Name = "Test", Value = 42 };
        string variableName = "testVariable";
        var options = new[] 
        { 
            new { Name = "Test", Value = 42 },
            new { Name = "Other", Value = 100 }
        };

        // Act
        var result = value.ValidateIsOneOf(null, variableName, options);

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsOneOf_WithComplexObject_ReturnsInvalidResult()
    {
        // Arrange
        var value = new { Name = "Test", Value = 42 };
        string variableName = "testVariable";
        var options = new[] 
        { 
            new { Name = "Other", Value = 100 },
            new { Name = "Another", Value = 200 }
        };

        // Act
        var result = value.ValidateIsOneOf(null, variableName, options);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be one of the specified values"));
    }
}
