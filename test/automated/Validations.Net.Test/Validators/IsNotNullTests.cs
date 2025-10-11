using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

/// <summary>
/// Unit tests for the IsNotNull validator class.
/// Tests the CheckIsNotNull, ValidateIsNotNull, and EnsureIsNotNull methods.
/// </summary>
[TestFixture]
public class IsNotNullTests
{
    #region CheckIsNotNull Tests

    [Test]
    public void CheckIsNotNull_WithNullValue_ReturnsFalse()
    {
        // Arrange
        string? nullString = null;

        // Act
        var result = nullString.CheckIsNotNull();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotNull_WithNonNullValue_ReturnsTrue()
    {
        // Arrange
        string nonNullString = "test";

        // Act
        var result = nonNullString.CheckIsNotNull();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotNull_WithNullObject_ReturnsFalse()
    {
        // Arrange
        object? nullObject = null;

        // Act
        var result = nullObject.CheckIsNotNull();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotNull_WithNonNullObject_ReturnsTrue()
    {
        // Arrange
        object nonNullObject = new();

        // Act
        var result = nonNullObject.CheckIsNotNull();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotNull_WithNullableIntNull_ReturnsFalse()
    {
        // Arrange
        int? nullableInt = null;

        // Act
        var result = nullableInt.CheckIsNotNull();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotNull_WithNullableIntValue_ReturnsTrue()
    {
        // Arrange
        int? nullableInt = 42;

        // Act
        var result = nullableInt.CheckIsNotNull();

        // Assert
        Assert.That(result, Is.True);
    }

    #endregion

    #region ValidateIsNotNull Tests

    [Test]
    public void ValidateIsNotNull_WithNonNullValue_ReturnsValidResult()
    {
        // Arrange
        string nonNullString = "test";

        // Act
        var result = nonNullString.ValidateIsNotNull();

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
        Assert.That(result.ExceptionMessage, Is.Null);
    }

    [Test]
    public void ValidateIsNotNull_WithNullValue_ReturnsInvalidResult()
    {
        // Arrange
        string? nullString = null;

        // Act
        var result = nullString.ValidateIsNotNull();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsNotNull.ValidatorName));
        Assert.That(result.ValidationException.Message, Does.Contain(IsNotNull.DefaultValidationFailureMessage));
    }

    [Test]
    public void ValidateIsNotNull_WithCustomMessage_UsesCustomMessage()
    {
        // Arrange
        string? nullString = null;
        string customMessage = "Custom error message";

        // Act
        var result = nullString.ValidateIsNotNull(validationFailureMessage: customMessage);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain(customMessage));
    }

    [Test]
    public void ValidateIsNotNull_WithParameterName_IncludesParameterName()
    {
        // Arrange
        string? nullString = null;

        // Act
        var result = nullString.ValidateIsNotNull(parameterName: "testParam");

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("testParam"));
    }

    [Test]
    public void ValidateIsNotNull_WithNonNullObject_ReturnsValidResult()
    {
        // Arrange
        object nonNullObject = new();

        // Act
        var result = nonNullObject.ValidateIsNotNull();

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsNotNull_WithNullObject_ReturnsInvalidResult()
    {
        // Arrange
        object? nullObject = null;

        // Act
        var result = nullObject.ValidateIsNotNull();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
    }

    [Test]
    public void ValidateIsNotNull_WithEmptyString_ReturnsValidResult()
    {
        // Arrange
        string emptyString = string.Empty;

        // Act
        var result = emptyString.ValidateIsNotNull();

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNotNull_WithWhitespaceString_ReturnsValidResult()
    {
        // Arrange
        string whitespaceString = "   ";

        // Act
        var result = whitespaceString.ValidateIsNotNull();

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    #endregion

    #region EnsureIsNotNull Tests

    [Test]
    public void EnsureIsNotNull_WithNonNullValue_DoesNotThrow()
    {
        // Arrange
        string nonNullString = "test";

        // Act & Assert
        Assert.DoesNotThrow(() => nonNullString.EnsureIsNotNull());
    }

    [Test]
    public void EnsureIsNotNull_WithNonNullValue_ReturnsValue()
    {
        // Arrange
        string nonNullString = "test";

        // Act
        var result = nonNullString.EnsureIsNotNull();

        // Assert
        Assert.That(result, Is.EqualTo(nonNullString));
    }

    [Test]
    public void EnsureIsNotNull_WithNullValue_ThrowsValidationException()
    {
        // Arrange
        string? nullString = null;

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => nullString.EnsureIsNotNull());
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.Message, Does.Contain(IsNotNull.ValidatorName));
        Assert.That(exception.Message, Does.Contain(IsNotNull.DefaultValidationFailureMessage));
    }

    [Test]
    public void EnsureIsNotNull_WithCustomMessage_ThrowsWithCustomMessage()
    {
        // Arrange
        string? nullString = null;
        string customMessage = "Custom error message";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => 
            nullString.EnsureIsNotNull(validationFailureMessage: customMessage));
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.Message, Does.Contain(customMessage));
    }

    [Test]
    public void EnsureIsNotNull_WithParameterName_ThrowsWithParameterName()
    {
        // Arrange
        string? nullString = null;
        string paramName = "testParam";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => 
            nullString.EnsureIsNotNull(parameterName: paramName));
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.Message, Does.Contain(paramName));
        Assert.That(exception.ParameterName, Is.EqualTo(paramName));
    }

    [Test]
    public void EnsureIsNotNull_WithNullObject_ThrowsValidationException()
    {
        // Arrange
        object? nullObject = null;

        // Act & Assert
        Assert.Throws<ValidationException>(() => nullObject.EnsureIsNotNull());
    }

    [Test]
    public void EnsureIsNotNull_WithNonNullObject_DoesNotThrow()
    {
        // Arrange
        object nonNullObject = new();

        // Act & Assert
        Assert.DoesNotThrow(() => nonNullObject.EnsureIsNotNull());
    }

    [Test]
    public void EnsureIsNotNull_ExceptionContainsValidator_IsCorrect()
    {
        // Arrange
        string? nullString = null;

        // Act
        var exception = Assert.Throws<ValidationException>(() => nullString.EnsureIsNotNull());

        // Assert
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.Validator, Is.EqualTo(IsNotNull.ValidatorName));
    }

    [Test]
    public void EnsureIsNotNull_WithEmptyString_ReturnsValue()
    {
        // Arrange
        string emptyString = string.Empty;

        // Act
        var result = emptyString.EnsureIsNotNull();

        // Assert
        Assert.That(result, Is.EqualTo(emptyString));
    }

    [Test]
    public void EnsureIsNotNull_WithZeroInteger_ReturnsValue()
    {
        // Arrange
        int zero = 0;

        // Act
        var result = zero.EnsureIsNotNull();

        // Assert
        Assert.That(result, Is.EqualTo(zero));
    }

    #endregion

    #region Edge Cases

    [Test]
    public void ValidateIsNotNull_WithDefaultStruct_ReturnsValidResult()
    {
        // Arrange
        int defaultInt = default;

        // Act
        var result = defaultInt.ValidateIsNotNull();

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNotNull_WithComplexObject_ReturnsValidResult()
    {
        // Arrange
        var complexObject = new { Name = "Test", Value = 42 };

        // Act
        var result = complexObject.ValidateIsNotNull();

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    #endregion
}

