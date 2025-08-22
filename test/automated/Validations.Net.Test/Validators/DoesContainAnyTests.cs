using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class DoesContainAnyTests
{
    #region Check Tests

    [Test]
    public void CheckDoesContainAny_StringWithParamsArray_ReturnsTrue()
    {
        // Arrange
        var value = "Hello World";
        var substrings = new[] { "World", "Test" };

        // Act
        var result = value.CheckDoesContainAny(substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAny_StringWithIList_ReturnsTrue()
    {
        // Arrange
        var value = "Hello World";
        var substrings = new List<string?> { "World", "Test" };

        // Act
        var result = value.CheckDoesContainAny(substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAny_StringWithStringComparison_ReturnsTrue()
    {
        // Arrange
        var value = "Hello WORLD";
        var substrings = new[] { "world", "test" };

        // Act
        var result = value.CheckDoesContainAny(StringComparison.OrdinalIgnoreCase, substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAny_StringWithIListAndStringComparison_ReturnsTrue()
    {
        // Arrange
        var value = "Hello WORLD";
        var substrings = new List<string?> { "world", "test" };

        // Act
        var result = value.CheckDoesContainAny(StringComparison.OrdinalIgnoreCase, substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAny_StringWithoutMatch_ReturnsFalse()
    {
        // Arrange
        var value = "Hello World";
        var substrings = new[] { "Test", "Example" };

        // Act
        var result = value.CheckDoesContainAny(substrings);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAny_NullString_ReturnsFalse()
    {
        // Arrange
        string? value = null;
        var substrings = new[] { "Test", "Example" };

        // Act
        var result = value.CheckDoesContainAny(substrings);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAny_EmptySubstrings_ReturnsFalse()
    {
        // Arrange
        var value = "Hello World";
        var substrings = new string[0];

        // Act
        var result = value.CheckDoesContainAny(substrings);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAny_NullSubstrings_ReturnsFalse()
    {
        // Arrange
        var value = "Hello World";
        string[]? substrings = null;

        // Act
        var result = value.CheckDoesContainAny(substrings);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAny_GenericCollectionWithParamsArray_ReturnsTrue()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new[] { 3, 6 };

        // Act
        var result = value.CheckDoesContainAny(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAny_GenericCollectionWithIList_ReturnsTrue()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new List<int> { 3, 6 }; // Use non-nullable int to match the collection type

        // Act
        var result = value.CheckDoesContainAny(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAny_GenericCollectionWithoutMatch_ReturnsFalse()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new[] { 6, 7 };

        // Act
        var result = value.CheckDoesContainAny(items);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAny_NonGenericCollectionWithParamsArray_ReturnsTrue()
    {
        // Arrange
        var value = new ArrayList { 1, "test", 3.14 };
        var items = new object[] { "test", "example" };

        // Act
        var result = value.CheckDoesContainAny(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAny_NonGenericCollectionWithIList_ReturnsTrue()
    {
        // Arrange
        var value = new ArrayList { 1, "test", 3.14 };
        var items = new List<object?> { "test", "example" };

        // Act
        var result = value.CheckDoesContainAny(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAny_NonGenericCollectionWithoutMatch_ReturnsFalse()
    {
        // Arrange
        var value = new ArrayList { 1, "test", 3.14 };
        var items = new object[] { "example", "sample" };

        // Act
        var result = value.CheckDoesContainAny(items);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAny_ReadOnlySpanWithParamsArray_ReturnsTrue()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new ReadOnlySpan<string>(array);
        var items = new[] { "c", "f" };

        // Act
        var result = value.CheckDoesContainAny(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAny_ReadOnlySpanWithIList_ReturnsTrue()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new ReadOnlySpan<string>(array);
        var items = new List<string?> { "c", "f" };

        // Act
        var result = value.CheckDoesContainAny(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAny_ReadOnlySpanWithoutMatch_ReturnsFalse()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new ReadOnlySpan<string>(array);
        var items = new[] { "f", "g" };

        // Act
        var result = value.CheckDoesContainAny(items);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAny_SpanWithParamsArray_ReturnsTrue()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new Span<string>(array);
        var items = new[] { "c", "f" };

        // Act
        var result = value.CheckDoesContainAny(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAny_SpanWithIList_ReturnsTrue()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new Span<string>(array);
        var items = new List<string?> { "c", "f" };

        // Act
        var result = value.CheckDoesContainAny(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAny_SpanWithoutMatch_ReturnsFalse()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new Span<string>(array);
        var items = new[] { "f", "g" };

        // Act
        var result = value.CheckDoesContainAny(items);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAny_MemoryWithParamsArray_ReturnsTrue()
    {
        // Arrange
        var value = new Memory<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new[] { "c", "f" };

        // Act
        var result = value.CheckDoesContainAny(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAny_MemoryWithIList_ReturnsTrue()
    {
        // Arrange
        var value = new Memory<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new List<string?> { "c", "f" };

        // Act
        var result = value.CheckDoesContainAny(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAny_MemoryWithoutMatch_ReturnsFalse()
    {
        // Arrange
        var value = new Memory<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new[] { "f", "g" };

        // Act
        var result = value.CheckDoesContainAny(items);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAny_StringBuilderWithParamsArray_ReturnsTrue()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substrings = new[] { "World", "Test" };

        // Act
        var result = value.CheckDoesContainAny(substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAny_StringBuilderWithIList_ReturnsTrue()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substrings = new List<string?> { "World", "Test" };

        // Act
        var result = value.CheckDoesContainAny(substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAny_StringBuilderWithoutMatch_ReturnsFalse()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substrings = new[] { "Test", "Example" };

        // Act
        var result = value.CheckDoesContainAny(substrings);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAny_NullStringBuilder_ReturnsFalse()
    {
        // Arrange
        StringBuilder? value = null;
        var substrings = new[] { "Test", "Example" };

        // Act
        var result = value.CheckDoesContainAny(substrings);

        // Assert
        Assert.That(result, Is.False);
    }

    #endregion

    #region Validate Tests

    [Test]
    public void ValidateDoesContainAny_StringWithMatch_ReturnsSuccess()
    {
        // Arrange
        var value = "Hello World";
        var substrings = new[] { "World", "Test" };
        var blackboard = new Blackboard();

        // Act
        var result = value.ValidateDoesContainAny(blackboard, "testValue", substrings);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesContainAny_StringWithoutMatch_ReturnsFailure()
    {
        // Arrange
        var value = "Hello World";
        var substrings = new[] { "Test", "Example" };
        var blackboard = new Blackboard();

        // Act
        var result = value.ValidateDoesContainAny(blackboard, "testValue", substrings);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("does not contain any of the specified substrings"));
        Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        Assert.That(result.ValidationException!.Context.Context.ContainsKey("value"), Is.True);
        Assert.That(result.ValidationException!.Context.Context.ContainsKey("substrings"), Is.True);
    }

    [Test]
    public void ValidateDoesContainAny_GenericCollectionWithMatch_ReturnsSuccess()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new[] { 3, 6 };
        var blackboard = new Blackboard();

        // Act
        var result = value.ValidateDoesContainAny(blackboard, "testValue", items);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesContainAny_GenericCollectionWithoutMatch_ReturnsFailure()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new[] { 6, 7 };
        var blackboard = new Blackboard();

        // Act
        var result = value.ValidateDoesContainAny(blackboard, "testValue", items);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("does not contain any of the specified items"));
        Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        Assert.That(result.ValidationException!.Context.Context.ContainsKey("value"), Is.True);
        Assert.That(result.ValidationException!.Context.Context.ContainsKey("items"), Is.True);
    }

    [Test]
    public void ValidateDoesContainAny_ReadOnlySpanWithMatch_ReturnsSuccess()
    {
        // Arrange
        var value = new ReadOnlySpan<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new[] { "c", "f" };
        var blackboard = new Blackboard();

        // Act
        var result = value.ValidateDoesContainAny(blackboard, "testValue", items);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesContainAny_ReadOnlySpanWithoutMatch_ReturnsFailure()
    {
        // Arrange
        var value = new ReadOnlySpan<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new[] { "f", "g" };
        var blackboard = new Blackboard();

        // Act
        var result = value.ValidateDoesContainAny(blackboard, "testValue", items);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("does not contain any of the specified items"));
        Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        Assert.That(result.ValidationException!.Context.Context.ContainsKey("value"), Is.True);
        Assert.That(result.ValidationException!.Context.Context.ContainsKey("items"), Is.True);
    }

    #endregion

    #region Ensure Tests

    [Test]
    public void EnsureDoesContainAny_StringWithMatch_ReturnsValue()
    {
        // Arrange
        var value = "Hello World";
        var substrings = new[] { "World", "Test" };

        // Act
        var result = value.EnsureDoesContainAny(substrings);

        // Assert
        Assert.That(result, Is.SameAs(value));
    }

    [Test]
    public void EnsureDoesContainAny_StringWithoutMatch_ThrowsValidationException()
    {
        // Arrange
        var value = "Hello World";
        var substrings = new[] { "Test", "Example" };

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureDoesContainAny(substrings));
        Assert.That(exception.Message, Does.Contain("does not contain any of the specified substrings"));
    }

    [Test]
    public void EnsureDoesContainAny_GenericCollectionWithMatch_ReturnsValue()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new[] { 3, 6 };

        // Act
        var result = value.EnsureDoesContainAny(items);

        // Assert
        Assert.That(result, Is.SameAs(value));
    }

    [Test]
    public void EnsureDoesContainAny_GenericCollectionWithoutMatch_ThrowsValidationException()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new[] { 6, 7 };

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureDoesContainAny(items));
        Assert.That(exception.Message, Does.Contain("does not contain any of the specified items"));
    }

    // [Test]
    // public void EnsureDoesContainAny_ReadOnlySpanWithMatch_ReturnsValue()
    // {
    //     // Arrange
    //     var value = new ReadOnlySpan<string>(new[] { "a", "b", "c", "d", "e" });
    //     var items = new List<string?> { "c", "f" };

    //     // Act
    //     var result = value.EnsureDoesContainAny<string>(items);

    //     // Assert
    //     Assert.That(result, Is.EqualTo(value));
    // }

    // [Test]
    // public void EnsureDoesContainAny_ReadOnlySpanWithoutMatch_ThrowsValidationException()
    // {
    //     // Arrange
    //     var array = new[] { "a", "b", "c", "d", "e" };
    //     var value = new ReadOnlySpan<string>(array);
    //     var items = new List<string?> { "f", "g" };

    //     // Act & Assert
    //     ValidationException? exception = null;
    //     try
    //     {
    //         value.EnsureDoesContainAny<string>(items);
    //     }
    //     catch (ValidationException ex)
    //     {
    //         exception = ex;
    //     }
    //     Assert.That(exception, Is.Not.Null);
    //     Assert.That(exception!.Message, Does.Contain("does not contain any of the specified items"));
    // }

    // [Test]
    // public void EnsureDoesContainAny_SpanWithMatch_ReturnsValue()
    // {
    //     // Arrange
    //     var array = new[] { "a", "b", "c", "d", "e" };
    //     var value = new Span<string>(array);
    //     var items = new List<string?> { "c", "f" };

    //     // Act
    //     var result = value.EnsureDoesContainAny<string>(items);

    //     // Assert
    //     Assert.That(result, Is.EqualTo(value));
    // }

    // [Test]
    // public void EnsureDoesContainAny_SpanWithoutMatch_ThrowsValidationException()
    // {
    //     // Arrange
    //     var array = new[] { "a", "b", "c", "d", "e" };
    //     var value = new Span<string>(array);
    //     var items = new List<string?> { "f", "g" };

    //     // Act & Assert
    //     ValidationException? exception = null;
    //     try
    //     {
    //         value.EnsureDoesContainAny<string>(items);
    //     }
    //     catch (ValidationException ex)
    //     {
    //         exception = ex;
    //     }
    //     Assert.That(exception, Is.Not.Null);
    //     Assert.That(exception!.Message, Does.Contain("does not contain any of the specified items"));
    // }

    [Test]
    public void EnsureDoesContainAny_MemoryWithMatch_ReturnsValue()
    {
        // Arrange
        var value = new Memory<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new List<string?> { "c", "f" };

        // Act
        var result = value.EnsureDoesContainAny<string>(items);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void EnsureDoesContainAny_MemoryWithoutMatch_ThrowsValidationException()
    {
        // Arrange
        var value = new Memory<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new List<string?> { "f", "g" };

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureDoesContainAny<string>(items));
        Assert.That(exception.Message, Does.Contain("does not contain any of the specified items"));
    }

    [Test]
    public void EnsureDoesContainAny_StringBuilderWithMatch_ReturnsValue()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substrings = new[] { "World", "Test" };

        // Act
        var result = value.EnsureDoesContainAny(substrings);

        // Assert
        Assert.That(result, Is.SameAs(value));
    }

    [Test]
    public void EnsureDoesContainAny_StringBuilderWithoutMatch_ThrowsValidationException()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substrings = new[] { "Test", "Example" };

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureDoesContainAny(substrings));
        Assert.That(exception.Message, Does.Contain("does not contain any of the specified substrings"));
    }

    #endregion
}
