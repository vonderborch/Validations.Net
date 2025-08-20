using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsWhiteSpaceTests
{
    [Test]
    public void ValidatorName_ShouldBeCorrect()
    {
        Assert.That(IsWhiteSpace.ValidatorName, Is.EqualTo("IsWhiteSpace"));
    }

    [Test]
    public void ValidationFailureMessage_ShouldBeCorrect()
    {
        Assert.That(IsWhiteSpace.ValidationFailureMessage, Is.EqualTo("Parameter must be whitespace"));
    }

    [Test]
    public void CheckIsWhiteSpace_WithWhitespaceString_ReturnsTrue()
    {
        // Arrange
        string value = "   \t\n\r";

        // Act
        bool result = value.CheckIsWhiteSpace();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsWhiteSpace_WithSpaceOnly_ReturnsTrue()
    {
        // Arrange
        string value = " ";

        // Act
        bool result = value.CheckIsWhiteSpace();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsWhiteSpace_WithTabOnly_ReturnsTrue()
    {
        // Arrange
        string value = "\t";

        // Act
        bool result = value.CheckIsWhiteSpace();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsWhiteSpace_WithNewLineOnly_ReturnsTrue()
    {
        // Arrange
        string value = "\n";

        // Act
        bool result = value.CheckIsWhiteSpace();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsWhiteSpace_WithCarriageReturnOnly_ReturnsTrue()
    {
        // Arrange
        string value = "\r";

        // Act
        bool result = value.CheckIsWhiteSpace();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsWhiteSpace_WithNull_ReturnsFalse()
    {
        // Arrange
        string? value = null;

        // Act
        bool result = value.CheckIsWhiteSpace();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsWhiteSpace_WithEmptyString_ReturnsFalse()
    {
        // Arrange
        string value = "";

        // Act
        bool result = value.CheckIsWhiteSpace();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsWhiteSpace_WithNonWhitespaceString_ReturnsFalse()
    {
        // Arrange
        string value = "hello";

        // Act
        bool result = value.CheckIsWhiteSpace();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsWhiteSpace_WithMixedWhitespaceAndText_ReturnsFalse()
    {
        // Arrange
        string value = "  hello  ";

        // Act
        bool result = value.CheckIsWhiteSpace();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void EnsureIsWhiteSpace_WithWhitespaceString_ReturnsValue()
    {
        // Arrange
        string value = "   \t\n\r";

        // Act
        string? result = value.EnsureIsWhiteSpace();

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void EnsureIsWhiteSpace_WithSpaceOnly_ReturnsValue()
    {
        // Arrange
        string value = " ";

        // Act
        string? result = value.EnsureIsWhiteSpace();

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void EnsureIsWhiteSpace_WithNull_ThrowsValidationException()
    {
        // Arrange
        string? value = null;

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsWhiteSpace());
        Assert.That(exception.Message, Does.Contain("Parameter must be whitespace"));
    }

    [Test]
    public void EnsureIsWhiteSpace_WithEmptyString_ThrowsValidationException()
    {
        // Arrange
        string value = "";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsWhiteSpace());
        Assert.That(exception.Message, Does.Contain("Parameter must be whitespace"));
    }

    [Test]
    public void EnsureIsWhiteSpace_WithNonWhitespaceString_ThrowsValidationException()
    {
        // Arrange
        string value = "hello";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsWhiteSpace());
        Assert.That(exception.Message, Does.Contain("Parameter must be whitespace"));
    }

    [Test]
    public void EnsureIsWhiteSpace_WithParameterName_ThrowsValidationExceptionWithParameterName()
    {
        // Arrange
        string value = "hello";
        string parameterName = "testParam";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsWhiteSpace(parameterName));
        Assert.That(exception.Message, Does.Contain("testParam"));
    }

    [Test]
    public void EnsureIsWhiteSpace_WithBlackboard_ThrowsValidationExceptionWithBlackboard()
    {
        // Arrange
        string value = "hello";
        var blackboard = new Blackboard();

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsWhiteSpace(null, blackboard));
        Assert.That(exception.Message, Does.Contain("Parameter must be whitespace"));
    }

    [Test]
    public void ValidateIsWhiteSpace_WithWhitespaceString_ReturnsValidResult()
    {
        // Arrange
        string value = "   \t\n\r";

        // Act
        ValidationResult result = value.ValidateIsWhiteSpace();

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsWhiteSpace_WithSpaceOnly_ReturnsValidResult()
    {
        // Arrange
        string value = " ";

        // Act
        ValidationResult result = value.ValidateIsWhiteSpace();

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsWhiteSpace_WithNull_ReturnsInvalidResult()
    {
        // Arrange
        string? value = null;

        // Act
        ValidationResult result = value.ValidateIsWhiteSpace();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be whitespace"));
    }

    [Test]
    public void ValidateIsWhiteSpace_WithEmptyString_ReturnsInvalidResult()
    {
        // Arrange
        string value = "";

        // Act
        ValidationResult result = value.ValidateIsWhiteSpace();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be whitespace"));
    }

    [Test]
    public void ValidateIsWhiteSpace_WithNonWhitespaceString_ReturnsInvalidResult()
    {
        // Arrange
        string value = "hello";

        // Act
        ValidationResult result = value.ValidateIsWhiteSpace();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be whitespace"));
    }

    [Test]
    public void ValidateIsWhiteSpace_WithParameterName_ReturnsInvalidResultWithParameterName()
    {
        // Arrange
        string value = "hello";
        string parameterName = "testParam";

        // Act
        ValidationResult result = value.ValidateIsWhiteSpace(parameterName);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("testParam"));
    }

    [Test]
    public void ValidateIsWhiteSpace_WithBlackboard_ReturnsInvalidResultWithBlackboard()
    {
        // Arrange
        string value = "hello";
        var blackboard = new Blackboard();

        // Act
        ValidationResult result = value.ValidateIsWhiteSpace(null, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be whitespace"));
    }

    [Test]
    public void ValidateIsWhiteSpace_WithMixedWhitespaceAndText_ReturnsInvalidResult()
    {
        // Arrange
        string value = "  hello  ";

        // Act
        ValidationResult result = value.ValidateIsWhiteSpace();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be whitespace"));
    }
}
