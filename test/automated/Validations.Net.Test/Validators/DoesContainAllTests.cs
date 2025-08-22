using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class DoesContainAllTests
{
    #region Check Tests

    [Test]
    public void CheckDoesContainAll_StringWithAllSubstrings_ReturnsTrue()
    {
        // Arrange
        var value = "Hello World Test";
        var substrings = new[] { "World", "Test" };

        // Act
        var result = value.CheckDoesContainAll(substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAll_StringWithIList_ReturnsTrue()
    {
        // Arrange
        var value = "Hello World Test";
        var substrings = new List<string?> { "World", "Test" };

        // Act
        var result = value.CheckDoesContainAll(substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAll_StringWithStringComparison_ReturnsTrue()
    {
        // Arrange
        var value = "Hello WORLD TEST";
        var substrings = new[] { "world", "test" };

        // Act
        var result = value.CheckDoesContainAll(StringComparison.OrdinalIgnoreCase, substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAll_StringWithIListAndStringComparison_ReturnsTrue()
    {
        // Arrange
        var value = "Hello WORLD TEST";
        var substrings = new List<string?> { "world", "test" };

        // Act
        var result = value.CheckDoesContainAll(StringComparison.OrdinalIgnoreCase, substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAll_StringWithPartialMatch_ReturnsFalse()
    {
        // Arrange
        var value = "Hello World";
        var substrings = new[] { "World", "Test" };

        // Act
        var result = value.CheckDoesContainAll(substrings);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAll_StringWithoutMatch_ReturnsFalse()
    {
        // Arrange
        var value = "Hello World";
        var substrings = new[] { "Test", "Example" };

        // Act
        var result = value.CheckDoesContainAll(substrings);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAll_NullString_ReturnsFalse()
    {
        // Arrange
        string? value = null;
        var substrings = new[] { "Test", "Example" };

        // Act
        var result = value.CheckDoesContainAll(substrings);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAll_EmptySubstrings_ReturnsFalse()
    {
        // Arrange
        var value = "Hello World";
        var substrings = new string[0];

        // Act
        var result = value.CheckDoesContainAll(substrings);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAll_NullSubstrings_ReturnsFalse()
    {
        // Arrange
        var value = "Hello World";
        string[]? substrings = null;

        // Act
        var result = value.CheckDoesContainAll(substrings);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAll_GenericCollectionWithAllItems_ReturnsTrue()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new[] { 3, 5 };

        // Act
        var result = value.CheckDoesContainAll(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAll_GenericCollectionWithIList_ReturnsTrue()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new List<int> { 3, 5 };

        // Act
        var result = value.CheckDoesContainAll(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAll_GenericCollectionWithPartialMatch_ReturnsFalse()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new[] { 3, 6 };

        // Act
        var result = value.CheckDoesContainAll(items);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAll_GenericCollectionWithoutMatch_ReturnsFalse()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new[] { 6, 7 };

        // Act
        var result = value.CheckDoesContainAll(items);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAll_NonGenericCollectionWithAllItems_ReturnsTrue()
    {
        // Arrange
        var value = new ArrayList { 1, "test", 3.14 };
        var items = new object[] { "test", 3.14 };

        // Act
        var result = value.CheckDoesContainAll(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAll_NonGenericCollectionWithIList_ReturnsTrue()
    {
        // Arrange
        var value = new ArrayList { 1, "test", 3.14 };
        var items = new List<object?> { "test", 3.14 };

        // Act
        var result = value.CheckDoesContainAll(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAll_NonGenericCollectionWithPartialMatch_ReturnsFalse()
    {
        // Arrange
        var value = new ArrayList { 1, "test", 3.14 };
        var items = new object[] { "test", "example" };

        // Act
        var result = value.CheckDoesContainAll(items);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAll_NonGenericCollectionWithoutMatch_ReturnsFalse()
    {
        // Arrange
        var value = new ArrayList { 1, "test", 3.14 };
        var items = new object[] { "example", "sample" };

        // Act
        var result = value.CheckDoesContainAll(items);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAll_ReadOnlySpanWithAllItems_ReturnsTrue()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new ReadOnlySpan<string>(array);
        var items = new[] { "c", "e" };

        // Act
        var result = value.CheckDoesContainAll(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAll_ReadOnlySpanWithIList_ReturnsTrue()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new ReadOnlySpan<string>(array);
        var items = new List<string?> { "c", "e" };

        // Act
        var result = value.CheckDoesContainAll(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAll_ReadOnlySpanWithPartialMatch_ReturnsFalse()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new ReadOnlySpan<string>(array);
        var items = new[] { "c", "f" };

        // Act
        var result = value.CheckDoesContainAll(items);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAll_ReadOnlySpanWithoutMatch_ReturnsFalse()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new ReadOnlySpan<string>(array);
        var items = new[] { "f", "g" };

        // Act
        var result = value.CheckDoesContainAll(items);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAll_SpanWithAllItems_ReturnsTrue()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new Span<string>(array);
        var items = new[] { "c", "e" };

        // Act
        var result = value.CheckDoesContainAll(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAll_SpanWithIList_ReturnsTrue()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new Span<string>(array);
        var items = new List<string?> { "c", "e" };

        // Act
        var result = value.CheckDoesContainAll(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAll_SpanWithPartialMatch_ReturnsFalse()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new Span<string>(array);
        var items = new[] { "c", "f" };

        // Act
        var result = value.CheckDoesContainAll(items);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAll_SpanWithoutMatch_ReturnsFalse()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new Span<string>(array);
        var items = new[] { "f", "g" };

        // Act
        var result = value.CheckDoesContainAll(items);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAll_MemoryWithAllItems_ReturnsTrue()
    {
        // Arrange
        var value = new Memory<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new[] { "c", "e" };

        // Act
        var result = value.CheckDoesContainAll(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAll_MemoryWithIList_ReturnsTrue()
    {
        // Arrange
        var value = new Memory<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new List<string?> { "c", "e" };

        // Act
        var result = value.CheckDoesContainAll(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAll_MemoryWithPartialMatch_ReturnsFalse()
    {
        // Arrange
        var value = new Memory<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new[] { "c", "f" };

        // Act
        var result = value.CheckDoesContainAll(items);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAll_MemoryWithoutMatch_ReturnsFalse()
    {
        // Arrange
        var value = new Memory<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new[] { "f", "g" };

        // Act
        var result = value.CheckDoesContainAll(items);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAll_StringBuilderWithAllSubstrings_ReturnsTrue()
    {
        // Arrange
        var value = new StringBuilder("Hello World Test");
        var substrings = new[] { "World", "Test" };

        // Act
        var result = value.CheckDoesContainAll(substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAll_StringBuilderWithIList_ReturnsTrue()
    {
        // Arrange
        var value = new StringBuilder("Hello World Test");
        var substrings = new List<string?> { "World", "Test" };

        // Act
        var result = value.CheckDoesContainAll(substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAll_StringBuilderWithPartialMatch_ReturnsFalse()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substrings = new[] { "World", "Test" };

        // Act
        var result = value.CheckDoesContainAll(substrings);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAll_StringBuilderWithoutMatch_ReturnsFalse()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substrings = new[] { "Test", "Example" };

        // Act
        var result = value.CheckDoesContainAll(substrings);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAll_NullStringBuilder_ReturnsFalse()
    {
        // Arrange
        StringBuilder? value = null;
        var substrings = new[] { "Test", "Example" };

        // Act
        var result = value.CheckDoesContainAll(substrings);

        // Assert
        Assert.That(result, Is.False);
    }

    #endregion

    #region Validate Tests

    [Test]
    public void ValidateDoesContainAll_StringWithAllSubstrings_ReturnsSuccess()
    {
        // Arrange
        var value = "Hello World Test";
        var substrings = new[] { "World", "Test" };
        var blackboard = new Blackboard();

        // Act
        var result = value.ValidateDoesContainAll(blackboard, "testValue", substrings);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesContainAll_StringWithPartialMatch_ReturnsFailure()
    {
        // Arrange
        var value = "Hello World";
        var substrings = new[] { "World", "Test" };
        var blackboard = new Blackboard();

        // Act
        var result = value.ValidateDoesContainAll(blackboard, "testValue", substrings);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("does not contain all of the specified substrings"));
        Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        Assert.That(result.ValidationException!.Context.Context.ContainsKey("value"), Is.True);
        Assert.That(result.ValidationException!.Context.Context.ContainsKey("substrings"), Is.True);
    }

    [Test]
    public void ValidateDoesContainAll_GenericCollectionWithAllItems_ReturnsSuccess()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new[] { 3, 5 };
        var blackboard = new Blackboard();

        // Act
        var result = value.ValidateDoesContainAll(blackboard, "testValue", items);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesContainAll_GenericCollectionWithPartialMatch_ReturnsFailure()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new[] { 3, 6 };
        var blackboard = new Blackboard();

        // Act
        var result = value.ValidateDoesContainAll(blackboard, "testValue", items);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("does not contain all of the specified items"));
        Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        Assert.That(result.ValidationException!.Context.Context.ContainsKey("value"), Is.True);
        Assert.That(result.ValidationException!.Context.Context.ContainsKey("items"), Is.True);
    }

    [Test]
    public void ValidateDoesContainAll_ReadOnlySpanWithAllItems_ReturnsSuccess()
    {
        // Arrange
        var value = new ReadOnlySpan<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new[] { "c", "e" };
        var blackboard = new Blackboard();

        // Act
        var result = value.ValidateDoesContainAll(blackboard, "testValue", items);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesContainAll_ReadOnlySpanWithPartialMatch_ReturnsFailure()
    {
        // Arrange
        var value = new ReadOnlySpan<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new[] { "c", "f" };
        var blackboard = new Blackboard();

        // Act
        var result = value.ValidateDoesContainAll(blackboard, "testValue", items);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("does not contain all of the specified items"));
        Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        Assert.That(result.ValidationException!.Context.Context.ContainsKey("value"), Is.True);
        Assert.That(result.ValidationException!.Context.Context.ContainsKey("items"), Is.True);
    }

    #endregion

    #region Ensure Tests

    [Test]
    public void EnsureDoesContainAll_StringWithAllSubstrings_ReturnsValue()
    {
        // Arrange
        var value = "Hello World Test";
        var substrings = new[] { "World", "Test" };

        // Act
        var result = value.EnsureDoesContainAll(substrings);

        // Assert
        Assert.That(result, Is.SameAs(value));
    }

    [Test]
    public void EnsureDoesContainAll_StringWithPartialMatch_ThrowsValidationException()
    {
        // Arrange
        var value = "Hello World";
        var substrings = new[] { "World", "Test" };

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureDoesContainAll(substrings));
        Assert.That(exception.Message, Does.Contain("does not contain all of the specified substrings"));
    }

    [Test]
    public void EnsureDoesContainAll_GenericCollectionWithAllItems_ReturnsValue()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new[] { 3, 5 };

        // Act
        var result = value.EnsureDoesContainAll(items);

        // Assert
        Assert.That(result, Is.SameAs(value));
    }

    [Test]
    public void EnsureDoesContainAll_GenericCollectionWithPartialMatch_ThrowsValidationException()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new[] { 3, 6 };

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureDoesContainAll(items));
        Assert.That(exception.Message, Does.Contain("does not contain all of the specified items"));
    }

    [Test]
    public void EnsureDoesContainAll_MemoryWithAllItems_ReturnsValue()
    {
        // Arrange
        var value = new Memory<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new[] { "c", "e" };

        // Act
        var result = value.EnsureDoesContainAll(items);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void EnsureDoesContainAll_MemoryWithPartialMatch_ThrowsValidationException()
    {
        // Arrange
        var value = new Memory<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new[] { "c", "f" };

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureDoesContainAll(items));
        Assert.That(exception.Message, Does.Contain("does not contain all of the specified items"));
    }

    [Test]
    public void EnsureDoesContainAll_StringBuilderWithAllSubstrings_ReturnsValue()
    {
        // Arrange
        var value = new StringBuilder("Hello World Test");
        var substrings = new[] { "World", "Test" };

        // Act
        var result = value.EnsureDoesContainAll(substrings);

        // Assert
        Assert.That(result, Is.SameAs(value));
    }

    [Test]
    public void EnsureDoesContainAll_StringBuilderWithPartialMatch_ThrowsValidationException()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substrings = new[] { "World", "Test" };

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureDoesContainAll(substrings));
        Assert.That(exception.Message, Does.Contain("does not contain all of the specified substrings"));
    }

    #endregion
}
