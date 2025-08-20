using NUnit.Framework;
using SimpleBlackboard.Net;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNotEmptyTests
{
    [Test]
    public void ValidatorName_ShouldBeCorrect()
    {
        Assert.That(IsNotEmpty.ValidatorName, Is.EqualTo("IsNotEmpty"));
    }

    [Test]
    public void ValidationFailureMessage_ShouldBeCorrect()
    {
        Assert.That(IsNotEmpty.ValidationFailureMessage, Is.EqualTo("Parameter must not be empty"));
    }

    #region String Tests

    [Test]
    public void CheckIsNotEmpty_WithNullString_ReturnsFalse()
    {
        // Arrange
        string? value = null;

        // Act
        bool result = value.CheckIsNotEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotEmpty_WithEmptyString_ReturnsFalse()
    {
        // Arrange
        string value = "";

        // Act
        bool result = value.CheckIsNotEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotEmpty_WithNonEmptyString_ReturnsTrue()
    {
        // Arrange
        string value = "hello";

        // Act
        bool result = value.CheckIsNotEmpty();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotEmpty_WithWhitespaceString_ReturnsTrue()
    {
        // Arrange
        string value = "   ";

        // Act
        bool result = value.CheckIsNotEmpty();

        // Assert
        Assert.That(result, Is.True);
    }

    #endregion

    #region Collection Tests

    [Test]
    public void CheckIsNotEmpty_WithNullCollection_ReturnsFalse()
    {
        // Arrange
        IEnumerable? value = null;

        // Act
        bool result = value.CheckIsNotEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotEmpty_WithEmptyList_ReturnsFalse()
    {
        // Arrange
        List<int> value = new List<int>();

        // Act
        bool result = ((IEnumerable)value).CheckIsNotEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotEmpty_WithNonEmptyList_ReturnsTrue()
    {
        // Arrange
        List<int> value = new List<int> { 1, 2, 3 };

        // Act
        bool result = ((IEnumerable)value).CheckIsNotEmpty();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotEmpty_WithEmptyArray_ReturnsFalse()
    {
        // Arrange
        int[] value = [];

        // Act
        bool result = value.CheckIsNotEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotEmpty_WithNonEmptyArray_ReturnsTrue()
    {
        // Arrange
        int[] value = [1, 2, 3];

        // Act
        bool result = value.CheckIsNotEmpty();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotEmpty_WithEmptyEnumerable_ReturnsFalse()
    {
        // Arrange
        IEnumerable value = Enumerable.Empty<int>();

        // Act
        bool result = value.CheckIsNotEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotEmpty_WithNonEmptyEnumerable_ReturnsTrue()
    {
        // Arrange
        IEnumerable value = Enumerable.Range(1, 3);

        // Act
        bool result = value.CheckIsNotEmpty();

        // Assert
        Assert.That(result, Is.True);
    }

    #endregion



    #region Span Tests

    [Test]
    public void CheckIsNotEmpty_WithEmptyReadOnlySpan_ReturnsFalse()
    {
        // Arrange
        ReadOnlySpan<int> value = [];

        // Act
        bool result = value.CheckIsNotEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotEmpty_WithNonEmptyReadOnlySpan_ReturnsTrue()
    {
        // Arrange
        int[] array = [1, 2, 3];
        ReadOnlySpan<int> value = array;

        // Act
        bool result = value.CheckIsNotEmpty();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotEmpty_WithEmptySpan_ReturnsFalse()
    {
        // Arrange
        Span<int> value = [];

        // Act
        bool result = value.CheckIsNotEmpty();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotEmpty_WithNonEmptySpan_ReturnsTrue()
    {
        // Arrange
        int[] array = [1, 2, 3];
        Span<int> value = array;

        // Act
        bool result = value.CheckIsNotEmpty();

        // Assert
        Assert.That(result, Is.True);
    }

    #endregion

    #region Ensure Tests

    [Test]
    public void EnsureIsNotEmpty_WithNonEmptyString_ReturnsValue()
    {
        // Arrange
        string value = "hello";

        // Act
        string? result = value.EnsureIsNotEmpty();

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void EnsureIsNotEmpty_WithEmptyString_ThrowsValidationException()
    {
        // Arrange
        string value = "";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotEmpty());
        Assert.That(exception.Message, Does.Contain("Parameter must not be empty"));
    }

    [Test]
    public void EnsureIsNotEmpty_WithNullString_ThrowsValidationException()
    {
        // Arrange
        string? value = null;

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotEmpty());
        Assert.That(exception.Message, Does.Contain("Parameter must not be empty"));
    }

    [Test]
    public void EnsureIsNotEmpty_WithNonEmptyList_ReturnsValue()
    {
        // Arrange
        List<int> value = new List<int> { 1, 2, 3 };

        // Act
        IEnumerable? result = ((IEnumerable)value).EnsureIsNotEmpty();

        // Assert
        Assert.That(result, Is.SameAs(value));
    }

    [Test]
    public void EnsureIsNotEmpty_WithEmptyList_ThrowsValidationException()
    {
        // Arrange
        List<int> value = new List<int>();

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => ((IEnumerable)value).EnsureIsNotEmpty());
        Assert.That(exception.Message, Does.Contain("Parameter must not be empty"));
    }

    [Test]
    public void EnsureIsNotEmpty_WithNullList_ThrowsValidationException()
    {
        // Arrange
        List<int>? value = null;

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => ((IEnumerable?)value).EnsureIsNotEmpty());
        Assert.That(exception.Message, Does.Contain("Parameter must not be empty"));
    }

    [Test]
    public void EnsureIsNotEmpty_WithNonEmptyArray_ReturnsValue()
    {
        // Arrange
        int[] value = [1, 2, 3];

        // Act
        int[]? result = value.EnsureIsNotEmpty();

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void EnsureIsNotEmpty_WithEmptyArray_ThrowsValidationException()
    {
        // Arrange
        int[] value = [];

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotEmpty());
        Assert.That(exception.Message, Does.Contain("Parameter must not be empty"));
    }

    [Test]
    public void EnsureIsNotEmpty_WithNullArray_ThrowsValidationException()
    {
        // Arrange
        int[]? value = null;

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotEmpty());
        Assert.That(exception.Message, Does.Contain("Parameter must not be empty"));
    }

    [Test]
    public void EnsureIsNotEmpty_WithNonEmptyReadOnlySpan_ReturnsValue()
    {
        // Arrange
        int[] array = [1, 2, 3];
        ReadOnlySpan<int> value = array;

        // Act
        ReadOnlySpan<int> result = value.EnsureIsNotEmpty();

        // Assert
        Assert.That(result.IsEmpty, Is.False);
    }

    [Test]
    public void EnsureIsNotEmpty_WithEmptyReadOnlySpan_ThrowsValidationException()
    {
        // Arrange
        ReadOnlySpan<int> value = [];

        // Act & Assert
        ValidationException? exception = null;
        try
        {
            value.EnsureIsNotEmpty();
        }
        catch (ValidationException ex)
        {
            exception = ex;
        }
        
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.Message, Does.Contain("Parameter must not be empty"));
    }

    [Test]
    public void EnsureIsNotEmpty_WithNonEmptySpan_ReturnsValue()
    {
        // Arrange
        int[] array = [1, 2, 3];
        Span<int> value = array;

        // Act
        Span<int> result = value.EnsureIsNotEmpty();

        // Assert
        Assert.That(result.IsEmpty, Is.False);
    }

    [Test]
    public void EnsureIsNotEmpty_WithEmptySpan_ThrowsValidationException()
    {
        // Arrange
        Span<int> value = [];

        // Act & Assert
        ValidationException? exception = null;
        try
        {
            value.EnsureIsNotEmpty();
        }
        catch (ValidationException ex)
        {
            exception = ex;
        }
        
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.Message, Does.Contain("Parameter must not be empty"));
    }

    #endregion

    #region Validate Tests

    [Test]
    public void ValidateIsNotEmpty_WithNonEmptyString_ReturnsValidResult()
    {
        // Arrange
        string value = "hello";

        // Act
        ValidationResult result = value.ValidateIsNotEmpty();

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsNotEmpty_WithEmptyString_ReturnsInvalidResult()
    {
        // Arrange
        string value = "";

        // Act
        ValidationResult result = value.ValidateIsNotEmpty();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be empty"));
    }

    [Test]
    public void ValidateIsNotEmpty_WithNullString_ReturnsInvalidResult()
    {
        // Arrange
        string? value = null;

        // Act
        ValidationResult result = value.ValidateIsNotEmpty();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be empty"));
    }

    [Test]
    public void ValidateIsNotEmpty_WithNonEmptyList_ReturnsValidResult()
    {
        // Arrange
        List<int> value = new List<int> { 1, 2, 3 };

        // Act
        ValidationResult result = ((IEnumerable)value).ValidateIsNotEmpty();

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsNotEmpty_WithEmptyList_ReturnsInvalidResult()
    {
        // Arrange
        List<int> value = new List<int>();

        // Act
        ValidationResult result = ((IEnumerable)value).ValidateIsNotEmpty();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be empty"));
    }

    [Test]
    public void ValidateIsNotEmpty_WithNullList_ReturnsInvalidResult()
    {
        // Arrange
        List<int>? value = null;

        // Act
        ValidationResult result = ((IEnumerable?)value).ValidateIsNotEmpty();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be empty"));
    }

    [Test]
    public void ValidateIsNotEmpty_WithNonEmptyArray_ReturnsValidResult()
    {
        // Arrange
        int[] value = [1, 2, 3];

        // Act
        ValidationResult result = value.ValidateIsNotEmpty();

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsNotEmpty_WithEmptyArray_ReturnsInvalidResult()
    {
        // Arrange
        int[] value = [];

        // Act
        ValidationResult result = value.ValidateIsNotEmpty();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be empty"));
    }

    [Test]
    public void ValidateIsNotEmpty_WithNullArray_ReturnsInvalidResult()
    {
        // Arrange
        int[]? value = null;

        // Act
        ValidationResult result = value.ValidateIsNotEmpty();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be empty"));
    }

    [Test]
    public void ValidateIsNotEmpty_WithNonEmptyReadOnlySpan_ReturnsValidResult()
    {
        // Arrange
        int[] array = [1, 2, 3];
        ReadOnlySpan<int> value = array;

        // Act
        ValidationResult result = value.ValidateIsNotEmpty();

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsNotEmpty_WithEmptyReadOnlySpan_ReturnsInvalidResult()
    {
        // Arrange
        ReadOnlySpan<int> value = [];

        // Act
        ValidationResult result = value.ValidateIsNotEmpty();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be empty"));
    }

    [Test]
    public void ValidateIsNotEmpty_WithNonEmptySpan_ReturnsValidResult()
    {
        // Arrange
        int[] array = [1, 2, 3];
        Span<int> value = array;

        // Act
        ValidationResult result = value.ValidateIsNotEmpty();

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateIsNotEmpty_WithEmptySpan_ReturnsInvalidResult()
    {
        // Arrange
        Span<int> value = [];

        // Act
        ValidationResult result = value.ValidateIsNotEmpty();

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be empty"));
    }

    #endregion

    #region Parameter Name and Blackboard Tests

    [Test]
    public void ValidateIsNotEmpty_WithParameterName_ReturnsInvalidResultWithParameterName()
    {
        // Arrange
        string value = "";
        string parameterName = "testParam";

        // Act
        ValidationResult result = value.ValidateIsNotEmpty(parameterName);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("testParam"));
    }

    [Test]
    public void ValidateIsNotEmpty_WithBlackboard_ReturnsInvalidResultWithBlackboard()
    {
        // Arrange
        string value = "";
        var blackboard = new Blackboard();

        // Act
        ValidationResult result = value.ValidateIsNotEmpty(null, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("Parameter must not be empty"));
    }

    #endregion

    #region IReadOnlyCollection<T> Tests

    [Test]
    public void CheckIsNotEmpty_WithEmptyReadOnlyCollection_ReturnsFalse()
    {
        IReadOnlyCollection<int> value = new List<int>();
        Assert.That(value.CheckIsNotEmpty(), Is.False);
    }

    [Test]
    public void CheckIsNotEmpty_WithNonEmptyReadOnlyCollection_ReturnsTrue()
    {
        IReadOnlyCollection<int> value = new List<int> { 1, 2, 3 };
        Assert.That(value.CheckIsNotEmpty(), Is.True);
    }

    [Test]
    public void CheckIsNotEmpty_WithNullReadOnlyCollection_ReturnsFalse()
    {
        IReadOnlyCollection<int>? value = null;
        Assert.That(value.CheckIsNotEmpty(), Is.False);
    }

    [Test]
    public void EnsureIsNotEmpty_WithNonEmptyReadOnlyCollection_ReturnsValue()
    {
        IReadOnlyCollection<int> value = new List<int> { 1, 2, 3 };
        Assert.That(value.EnsureIsNotEmpty(), Is.EqualTo(value));
    }

    [Test]
    public void EnsureIsNotEmpty_WithEmptyReadOnlyCollection_ThrowsValidationException()
    {
        IReadOnlyCollection<int> value = new List<int>();
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotEmpty());
        Assert.That(exception.Message, Does.Contain("Parameter must not be empty"));
    }

    [Test]
    public void ValidateIsNotEmpty_WithNonEmptyReadOnlyCollection_ReturnsValidResult()
    {
        IReadOnlyCollection<int> value = new List<int> { 1, 2, 3 };
        var result = value.ValidateIsNotEmpty();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNotEmpty_WithEmptyReadOnlyCollection_ReturnsInvalidResult()
    {
        IReadOnlyCollection<int> value = new List<int>();
        var result = value.ValidateIsNotEmpty();
        Assert.That(result.IsValid, Is.False);
    }

    #endregion

    #region IDictionary<TKey, TValue> Tests

    [Test]
    public void CheckIsNotEmpty_WithEmptyDictionary_ReturnsFalse()
    {
        IDictionary<string, int> value = new Dictionary<string, int>();
        Assert.That(value.CheckIsNotEmpty(), Is.False);
    }

    [Test]
    public void CheckIsNotEmpty_WithNonEmptyDictionary_ReturnsTrue()
    {
        IDictionary<string, int> value = new Dictionary<string, int> { { "key1", 1 }, { "key2", 2 } };
        Assert.That(value.CheckIsNotEmpty(), Is.True);
    }

    [Test]
    public void CheckIsNotEmpty_WithNullDictionary_ReturnsFalse()
    {
        IDictionary<string, int>? value = null;
        Assert.That(value.CheckIsNotEmpty(), Is.False);
    }

    [Test]
    public void EnsureIsNotEmpty_WithNonEmptyDictionary_ReturnsValue()
    {
        IDictionary<string, int> value = new Dictionary<string, int> { { "key1", 1 }, { "key2", 2 } };
        Assert.That(value.EnsureIsNotEmpty(), Is.EqualTo(value));
    }

    [Test]
    public void EnsureIsNotEmpty_WithEmptyDictionary_ThrowsValidationException()
    {
        IDictionary<string, int> value = new Dictionary<string, int>();
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotEmpty());
        Assert.That(exception.Message, Does.Contain("Parameter must not be empty"));
    }

    [Test]
    public void ValidateIsNotEmpty_WithNonEmptyDictionary_ReturnsValidResult()
    {
        IDictionary<string, int> value = new Dictionary<string, int> { { "key1", 1 }, { "key2", 2 } };
        var result = value.ValidateIsNotEmpty();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNotEmpty_WithEmptyDictionary_ReturnsInvalidResult()
    {
        IDictionary<string, int> value = new Dictionary<string, int>();
        var result = value.ValidateIsNotEmpty();
        Assert.That(result.IsValid, Is.False);
    }

    #endregion







    #region Memory<T> Tests

    [Test]
    public void CheckIsNotEmpty_WithEmptyMemory_ReturnsFalse()
    {
        Memory<int> value = Memory<int>.Empty;
        Assert.That(value.CheckIsNotEmpty(), Is.False);
    }

    [Test]
    public void CheckIsNotEmpty_WithNonEmptyMemory_ReturnsTrue()
    {
        Memory<int> value = new int[] { 1, 2, 3 }.AsMemory();
        Assert.That(value.CheckIsNotEmpty(), Is.True);
    }

    [Test]
    public void EnsureIsNotEmpty_WithNonEmptyMemory_ReturnsValue()
    {
        Memory<int> value = new int[] { 1, 2, 3 }.AsMemory();
        Assert.That(value.EnsureIsNotEmpty(), Is.EqualTo(value));
    }

    [Test]
    public void EnsureIsNotEmpty_WithEmptyMemory_ThrowsValidationException()
    {
        Memory<int> value = Memory<int>.Empty;
        try
        {
            value.EnsureIsNotEmpty();
            Assert.Fail("Expected ValidationException to be thrown");
        }
        catch (ValidationException exception)
        {
            Assert.That(exception.Message, Does.Contain("Parameter must not be empty"));
        }
    }

    [Test]
    public void ValidateIsNotEmpty_WithNonEmptyMemory_ReturnsValidResult()
    {
        Memory<int> value = new int[] { 1, 2, 3 }.AsMemory();
        var result = value.ValidateIsNotEmpty();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNotEmpty_WithEmptyMemory_ReturnsInvalidResult()
    {
        Memory<int> value = Memory<int>.Empty;
        var result = value.ValidateIsNotEmpty();
        Assert.That(result.IsValid, Is.False);
    }

    #endregion



    #region StringBuilder Tests

    [Test]
    public void CheckIsNotEmpty_WithEmptyStringBuilder_ReturnsFalse()
    {
        StringBuilder value = new StringBuilder();
        Assert.That(value.CheckIsNotEmpty(), Is.False);
    }

    [Test]
    public void CheckIsNotEmpty_WithNonEmptyStringBuilder_ReturnsTrue()
    {
        StringBuilder value = new StringBuilder("Hello World");
        Assert.That(value.CheckIsNotEmpty(), Is.True);
    }

    [Test]
    public void CheckIsNotEmpty_WithNullStringBuilder_ReturnsFalse()
    {
        StringBuilder? value = null;
        Assert.That(value.CheckIsNotEmpty(), Is.False);
    }

    [Test]
    public void EnsureIsNotEmpty_WithNonEmptyStringBuilder_ReturnsValue()
    {
        StringBuilder value = new StringBuilder("Hello World");
        Assert.That(value.EnsureIsNotEmpty(), Is.EqualTo(value));
    }

    [Test]
    public void EnsureIsNotEmpty_WithEmptyStringBuilder_ThrowsValidationException()
    {
        StringBuilder value = new StringBuilder();
        var exception = Assert.Throws<ValidationException>(() => value.EnsureIsNotEmpty());
        Assert.That(exception.Message, Does.Contain("Parameter must not be empty"));
    }

    [Test]
    public void ValidateIsNotEmpty_WithNonEmptyStringBuilder_ReturnsValidResult()
    {
        StringBuilder value = new StringBuilder("Hello World");
        var result = value.ValidateIsNotEmpty();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNotEmpty_WithEmptyStringBuilder_ReturnsInvalidResult()
    {
        StringBuilder value = new StringBuilder();
        var result = value.ValidateIsNotEmpty();
        Assert.That(result.IsValid, Is.False);
    }

    #endregion
}
