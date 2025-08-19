using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNotOneOfTests
{
    [Test]
    public void ValidatorName_IsCorrect()
    {
        // Assert
        Assert.That(IsNotOneOf.ValidatorName, Is.EqualTo("IsNotOneOf"));
    }

    [Test]
    public void ValidationFailureMessage_IsCorrect()
    {
        // Assert
        Assert.That(IsNotOneOf.ValidationFailureMessage, Is.EqualTo("Parameter must not be any of the specified values"));
    }

    [Test]
    public void CheckIsNotOneOf_WithValueNotInOptions_ReturnsTrue()
    {
        // Arrange
        string value = "notfound";
        string[] options = { "test", "other", "another" };

        // Act
        var result = value.CheckIsNotOneOf(options);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotOneOf_WithValueInOptions_ReturnsFalse()
    {
        // Arrange
        string value = "test";
        string[] options = { "test", "other", "another" };

        // Act
        var result = value.CheckIsNotOneOf(options);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotOneOf_WithNullValue_ReturnsFalse()
    {
        // Arrange
        string? value = null;
        string[] options = { "test", "other", "another" };

        // Act
        var result = value.CheckIsNotOneOf(options);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotOneOf_WithEmptyOptions_ReturnsFalse()
    {
        // Arrange
        string value = "test";
        string[] options = { };

        // Act
        var result = value.CheckIsNotOneOf(options);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotOneOf_WithNullValueAndEmptyOptions_ReturnsFalse()
    {
        // Arrange
        string? value = null;
        string[] options = { };

        // Act
        var result = value.CheckIsNotOneOf(options);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotOneOf_WithValueType_ReturnsTrue()
    {
        // Arrange
        int value = 999;
        int[] options = { 10, 42, 100 };

        // Act
        var result = value.CheckIsNotOneOf(options);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotOneOf_WithValueType_ReturnsFalse()
    {
        // Arrange
        int value = 42;
        int[] options = { 10, 42, 100 };

        // Act
        var result = value.CheckIsNotOneOf(options);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotOneOf_WithCollectionOverload_ReturnsTrue()
    {
        // Arrange
        string value = "notfound";
        var options = new List<string> { "test", "other", "another" };

        // Act
        var result = value.CheckIsNotOneOf(options);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotOneOf_WithCollectionOverload_ReturnsFalse()
    {
        // Arrange
        string value = "test";
        var options = new List<string> { "test", "other", "another" };

        // Act
        var result = value.CheckIsNotOneOf(options);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotOneOf_WithEmptyCollection_ReturnsFalse()
    {
        // Arrange
        string value = "test";
        var options = new List<string>();

        // Act
        var result = value.CheckIsNotOneOf(options);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void EnsureIsNotOneOf_WithValueNotInOptions_ReturnsValue()
    {
        // Arrange
        string value = "notfound";
        string propertyName = "testProperty";
        string[] options = { "test", "other", "another" };

        // Act
        var result = value.EnsureIsNotOneOf(propertyName, null, options);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void EnsureIsNotOneOf_WithValueInOptions_ThrowsValidationException()
    {
        // Arrange
        string value = "test";
        string propertyName = "testProperty";
        string[] options = { "test", "other", "another" };

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotOneOf(propertyName, null, options));
        Assert.That(exception.Message, Does.Contain("Parameter must not be any of the specified values"));
    }

    [Test]
    public void EnsureIsNotOneOf_WithCollectionOverload_ReturnsValue()
    {
        // Arrange
        string value = "notfound";
        string propertyName = "testProperty";
        var options = new List<string> { "test", "other", "another" };

        // Act
        var result = value.EnsureIsNotOneOf(options, propertyName);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void EnsureIsNotOneOf_WithCollectionOverload_ThrowsValidationException()
    {
        // Arrange
        string value = "test";
        string propertyName = "testProperty";
        var options = new List<string> { "test", "other", "another" };

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotOneOf(options, propertyName));
        Assert.That(exception.Message, Does.Contain("Parameter must not be any of the specified values"));
    }

    [Test]
    public void ValidateIsNotOneOf_WithValueNotInOptions_ReturnsValidResult()
    {
        // Arrange
        string value = "notfound";
        string variableName = "testVariable";
        string[] options = { "test", "other", "another" };

        // Act
        var result = value.ValidateIsNotOneOf(variableName, null, options);

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsNotOneOf_WithValueInOptions_ReturnsInvalidResult()
    {
        // Arrange
        string value = "test";
        string variableName = "testVariable";
        string[] options = { "test", "other", "another" };

        // Act
        var result = value.ValidateIsNotOneOf(variableName, null, options);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be any of the specified values"));
    }

    [Test]
    public void ValidateIsNotOneOf_WithCollectionOverload_ReturnsValidResult()
    {
        // Arrange
        string value = "notfound";
        string variableName = "testVariable";
        var options = new List<string> { "test", "other", "another" };

        // Act
        var result = value.ValidateIsNotOneOf(options, variableName);

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsNotOneOf_WithCollectionOverload_ReturnsInvalidResult()
    {
        // Arrange
        string value = "test";
        string variableName = "testVariable";
        var options = new List<string> { "test", "other", "another" };

        // Act
        var result = value.ValidateIsNotOneOf(options, variableName);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be any of the specified values"));
    }

    [Test]
    public void ValidateIsNotOneOf_WithNullValue_ReturnsInvalidResult()
    {
        // Arrange
        string? value = null;
        string variableName = "testVariable";
        string[] options = { "test", "other", "another" };

        // Act
        var result = value.ValidateIsNotOneOf(variableName, null, options);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be any of the specified values"));
    }

    [Test]
    public void ValidateIsNotOneOf_WithEmptyOptions_ReturnsInvalidResult()
    {
        // Arrange
        string value = "test";
        string variableName = "testVariable";
        string[] options = { };

        // Act
        var result = value.ValidateIsNotOneOf(variableName, null, options);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be any of the specified values"));
    }

    [Test]
    public void ValidateIsNotOneOf_WithBlackboard_ReturnsValidResult()
    {
        // Arrange
        string value = "notfound";
        string variableName = "testVariable";
        var blackboard = new Blackboard();
        string[] options = { "test", "other", "another" };

        // Act
        var result = value.ValidateIsNotOneOf(variableName, blackboard, options);

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsNotOneOf_WithBlackboard_ReturnsInvalidResult()
    {
        // Arrange
        string value = "test";
        string variableName = "testVariable";
        var blackboard = new Blackboard();
        string[] options = { "test", "other", "another" };

        // Act
        var result = value.ValidateIsNotOneOf(variableName, blackboard, options);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be any of the specified values"));
    }

    [Test]
    public void ValidateIsNotOneOf_WithValueType_ReturnsValidResult()
    {
        // Arrange
        int value = 999;
        string variableName = "testVariable";
        int[] options = { 10, 42, 100 };

        // Act
        var result = value.ValidateIsNotOneOf(variableName, null, options);

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsNotOneOf_WithValueType_ReturnsInvalidResult()
    {
        // Arrange
        int value = 42;
        string variableName = "testVariable";
        int[] options = { 10, 42, 100 };

        // Act
        var result = value.ValidateIsNotOneOf(variableName, null, options);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be any of the specified values"));
    }

    [Test]
    public void ValidateIsNotOneOf_WithComplexObject_ReturnsValidResult()
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
        var result = value.ValidateIsNotOneOf(variableName, null, options);

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsNotOneOf_WithComplexObject_ReturnsInvalidResult()
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
        var result = value.ValidateIsNotOneOf(variableName, null, options);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be any of the specified values"));
    }

    [Test]
    public void ValidateIsNotOneOf_WithSingleOption_ReturnsValidResult()
    {
        // Arrange
        string value = "notfound";
        string variableName = "testVariable";
        string[] options = { "test" };

        // Act
        var result = value.ValidateIsNotOneOf(variableName, null, options);

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsNotOneOf_WithSingleOption_ReturnsInvalidResult()
    {
        // Arrange
        string value = "test";
        string variableName = "testVariable";
        string[] options = { "test" };

        // Act
        var result = value.ValidateIsNotOneOf(variableName, null, options);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be any of the specified values"));
    }
}
