using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNullTests
{
    [Test]
    public void ValidatorName_IsCorrect()
    {
        // Assert
        Assert.That(IsNull.ValidatorName, Is.EqualTo("IsNull"));
    }

    [Test]
    public void ValidationFailureMessage_IsCorrect()
    {
        // Assert
        Assert.That(IsNull.ValidationFailureMessage, Is.EqualTo("Parameter must be null"));
    }

    [Test]
    public void CheckIsNull_WithNullValue_ReturnsTrue()
    {
        // Arrange
        string? value = null;

        // Act
        var result = value.CheckIsNull();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNull_WithNonNullValue_ReturnsFalse()
    {
        // Arrange
        string value = "test";

        // Act
        var result = value.CheckIsNull();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNull_WithValueType_ReturnsFalse()
    {
        // Arrange
        int value = 42;

        // Act
        var result = value.CheckIsNull();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNull_WithNullableValueType_NonNull_ReturnsFalse()
    {
        // Arrange
        int? value = 42;

        // Act
        var result = value.CheckIsNull();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNull_WithNullableValueType_Null_ReturnsTrue()
    {
        // Arrange
        int? value = null;

        // Act
        var result = value.CheckIsNull();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void EnsureIsNull_WithNullValue_ReturnsValue()
    {
        // Arrange
        string? value = null;

        // Act
        var result = value.EnsureIsNull();

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void EnsureIsNull_WithNonNullValue_ThrowsValidationException()
    {
        // Arrange
        string value = "test";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNull());
        Assert.That(exception.Message, Does.Contain("Parameter must be null"));
    }

    [Test]
    public void EnsureIsNull_WithNonNullValueAndVariableName_ThrowsValidationExceptionWithVariableName()
    {
        // Arrange
        string value = "test";
        string variableName = "testVariable";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNull(variableName));
        Assert.That(exception.Message, Does.Contain("Parameter must be null"));
    }

    [Test]
    public void EnsureIsNull_WithNonNullValueAndBlackboard_ThrowsValidationException()
    {
        // Arrange
        string value = "test";
        var blackboard = new Blackboard();

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNull(null, blackboard));
        Assert.That(exception.Message, Does.Contain("Parameter must be null"));
    }

    [Test]
    public void ValidateIsNull_WithNullValue_ReturnsValidResult()
    {
        // Arrange
        string? value = null;

        // Act
        var result = value.ValidateIsNull();

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsNull_WithNonNullValue_ReturnsInvalidResult()
    {
        // Arrange
        string value = "test";

        // Act
        var result = value.ValidateIsNull();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be null"));
    }

    [Test]
    public void ValidateIsNull_WithNonNullValueAndVariableName_ReturnsInvalidResultWithVariableName()
    {
        // Arrange
        string value = "test";
        string variableName = "testVariable";

        // Act
        var result = value.ValidateIsNull(variableName);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be null"));
    }

    [Test]
    public void ValidateIsNull_WithNonNullValueAndBlackboard_ReturnsInvalidResult()
    {
        // Arrange
        string value = "test";
        var blackboard = new Blackboard();

        // Act
        var result = value.ValidateIsNull(null, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be null"));
    }

    [Test]
    public void ValidateIsNull_WithComplexObject_Null_ReturnsValidResult()
    {
        // Arrange
        object? obj = null;

        // Act
        var result = obj.ValidateIsNull();

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsNull_WithComplexObject_NonNull_ReturnsInvalidResult()
    {
        // Arrange
        var obj = new { Name = "Test", Value = 42 };

        // Act
        var result = obj.ValidateIsNull();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be null"));
    }

    [Test]
    public void ValidateIsNull_WithEmptyString_ReturnsInvalidResult()
    {
        // Arrange
        string value = "";

        // Act
        var result = value.ValidateIsNull();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be null"));
    }

    [Test]
    public void ValidateIsNull_WithWhitespaceString_ReturnsInvalidResult()
    {
        // Arrange
        string value = "   ";

        // Act
        var result = value.ValidateIsNull();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be null"));
    }

    [Test]
    public void ValidateIsNull_WithZeroValue_ReturnsInvalidResult()
    {
        // Arrange
        int value = 0;

        // Act
        var result = value.ValidateIsNull();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be null"));
    }

    [Test]
    public void ValidateIsNull_WithFalseValue_ReturnsInvalidResult()
    {
        // Arrange
        bool value = false;

        // Act
        var result = value.ValidateIsNull();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be null"));
    }
}
