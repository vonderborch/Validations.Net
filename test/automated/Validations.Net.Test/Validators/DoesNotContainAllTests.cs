using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class DoesNotContainAllTests
{
    #region Check Tests

    [Test]
    public void CheckDoesNotContainAll_StringWithPartialMatch_ReturnsTrue()
    {
        // Arrange
        var value = "Hello World";
        var substrings = new[] { "World", "Test" };

        // Act
        var result = value.CheckDoesNotContainAll(substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_StringWithIList_ReturnsTrue()
    {
        // Arrange
        var value = "Hello World";
        var substrings = new List<string?> { "World", "Test" };

        // Act
        var result = value.CheckDoesNotContainAll(substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_StringWithStringComparison_ReturnsTrue()
    {
        // Arrange
        var value = "Hello WORLD";
        var substrings = new[] { "world", "test" };

        // Act
        var result = value.CheckDoesNotContainAll(StringComparison.OrdinalIgnoreCase, substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_StringWithIListAndStringComparison_ReturnsTrue()
    {
        // Arrange
        var value = "Hello WORLD";
        var substrings = new List<string?> { "world", "test" };

        // Act
        var result = value.CheckDoesNotContainAll(StringComparison.OrdinalIgnoreCase, substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_StringWithAllSubstrings_ReturnsFalse()
    {
        // Arrange
        var value = "Hello World Test";
        var substrings = new[] { "World", "Test" };

        // Act
        var result = value.CheckDoesNotContainAll(substrings);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContainAll_StringWithoutMatch_ReturnsTrue()
    {
        // Arrange
        var value = "Hello World";
        var substrings = new[] { "Test", "Example" };

        // Act
        var result = value.CheckDoesNotContainAll(substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_NullString_ReturnsTrue()
    {
        // Arrange
        string? value = null;
        var substrings = new[] { "Test", "Example" };

        // Act
        var result = value.CheckDoesNotContainAll(substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_EmptySubstrings_ReturnsTrue()
    {
        // Arrange
        var value = "Hello World";
        var substrings = new string[0];

        // Act
        var result = value.CheckDoesNotContainAll(substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_NullSubstrings_ReturnsTrue()
    {
        // Arrange
        var value = "Hello World";
        string[]? substrings = null;

        // Act
        var result = value.CheckDoesNotContainAll(substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_GenericCollectionWithPartialMatch_ReturnsTrue()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new[] { 3, 6 };

        // Act
        var result = value.CheckDoesNotContainAll(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_GenericCollectionWithIList_ReturnsTrue()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new List<int> { 3, 6 };

        // Act
        var result = value.CheckDoesNotContainAll(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_GenericCollectionWithAllItems_ReturnsFalse()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new[] { 3, 5 };

        // Act
        var result = value.CheckDoesNotContainAll(items);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContainAll_GenericCollectionWithoutMatch_ReturnsTrue()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new[] { 6, 7 };

        // Act
        var result = value.CheckDoesNotContainAll(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_NonGenericCollectionWithPartialMatch_ReturnsTrue()
    {
        // Arrange
        var value = new ArrayList { 1, "test", 3.14 };
        var items = new object[] { "test", "example" };

        // Act
        var result = value.CheckDoesNotContainAll(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_NonGenericCollectionWithIList_ReturnsTrue()
    {
        // Arrange
        var value = new ArrayList { 1, "test", 3.14 };
        var items = new List<object?> { "test", "example" };

        // Act
        var result = value.CheckDoesNotContainAll(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_NonGenericCollectionWithAllItems_ReturnsFalse()
    {
        // Arrange
        var value = new ArrayList { 1, "test", 3.14 };
        var items = new object[] { "test", 3.14 };

        // Act
        var result = value.CheckDoesNotContainAll(items);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContainAll_NonGenericCollectionWithoutMatch_ReturnsTrue()
    {
        // Arrange
        var value = new ArrayList { 1, "test", 3.14 };
        var items = new object[] { "example", "sample" };

        // Act
        var result = value.CheckDoesNotContainAll(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_ReadOnlySpanWithPartialMatch_ReturnsTrue()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new ReadOnlySpan<string>(array);
        var items = new[] { "c", "f" };

        // Act
        var result = value.CheckDoesNotContainAll(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_ReadOnlySpanWithIList_ReturnsTrue()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new ReadOnlySpan<string>(array);
        var items = new List<string?> { "c", "f" };

        // Act
        var result = value.CheckDoesNotContainAll(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_ReadOnlySpanWithAllItems_ReturnsFalse()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new ReadOnlySpan<string>(array);
        var items = new[] { "c", "e" };

        // Act
        var result = value.CheckDoesNotContainAll(items);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContainAll_ReadOnlySpanWithoutMatch_ReturnsTrue()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new ReadOnlySpan<string>(array);
        var items = new[] { "f", "g" };

        // Act
        var result = value.CheckDoesNotContainAll(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_SpanWithPartialMatch_ReturnsTrue()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new Span<string>(array);
        var items = new[] { "c", "f" };

        // Act
        var result = value.CheckDoesNotContainAll(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_SpanWithIList_ReturnsTrue()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new Span<string>(array);
        var items = new List<string?> { "c", "f" };

        // Act
        var result = value.CheckDoesNotContainAll(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_SpanWithAllItems_ReturnsFalse()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new Span<string>(array);
        var items = new[] { "c", "e" };

        // Act
        var result = value.CheckDoesNotContainAll(items);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContainAll_SpanWithoutMatch_ReturnsTrue()
    {
        // Arrange
        var array = new[] { "a", "b", "c", "d", "e" };
        var value = new Span<string>(array);
        var items = new[] { "f", "g" };

        // Act
        var result = value.CheckDoesNotContainAll(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_MemoryWithPartialMatch_ReturnsTrue()
    {
        // Arrange
        var value = new Memory<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new[] { "c", "f" };

        // Act
        var result = value.CheckDoesNotContainAll(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_MemoryWithIList_ReturnsTrue()
    {
        // Arrange
        var value = new Memory<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new List<string?> { "c", "f" };

        // Act
        var result = value.CheckDoesNotContainAll(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_MemoryWithAllItems_ReturnsFalse()
    {
        // Arrange
        var value = new Memory<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new[] { "c", "e" };

        // Act
        var result = value.CheckDoesNotContainAll(items);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContainAll_MemoryWithoutMatch_ReturnsTrue()
    {
        // Arrange
        var value = new Memory<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new[] { "f", "g" };

        // Act
        var result = value.CheckDoesNotContainAll(items);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_StringBuilderWithPartialMatch_ReturnsTrue()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substrings = new[] { "World", "Test" };

        // Act
        var result = value.CheckDoesNotContainAll(substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_StringBuilderWithIList_ReturnsTrue()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substrings = new List<string?> { "World", "Test" };

        // Act
        var result = value.CheckDoesNotContainAll(substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_StringBuilderWithAllSubstrings_ReturnsFalse()
    {
        // Arrange
        var value = new StringBuilder("Hello World Test");
        var substrings = new[] { "World", "Test" };

        // Act
        var result = value.CheckDoesNotContainAll(substrings);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContainAll_StringBuilderWithoutMatch_ReturnsTrue()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substrings = new[] { "Test", "Example" };

        // Act
        var result = value.CheckDoesNotContainAll(substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_NullStringBuilder_ReturnsTrue()
    {
        // Arrange
        StringBuilder? value = null;
        var substrings = new[] { "Test", "Example" };

        // Act
        var result = value.CheckDoesNotContainAll(substrings);

        // Assert
        Assert.That(result, Is.True);
    }

    #endregion

    #region Validate Tests

    [Test]
    public void ValidateDoesNotContainAll_StringWithPartialMatch_ReturnsSuccess()
    {
        // Arrange
        var value = "Hello World";
        var substrings = new[] { "World", "Test" };
        var blackboard = new Blackboard();

        // Act
        var result = value.ValidateDoesNotContainAll(blackboard, "testValue", substrings);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAll_StringWithAllSubstrings_ReturnsFailure()
    {
        // Arrange
        var value = "Hello World Test";
        var substrings = new[] { "World", "Test" };
        var blackboard = new Blackboard();

        // Act
        var result = value.ValidateDoesNotContainAll(blackboard, "testValue", substrings);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("contains all of the specified substrings"));
        Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        Assert.That(result.ValidationException!.Context.Context.ContainsKey("value"), Is.True);
        Assert.That(result.ValidationException!.Context.Context.ContainsKey("substrings"), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAll_GenericCollectionWithPartialMatch_ReturnsSuccess()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new[] { 3, 6 };
        var blackboard = new Blackboard();

        // Act
        var result = value.ValidateDoesNotContainAll(blackboard, "testValue", items);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAll_GenericCollectionWithAllItems_ReturnsFailure()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new[] { 3, 5 };
        var blackboard = new Blackboard();

        // Act
        var result = value.ValidateDoesNotContainAll(blackboard, "testValue", items);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("contains all of the specified items"));
        Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        Assert.That(result.ValidationException!.Context.Context.ContainsKey("value"), Is.True);
        Assert.That(result.ValidationException!.Context.Context.ContainsKey("items"), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAll_ReadOnlySpanWithPartialMatch_ReturnsSuccess()
    {
        // Arrange
        var value = new ReadOnlySpan<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new[] { "c", "f" };
        var blackboard = new Blackboard();

        // Act
        var result = value.ValidateDoesNotContainAll(blackboard, "testValue", items);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAll_ReadOnlySpanWithAllItems_ReturnsFailure()
    {
        // Arrange
        var value = new ReadOnlySpan<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new[] { "c", "e" };
        var blackboard = new Blackboard();

        // Act
        var result = value.ValidateDoesNotContainAll(blackboard, "testValue", items);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("contains all of the specified items"));
        Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        Assert.That(result.ValidationException!.Context.Context.ContainsKey("value"), Is.True);
        Assert.That(result.ValidationException!.Context.Context.ContainsKey("items"), Is.True);
    }

    #endregion

    #region Ensure Tests

    [Test]
    public void EnsureDoesNotContainAll_StringWithPartialMatch_ReturnsValue()
    {
        // Arrange
        var value = "Hello World";
        var substrings = new[] { "World", "Test" };

        // Act
        var result = value.EnsureDoesNotContainAll(substrings);

        // Assert
        Assert.That(result, Is.SameAs(value));
    }

    [Test]
    public void EnsureDoesNotContainAll_StringWithAllSubstrings_ThrowsValidationException()
    {
        // Arrange
        var value = "Hello World Test";
        var substrings = new[] { "World", "Test" };

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureDoesNotContainAll(substrings));
        Assert.That(exception.Message, Does.Contain("contains all of the specified substrings"));
    }

    [Test]
    public void EnsureDoesNotContainAll_GenericCollectionWithPartialMatch_ReturnsValue()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new[] { 3, 6 };

        // Act
        var result = value.EnsureDoesNotContainAll(items);

        // Assert
        Assert.That(result, Is.SameAs(value));
    }

    [Test]
    public void EnsureDoesNotContainAll_GenericCollectionWithAllItems_ThrowsValidationException()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var items = new[] { 3, 5 };

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureDoesNotContainAll(items));
        Assert.That(exception.Message, Does.Contain("contains all of the specified items"));
    }

    [Test]
    public void EnsureDoesNotContainAll_MemoryWithPartialMatch_ReturnsValue()
    {
        // Arrange
        var value = new Memory<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new[] { "c", "f" };

        // Act
        var result = value.EnsureDoesNotContainAll(items);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void EnsureDoesNotContainAll_MemoryWithAllItems_ThrowsValidationException()
    {
        // Arrange
        var value = new Memory<string>(new[] { "a", "b", "c", "d", "e" });
        var items = new[] { "c", "e" };

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureDoesNotContainAll(items));
        Assert.That(exception.Message, Does.Contain("contains all of the specified items"));
    }

    [Test]
    public void EnsureDoesNotContainAll_StringBuilderWithPartialMatch_ReturnsValue()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substrings = new[] { "World", "Test" };

        // Act
        var result = value.EnsureDoesNotContainAll(substrings);

        // Assert
        Assert.That(result, Is.SameAs(value));
    }

    [Test]
    public void EnsureDoesNotContainAll_StringBuilderWithAllSubstrings_ThrowsValidationException()
    {
        // Arrange
        var value = new StringBuilder("Hello World Test");
        var substrings = new[] { "World", "Test" };

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureDoesNotContainAll(substrings));
        Assert.That(exception.Message, Does.Contain("contains all of the specified substrings"));
    }

    #endregion
}
