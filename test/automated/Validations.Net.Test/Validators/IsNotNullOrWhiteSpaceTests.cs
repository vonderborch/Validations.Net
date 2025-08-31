using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNotNullOrWhiteSpaceTests
{
    [TestFixture]
    public class CheckIsNotNullOrWhiteSpaceTests
    {
        [Test]
        public void CheckIsNotNullOrWhiteSpace_WithNullString_ReturnsFalse()
        {
            // Arrange
            string? value = null;

            // Act
            bool result = value.CheckIsNotNullOrWhiteSpace();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotNullOrWhiteSpace_WithEmptyString_ReturnsFalse()
        {
            // Arrange
            string value = string.Empty;

            // Act
            bool result = value.CheckIsNotNullOrWhiteSpace();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotNullOrWhiteSpace_WithWhitespaceString_ReturnsFalse()
        {
            // Arrange
            string value = "   ";

            // Act
            bool result = value.CheckIsNotNullOrWhiteSpace();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotNullOrWhiteSpace_WithTabString_ReturnsFalse()
        {
            // Arrange
            string value = "\t";

            // Act
            bool result = value.CheckIsNotNullOrWhiteSpace();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotNullOrWhiteSpace_WithNewlineString_ReturnsFalse()
        {
            // Arrange
            string value = "\n";

            // Act
            bool result = value.CheckIsNotNullOrWhiteSpace();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotNullOrWhiteSpace_WithMixedWhitespaceString_ReturnsFalse()
        {
            // Arrange
            string value = " \t\n\r";

            // Act
            bool result = value.CheckIsNotNullOrWhiteSpace();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotNullOrWhiteSpace_WithNonEmptyString_ReturnsTrue()
        {
            // Arrange
            string value = "Hello World";

            // Act
            bool result = value.CheckIsNotNullOrWhiteSpace();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotNullOrWhiteSpace_WithStringWithLeadingWhitespace_ReturnsTrue()
        {
            // Arrange
            string value = "  Hello";

            // Act
            bool result = value.CheckIsNotNullOrWhiteSpace();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotNullOrWhiteSpace_WithStringWithTrailingWhitespace_ReturnsTrue()
        {
            // Arrange
            string value = "Hello  ";

            // Act
            bool result = value.CheckIsNotNullOrWhiteSpace();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotNullOrWhiteSpace_WithSingleCharacterString_ReturnsTrue()
        {
            // Arrange
            string value = "a";

            // Act
            bool result = value.CheckIsNotNullOrWhiteSpace();

            // Assert
            Assert.That(result, Is.True);
        }
    }

    [TestFixture]
    public class EnsureIsNotNullOrWhiteSpaceTests
    {
        [Test]
        public void EnsureIsNotNullOrWhiteSpace_WithNullString_ThrowsValidationException()
        {
            // Arrange
            string? value = null;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotNullOrWhiteSpace());
            Assert.That(exception.Message, Does.Contain("Parameter must not be null or whitespace"));
        }

        [Test]
        public void EnsureIsNotNullOrWhiteSpace_WithEmptyString_ThrowsValidationException()
        {
            // Arrange
            string value = string.Empty;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotNullOrWhiteSpace());
            Assert.That(exception.Message, Does.Contain("Parameter must not be null or whitespace"));
        }

        [Test]
        public void EnsureIsNotNullOrWhiteSpace_WithWhitespaceString_ThrowsValidationException()
        {
            // Arrange
            string value = "   ";

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotNullOrWhiteSpace());
            Assert.That(exception.Message, Does.Contain("Parameter must not be null or whitespace"));
        }

        [Test]
        public void EnsureIsNotNullOrWhiteSpace_WithTabString_ThrowsValidationException()
        {
            // Arrange
            string value = "\t";

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotNullOrWhiteSpace());
            Assert.That(exception.Message, Does.Contain("Parameter must not be null or whitespace"));
        }

        [Test]
        public void EnsureIsNotNullOrWhiteSpace_WithNonEmptyString_ReturnsValue()
        {
            // Arrange
            string value = "Hello World";

            // Act
            string? result = value.EnsureIsNotNullOrWhiteSpace();

            // Assert
            Assert.That(result, Is.EqualTo("Hello World"));
        }

        [Test]
        public void EnsureIsNotNullOrWhiteSpace_WithStringWithLeadingWhitespace_ReturnsValue()
        {
            // Arrange
            string value = "  Hello";

            // Act
            string? result = value.EnsureIsNotNullOrWhiteSpace();

            // Assert
            Assert.That(result, Is.EqualTo("  Hello"));
        }

        [Test]
        public void EnsureIsNotNullOrWhiteSpace_WithSingleCharacterString_ReturnsValue()
        {
            // Arrange
            string value = "a";

            // Act
            string? result = value.EnsureIsNotNullOrWhiteSpace();

            // Assert
            Assert.That(result, Is.EqualTo("a"));
        }

        [Test]
        public void EnsureIsNotNullOrWhiteSpace_WithParameterName_IncludesParameterNameInException()
        {
            // Arrange
            string? value = null;
            string parameterName = "testParam";

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotNullOrWhiteSpace(null, parameterName));
            Assert.That(exception.Message, Does.Contain(parameterName));
        }

        [Test]
        public void EnsureIsNotNullOrWhiteSpace_WithBlackboard_IncludesBlackboardInException()
        {
            // Arrange
            string? value = null;
            var blackboard = new Blackboard();

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotNullOrWhiteSpace(blackboard));
            Assert.That(exception.Message, Does.Contain("Parameter must not be null or whitespace"));
        }
    }

    [TestFixture]
    public class ValidateIsNotNullOrWhiteSpaceTests
    {
        [Test]
        public void ValidateIsNotNullOrWhiteSpace_WithNullString_ReturnsInvalidResult()
        {
            // Arrange
            string? value = null;

            // Act
            ValidationResult result = value.ValidateIsNotNullOrWhiteSpace();

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be null or whitespace"));
        }

        [Test]
        public void ValidateIsNotNullOrWhiteSpace_WithEmptyString_ReturnsInvalidResult()
        {
            // Arrange
            string value = string.Empty;

            // Act
            ValidationResult result = value.ValidateIsNotNullOrWhiteSpace();

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be null or whitespace"));
        }

        [Test]
        public void ValidateIsNotNullOrWhiteSpace_WithWhitespaceString_ReturnsInvalidResult()
        {
            // Arrange
            string value = "   ";

            // Act
            ValidationResult result = value.ValidateIsNotNullOrWhiteSpace();

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be null or whitespace"));
        }

        [Test]
        public void ValidateIsNotNullOrWhiteSpace_WithTabString_ReturnsInvalidResult()
        {
            // Arrange
            string value = "\t";

            // Act
            ValidationResult result = value.ValidateIsNotNullOrWhiteSpace();

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be null or whitespace"));
        }

        [Test]
        public void ValidateIsNotNullOrWhiteSpace_WithNonEmptyString_ReturnsValidResult()
        {
            // Arrange
            string value = "Hello World";

            // Act
            ValidationResult result = value.ValidateIsNotNullOrWhiteSpace();

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNotNullOrWhiteSpace_WithStringWithLeadingWhitespace_ReturnsValidResult()
        {
            // Arrange
            string value = "  Hello";

            // Act
            ValidationResult result = value.ValidateIsNotNullOrWhiteSpace();

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNotNullOrWhiteSpace_WithSingleCharacterString_ReturnsValidResult()
        {
            // Arrange
            string value = "a";

            // Act
            ValidationResult result = value.ValidateIsNotNullOrWhiteSpace();

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNotNullOrWhiteSpace_WithParameterName_IncludesParameterNameInResult()
        {
            // Arrange
            string? value = null;
            string parameterName = "testParam";

            // Act
            ValidationResult result = value.ValidateIsNotNullOrWhiteSpace(null, parameterName);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain(parameterName));
        }

        [Test]
        public void ValidateIsNotNullOrWhiteSpace_WithBlackboard_IncludesBlackboardInResult()
        {
            // Arrange
            string? value = null;
            var blackboard = new Blackboard();

            // Act
            ValidationResult result = value.ValidateIsNotNullOrWhiteSpace(blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be null or whitespace"));
        }
    }
}
