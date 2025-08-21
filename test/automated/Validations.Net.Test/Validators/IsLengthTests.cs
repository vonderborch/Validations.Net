using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;
using System.Text;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsLengthTests
{
    [TestFixture]
    public class CheckIsLengthTests
    {
        [Test]
        public void CheckIsLength_WithStringExactLength_ReturnsTrue()
        {
            // Arrange
            var value = "Hello";
            var expectedLength = 5;

            // Act
            var result = value.CheckIsLength(expectedLength, LengthCheckMode.ExactLength);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsLength_WithStringWrongLength_ReturnsFalse()
        {
            // Arrange
            var value = "Hello";
            var expectedLength = 3;

            // Act
            var result = value.CheckIsLength(expectedLength, LengthCheckMode.ExactLength);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsLength_WithNullString_ReturnsFalse()
        {
            // Arrange
            string? value = null;
            var expectedLength = 0;

            // Act
            var result = value.CheckIsLength(expectedLength, LengthCheckMode.ExactLength);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsLength_WithStringLessThan_ReturnsTrue()
        {
            // Arrange
            var value = "Hi";
            var expectedLength = 5;

            // Act
            var result = value.CheckIsLength(expectedLength, LengthCheckMode.LessThan);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsLength_WithStringLessThanOrEqual_ReturnsTrue()
        {
            // Arrange
            var value = "Hi";
            var expectedLength = 2;

            // Act
            var result = value.CheckIsLength(expectedLength, LengthCheckMode.LessThanOrEqual);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsLength_WithStringGreaterThan_ReturnsTrue()
        {
            // Arrange
            var value = "Hello World";
            var expectedLength = 5;

            // Act
            var result = value.CheckIsLength(expectedLength, LengthCheckMode.GreaterThan);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsLength_WithStringGreaterThanOrEqual_ReturnsTrue()
        {
            // Arrange
            var value = "Hello";
            var expectedLength = 5;

            // Act
            var result = value.CheckIsLength(expectedLength, LengthCheckMode.GreaterThanOrEqual);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsLength_WithListExactLength_ReturnsTrue()
        {
            // Arrange
            var value = new List<int> { 1, 2, 3 };
            var expectedLength = 3;

            // Act
            var result = value.CheckIsLength(expectedLength, LengthCheckMode.ExactLength);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsLength_WithArrayExactLength_ReturnsTrue()
        {
            // Arrange
            var value = new int[] { 1, 2, 3, 4 };
            var expectedLength = 4;

            // Act
            var result = value.CheckIsLength(expectedLength, LengthCheckMode.ExactLength);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsLength_WithNullCollection_ReturnsFalse()
        {
            // Arrange
            System.Collections.IEnumerable? value = null;
            var expectedLength = 0;

            // Act
            var result = value.CheckIsLength(expectedLength, LengthCheckMode.ExactLength);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsLength_WithSpanExactLength_ReturnsTrue()
        {
            // Arrange
            var array = new int[] { 1, 2, 3 };
            var value = new Span<int>(array);
            var expectedLength = 3;

            // Act
            var result = value.CheckIsLength(expectedLength, LengthCheckMode.ExactLength);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsLength_WithReadOnlySpanExactLength_ReturnsTrue()
        {
            // Arrange
            var array = new int[] { 1, 2, 3, 4, 5 };
            var value = new ReadOnlySpan<int>(array);
            var expectedLength = 5;

            // Act
            var result = value.CheckIsLength(expectedLength, LengthCheckMode.ExactLength);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsLength_WithMemoryExactLength_ReturnsTrue()
        {
            // Arrange
            var array = new int[] { 1, 2, 3, 4 };
            var value = new Memory<int>(array);
            var expectedLength = 4;

            // Act
            var result = value.CheckIsLength(expectedLength, LengthCheckMode.ExactLength);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsLength_WithStringBuilderExactLength_ReturnsTrue()
        {
            // Arrange
            var value = new StringBuilder("Hello");
            var expectedLength = 5;

            // Act
            var result = value.CheckIsLength(expectedLength, LengthCheckMode.ExactLength);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsLength_WithNullStringBuilder_ReturnsFalse()
        {
            // Arrange
            System.Text.StringBuilder? value = null;
            var expectedLength = 0;

            // Act
            var result = value.CheckIsLength(expectedLength, LengthCheckMode.ExactLength);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsLength_WithICollectionExactLength_ReturnsTrue()
        {
            // Arrange
            ICollection<int> value = new List<int> { 1, 2, 3, 4, 5 };
            var expectedLength = 5;

            // Act
            var result = value.CheckIsLength(expectedLength, LengthCheckMode.ExactLength);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsLength_WithNullICollection_ReturnsFalse()
        {
            // Arrange
            ICollection<int>? value = null;
            var expectedLength = 0;

            // Act
            var result = value.CheckIsLength(expectedLength, LengthCheckMode.ExactLength);

            // Assert
            Assert.That(result, Is.False);
        }
    }

    [TestFixture]
    public class EnsureIsLengthTests
    {
        [Test]
        public void EnsureIsLength_WithStringExactLength_DoesNotThrow()
        {
            // Arrange
            var value = "Hello";
            var expectedLength = 5;

            // Act & Assert
            Assert.DoesNotThrow(() => value.EnsureIsLength(expectedLength, LengthCheckMode.ExactLength));
        }

        [Test]
        public void EnsureIsLength_WithStringWrongLength_ThrowsValidationException()
        {
            // Arrange
            var value = "Hello";
            var expectedLength = 3;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsLength(expectedLength, LengthCheckMode.ExactLength));
            Assert.That(exception.Message, Does.Contain("string length must be exactly 3"));
            Assert.That(exception.Message, Does.Contain("Actual length: 5"));
        }

        [Test]
        public void EnsureIsLength_WithNullString_ThrowsValidationException()
        {
            // Arrange
            string? value = null;
            var expectedLength = 0;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsLength(expectedLength, LengthCheckMode.ExactLength));
            Assert.That(exception.Message, Does.Contain("string length must be exactly 0"));
            Assert.That(exception.Message, Does.Contain("Actual length: null"));
        }

        [Test]
        public void EnsureIsLength_WithStringLessThan_DoesNotThrow()
        {
            // Arrange
            var value = "Hi";
            var expectedLength = 5;

            // Act & Assert
            Assert.DoesNotThrow(() => value.EnsureIsLength(expectedLength, LengthCheckMode.LessThan));
        }

        [Test]
        public void EnsureIsLength_WithStringGreaterThan_DoesNotThrow()
        {
            // Arrange
            var value = "Hello World";
            var expectedLength = 5;

            // Act & Assert
            Assert.DoesNotThrow(() => value.EnsureIsLength(expectedLength, LengthCheckMode.GreaterThan));
        }

        [Test]
        public void EnsureIsLength_WithListExactLength_DoesNotThrow()
        {
            // Arrange
            var value = new List<int> { 1, 2, 3 };
            var expectedLength = 3;

            // Act & Assert
            Assert.DoesNotThrow(() => value.EnsureIsLength(expectedLength, LengthCheckMode.ExactLength));
        }

        [Test]
        public void EnsureIsLength_WithListWrongLength_ThrowsValidationException()
        {
            // Arrange
            var value = new List<int> { 1, 2, 3 };
            var expectedLength = 5;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsLength(expectedLength, LengthCheckMode.ExactLength));
            Assert.That(exception.Message, Does.Contain("collection length must be exactly 5"));
            Assert.That(exception.Message, Does.Contain("Actual length: 3"));
        }

        [Test]
        public void EnsureIsLength_WithCustomParameterName_IncludesParameterNameInException()
        {
            // Arrange
            var value = "Hello";
            var expectedLength = 3;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsLength(expectedLength, LengthCheckMode.ExactLength, parameterName: "testValue"));
            Assert.That(exception.ParameterName, Is.EqualTo("testValue"));
        }
    }

    [TestFixture]
    public class ValidateIsLengthTests
    {
        [Test]
        public void ValidateIsLength_WithStringExactLength_ReturnsSuccess()
        {
            // Arrange
            var value = "Hello";
            var expectedLength = 5;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsLength(expectedLength, LengthCheckMode.ExactLength, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsLength_WithStringWrongLength_ReturnsFailure()
        {
            // Arrange
            var value = "Hello";
            var expectedLength = 3;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsLength(expectedLength, LengthCheckMode.ExactLength, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain("string length must be exactly 3"));
            Assert.That(result.ValidationException!.Message, Does.Contain("Actual length: 5"));
        }

        [Test]
        public void ValidateIsLength_WithNullString_ReturnsFailure()
        {
            // Arrange
            string? value = null;
            var expectedLength = 0;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsLength(expectedLength, LengthCheckMode.ExactLength, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain("string length must be exactly 0"));
            Assert.That(result.ValidationException!.Message, Does.Contain("Actual length: null"));
        }

        [Test]
        public void ValidateIsLength_WithStringLessThan_ReturnsSuccess()
        {
            // Arrange
            var value = "Hi";
            var expectedLength = 5;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsLength(expectedLength, LengthCheckMode.LessThan, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsLength_WithStringGreaterThan_ReturnsSuccess()
        {
            // Arrange
            var value = "Hello World";
            var expectedLength = 5;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsLength(expectedLength, LengthCheckMode.GreaterThan, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsLength_WithListExactLength_ReturnsSuccess()
        {
            // Arrange
            var value = new List<int> { 1, 2, 3 };
            var expectedLength = 3;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsLength(expectedLength, LengthCheckMode.ExactLength, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsLength_WithListWrongLength_ReturnsFailure()
        {
            // Arrange
            var value = new List<int> { 1, 2, 3 };
            var expectedLength = 5;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsLength(expectedLength, LengthCheckMode.ExactLength, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain("collection length must be exactly 5"));
            Assert.That(result.ValidationException!.Message, Does.Contain("Actual length: 3"));
        }

        [Test]
        public void ValidateIsLength_WithCustomParameterName_IncludesParameterNameInResult()
        {
            // Arrange
            var value = "Hello";
            var expectedLength = 3;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsLength(expectedLength, LengthCheckMode.ExactLength, blackboard: blackboard, parameterName: "testValue");

            // Assert
            Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("testValue"));
        }

        [Test]
        public void ValidateIsLength_WithBlackboard_IncludesContextInResult()
        {
            // Arrange
            var value = "Hello";
            var expectedLength = 3;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsLength(expectedLength, LengthCheckMode.ExactLength, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        }

        [Test]
        public void ValidateIsLength_WithFailure_IncludesValidationData()
        {
            // Arrange
            var value = "Hello";
            var expectedLength = 3;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsLength(expectedLength, LengthCheckMode.ExactLength, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Context, Is.Not.Null);
            Assert.That(result.ValidationException!.Context.Context.Count, Is.EqualTo(4));
            Assert.That(result.ValidationException!.Context.Context["value"], Is.EqualTo("Hello"));
            Assert.That(result.ValidationException!.Context.Context["actualLength"], Is.EqualTo(5));
            Assert.That(result.ValidationException!.Context.Context["expectedLength"], Is.EqualTo(3));
            Assert.That(result.ValidationException!.Context.Context["mode"], Is.EqualTo(LengthCheckMode.ExactLength));
        }
    }
}
