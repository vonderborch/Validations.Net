using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNullOrWhiteSpaceTests
{
    [TestFixture]
    public class CheckIsNullOrWhiteSpaceTests
    {
        [Test]
        public void CheckIsNullOrWhiteSpace_WithNullString_ReturnsTrue()
        {
            // Arrange
            string? value = null;

            // Act
            bool result = value.CheckIsNullOrWhiteSpace();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNullOrWhiteSpace_WithEmptyString_ReturnsTrue()
        {
            // Arrange
            string value = string.Empty;

            // Act
            bool result = value.CheckIsNullOrWhiteSpace();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNullOrWhiteSpace_WithWhitespaceString_ReturnsTrue()
        {
            // Arrange
            string value = "   ";

            // Act
            bool result = value.CheckIsNullOrWhiteSpace();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNullOrWhiteSpace_WithTabString_ReturnsTrue()
        {
            // Arrange
            string value = "\t";

            // Act
            bool result = value.CheckIsNullOrWhiteSpace();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNullOrWhiteSpace_WithNewlineString_ReturnsTrue()
        {
            // Arrange
            string value = "\n";

            // Act
            bool result = value.CheckIsNullOrWhiteSpace();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNullOrWhiteSpace_WithMixedWhitespaceString_ReturnsTrue()
        {
            // Arrange
            string value = " \t\n\r";

            // Act
            bool result = value.CheckIsNullOrWhiteSpace();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNullOrWhiteSpace_WithNonEmptyString_ReturnsFalse()
        {
            // Arrange
            string value = "Hello World";

            // Act
            bool result = value.CheckIsNullOrWhiteSpace();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNullOrWhiteSpace_WithStringWithLeadingWhitespace_ReturnsFalse()
        {
            // Arrange
            string value = "  Hello";

            // Act
            bool result = value.CheckIsNullOrWhiteSpace();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNullOrWhiteSpace_WithStringWithTrailingWhitespace_ReturnsFalse()
        {
            // Arrange
            string value = "Hello  ";

            // Act
            bool result = value.CheckIsNullOrWhiteSpace();

            // Assert
            Assert.That(result, Is.False);
        }
    }

    [TestFixture]
    public class EnsureIsNullOrWhiteSpaceTests
    {
        [Test]
        public void EnsureIsNullOrWhiteSpace_WithNullString_ReturnsValue()
        {
            // Arrange
            string? value = null;

            // Act
            string? result = value.EnsureIsNullOrWhiteSpace();

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void EnsureIsNullOrWhiteSpace_WithEmptyString_ReturnsValue()
        {
            // Arrange
            string value = string.Empty;

            // Act
            string? result = value.EnsureIsNullOrWhiteSpace();

            // Assert
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void EnsureIsNullOrWhiteSpace_WithWhitespaceString_ReturnsValue()
        {
            // Arrange
            string value = "   ";

            // Act
            string? result = value.EnsureIsNullOrWhiteSpace();

            // Assert
            Assert.That(result, Is.EqualTo("   "));
        }

        [Test]
        public void EnsureIsNullOrWhiteSpace_WithNonEmptyString_ThrowsValidationException()
        {
            // Arrange
            string value = "Hello World";

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNullOrWhiteSpace());
            Assert.That(exception.Message, Does.Contain("Parameter must be null or whitespace"));
        }

        [Test]
        public void EnsureIsNullOrWhiteSpace_WithStringWithLeadingWhitespace_ThrowsValidationException()
        {
            // Arrange
            string value = "  Hello";

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNullOrWhiteSpace());
            Assert.That(exception.Message, Does.Contain("Parameter must be null or whitespace"));
        }

        [Test]
        public void EnsureIsNullOrWhiteSpace_WithParameterName_IncludesParameterNameInException()
        {
            // Arrange
            string value = "test";
            string parameterName = "testParam";

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNullOrWhiteSpace(parameterName));
            Assert.That(exception.Message, Does.Contain(parameterName));
        }

        [Test]
        public void EnsureIsNullOrWhiteSpace_WithBlackboard_IncludesBlackboardInException()
        {
            // Arrange
            string value = "test";
            var blackboard = new Blackboard();

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNullOrWhiteSpace(null, blackboard));
            Assert.That(exception.Message, Does.Contain("Parameter must be null or whitespace"));
        }
    }

    [TestFixture]
    public class ValidateIsNullOrWhiteSpaceTests
    {
        [Test]
        public void ValidateIsNullOrWhiteSpace_WithNullString_ReturnsValidResult()
        {
            // Arrange
            string? value = null;

            // Act
            ValidationResult result = value.ValidateIsNullOrWhiteSpace();

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNullOrWhiteSpace_WithEmptyString_ReturnsValidResult()
        {
            // Arrange
            string value = string.Empty;

            // Act
            ValidationResult result = value.ValidateIsNullOrWhiteSpace();

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNullOrWhiteSpace_WithWhitespaceString_ReturnsValidResult()
        {
            // Arrange
            string value = "   ";

            // Act
            ValidationResult result = value.ValidateIsNullOrWhiteSpace();

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNullOrWhiteSpace_WithNonEmptyString_ReturnsInvalidResult()
        {
            // Arrange
            string value = "Hello World";

            // Act
            ValidationResult result = value.ValidateIsNullOrWhiteSpace();

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be null or whitespace"));
        }

        [Test]
        public void ValidateIsNullOrWhiteSpace_WithStringWithLeadingWhitespace_ReturnsInvalidResult()
        {
            // Arrange
            string value = "  Hello";

            // Act
            ValidationResult result = value.ValidateIsNullOrWhiteSpace();

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be null or whitespace"));
        }

        [Test]
        public void ValidateIsNullOrWhiteSpace_WithParameterName_IncludesParameterNameInResult()
        {
            // Arrange
            string value = "test";
            string parameterName = "testParam";

            // Act
            ValidationResult result = value.ValidateIsNullOrWhiteSpace(parameterName);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain(parameterName));
        }

        [Test]
        public void ValidateIsNullOrWhiteSpace_WithBlackboard_IncludesBlackboardInResult()
        {
            // Arrange
            string value = "test";
            var blackboard = new Blackboard();

            // Act
            ValidationResult result = value.ValidateIsNullOrWhiteSpace(null, blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be null or whitespace"));
        }
    }
}
