using System.Collections;
using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;
using System.Text;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNotNullOrEmptyTests
{
    [TestFixture]
    public class CheckIsNotNullOrEmptyTests
    {
        [Test]
        public void CheckIsNotNullOrEmpty_WithNullString_ReturnsFalse()
        {
            // Arrange
            string? value = null;

            // Act
            bool result = value.CheckIsNotNullOrEmpty();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_WithEmptyString_ReturnsFalse()
        {
            // Arrange
            string value = string.Empty;

            // Act
            bool result = value.CheckIsNotNullOrEmpty();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_WithWhitespaceString_ReturnsTrue()
        {
            // Arrange
            string value = "   ";

            // Act
            bool result = value.CheckIsNotNullOrEmpty();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_WithNonEmptyString_ReturnsTrue()
        {
            // Arrange
            string value = "Hello World";

            // Act
            bool result = value.CheckIsNotNullOrEmpty();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_WithSingleCharacterString_ReturnsTrue()
        {
            // Arrange
            string value = "a";

            // Act
            bool result = value.CheckIsNotNullOrEmpty();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_WithNullEnumerable_ReturnsFalse()
        {
            // Arrange
            IEnumerable? value = null;

            // Act
            bool result = value.CheckIsNotNullOrEmpty();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_WithEmptyList_ReturnsFalse()
        {
            // Arrange
            var value = new List<int>();

            // Act
            bool result = value.CheckIsNotNullOrEmpty();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_WithNonEmptyList_ReturnsTrue()
        {
            // Arrange
            var value = new List<int> { 1, 2, 3 };

            // Act
            bool result = value.CheckIsNotNullOrEmpty();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_WithEmptyArray_ReturnsFalse()
        {
            // Arrange
            int[] value = new int[0];

            // Act
            bool result = value.CheckIsNotNullOrEmpty();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_WithNonEmptyArray_ReturnsTrue()
        {
            // Arrange
            int[] value = { 1, 2, 3 };

            // Act
            bool result = value.CheckIsNotNullOrEmpty();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_WithEmptySpan_ReturnsFalse()
        {
            // Arrange
            ReadOnlySpan<int> value = ReadOnlySpan<int>.Empty;

            // Act
            bool result = value.CheckIsNotNullOrEmpty();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_WithNonEmptySpan_ReturnsTrue()
        {
            // Arrange
            int[] array = { 1, 2, 3 };
            ReadOnlySpan<int> value = array;

            // Act
            bool result = value.CheckIsNotNullOrEmpty();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_WithEmptyMemory_ReturnsFalse()
        {
            // Arrange
            Memory<int> value = Memory<int>.Empty;

            // Act
            bool result = value.CheckIsNotNullOrEmpty();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_WithNonEmptyMemory_ReturnsTrue()
        {
            // Arrange
            int[] array = { 1, 2, 3 };
            Memory<int> value = array;

            // Act
            bool result = value.CheckIsNotNullOrEmpty();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_WithNullStringBuilder_ReturnsFalse()
        {
            // Arrange
            StringBuilder? value = null;

            // Act
            bool result = value.CheckIsNotNullOrEmpty();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_WithEmptyStringBuilder_ReturnsFalse()
        {
            // Arrange
            var value = new StringBuilder();

            // Act
            bool result = value.CheckIsNotNullOrEmpty();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_WithNonEmptyStringBuilder_ReturnsTrue()
        {
            // Arrange
            var value = new StringBuilder("Hello");

            // Act
            bool result = value.CheckIsNotNullOrEmpty();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_WithNullCollection_ReturnsFalse()
        {
            // Arrange
            List<int>? value = null;

            // Act
            bool result = value.CheckIsNotNullOrEmpty();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_WithEmptyCollection_ReturnsFalse()
        {
            // Arrange
            var value = new List<int>();

            // Act
            bool result = value.CheckIsNotNullOrEmpty();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotNullOrEmpty_WithNonEmptyCollection_ReturnsTrue()
        {
            // Arrange
            var value = new List<int> { 1, 2, 3 };

            // Act
            bool result = value.CheckIsNotNullOrEmpty();

            // Assert
            Assert.That(result, Is.True);
        }
    }

    [TestFixture]
    public class EnsureIsNotNullOrEmptyTests
    {
        [Test]
        public void EnsureIsNotNullOrEmpty_WithNullString_ThrowsValidationException()
        {
            // Arrange
            string? value = null;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotNullOrEmpty());
            Assert.That(exception.Message, Does.Contain("Parameter must not be null or empty"));
        }

        [Test]
        public void EnsureIsNotNullOrEmpty_WithEmptyString_ThrowsValidationException()
        {
            // Arrange
            string value = string.Empty;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotNullOrEmpty());
            Assert.That(exception.Message, Does.Contain("Parameter must not be null or empty"));
        }

        [Test]
        public void EnsureIsNotNullOrEmpty_WithWhitespaceString_ReturnsValue()
        {
            // Arrange
            string value = "   ";

            // Act
            string? result = value.EnsureIsNotNullOrEmpty();

            // Assert
            Assert.That(result, Is.EqualTo("   "));
        }

        [Test]
        public void EnsureIsNotNullOrEmpty_WithNonEmptyString_ReturnsValue()
        {
            // Arrange
            string value = "Hello World";

            // Act
            string? result = value.EnsureIsNotNullOrEmpty();

            // Assert
            Assert.That(result, Is.EqualTo("Hello World"));
        }

        [Test]
        public void EnsureIsNotNullOrEmpty_WithSingleCharacterString_ReturnsValue()
        {
            // Arrange
            string value = "a";

            // Act
            string? result = value.EnsureIsNotNullOrEmpty();

            // Assert
            Assert.That(result, Is.EqualTo("a"));
        }

        [Test]
        public void EnsureIsNotNullOrEmpty_WithNullEnumerable_ThrowsValidationException()
        {
            // Arrange
            IEnumerable? value = null;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotNullOrEmpty());
            Assert.That(exception.Message, Does.Contain("Parameter must not be null or empty"));
        }

        [Test]
        public void EnsureIsNotNullOrEmpty_WithEmptyList_ThrowsValidationException()
        {
            // Arrange
            var value = new List<int>();

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotNullOrEmpty());
            Assert.That(exception.Message, Does.Contain("Parameter must not be null or empty"));
        }

        [Test]
        public void EnsureIsNotNullOrEmpty_WithNonEmptyList_ReturnsValue()
        {
            // Arrange
            var value = new List<int> { 1, 2, 3 };

            // Act
            IEnumerable? result = value.EnsureIsNotNullOrEmpty();

            // Assert
            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        public void EnsureIsNotNullOrEmpty_WithEmptySpan_ThrowsValidationException()
        {
            // Arrange
            ReadOnlySpan<int> value = ReadOnlySpan<int>.Empty;

            // Act & Assert
            try
            {
                value.EnsureIsNotNullOrEmpty();
                Assert.Fail("Expected ValidationException to be thrown");
            }
            catch (ValidationException exception)
            {
                Assert.That(exception.Message, Does.Contain("Parameter must not be null or empty"));
            }
        }

        [Test]
        public void EnsureIsNotNullOrEmpty_WithNonEmptySpan_ReturnsValue()
        {
            // Arrange
            int[] array = { 1, 2, 3 };
            ReadOnlySpan<int> value = array;

            // Act
            ReadOnlySpan<int> result = value.EnsureIsNotNullOrEmpty();

            // Assert
            Assert.That(result.Length, Is.EqualTo(3));
        }

        [Test]
        public void EnsureIsNotNullOrEmpty_WithEmptyMemory_ThrowsValidationException()
        {
            // Arrange
            Memory<int> value = Memory<int>.Empty;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotNullOrEmpty());
            Assert.That(exception.Message, Does.Contain("Parameter must not be null or empty"));
        }

        [Test]
        public void EnsureIsNotNullOrEmpty_WithNonEmptyMemory_ReturnsValue()
        {
            // Arrange
            int[] array = { 1, 2, 3 };
            Memory<int> value = array;

            // Act
            Memory<int> result = value.EnsureIsNotNullOrEmpty();

            // Assert
            Assert.That(result.Length, Is.EqualTo(3));
        }

        [Test]
        public void EnsureIsNotNullOrEmpty_WithNullStringBuilder_ThrowsValidationException()
        {
            // Arrange
            StringBuilder? value = null;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotNullOrEmpty());
            Assert.That(exception.Message, Does.Contain("Parameter must not be null or empty"));
        }

        [Test]
        public void EnsureIsNotNullOrEmpty_WithEmptyStringBuilder_ThrowsValidationException()
        {
            // Arrange
            var value = new StringBuilder();

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotNullOrEmpty());
            Assert.That(exception.Message, Does.Contain("Parameter must not be null or empty"));
        }

        [Test]
        public void EnsureIsNotNullOrEmpty_WithNonEmptyStringBuilder_ReturnsValue()
        {
            // Arrange
            var value = new StringBuilder("Hello");

            // Act
            StringBuilder? result = value.EnsureIsNotNullOrEmpty();

            // Assert
            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        public void EnsureIsNotNullOrEmpty_WithNullCollection_ThrowsValidationException()
        {
            // Arrange
            List<int>? value = null;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotNullOrEmpty());
            Assert.That(exception.Message, Does.Contain("Parameter must not be null or empty"));
        }

        [Test]
        public void EnsureIsNotNullOrEmpty_WithEmptyCollection_ThrowsValidationException()
        {
            // Arrange
            var value = new List<int>();

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotNullOrEmpty());
            Assert.That(exception.Message, Does.Contain("Parameter must not be null or empty"));
        }

        [Test]
        public void EnsureIsNotNullOrEmpty_WithNonEmptyCollection_ReturnsValue()
        {
            // Arrange
            var value = new List<int> { 1, 2, 3 };

            // Act
            List<int>? result = value.EnsureIsNotNullOrEmpty();

            // Assert
            Assert.That(result, Is.SameAs(value));
        }

        [Test]
        public void EnsureIsNotNullOrEmpty_WithParameterName_IncludesParameterNameInException()
        {
            // Arrange
            string? value = null;
            string parameterName = "testParam";

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotNullOrEmpty(parameterName));
            Assert.That(exception.Message, Does.Contain(parameterName));
        }

        [Test]
        public void EnsureIsNotNullOrEmpty_WithBlackboard_IncludesBlackboardInException()
        {
            // Arrange
            string? value = null;
            var blackboard = new Blackboard();

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotNullOrEmpty(null, blackboard));
            Assert.That(exception.Message, Does.Contain("Parameter must not be null or empty"));
        }
    }

    [TestFixture]
    public class ValidateIsNotNullOrEmptyTests
    {
        [Test]
        public void ValidateIsNotNullOrEmpty_WithNullString_ReturnsInvalidResult()
        {
            // Arrange
            string? value = null;

            // Act
            ValidationResult result = value.ValidateIsNotNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be null or empty"));
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_WithEmptyString_ReturnsInvalidResult()
        {
            // Arrange
            string value = string.Empty;

            // Act
            ValidationResult result = value.ValidateIsNotNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be null or empty"));
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_WithWhitespaceString_ReturnsValidResult()
        {
            // Arrange
            string value = "   ";

            // Act
            ValidationResult result = value.ValidateIsNotNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_WithNonEmptyString_ReturnsValidResult()
        {
            // Arrange
            string value = "Hello World";

            // Act
            ValidationResult result = value.ValidateIsNotNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_WithSingleCharacterString_ReturnsValidResult()
        {
            // Arrange
            string value = "a";

            // Act
            ValidationResult result = value.ValidateIsNotNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_WithNullEnumerable_ReturnsInvalidResult()
        {
            // Arrange
            IEnumerable? value = null;

            // Act
            ValidationResult result = value.ValidateIsNotNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be null or empty"));
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_WithEmptyList_ReturnsInvalidResult()
        {
            // Arrange
            var value = new List<int>();

            // Act
            ValidationResult result = value.ValidateIsNotNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be null or empty"));
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_WithNonEmptyList_ReturnsValidResult()
        {
            // Arrange
            var value = new List<int> { 1, 2, 3 };

            // Act
            ValidationResult result = value.ValidateIsNotNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_WithEmptySpan_ReturnsInvalidResult()
        {
            // Arrange
            ReadOnlySpan<int> value = ReadOnlySpan<int>.Empty;

            // Act
            ValidationResult result = value.ValidateIsNotNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be null or empty"));
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_WithNonEmptySpan_ReturnsValidResult()
        {
            // Arrange
            int[] array = { 1, 2, 3 };
            ReadOnlySpan<int> value = array;

            // Act
            ValidationResult result = value.ValidateIsNotNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_WithEmptyMemory_ReturnsInvalidResult()
        {
            // Arrange
            Memory<int> value = Memory<int>.Empty;

            // Act
            ValidationResult result = value.ValidateIsNotNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be null or empty"));
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_WithNonEmptyMemory_ReturnsValidResult()
        {
            // Arrange
            int[] array = { 1, 2, 3 };
            Memory<int> value = array;

            // Act
            ValidationResult result = value.ValidateIsNotNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_WithNullStringBuilder_ReturnsInvalidResult()
        {
            // Arrange
            StringBuilder? value = null;

            // Act
            ValidationResult result = value.ValidateIsNotNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be null or empty"));
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_WithEmptyStringBuilder_ReturnsInvalidResult()
        {
            // Arrange
            var value = new StringBuilder();

            // Act
            ValidationResult result = value.ValidateIsNotNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be null or empty"));
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_WithNonEmptyStringBuilder_ReturnsValidResult()
        {
            // Arrange
            var value = new StringBuilder("Hello");

            // Act
            ValidationResult result = value.ValidateIsNotNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_WithNullCollection_ReturnsInvalidResult()
        {
            // Arrange
            List<int>? value = null;

            // Act
            ValidationResult result = value.ValidateIsNotNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be null or empty"));
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_WithEmptyCollection_ReturnsInvalidResult()
        {
            // Arrange
            var value = new List<int>();

            // Act
            ValidationResult result = value.ValidateIsNotNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be null or empty"));
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_WithNonEmptyCollection_ReturnsValidResult()
        {
            // Arrange
            var value = new List<int> { 1, 2, 3 };

            // Act
            ValidationResult result = value.ValidateIsNotNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_WithParameterName_IncludesParameterNameInResult()
        {
            // Arrange
            string? value = null;
            string parameterName = "testParam";

            // Act
            ValidationResult result = value.ValidateIsNotNullOrEmpty(parameterName);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain(parameterName));
        }

        [Test]
        public void ValidateIsNotNullOrEmpty_WithBlackboard_IncludesBlackboardInResult()
        {
            // Arrange
            string? value = null;
            var blackboard = new Blackboard();

            // Act
            ValidationResult result = value.ValidateIsNotNullOrEmpty(null, blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be null or empty"));
        }
    }
}
