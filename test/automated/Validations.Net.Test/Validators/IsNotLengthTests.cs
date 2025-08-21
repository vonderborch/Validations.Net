using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;
using System.Text;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNotLengthTests
{
    [TestFixture]
    public class CheckIsNotLengthTests
    {
        [Test]
        public void CheckIsNotLength_WithStringExactLength_ReturnsFalse()
        {
            // Arrange
            var value = "Hello";
            var expectedLength = 5;

            // Act
            var result = value.CheckIsNotLength(expectedLength, LengthCheckMode.ExactLength);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotLength_WithStringWrongLength_ReturnsTrue()
        {
            // Arrange
            var value = "Hello";
            var expectedLength = 3;

            // Act
            var result = value.CheckIsNotLength(expectedLength, LengthCheckMode.ExactLength);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotLength_WithNullString_ReturnsTrue()
        {
            // Arrange
            string? value = null;
            var expectedLength = 0;

            // Act
            var result = value.CheckIsNotLength(expectedLength, LengthCheckMode.ExactLength);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotLength_WithStringLessThan_ReturnsFalse()
        {
            // Arrange
            var value = "Hi";
            var expectedLength = 5;

            // Act
            var result = value.CheckIsNotLength(expectedLength, LengthCheckMode.LessThan);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotLength_WithStringLessThanOrEqual_ReturnsFalse()
        {
            // Arrange
            var value = "Hi";
            var expectedLength = 2;

            // Act
            var result = value.CheckIsNotLength(expectedLength, LengthCheckMode.LessThanOrEqual);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotLength_WithStringGreaterThan_ReturnsFalse()
        {
            // Arrange
            var value = "Hello World";
            var expectedLength = 5;

            // Act
            var result = value.CheckIsNotLength(expectedLength, LengthCheckMode.GreaterThan);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotLength_WithStringGreaterThanOrEqual_ReturnsFalse()
        {
            // Arrange
            var value = "Hello";
            var expectedLength = 5;

            // Act
            var result = value.CheckIsNotLength(expectedLength, LengthCheckMode.GreaterThanOrEqual);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotLength_WithListExactLength_ReturnsFalse()
        {
            // Arrange
            var value = new List<int> { 1, 2, 3 };
            var expectedLength = 3;

            // Act
            var result = value.CheckIsNotLength(expectedLength, LengthCheckMode.ExactLength);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotLength_WithArrayExactLength_ReturnsFalse()
        {
            // Arrange
            var value = new int[] { 1, 2, 3, 4 };
            var expectedLength = 4;

            // Act
            var result = value.CheckIsNotLength(expectedLength, LengthCheckMode.ExactLength);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotLength_WithNullCollection_ReturnsTrue()
        {
            // Arrange
            System.Collections.IEnumerable? value = null;
            var expectedLength = 0;

            // Act
            var result = value.CheckIsNotLength(expectedLength, LengthCheckMode.ExactLength);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotLength_WithSpanExactLength_ReturnsFalse()
        {
            // Arrange
            var array = new int[] { 1, 2, 3 };
            var value = new Span<int>(array);
            var expectedLength = 3;

            // Act
            var result = value.CheckIsNotLength(expectedLength, LengthCheckMode.ExactLength);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotLength_WithReadOnlySpanExactLength_ReturnsFalse()
        {
            // Arrange
            var array = new int[] { 1, 2, 3, 4, 5 };
            var value = new ReadOnlySpan<int>(array);
            var expectedLength = 5;

            // Act
            var result = value.CheckIsNotLength(expectedLength, LengthCheckMode.ExactLength);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotLength_WithMemoryExactLength_ReturnsFalse()
        {
            // Arrange
            var array = new int[] { 1, 2, 3, 4 };
            var value = new Memory<int>(array);
            var expectedLength = 4;

            // Act
            var result = value.CheckIsNotLength(expectedLength, LengthCheckMode.ExactLength);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotLength_WithStringBuilderExactLength_ReturnsFalse()
        {
            // Arrange
            var value = new StringBuilder("Hello");
            var expectedLength = 5;

            // Act
            var result = value.CheckIsNotLength(expectedLength, LengthCheckMode.ExactLength);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotLength_WithNullStringBuilder_ReturnsTrue()
        {
            // Arrange
            System.Text.StringBuilder? value = null;
            var expectedLength = 0;

            // Act
            var result = value.CheckIsNotLength(expectedLength, LengthCheckMode.ExactLength);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIsNotLength_WithICollectionExactLength_ReturnsFalse()
        {
            // Arrange
            ICollection<int> value = new List<int> { 1, 2, 3, 4, 5 };
            var expectedLength = 5;

            // Act
            var result = value.CheckIsNotLength(expectedLength, LengthCheckMode.ExactLength);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckIsNotLength_WithNullICollection_ReturnsTrue()
        {
            // Arrange
            ICollection<int>? value = null;
            var expectedLength = 0;

            // Act
            var result = value.CheckIsNotLength(expectedLength, LengthCheckMode.ExactLength);

            // Assert
            Assert.That(result, Is.True);
        }
    }

    [TestFixture]
    public class EnsureIsNotLengthTests
    {
        [Test]
        public void EnsureIsNotLength_WithStringExactLength_ThrowsValidationException()
        {
            // Arrange
            var value = "Hello";
            var expectedLength = 5;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotLength(expectedLength, LengthCheckMode.ExactLength));
            Assert.That(exception.Message, Does.Contain("string length must not be exactly 5"));
            Assert.That(exception.Message, Does.Contain("Actual length: 5"));
        }

        [Test]
        public void EnsureIsNotLength_WithStringWrongLength_DoesNotThrow()
        {
            // Arrange
            var value = "Hello";
            var expectedLength = 3;

            // Act & Assert
            Assert.DoesNotThrow(() => value.EnsureIsNotLength(expectedLength, LengthCheckMode.ExactLength));
        }

        [Test]
        public void EnsureIsNotLength_WithNullString_DoesNotThrow()
        {
            // Arrange
            string? value = null;
            var expectedLength = 0;

            // Act & Assert
            Assert.DoesNotThrow(() => value.EnsureIsNotLength(expectedLength, LengthCheckMode.ExactLength));
        }

        [Test]
        public void EnsureIsNotLength_WithStringLessThan_ThrowsValidationException()
        {
            // Arrange
            var value = "Hi";
            var expectedLength = 5;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotLength(expectedLength, LengthCheckMode.LessThan));
            Assert.That(exception.Message, Does.Contain("string length must not be less than 5"));
            Assert.That(exception.Message, Does.Contain("Actual length: 2"));
        }

        [Test]
        public void EnsureIsNotLength_WithStringGreaterThan_ThrowsValidationException()
        {
            // Arrange
            var value = "Hello World";
            var expectedLength = 5;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotLength(expectedLength, LengthCheckMode.GreaterThan));
            Assert.That(exception.Message, Does.Contain("string length must not be greater than 5"));
            Assert.That(exception.Message, Does.Contain("Actual length: 11"));
        }

        [Test]
        public void EnsureIsNotLength_WithListExactLength_ThrowsValidationException()
        {
            // Arrange
            var value = new List<int> { 1, 2, 3 };
            var expectedLength = 3;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotLength(expectedLength, LengthCheckMode.ExactLength));
            Assert.That(exception.Message, Does.Contain("collection length must not be exactly 3"));
            Assert.That(exception.Message, Does.Contain("Actual length: 3"));
        }

        [Test]
        public void EnsureIsNotLength_WithListWrongLength_DoesNotThrow()
        {
            // Arrange
            var value = new List<int> { 1, 2, 3 };
            var expectedLength = 5;

            // Act & Assert
            Assert.DoesNotThrow(() => value.EnsureIsNotLength(expectedLength, LengthCheckMode.ExactLength));
        }

        [Test]
        public void EnsureIsNotLength_WithCustomParameterName_IncludesParameterNameInException()
        {
            // Arrange
            var value = "Hello";
            var expectedLength = 5;

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotLength(expectedLength, LengthCheckMode.ExactLength, parameterName: "testValue"));
            Assert.That(exception.ParameterName, Is.EqualTo("testValue"));
        }
    }

    [TestFixture]
    public class ValidateIsNotLengthTests
    {
        [Test]
        public void ValidateIsNotLength_WithStringExactLength_ReturnsFailure()
        {
            // Arrange
            var value = "Hello";
            var expectedLength = 5;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsNotLength(expectedLength, LengthCheckMode.ExactLength, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain("string length must not be exactly 5"));
            Assert.That(result.ValidationException!.Message, Does.Contain("Actual length: 5"));
        }

        [Test]
        public void ValidateIsNotLength_WithStringWrongLength_ReturnsSuccess()
        {
            // Arrange
            var value = "Hello";
            var expectedLength = 3;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsNotLength(expectedLength, LengthCheckMode.ExactLength, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNotLength_WithNullString_ReturnsSuccess()
        {
            // Arrange
            string? value = null;
            var expectedLength = 0;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsNotLength(expectedLength, LengthCheckMode.ExactLength, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNotLength_WithStringLessThan_ReturnsFailure()
        {
            // Arrange
            var value = "Hi";
            var expectedLength = 5;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsNotLength(expectedLength, LengthCheckMode.LessThan, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain("string length must not be less than 5"));
            Assert.That(result.ValidationException!.Message, Does.Contain("Actual length: 2"));
        }

        [Test]
        public void ValidateIsNotLength_WithStringGreaterThan_ReturnsFailure()
        {
            // Arrange
            var value = "Hello World";
            var expectedLength = 5;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsNotLength(expectedLength, LengthCheckMode.GreaterThan, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain("string length must not be greater than 5"));
            Assert.That(result.ValidationException!.Message, Does.Contain("Actual length: 11"));
        }

        [Test]
        public void ValidateIsNotLength_WithListExactLength_ReturnsFailure()
        {
            // Arrange
            var value = new List<int> { 1, 2, 3 };
            var expectedLength = 3;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsNotLength(expectedLength, LengthCheckMode.ExactLength, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Message, Does.Contain("collection length must not be exactly 3"));
            Assert.That(result.ValidationException!.Message, Does.Contain("Actual length: 3"));
        }

        [Test]
        public void ValidateIsNotLength_WithListWrongLength_ReturnsSuccess()
        {
            // Arrange
            var value = new List<int> { 1, 2, 3 };
            var expectedLength = 5;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsNotLength(expectedLength, LengthCheckMode.ExactLength, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidateIsNotLength_WithCustomParameterName_IncludesParameterNameInResult()
        {
            // Arrange
            var value = "Hello";
            var expectedLength = 5;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsNotLength(expectedLength, LengthCheckMode.ExactLength, blackboard: blackboard, parameterName: "testValue");

            // Assert
            Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("testValue"));
        }

        [Test]
        public void ValidateIsNotLength_WithBlackboard_IncludesContextInResult()
        {
            // Arrange
            var value = "Hello";
            var expectedLength = 5;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsNotLength(expectedLength, LengthCheckMode.ExactLength, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        }

        [Test]
        public void ValidateIsNotLength_WithFailure_IncludesValidationData()
        {
            // Arrange
            var value = "Hello";
            var expectedLength = 5;
            var blackboard = new Blackboard();

            // Act
            var result = value.ValidateIsNotLength(expectedLength, LengthCheckMode.ExactLength, blackboard: blackboard);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ValidationException!.Context, Is.Not.Null);
            Assert.That(result.ValidationException!.Context.Context.Count, Is.EqualTo(4));
            Assert.That(result.ValidationException!.Context.Context["value"], Is.EqualTo("Hello"));
            Assert.That(result.ValidationException!.Context.Context["actualLength"], Is.EqualTo(5));
            Assert.That(result.ValidationException!.Context.Context["expectedLength"], Is.EqualTo(5));
            Assert.That(result.ValidationException!.Context.Context["mode"], Is.EqualTo(LengthCheckMode.ExactLength));
        }
    }
}
