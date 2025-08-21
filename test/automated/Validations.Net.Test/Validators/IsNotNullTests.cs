using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNotNullTests
{
    [Test]
    public void ValidatorName_IsCorrect()
    {
        // Assert
        Assert.That(IsNotNull.ValidatorName, Is.EqualTo("IsNotNull"));
    }

    [Test]
    public void ValidationFailureMessage_IsCorrect()
    {
        // Assert
        Assert.That(IsNotNull.ValidationFailureMessage, Is.EqualTo("Parameter must not be null"));
    }

    [Test]
    public void CheckIsNotNull_WithNonNullValue_ReturnsTrue()
    {
        // Arrange
        string value = "test";

        // Act
        var result = value.CheckIsNotNull();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotNull_WithNullValue_ReturnsFalse()
    {
        // Arrange
        string? value = null;

        // Act
        var result = value.CheckIsNotNull();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotNull_WithValueType_ReturnsTrue()
    {
        // Arrange
        int value = 42;

        // Act
        var result = value.CheckIsNotNull();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotNull_WithNullableValueType_NonNull_ReturnsTrue()
    {
        // Arrange
        int? value = 42;

        // Act
        var result = value.CheckIsNotNull();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotNull_WithNullableValueType_Null_ReturnsFalse()
    {
        // Arrange
        int? value = null;

        // Act
        var result = value.CheckIsNotNull();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void EnsureIsNotNull_WithNonNullValue_ReturnsValue()
    {
        // Arrange
        string value = "test";

        // Act
        var result = value.EnsureIsNotNull();

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void EnsureIsNotNull_WithNullValue_ThrowsValidationException()
    {
        // Arrange
        string? value = null;

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotNull());
        Assert.That(exception.Message, Does.Contain("Parameter must not be null"));
    }

    [Test]
    public void EnsureIsNotNull_WithNullValueAndVariableName_ThrowsValidationException()
    {
        // Arrange
        string? value = null;
        string variableName = "testVariable";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotNull(null, variableName));
        Assert.That(exception.Message, Does.Contain("Parameter must not be null"));
    }

    [Test]
    public void EnsureIsNotNull_WithNullValueAndBlackboard_ThrowsValidationException()
    {
        // Arrange
        string? value = null;
        var blackboard = new Blackboard();

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotNull(blackboard));
        Assert.That(exception.Message, Does.Contain("Parameter must not be null"));
    }

    [Test]
    public void ValidateIsNotNull_WithNonNullValue_ReturnsValidResult()
    {
        // Arrange
        string value = "test";

        // Act
        var result = value.ValidateIsNotNull();

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsNotNull_WithNullValue_ReturnsInvalidResult()
    {
        // Arrange
        string? value = null;

        // Act
        var result = value.ValidateIsNotNull();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be null"));
    }

    [Test]
    public void ValidateIsNotNull_WithNullValueAndVariableName_ReturnsInvalidResultWithVariableName()
    {
        // Arrange
        string? value = null;
        string variableName = "testVariable";

        // Act
        var result = value.ValidateIsNotNull(null, variableName);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be null"));
    }

    [Test]
    public void ValidateIsNotNull_WithNullValueAndBlackboard_ReturnsInvalidResult()
    {
        // Arrange
        string? value = null;
        var blackboard = new Blackboard();

        // Act
        var result = value.ValidateIsNotNull(blackboard);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be null"));
    }

    [Test]
    public void ValidateIsNotNull_WithComplexObject_NonNull_ReturnsValidResult()
    {
        // Arrange
        var obj = new { Name = "Test", Value = 42 };

        // Act
        var result = obj.ValidateIsNotNull();

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsNotNull_WithComplexObject_Null_ReturnsInvalidResult()
    {
        // Arrange
        object? obj = null;

        // Act
        var result = obj.ValidateIsNotNull();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be null"));
    }

    [Test]
    public void ValidateIsNotNull_WithEmptyString_ReturnsValidResult()
    {
        // Arrange
        string value = "";

        // Act
        var result = value.ValidateIsNotNull();

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsNotNull_WithWhitespaceString_ReturnsValidResult()
    {
        // Arrange
        string value = "   ";

        // Act
        var result = value.ValidateIsNotNull();

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }
}
