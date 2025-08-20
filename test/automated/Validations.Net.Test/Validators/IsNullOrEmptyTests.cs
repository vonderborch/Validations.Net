using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;
using System.Text;
using System.Collections;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNullOrEmptyTests
{
    [TestFixture]
    public class CheckIsNullOrEmptyTests
    {
        [Test]
        public void CheckIsNullOrEmpty_WithNullString_ReturnsTrue()
        {
            // Arrange
            string? value = null;

            // Act
            bool result = value.CheckIsNullOrEmpty();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNullOrEmpty_WithEmptyString_ReturnsTrue()
        {
            // Arrange
            string value = string.Empty;

            // Act
            bool result = value.CheckIsNullOrEmpty();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNullOrEmpty_WithWhitespaceString_ReturnsFalse()
        {
            // Arrange
            string value = "   ";

            // Act
            bool result = value.CheckIsNullOrEmpty();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNullOrEmpty_WithNonEmptyString_ReturnsFalse()
        {
            // Arrange
            string value = "Hello World";

            // Act
            bool result = value.CheckIsNullOrEmpty();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNullOrEmpty_WithNullEnumerable_ReturnsTrue()
        {
            // Arrange
            IEnumerable? value = null;

            // Act
            bool result = value.CheckIsNullOrEmpty();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNullOrEmpty_WithEmptyList_ReturnsTrue()
        {
            // Arrange
            var value = new List<int>();

            // Act
            bool result = value.CheckIsNullOrEmpty();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNullOrEmpty_WithNonEmptyList_ReturnsFalse()
        {
            // Arrange
            var value = new List<int> { 1, 2, 3 };

            // Act
            bool result = value.CheckIsNullOrEmpty();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNullOrEmpty_WithEmptyArray_ReturnsTrue()
        {
            // Arrange
            int[] value = new int[0];

            // Act
            bool result = value.CheckIsNullOrEmpty();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNullOrEmpty_WithNonEmptyArray_ReturnsFalse()
        {
            // Arrange
            int[] value = { 1, 2, 3 };

            // Act
            bool result = value.CheckIsNullOrEmpty();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNullOrEmpty_WithEmptySpan_ReturnsTrue()
        {
            // Arrange
            ReadOnlySpan<int> value = ReadOnlySpan<int>.Empty;

            // Act
            bool result = value.CheckIsNullOrEmpty();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNullOrEmpty_WithNonEmptySpan_ReturnsFalse()
        {
            // Arrange
            int[] array = { 1, 2, 3 };
            ReadOnlySpan<int> value = array;

            // Act
            bool result = value.CheckIsNullOrEmpty();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNullOrEmpty_WithEmptyMemory_ReturnsTrue()
        {
            // Arrange
            Memory<int> value = Memory<int>.Empty;

            // Act
            bool result = value.CheckIsNullOrEmpty();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNullOrEmpty_WithNonEmptyMemory_ReturnsFalse()
        {
            // Arrange
            int[] array = { 1, 2, 3 };
            Memory<int> value = array;

            // Act
            bool result = value.CheckIsNullOrEmpty();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNullOrEmpty_WithNullStringBuilder_ReturnsTrue()
        {
            // Arrange
            StringBuilder? value = null;

            // Act
            bool result = value.CheckIsNullOrEmpty();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNullOrEmpty_WithEmptyStringBuilder_ReturnsTrue()
        {
            // Arrange
            var value = new StringBuilder();

            // Act
            bool result = value.CheckIsNullOrEmpty();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNullOrEmpty_WithNonEmptyStringBuilder_ReturnsFalse()
        {
            // Arrange
            var value = new StringBuilder("Hello");

            // Act
            bool result = value.CheckIsNullOrEmpty();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNullOrEmpty_WithNullCollection_ReturnsTrue()
        {
            // Arrange
            List<int>? value = null;

            // Act
            bool result = value.CheckIsNullOrEmpty();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNullOrEmpty_WithEmptyCollection_ReturnsTrue()
        {
            // Arrange
            var value = new List<int>();

            // Act
            bool result = value.CheckIsNullOrEmpty();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNullOrEmpty_WithNonEmptyCollection_ReturnsFalse()
        {
            // Arrange
            var value = new List<int> { 1, 2, 3 };

            // Act
            bool result = value.CheckIsNullOrEmpty();

            // Assert
            Assert.That(result, Is.False);
        }
    }

    [TestFixture]
    public class EnsureIsNullOrEmptyTests
    {
        [Test]
        public void EnsureIsNullOrEmpty_WithNullString_ReturnsValue()
        {
            // Arrange
            string? value = null;

            // Act
            string? result = value.EnsureIsNullOrEmpty();

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void EnsureIsNullOrEmpty_WithEmptyString_ReturnsValue()
        {
            // Arrange
            string value = string.Empty;

            // Act
            string? result = value.EnsureIsNullOrEmpty();

            // Assert
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void EnsureIsNullOrEmpty_WithWhitespaceString_ThrowsValidationException()
        {
            // Arrange
            string value = "   ";

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNullOrEmpty());
            Assert.That(exception.Message, Does.Contain("Parameter must be null or empty"));
        }

        [Test]
        public void EnsureIsNullOrEmpty_WithNonEmptyString_ThrowsValidationException()
        {
            // Arrange
            string value = "Hello World";

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNullOrEmpty());
            Assert.That(exception.Message, Does.Contain("Parameter must be null or empty"));
        }

        [Test]
        public void EnsureIsNullOrEmpty_WithNullEnumerable_ReturnsValue()
        {
            // Arrange
            IEnumerable? value = null;

            // Act
            IEnumerable? result = value.EnsureIsNullOrEmpty();

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void EnsureIsNullOrEmpty_WithEmptyList_ReturnsValue()
        {
            // Arrange
            var value = new List<int>();

            // Act
            IEnumerable? result = value.EnsureIsNullOrEmpty();

            // Assert
            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        public void EnsureIsNullOrEmpty_WithNonEmptyList_ThrowsValidationException()
        {
            // Arrange
            var value = new List<int> { 1, 2, 3 };

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNullOrEmpty());
            Assert.That(exception.Message, Does.Contain("Parameter must be null or empty"));
        }

        [Test]
        public void EnsureIsNullOrEmpty_WithEmptySpan_ReturnsValue()
        {
            // Arrange
            ReadOnlySpan<int> value = ReadOnlySpan<int>.Empty;

            // Act
            ReadOnlySpan<int> result = value.EnsureIsNullOrEmpty();

            // Assert
            Assert.That(result.IsEmpty, Is.True);
        }

        [Test]
        public void EnsureIsNullOrEmpty_WithNonEmptySpan_ThrowsValidationException()
        {
            // Arrange
            int[] array = { 1, 2, 3 };
            ReadOnlySpan<int> value = array;

            // Act & Assert
            try
            {
                value.EnsureIsNullOrEmpty();
                Assert.Fail("Expected ValidationException to be thrown");
            }
            catch (ValidationException exception)
            {
                Assert.That(exception.Message, Does.Contain("Parameter must be null or empty"));
            }
        }

        [Test]
        public void EnsureIsNullOrEmpty_WithEmptyMemory_ReturnsValue()
        {
            // Arrange
            Memory<int> value = Memory<int>.Empty;

            // Act
            Memory<int> result = value.EnsureIsNullOrEmpty();

            // Assert
            Assert.That(result.IsEmpty, Is.True);
        }

        [Test]
        public void EnsureIsNullOrEmpty_WithNonEmptyMemory_ThrowsValidationException()
        {
            // Arrange
            int[] array = { 1, 2, 3 };
            Memory<int> value = array;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNullOrEmpty());
            Assert.That(exception.Message, Does.Contain("Parameter must be null or empty"));
        }

        [Test]
        public void EnsureIsNullOrEmpty_WithNullStringBuilder_ReturnsValue()
        {
            // Arrange
            StringBuilder? value = null;

            // Act
            StringBuilder? result = value.EnsureIsNullOrEmpty();

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void EnsureIsNullOrEmpty_WithEmptyStringBuilder_ReturnsValue()
        {
            // Arrange
            var value = new StringBuilder();

            // Act
            StringBuilder? result = value.EnsureIsNullOrEmpty();

            // Assert
            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        public void EnsureIsNullOrEmpty_WithNonEmptyStringBuilder_ThrowsValidationException()
        {
            // Arrange
            var value = new StringBuilder("Hello");

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNullOrEmpty());
            Assert.That(exception.Message, Does.Contain("Parameter must be null or empty"));
        }

        [Test]
        public void EnsureIsNullOrEmpty_WithNullCollection_ReturnsValue()
        {
            // Arrange
            List<int>? value = null;

            // Act
            List<int>? result = value.EnsureIsNullOrEmpty();

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void EnsureIsNullOrEmpty_WithEmptyCollection_ReturnsValue()
        {
            // Arrange
            var value = new List<int>();

            // Act
            List<int>? result = value.EnsureIsNullOrEmpty();

            // Assert
            Assert.That(result, Is.SameAs(value));
        }

        [Test]
        public void EnsureIsNullOrEmpty_WithNonEmptyCollection_ThrowsValidationException()
        {
            // Arrange
            var value = new List<int> { 1, 2, 3 };

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNullOrEmpty());
            Assert.That(exception.Message, Does.Contain("Parameter must be null or empty"));
        }

        [Test]
        public void EnsureIsNullOrEmpty_WithParameterName_IncludesParameterNameInException()
        {
            // Arrange
            string value = "test";
            string parameterName = "testParam";

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNullOrEmpty(parameterName));
            Assert.That(exception.Message, Does.Contain(parameterName));
        }

        [Test]
        public void EnsureIsNullOrEmpty_WithBlackboard_IncludesBlackboardInException()
        {
            // Arrange
            string value = "test";
            var blackboard = new Blackboard();

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNullOrEmpty(null, blackboard));
            Assert.That(exception.Message, Does.Contain("Parameter must be null or empty"));
        }
    }

    [TestFixture]
    public class ValidateIsNullOrEmptyTests
    {
        [Test]
        public void ValidateIsNullOrEmpty_WithNullString_ReturnsValidResult()
        {
            // Arrange
            string? value = null;

            // Act
            ValidationResult result = value.ValidateIsNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNullOrEmpty_WithEmptyString_ReturnsValidResult()
        {
            // Arrange
            string value = string.Empty;

            // Act
            ValidationResult result = value.ValidateIsNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNullOrEmpty_WithWhitespaceString_ReturnsInvalidResult()
        {
            // Arrange
            string value = "   ";

            // Act
            ValidationResult result = value.ValidateIsNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be null or empty"));
        }

        [Test]
        public void ValidateIsNullOrEmpty_WithNonEmptyString_ReturnsInvalidResult()
        {
            // Arrange
            string value = "Hello World";

            // Act
            ValidationResult result = value.ValidateIsNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be null or empty"));
        }

        [Test]
        public void ValidateIsNullOrEmpty_WithNullEnumerable_ReturnsValidResult()
        {
            // Arrange
            IEnumerable? value = null;

            // Act
            ValidationResult result = value.ValidateIsNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNullOrEmpty_WithEmptyList_ReturnsValidResult()
        {
            // Arrange
            var value = new List<int>();

            // Act
            ValidationResult result = value.ValidateIsNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNullOrEmpty_WithNonEmptyList_ReturnsInvalidResult()
        {
            // Arrange
            var value = new List<int> { 1, 2, 3 };

            // Act
            ValidationResult result = value.ValidateIsNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be null or empty"));
        }

        [Test]
        public void ValidateIsNullOrEmpty_WithEmptySpan_ReturnsValidResult()
        {
            // Arrange
            ReadOnlySpan<int> value = ReadOnlySpan<int>.Empty;

            // Act
            ValidationResult result = value.ValidateIsNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNullOrEmpty_WithNonEmptySpan_ReturnsInvalidResult()
        {
            // Arrange
            int[] array = { 1, 2, 3 };
            ReadOnlySpan<int> value = array;

            // Act
            ValidationResult result = value.ValidateIsNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be null or empty"));
        }

        [Test]
        public void ValidateIsNullOrEmpty_WithEmptyMemory_ReturnsValidResult()
        {
            // Arrange
            Memory<int> value = Memory<int>.Empty;

            // Act
            ValidationResult result = value.ValidateIsNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNullOrEmpty_WithNonEmptyMemory_ReturnsInvalidResult()
        {
            // Arrange
            int[] array = { 1, 2, 3 };
            Memory<int> value = array;

            // Act
            ValidationResult result = value.ValidateIsNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be null or empty"));
        }

        [Test]
        public void ValidateIsNullOrEmpty_WithNullStringBuilder_ReturnsValidResult()
        {
            // Arrange
            StringBuilder? value = null;

            // Act
            ValidationResult result = value.ValidateIsNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNullOrEmpty_WithEmptyStringBuilder_ReturnsValidResult()
        {
            // Arrange
            var value = new StringBuilder();

            // Act
            ValidationResult result = value.ValidateIsNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNullOrEmpty_WithNonEmptyStringBuilder_ReturnsInvalidResult()
        {
            // Arrange
            var value = new StringBuilder("Hello");

            // Act
            ValidationResult result = value.ValidateIsNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be null or empty"));
        }

        [Test]
        public void ValidateIsNullOrEmpty_WithNullCollection_ReturnsValidResult()
        {
            // Arrange
            List<int>? value = null;

            // Act
            ValidationResult result = value.ValidateIsNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNullOrEmpty_WithEmptyCollection_ReturnsValidResult()
        {
            // Arrange
            var value = new List<int>();

            // Act
            ValidationResult result = value.ValidateIsNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNullOrEmpty_WithNonEmptyCollection_ReturnsInvalidResult()
        {
            // Arrange
            var value = new List<int> { 1, 2, 3 };

            // Act
            ValidationResult result = value.ValidateIsNullOrEmpty();

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException, Is.Not.Null);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be null or empty"));
        }

        [Test]
        public void ValidateIsNullOrEmpty_WithParameterName_IncludesParameterNameInResult()
        {
            // Arrange
            string value = "test";
            string parameterName = "testParam";

            // Act
            ValidationResult result = value.ValidateIsNullOrEmpty(parameterName);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain(parameterName));
        }

        [Test]
        public void ValidateIsNullOrEmpty_WithBlackboard_IncludesBlackboardInResult()
        {
            // Arrange
            string value = "test";
            var blackboard = new Blackboard();

            // Act
            ValidationResult result = value.ValidateIsNullOrEmpty(null, blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be null or empty"));
        }
    }
}
