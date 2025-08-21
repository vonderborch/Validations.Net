using NUnit.Framework;
using SimpleBlackboard.Net;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsEmptyTests
{
    [Test]
    public void ValidatorName_ShouldBeCorrect()
    {
        Assert.That(IsEmpty.ValidatorName, Is.EqualTo("IsEmpty"));
    }

    [Test]
    public void ValidationFailureMessage_ShouldBeCorrect()
    {
        Assert.That(IsEmpty.ValidationFailureMessage, Is.EqualTo("Parameter must be empty"));
    }

    #region String Tests

    [Test]
    public void CheckIsEmpty_WithNullString_ReturnsTrue()
    {
        // Arrange
        string? value = null;

        // Act
        bool result = value.CheckIsEmpty();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsEmpty_WithEmptyString_ReturnsTrue()
    {
        // Arrange
        string value = "";

        // Act
        bool result = value.CheckIsEmpty();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsEmpty_WithNonEmptyString_ReturnsFalse()
    {
        // Arrange
        string value = "hello";

        // Act
        bool result = value.CheckIsEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsEmpty_WithWhitespaceString_ReturnsFalse()
    {
        // Arrange
        string value = "   ";

        // Act
        bool result = value.CheckIsEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    #endregion

    #region Collection Tests

    [Test]
    public void CheckIsEmpty_WithNullCollection_ReturnsTrue()
    {
        // Arrange
        IEnumerable? value = null;

        // Act
        bool result = value.CheckIsEmpty();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsEmpty_WithEmptyList_ReturnsTrue()
    {
        // Arrange
        var value = new List<int>();

        // Act
        bool result = ((IEnumerable)value).CheckIsEmpty();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsEmpty_WithNonEmptyList_ReturnsFalse()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3 };

        // Act
        bool result = ((IEnumerable)value).CheckIsEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsEmpty_WithEmptyArray_ReturnsTrue()
    {
        // Arrange
        int[] value = [];

        // Act
        bool result = value.CheckIsEmpty();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsEmpty_WithNonEmptyArray_ReturnsFalse()
    {
        // Arrange
        int[] value = [1, 2, 3];

        // Act
        bool result = value.CheckIsEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsEmpty_WithEmptyEnumerable_ReturnsTrue()
    {
        // Arrange
        IEnumerable value = Enumerable.Empty<int>();

        // Act
        bool result = value.CheckIsEmpty();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsEmpty_WithNonEmptyEnumerable_ReturnsFalse()
    {
        // Arrange
        IEnumerable value = Enumerable.Range(1, 3);

        // Act
        bool result = value.CheckIsEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    #endregion



    #region Span Tests

    [Test]
    public void CheckIsEmpty_WithEmptyReadOnlySpan_ReturnsTrue()
    {
        // Arrange
        ReadOnlySpan<int> value = [];

        // Act
        bool result = value.CheckIsEmpty();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsEmpty_WithNonEmptyReadOnlySpan_ReturnsFalse()
    {
        // Arrange
        int[] array = [1, 2, 3];
        ReadOnlySpan<int> value = array;

        // Act
        bool result = value.CheckIsEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsEmpty_WithEmptySpan_ReturnsTrue()
    {
        // Arrange
        Span<int> value = [];

        // Act
        bool result = value.CheckIsEmpty();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsEmpty_WithNonEmptySpan_ReturnsFalse()
    {
        // Arrange
        int[] array = [1, 2, 3];
        Span<int> value = array;

        // Act
        bool result = value.CheckIsEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    #endregion

    #region Ensure Tests

    [Test]
    public void EnsureIsEmpty_WithEmptyString_ReturnsValue()
    {
        // Arrange
        string value = "";

        // Act
        string? result = value.EnsureIsEmpty();

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void EnsureIsEmpty_WithNonEmptyString_ThrowsValidationException()
    {
        // Arrange
        string value = "hello";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsEmpty());
        Assert.That(exception.Message, Does.Contain("Parameter must be empty"));
    }

    [Test]
    public void EnsureIsEmpty_WithEmptyList_ReturnsValue()
    {
        // Arrange
        var value = new List<int>();

        // Act
        IEnumerable? result = ((IEnumerable)value).EnsureIsEmpty();

        // Assert
        Assert.That(result, Is.SameAs(value));
    }

    [Test]
    public void EnsureIsEmpty_WithNonEmptyList_ThrowsValidationException()
    {
        // Arrange
        List<int> value = new List<int> { 1, 2, 3 };

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => ((IEnumerable)value).EnsureIsEmpty());
        Assert.That(exception.Message, Does.Contain("Parameter must be empty"));
    }

    [Test]
    public void EnsureIsEmpty_WithEmptyArray_ReturnsValue()
    {
        // Arrange
        int[] value = [];

        // Act
        int[]? result = value.EnsureIsEmpty();

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void EnsureIsEmpty_WithNonEmptyArray_ThrowsValidationException()
    {
        // Arrange
        int[] value = [1, 2, 3];

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsEmpty());
        Assert.That(exception.Message, Does.Contain("Parameter must be empty"));
    }

    [Test]
    public void EnsureIsEmpty_WithEmptyReadOnlySpan_ReturnsValue()
    {
        // Arrange
        ReadOnlySpan<int> value = [];

        // Act
        ReadOnlySpan<int> result = value.EnsureIsEmpty();

        // Assert
        Assert.That(result.IsEmpty, Is.True);
    }

    [Test]
    public void EnsureIsEmpty_WithNonEmptyReadOnlySpan_ThrowsValidationException()
    {
        // Arrange
        int[] array = [1, 2, 3];
        ReadOnlySpan<int> value = array;

        // Act & Assert
        ValidationException? exception = null;
        try
        {
            value.EnsureIsEmpty();
        }
        catch (ValidationException ex)
        {
            exception = ex;
        }
        
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.Message, Does.Contain("Parameter must be empty"));
    }

    [Test]
    public void EnsureIsEmpty_WithEmptySpan_ReturnsValue()
    {
        // Arrange
        Span<int> value = [];

        // Act
        Span<int> result = value.EnsureIsEmpty();

        // Assert
        Assert.That(result.IsEmpty, Is.True);
    }

    [Test]
    public void EnsureIsEmpty_WithNonEmptySpan_ThrowsValidationException()
    {
        // Arrange
        int[] array = [1, 2, 3];
        Span<int> value = array;

        // Act & Assert
        ValidationException? exception = null;
        try
        {
            value.EnsureIsEmpty();
        }
        catch (ValidationException ex)
        {
            exception = ex;
        }
        
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.Message, Does.Contain("Parameter must be empty"));
    }

    #endregion

    #region Validate Tests

    [Test]
    public void ValidateIsEmpty_WithEmptyString_ReturnsValidResult()
    {
        // Arrange
        string value = "";

        // Act
        ValidationResult result = value.ValidateIsEmpty();

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsEmpty_WithNonEmptyString_ReturnsInvalidResult()
    {
        // Arrange
        string value = "hello";

        // Act
        ValidationResult result = value.ValidateIsEmpty();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be empty"));
    }

    [Test]
    public void ValidateIsEmpty_WithEmptyList_ReturnsValidResult()
    {
        // Arrange
        var value = new List<int>();

        // Act
        ValidationResult result = ((IEnumerable)value).ValidateIsEmpty();

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsEmpty_WithNonEmptyList_ReturnsInvalidResult()
    {
        // Arrange
        List<int> value = new List<int> { 1, 2, 3 };

        // Act
        ValidationResult result = ((IEnumerable)value).ValidateIsEmpty();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be empty"));
    }

    [Test]
    public void ValidateIsEmpty_WithEmptyArray_ReturnsValidResult()
    {
        // Arrange
        int[] value = [];

        // Act
        ValidationResult result = value.ValidateIsEmpty();

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsEmpty_WithNonEmptyArray_ReturnsInvalidResult()
    {
        // Arrange
        int[] value = [1, 2, 3];

        // Act
        ValidationResult result = value.ValidateIsEmpty();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be empty"));
    }

    [Test]
    public void ValidateIsEmpty_WithEmptyReadOnlySpan_ReturnsValidResult()
    {
        // Arrange
        ReadOnlySpan<int> value = [];

        // Act
        ValidationResult result = value.ValidateIsEmpty();

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsEmpty_WithNonEmptyReadOnlySpan_ReturnsInvalidResult()
    {
        // Arrange
        int[] array = [1, 2, 3];
        ReadOnlySpan<int> value = array;

        // Act
        ValidationResult result = value.ValidateIsEmpty();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be empty"));
    }

    [Test]
    public void ValidateIsEmpty_WithEmptySpan_ReturnsValidResult()
    {
        // Arrange
        Span<int> value = [];

        // Act
        ValidationResult result = value.ValidateIsEmpty();

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsEmpty_WithNonEmptySpan_ReturnsInvalidResult()
    {
        // Arrange
        int[] array = [1, 2, 3];
        Span<int> value = array;

        // Act
        ValidationResult result = value.ValidateIsEmpty();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be empty"));
    }

    #endregion

    #region Parameter Name and Blackboard Tests

    [Test]
    public void ValidateIsEmpty_WithParameterName_ReturnsInvalidResultWithParameterName()
    {
        // Arrange
        string value = "hello";
        string parameterName = "testParam";

        // Act
        ValidationResult result = value.ValidateIsEmpty(null, parameterName);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("testParam"));
    }

    [Test]
    public void ValidateIsEmpty_WithBlackboard_ReturnsInvalidResultWithBlackboard()
    {
        // Arrange
        string value = "hello";
        var blackboard = new Blackboard();

        // Act
        ValidationResult result = value.ValidateIsEmpty(blackboard);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must be empty"));
    }

    #endregion

    #region IReadOnlyCollection<T> Tests

    [Test]
    public void CheckIsEmpty_WithEmptyReadOnlyCollection_ReturnsTrue()
    {
        IReadOnlyCollection<int> value = new List<int>();
        Assert.That(value.CheckIsEmpty(), Is.True);
    }

    [Test]
    public void CheckIsEmpty_WithNonEmptyReadOnlyCollection_ReturnsFalse()
    {
        IReadOnlyCollection<int> value = new List<int> { 1, 2, 3 };
        Assert.That(value.CheckIsEmpty(), Is.False);
    }

    [Test]
    public void CheckIsEmpty_WithNullReadOnlyCollection_ReturnsTrue()
    {
        IReadOnlyCollection<int>? value = null;
        Assert.That(value.CheckIsEmpty(), Is.True);
    }

    [Test]
    public void EnsureIsEmpty_WithEmptyReadOnlyCollection_ReturnsValue()
    {
        IReadOnlyCollection<int> value = new List<int>();
        Assert.That(value.EnsureIsEmpty(), Is.EqualTo(value));
    }

    [Test]
    public void EnsureIsEmpty_WithNonEmptyReadOnlyCollection_ThrowsValidationException()
    {
        IReadOnlyCollection<int> value = new List<int> { 1, 2, 3 };
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsEmpty());
        Assert.That(exception.Message, Does.Contain("Parameter must be empty"));
    }

    [Test]
    public void ValidateIsEmpty_WithEmptyReadOnlyCollection_ReturnsValidResult()
    {
        IReadOnlyCollection<int> value = new List<int>();
        var result = value.ValidateIsEmpty();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsEmpty_WithNonEmptyReadOnlyCollection_ReturnsInvalidResult()
    {
        IReadOnlyCollection<int> value = new List<int> { 1, 2, 3 };
        var result = value.ValidateIsEmpty();
        Assert.That(result.IsValid, Is.False);
    }

    #endregion

    #region IDictionary<TKey, TValue> Tests

    [Test]
    public void CheckIsEmpty_WithEmptyDictionary_ReturnsTrue()
    {
        IDictionary<string, int> value = new Dictionary<string, int>();
        Assert.That(value.CheckIsEmpty(), Is.True);
    }

    [Test]
    public void CheckIsEmpty_WithNonEmptyDictionary_ReturnsFalse()
    {
        IDictionary<string, int> value = new Dictionary<string, int> { { "key1", 1 }, { "key2", 2 } };
        Assert.That(value.CheckIsEmpty(), Is.False);
    }

    [Test]
    public void CheckIsEmpty_WithNullDictionary_ReturnsTrue()
    {
        IDictionary<string, int>? value = null;
        Assert.That(value.CheckIsEmpty(), Is.True);
    }

    [Test]
    public void EnsureIsEmpty_WithEmptyDictionary_ReturnsValue()
    {
        IDictionary<string, int> value = new Dictionary<string, int>();
        Assert.That(value.EnsureIsEmpty(), Is.EqualTo(value));
    }

    [Test]
    public void EnsureIsEmpty_WithNonEmptyDictionary_ThrowsValidationException()
    {
        IDictionary<string, int> value = new Dictionary<string, int> { { "key1", 1 } };
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsEmpty());
        Assert.That(exception.Message, Does.Contain("Parameter must be empty"));
    }

    [Test]
    public void ValidateIsEmpty_WithEmptyDictionary_ReturnsValidResult()
    {
        IDictionary<string, int> value = new Dictionary<string, int>();
        var result = value.ValidateIsEmpty();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsEmpty_WithNonEmptyDictionary_ReturnsInvalidResult()
    {
        IDictionary<string, int> value = new Dictionary<string, int> { { "key1", 1 } };
        var result = value.ValidateIsEmpty();
        Assert.That(result.IsValid, Is.False);
    }

    #endregion





    #region Memory<T> Tests

    [Test]
    public void CheckIsEmpty_WithEmptyMemory_ReturnsTrue()
    {
        Memory<int> value = Memory<int>.Empty;
        Assert.That(value.CheckIsEmpty(), Is.True);
    }

    [Test]
    public void CheckIsEmpty_WithNonEmptyMemory_ReturnsFalse()
    {
        Memory<int> value = new int[] { 1, 2, 3 }.AsMemory();
        Assert.That(value.CheckIsEmpty(), Is.False);
    }

    [Test]
    public void EnsureIsEmpty_WithEmptyMemory_ReturnsValue()
    {
        Memory<int> value = Memory<int>.Empty;
        Assert.That(value.EnsureIsEmpty(), Is.EqualTo(value));
    }

    [Test]
    public void EnsureIsEmpty_WithNonEmptyMemory_ThrowsValidationException()
    {
        Memory<int> value = new int[] { 1, 2, 3 }.AsMemory();
        try
        {
            value.EnsureIsEmpty();
            Assert.Fail("Expected ValidationException to be thrown");
        }
        catch (ValidationException exception)
        {
            Assert.That(exception.Message, Does.Contain("Parameter must be empty"));
        }
    }

    [Test]
    public void ValidateIsEmpty_WithEmptyMemory_ReturnsValidResult()
    {
        Memory<int> value = Memory<int>.Empty;
        var result = value.ValidateIsEmpty();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsEmpty_WithNonEmptyMemory_ReturnsInvalidResult()
    {
        Memory<int> value = new int[] { 1, 2, 3 }.AsMemory();
        var result = value.ValidateIsEmpty();
        Assert.That(result.IsValid, Is.False);
    }

    #endregion



    #region StringBuilder Tests

    [Test]
    public void CheckIsEmpty_WithEmptyStringBuilder_ReturnsTrue()
    {
        StringBuilder value = new StringBuilder();
        Assert.That(value.CheckIsEmpty(), Is.True);
    }

    [Test]
    public void CheckIsEmpty_WithNonEmptyStringBuilder_ReturnsFalse()
    {
        StringBuilder value = new StringBuilder("Hello World");
        Assert.That(value.CheckIsEmpty(), Is.False);
    }

    [Test]
    public void CheckIsEmpty_WithNullStringBuilder_ReturnsTrue()
    {
        StringBuilder? value = null;
        Assert.That(value.CheckIsEmpty(), Is.True);
    }

    [Test]
    public void EnsureIsEmpty_WithEmptyStringBuilder_ReturnsValue()
    {
        StringBuilder value = new StringBuilder();
        Assert.That(value.EnsureIsEmpty(), Is.EqualTo(value));
    }

    [Test]
    public void EnsureIsEmpty_WithNonEmptyStringBuilder_ThrowsValidationException()
    {
        StringBuilder value = new StringBuilder("Hello World");
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsEmpty());
        Assert.That(exception.Message, Does.Contain("Parameter must be empty"));
    }

    [Test]
    public void ValidateIsEmpty_WithEmptyStringBuilder_ReturnsValidResult()
    {
        StringBuilder value = new StringBuilder();
        var result = value.ValidateIsEmpty();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsEmpty_WithNonEmptyStringBuilder_ReturnsInvalidResult()
    {
        StringBuilder value = new StringBuilder("Hello World");
        var result = value.ValidateIsEmpty();
        Assert.That(result.IsValid, Is.False);
    }

    #endregion
}
