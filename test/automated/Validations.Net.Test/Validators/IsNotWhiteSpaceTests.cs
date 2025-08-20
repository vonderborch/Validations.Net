using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNotWhiteSpaceTests
{
    [Test]
    public void ValidatorName_ShouldBeCorrect()
    {
        Assert.That(IsNotWhiteSpace.ValidatorName, Is.EqualTo("IsNotWhiteSpace"));
    }

    [Test]
    public void ValidationFailureMessage_ShouldBeCorrect()
    {
        Assert.That(IsNotWhiteSpace.ValidationFailureMessage, Is.EqualTo("Parameter must not be whitespace"));
    }

    [Test]
    public void CheckIsNotWhiteSpace_WithNull_ReturnsTrue()
    {
        // Arrange
        string? value = null;

        // Act
        bool result = value.CheckIsNotWhiteSpace();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotWhiteSpace_WithEmptyString_ReturnsTrue()
    {
        // Arrange
        string value = "";

        // Act
        bool result = value.CheckIsNotWhiteSpace();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotWhiteSpace_WithNonWhitespaceString_ReturnsTrue()
    {
        // Arrange
        string value = "hello";

        // Act
        bool result = value.CheckIsNotWhiteSpace();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotWhiteSpace_WithMixedWhitespaceAndText_ReturnsTrue()
    {
        // Arrange
        string value = "  hello  ";

        // Act
        bool result = value.CheckIsNotWhiteSpace();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotWhiteSpace_WithWhitespaceString_ReturnsFalse()
    {
        // Arrange
        string value = "   \t\n\r";

        // Act
        bool result = value.CheckIsNotWhiteSpace();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotWhiteSpace_WithSpaceOnly_ReturnsFalse()
    {
        // Arrange
        string value = " ";

        // Act
        bool result = value.CheckIsNotWhiteSpace();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotWhiteSpace_WithTabOnly_ReturnsFalse()
    {
        // Arrange
        string value = "\t";

        // Act
        bool result = value.CheckIsNotWhiteSpace();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotWhiteSpace_WithNewLineOnly_ReturnsFalse()
    {
        // Arrange
        string value = "\n";

        // Act
        bool result = value.CheckIsNotWhiteSpace();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotWhiteSpace_WithCarriageReturnOnly_ReturnsFalse()
    {
        // Arrange
        string value = "\r";

        // Act
        bool result = value.CheckIsNotWhiteSpace();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void EnsureIsNotWhiteSpace_WithNull_ReturnsValue()
    {
        // Arrange
        string? value = null;

        // Act
        string? result = value.EnsureIsNotWhiteSpace();

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void EnsureIsNotWhiteSpace_WithEmptyString_ReturnsValue()
    {
        // Arrange
        string value = "";

        // Act
        string? result = value.EnsureIsNotWhiteSpace();

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void EnsureIsNotWhiteSpace_WithNonWhitespaceString_ReturnsValue()
    {
        // Arrange
        string value = "hello";

        // Act
        string? result = value.EnsureIsNotWhiteSpace();

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void EnsureIsNotWhiteSpace_WithWhitespaceString_ThrowsValidationException()
    {
        // Arrange
        string value = "   \t\n\r";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotWhiteSpace());
        Assert.That(exception.Message, Does.Contain("Parameter must not be whitespace"));
    }

    [Test]
    public void EnsureIsNotWhiteSpace_WithSpaceOnly_ThrowsValidationException()
    {
        // Arrange
        string value = " ";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotWhiteSpace());
        Assert.That(exception.Message, Does.Contain("Parameter must not be whitespace"));
    }

    [Test]
    public void EnsureIsNotWhiteSpace_WithTabOnly_ThrowsValidationException()
    {
        // Arrange
        string value = "\t";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotWhiteSpace());
        Assert.That(exception.Message, Does.Contain("Parameter must not be whitespace"));
    }

    [Test]
    public void EnsureIsNotWhiteSpace_WithParameterName_ThrowsValidationExceptionWithParameterName()
    {
        // Arrange
        string value = "   ";
        string parameterName = "testParam";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotWhiteSpace(parameterName));
        Assert.That(exception.Message, Does.Contain("testParam"));
    }

    [Test]
    public void EnsureIsNotWhiteSpace_WithBlackboard_ThrowsValidationExceptionWithBlackboard()
    {
        // Arrange
        string value = "   ";
        var blackboard = new Blackboard();

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotWhiteSpace(null, blackboard));
        Assert.That(exception.Message, Does.Contain("Parameter must not be whitespace"));
    }

    [Test]
    public void ValidateIsNotWhiteSpace_WithNull_ReturnsValidResult()
    {
        // Arrange
        string? value = null;

        // Act
        ValidationResult result = value.ValidateIsNotWhiteSpace();

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsNotWhiteSpace_WithEmptyString_ReturnsValidResult()
    {
        // Arrange
        string value = "";

        // Act
        ValidationResult result = value.ValidateIsNotWhiteSpace();

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsNotWhiteSpace_WithNonWhitespaceString_ReturnsValidResult()
    {
        // Arrange
        string value = "hello";

        // Act
        ValidationResult result = value.ValidateIsNotWhiteSpace();

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsNotWhiteSpace_WithMixedWhitespaceAndText_ReturnsValidResult()
    {
        // Arrange
        string value = "  hello  ";

        // Act
        ValidationResult result = value.ValidateIsNotWhiteSpace();

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsNotWhiteSpace_WithWhitespaceString_ReturnsInvalidResult()
    {
        // Arrange
        string value = "   \t\n\r";

        // Act
        ValidationResult result = value.ValidateIsNotWhiteSpace();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be whitespace"));
    }

    [Test]
    public void ValidateIsNotWhiteSpace_WithSpaceOnly_ReturnsInvalidResult()
    {
        // Arrange
        string value = " ";

        // Act
        ValidationResult result = value.ValidateIsNotWhiteSpace();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be whitespace"));
    }

    [Test]
    public void ValidateIsNotWhiteSpace_WithTabOnly_ReturnsInvalidResult()
    {
        // Arrange
        string value = "\t";

        // Act
        ValidationResult result = value.ValidateIsNotWhiteSpace();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be whitespace"));
    }

    [Test]
    public void ValidateIsNotWhiteSpace_WithNewLineOnly_ReturnsInvalidResult()
    {
        // Arrange
        string value = "\n";

        // Act
        ValidationResult result = value.ValidateIsNotWhiteSpace();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be whitespace"));
    }

    [Test]
    public void ValidateIsNotWhiteSpace_WithCarriageReturnOnly_ReturnsInvalidResult()
    {
        // Arrange
        string value = "\r";

        // Act
        ValidationResult result = value.ValidateIsNotWhiteSpace();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be whitespace"));
    }

    [Test]
    public void ValidateIsNotWhiteSpace_WithParameterName_ReturnsInvalidResultWithParameterName()
    {
        // Arrange
        string value = "   ";
        string parameterName = "testParam";

        // Act
        ValidationResult result = value.ValidateIsNotWhiteSpace(parameterName);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("testParam"));
    }

    [Test]
    public void ValidateIsNotWhiteSpace_WithBlackboard_ReturnsInvalidResultWithBlackboard()
    {
        // Arrange
        string value = "   ";
        var blackboard = new Blackboard();

        // Act
        ValidationResult result = value.ValidateIsNotWhiteSpace(null, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be whitespace"));
    }
}
