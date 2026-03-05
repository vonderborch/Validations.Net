using Validations.Net.OLD;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

/// <summary>
/// Unit tests for the IsNull validator class.
/// Tests the CheckIsNull, ValidateIsNull, and EnsureIsNull methods.
/// </summary>
[TestFixture]
public class IsNullTests
{
    #region CheckIsNull Tests

    [Test]
    public void CheckIsNull_WithNullValue_ReturnsTrue()
    {
        // Arrange
        string? nullString = null;

        // Act
        var result = nullString.CheckIsNull();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNull_WithNonNullValue_ReturnsFalse()
    {
        // Arrange
        string nonNullString = "test";

        // Act
        var result = nonNullString.CheckIsNull();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNull_WithNullObject_ReturnsTrue()
    {
        // Arrange
        object? nullObject = null;

        // Act
        var result = nullObject.CheckIsNull();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNull_WithNonNullObject_ReturnsFalse()
    {
        // Arrange
        object nonNullObject = new();

        // Act
        var result = nonNullObject.CheckIsNull();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNull_WithNullableIntNull_ReturnsTrue()
    {
        // Arrange
        int? nullableInt = null;

        // Act
        var result = nullableInt.CheckIsNull();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNull_WithNullableIntValue_ReturnsFalse()
    {
        // Arrange
        int? nullableInt = 42;

        // Act
        var result = nullableInt.CheckIsNull();

        // Assert
        Assert.That(result, Is.False);
    }

    #endregion

    #region ValidateIsNull Tests

    [Test]
    public void ValidateIsNull_WithNullValue_ReturnsValidResult()
    {
        // Arrange
        string? nullString = null;

        // Act
        var result = nullString.ValidateIsNull();

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
        Assert.That(result.ExceptionMessage, Is.Null);
    }

    [Test]
    public void ValidateIsNull_WithNonNullValue_ReturnsInvalidResult()
    {
        // Arrange
        string nonNullString = "test";

        // Act
        var result = nonNullString.ValidateIsNull();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsNull.ValidatorName));
        Assert.That(result.ValidationException.Message, Does.Contain(IsNull.DefaultValidationFailureMessage));
    }

    [Test]
    public void ValidateIsNull_WithCustomMessage_UsesCustomMessage()
    {
        // Arrange
        string nonNullString = "test";
        string customMessage = "Custom error message";

        // Act
        var result = nonNullString.ValidateIsNull(validationFailureMessage: customMessage);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain(customMessage));
    }

    [Test]
    public void ValidateIsNull_WithParameterName_IncludesParameterName()
    {
        // Arrange
        string nonNullString = "test";

        // Act
        var result = nonNullString.ValidateIsNull(parameterName: "testParam");

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("testParam"));
    }

    [Test]
    public void ValidateIsNull_WithNullObject_ReturnsValidResult()
    {
        // Arrange
        object? nullObject = null;

        // Act
        var result = nullObject.ValidateIsNull();

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsNull_WithNonNullObject_ReturnsInvalidResult()
    {
        // Arrange
        object nonNullObject = new();

        // Act
        var result = nonNullObject.ValidateIsNull();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
    }

    #endregion

    #region EnsureIsNull Tests

    [Test]
    public void EnsureIsNull_WithNullValue_DoesNotThrow()
    {
        // Arrange
        string? nullString = null;

        // Act & Assert
        Assert.DoesNotThrow(() => nullString.EnsureIsNull());
    }

    [Test]
    public void EnsureIsNull_WithNullValue_ReturnsNull()
    {
        // Arrange
        string? nullString = null;

        // Act
        var result = nullString.EnsureIsNull();

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void EnsureIsNull_WithNonNullValue_ThrowsValidationException()
    {
        // Arrange
        string nonNullString = "test";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => nonNullString.EnsureIsNull());
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.Message, Does.Contain(IsNull.ValidatorName));
        Assert.That(exception.Message, Does.Contain(IsNull.DefaultValidationFailureMessage));
    }

    [Test]
    public void EnsureIsNull_WithCustomMessage_ThrowsWithCustomMessage()
    {
        // Arrange
        string nonNullString = "test";
        string customMessage = "Custom error message";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => 
            nonNullString.EnsureIsNull(validationFailureMessage: customMessage));
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.Message, Does.Contain(customMessage));
    }

    [Test]
    public void EnsureIsNull_WithParameterName_ThrowsWithParameterName()
    {
        // Arrange
        string nonNullString = "test";
        string paramName = "testParam";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => 
            nonNullString.EnsureIsNull(parameterName: paramName));
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.Message, Does.Contain(paramName));
        Assert.That(exception.ParameterName, Is.EqualTo(paramName));
    }

    [Test]
    public void EnsureIsNull_WithNonNullObject_ThrowsValidationException()
    {
        // Arrange
        object nonNullObject = new();

        // Act & Assert
        Assert.Throws<ValidationException>(() => nonNullObject.EnsureIsNull());
    }

    [Test]
    public void EnsureIsNull_WithNullObject_DoesNotThrow()
    {
        // Arrange
        object? nullObject = null;

        // Act & Assert
        Assert.DoesNotThrow(() => nullObject.EnsureIsNull());
    }

    [Test]
    public void EnsureIsNull_ExceptionContainsValidator_IsCorrect()
    {
        // Arrange
        string nonNullString = "test";

        // Act
        var exception = Assert.Throws<ValidationException>(() => nonNullString.EnsureIsNull());

        // Assert
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.Validator, Is.EqualTo(IsNull.ValidatorName));
    }

    #endregion

    #region Edge Cases

    [Test]
    public void ValidateIsNull_WithEmptyString_ReturnsInvalidResult()
    {
        // Arrange
        string emptyString = string.Empty;

        // Act
        var result = emptyString.ValidateIsNull();

        // Assert
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void ValidateIsNull_WithWhitespaceString_ReturnsInvalidResult()
    {
        // Arrange
        string whitespaceString = "   ";

        // Act
        var result = whitespaceString.ValidateIsNull();

        // Assert
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void ValidateIsNull_WithZeroInteger_ReturnsInvalidResult()
    {
        // Arrange
        int zero = 0;

        // Act
        var result = zero.ValidateIsNull();

        // Assert
        Assert.That(result.IsValid, Is.False);
    }

    #endregion
}

