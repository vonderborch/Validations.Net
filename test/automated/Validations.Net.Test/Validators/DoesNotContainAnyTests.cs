using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class DoesNotContainAnyTests
{
    #region Check Tests

    [Test]
    public void CheckDoesNotContainAny_StringWithoutMatch_ReturnsTrue()
    {
        // Arrange
        var value = "Hello World";
        var substrings = new[] { "Test", "Example" };

        // Act
        var result = value.CheckDoesNotContainAny(substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAny_StringWithIList_ReturnsTrue()
    {
        // Arrange
        var value = "Hello World";
        var substrings = new List<string?> { "Test", "Example" };

        // Act
        var result = value.CheckDoesNotContainAny(substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAny_StringWithStringComparison_ReturnsTrue()
    {
        // Arrange
        var value = "Hello WORLD";
        var substrings = new[] { "test", "example" };

        // Act
        var result = value.CheckDoesNotContainAny(StringComparison.OrdinalIgnoreCase, substrings);

        // Assert
        Assert.That(result, Is.True); // Should return true because "test" and "example" are not found in "Hello WORLD"
    }

    [Test]
    public void CheckDoesNotContainAny_StringWithIListAndStringComparison_ReturnsTrue()
    {
        // Arrange
        var value = "Hello WORLD";
        var substrings = new List<string?> { "test", "example" };

        // Act
        var result = value.CheckDoesNotContainAny(substrings, StringComparison.OrdinalIgnoreCase);

        // Assert
        Assert.That(result, Is.True); // Should return true because "test" and "example" are not found in "Hello WORLD"
    }

    [Test]
    public void CheckDoesNotContainAny_StringWithMatch_ReturnsFalse()
    {
        // Arrange
        var value = "Hello World";
        var substrings = new[] { "World", "Test" };

        // Act
        var result = value.CheckDoesNotContainAny(substrings);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContainAny_NullString_ReturnsTrue()
    {
        // Arrange
        string? value = null;
        var substrings = new[] { "Test", "Example" };

        // Act
        var result = value.CheckDoesNotContainAny(substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAny_EmptySubstrings_ReturnsTrue()
    {
        // Arrange
        var value = "Hello World";
        var substrings = new string[0];

        // Act
        var result = value.CheckDoesNotContainAny(substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAny_NullSubstrings_ReturnsTrue()
    {
        // Arrange
        var value = "Hello World";
        string[]? substrings = null;

        // Act
        var result = value.CheckDoesNotContainAny(substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAny_GenericCollectionWithoutMatch_ReturnsTrue()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new[] { 6, 7 };

        // Act
        var result = value.CheckDoesNotContainAny(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAny_GenericCollectionWithIList_ReturnsTrue()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new List<int> { 6, 7 }; // Use non-nullable int to match the collection type

        // Act
        var result = value.CheckDoesNotContainAny(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAny_GenericCollectionWithMatch_ReturnsFalse()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new[] { 3, 6 };

        // Act
        var result = value.CheckDoesNotContainAny(items);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContainAny_NonGenericCollectionWithoutMatch_ReturnsTrue()
    {
        // Arrange
        var value = new ArrayList { 1, "test", 3.14 };
        var items = new object[] { "example", "sample" };

        // Act
        var result = value.CheckDoesNotContainAny(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAny_NonGenericCollectionWithIList_ReturnsTrue()
    {
        // Arrange
        var value = new ArrayList { 1, "test", 3.14 };
        var items = new List<object?> { "example", "sample" };

        // Act
        var result = value.CheckDoesNotContainAny(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAny_NonGenericCollectionWithMatch_ReturnsFalse()
    {
        // Arrange
        var value = new ArrayList { 1, "test", 3.14 };
        var items = new object[] { "test", "example" };

        // Act
        var result = value.CheckDoesNotContainAny(items);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContainAny_ReadOnlySpanWithoutMatch_ReturnsTrue()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new ReadOnlySpan<string>(array);
        var items = new[] { "f", "g" };

        // Act
        var result = value.CheckDoesNotContainAny(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAny_ReadOnlySpanWithIList_ReturnsTrue()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new ReadOnlySpan<string>(array);
        var items = new List<string?> { "f", "g" };

        // Act
        var result = value.CheckDoesNotContainAny(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAny_ReadOnlySpanWithMatch_ReturnsFalse()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new ReadOnlySpan<string>(array);
        var items = new[] { "c", "f" };

        // Act
        var result = value.CheckDoesNotContainAny(items);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContainAny_SpanWithoutMatch_ReturnsTrue()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new Span<string>(array);
        var items = new[] { "f", "g" };

        // Act
        var result = value.CheckDoesNotContainAny(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAny_SpanWithIList_ReturnsTrue()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new Span<string>(array);
        var items = new List<string?> { "f", "g" };

        // Act
        var result = value.CheckDoesNotContainAny(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAny_SpanWithMatch_ReturnsFalse()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new Span<string>(array);
        var items = new[] { "c", "f" };

        // Act
        var result = value.CheckDoesNotContainAny(items);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContainAny_MemoryWithoutMatch_ReturnsTrue()
    {
        // Arrange
        var value = new Memory<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new[] { "f", "g" };

        // Act
        var result = value.CheckDoesNotContainAny(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAny_MemoryWithIList_ReturnsTrue()
    {
        // Arrange
        var value = new Memory<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new List<string?> { "f", "g" };

        // Act
        var result = value.CheckDoesNotContainAny(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAny_MemoryWithMatch_ReturnsFalse()
    {
        // Arrange
        var value = new Memory<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new[] { "c", "f" };

        // Act
        var result = value.CheckDoesNotContainAny(items);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContainAny_StringBuilderWithoutMatch_ReturnsTrue()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substrings = new[] { "Test", "Example" };

        // Act
        var result = value.CheckDoesNotContainAny(substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAny_StringBuilderWithIList_ReturnsTrue()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substrings = new List<string?> { "Test", "Example" };

        // Act
        var result = value.CheckDoesNotContainAny(substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAny_StringBuilderWithMatch_ReturnsFalse()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substrings = new[] { "World", "Test" };

        // Act
        var result = value.CheckDoesNotContainAny(substrings);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContainAny_NullStringBuilder_ReturnsTrue()
    {
        // Arrange
        StringBuilder? value = null;
        var substrings = new[] { "Test", "Example" };

        // Act
        var result = value.CheckDoesNotContainAny(substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    #endregion

    #region Validate Tests

    [Test]
    public void ValidateDoesNotContainAny_StringWithoutMatch_ReturnsSuccess()
    {
        // Arrange
        var value = "Hello World";
        var substrings = new[] { "Test", "Example" };
        var blackboard = new Blackboard();

        // Act
        var result = value.ValidateDoesNotContainAny(blackboard, "testValue", substrings);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAny_StringWithMatch_ReturnsFailure()
    {
        // Arrange
        var value = "Hello World";
        var substrings = new[] { "World", "Test" };
        var blackboard = new Blackboard();

        // Act
        var result = value.ValidateDoesNotContainAny(blackboard, "testValue", substrings);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("contains one or more of the specified substrings"));
        Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        Assert.That(result.ValidationException!.Context.Context.ContainsKey("value"), Is.True);
        Assert.That(result.ValidationException!.Context.Context.ContainsKey("substrings"), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAny_GenericCollectionWithoutMatch_ReturnsSuccess()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new[] { 6, 7 };
        var blackboard = new Blackboard();

        // Act
        var result = value.ValidateDoesNotContainAny(blackboard, "testValue", items);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAny_GenericCollectionWithMatch_ReturnsFailure()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new[] { 3, 6 };
        var blackboard = new Blackboard();

        // Act
        var result = value.ValidateDoesNotContainAny(blackboard, "testValue", items);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("contains one or more of the specified items"));
        Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        Assert.That(result.ValidationException!.Context.Context.ContainsKey("value"), Is.True);
        Assert.That(result.ValidationException!.Context.Context.ContainsKey("items"), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAny_ReadOnlySpanWithoutMatch_ReturnsSuccess()
    {
        // Arrange
        var value = new ReadOnlySpan<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new[] { "f", "g" };
        var blackboard = new Blackboard();

        // Act
        var result = value.ValidateDoesNotContainAny(blackboard, "testValue", items);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAny_ReadOnlySpanWithMatch_ReturnsFailure()
    {
        // Arrange
        var value = new ReadOnlySpan<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new[] { "c", "f" };
        var blackboard = new Blackboard();

        // Act
        var result = value.ValidateDoesNotContainAny(blackboard, "testValue", items);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("contains one or more of the specified items"));
        Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        Assert.That(result.ValidationException!.Context.Context.ContainsKey("value"), Is.True);
        Assert.That(result.ValidationException!.Context.Context.ContainsKey("items"), Is.True);
    }

    #endregion

    #region Ensure Tests

    [Test]
    public void EnsureDoesNotContainAny_StringWithoutMatch_ReturnsValue()
    {
        // Arrange
        var value = "Hello World";
        var substrings = new[] { "Test", "Example" };

        // Act
        var result = value.EnsureDoesNotContainAny(substrings);

        // Assert
        Assert.That(result, Is.SameAs(value));
    }

    [Test]
    public void EnsureDoesNotContainAny_StringWithMatch_ThrowsValidationException()
    {
        // Arrange
        var value = "Hello World";
        var substrings = new[] { "World", "Test" };

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureDoesNotContainAny(substrings));
        Assert.That(exception.Message, Does.Contain("contains one or more of the specified substrings"));
    }

    [Test]
    public void EnsureDoesNotContainAny_GenericCollectionWithoutMatch_ReturnsValue()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new[] { 6, 7 };

        // Act
        var result = value.EnsureDoesNotContainAny(items);

        // Assert
        Assert.That(result, Is.SameAs(value));
    }

    [Test]
    public void EnsureDoesNotContainAny_GenericCollectionWithMatch_ThrowsValidationException()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new[] { 3, 6 };

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureDoesNotContainAny(items));
        Assert.That(exception.Message, Does.Contain("contains one or more of the specified items"));
    }

    // [Test]
    // public void EnsureDoesNotContainAny_ReadOnlySpanWithoutMatch_ReturnsValue()
    // {
    //     // Arrange
    //     var array = new[] { "a", "b", "c", "d", "e" };
    //     var value = new ReadOnlySpan<string>(array);
    //     var items = new List<string?> { "f", "g" };

    //     // Act
    //     var result = value.EnsureDoesNotContainAny<string>(items);

    //     // Assert
    //     Assert.That(result, Is.EqualTo(value));
    // }

    // [Test]
    // public void EnsureDoesNotContainAny_ReadOnlySpanWithMatch_ThrowsValidationException()
    // {
    //     // Arrange
    //     var array = new[] { "a", "b", "c", "d", "e" };
    //     var value = new ReadOnlySpan<string>(array);
    //     var items = new List<string?> { "c", "f" };

    //     // Act & Assert
    //     ValidationException? exception = null;
    //     try
    //     {
    //         value.EnsureDoesNotContainAny<string>(items);
    //     }
    //     catch (ValidationException ex)
    //     {
    //         exception = ex;
    //     }
    //     Assert.That(exception, Is.Not.Null);
    //     Assert.That(exception!.Message, Does.Contain("contains one or more of the specified items"));
    // }

    // [Test]
    // public void EnsureDoesNotContainAny_SpanWithoutMatch_ReturnsValue()
    // {
    //     // Arrange
    //     var array = new[] { "a", "b", "c", "d", "e" };
    //     var value = new Span<string>(array);
    //     var items = new List<string?> { "f", "g" };

    //     // Act
    //     var result = value.EnsureDoesNotContainAny<string>(items);

    //     // Assert
    //     Assert.That(result, Is.EqualTo(value));
    // }

    // [Test]
    // public void EnsureDoesNotContainAny_SpanWithMatch_ThrowsValidationException()
    // {
    //     // Arrange
    //     var array = new[] { "a", "b", "c", "d", "e" };
    //     var value = new Span<string>(array);
    //     var items = new List<string?> { "c", "f" };

    //     // Act & Assert
    //     ValidationException? exception = null;
    //     try
    //     {
    //         value.EnsureDoesNotContainAny<string>(items);
    //     }
    //     catch (ValidationException ex)
    //     {
    //         exception = ex;
    //     }
    //     Assert.That(exception, Is.Not.Null);
    //     Assert.That(exception!.Message, Does.Contain("contains one or more of the specified items"));
    // }

    [Test]
    public void EnsureDoesNotContainAny_MemoryWithoutMatch_ReturnsValue()
    {
        // Arrange
        var value = new Memory<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new List<string?> { "f", "g" };

        // Act
        var result = value.EnsureDoesNotContainAny<string>(items);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void EnsureDoesNotContainAny_MemoryWithMatch_ThrowsValidationException()
    {
        // Arrange
        var value = new Memory<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new List<string?> { "c", "f" };

        // Act & Assert
        ValidationException? exception = null;
        try
        {
            value.EnsureDoesNotContainAny<string>(items);
        }
        catch (ValidationException ex)
        {
            exception = ex;
        }
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.Message, Does.Contain("contains one or more of the specified items"));
    }

    [Test]
    public void EnsureDoesNotContainAny_StringBuilderWithoutMatch_ReturnsValue()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substrings = new[] { "Test", "Example" };

        // Act
        var result = value.EnsureDoesNotContainAny(substrings);

        // Assert
        Assert.That(result, Is.SameAs(value));
    }

    [Test]
    public void EnsureDoesNotContainAny_StringBuilderWithMatch_ThrowsValidationException()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substrings = new[] { "World", "Test" };

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureDoesNotContainAny(substrings));
        Assert.That(exception.Message, Does.Contain("contains one or more of the specified substrings"));
    }

    #endregion
}
