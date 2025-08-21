using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class DoesContainTests
{
    private Blackboard blackboard = null!;

    [SetUp]
    public void SetUp()
    {
        blackboard = new Blackboard();
    }

    #region String Tests

    [Test]
    public void CheckDoesContain_WithStringContainingSubstring_ReturnsTrue()
    {
        // Arrange
        var value = "Hello World";
        var substring = "World";

        // Act
        var result = value.CheckDoesContain(substring);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContain_WithStringNotContainingSubstring_ReturnsFalse()
    {
        // Arrange
        var value = "Hello World";
        var substring = "Universe";

        // Act
        var result = value.CheckDoesContain(substring);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContain_WithNullString_ReturnsFalse()
    {
        // Arrange
        string? value = null;
        var substring = "test";

        // Act
        var result = value.CheckDoesContain(substring);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContain_WithNullSubstring_ReturnsFalse()
    {
        // Arrange
        var value = "Hello World";
        string? substring = null;

        // Act
        var result = value.CheckDoesContain(substring);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContain_WithStringComparison_ReturnsTrue()
    {
        // Arrange
        var value = "Hello World";
        var substring = "world";

        // Act
        var result = value.CheckDoesContain(substring, StringComparison.OrdinalIgnoreCase);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContain_WithStringComparison_ReturnsFalse()
    {
        // Arrange
        var value = "Hello World";
        var substring = "world";

        // Act
        var result = value.CheckDoesContain(substring, StringComparison.Ordinal);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void EnsureDoesContain_WithStringContainingSubstring_ReturnsString()
    {
        // Arrange
        var value = "Hello World";
        var substring = "World";

        // Act
        var result = value.EnsureDoesContain(substring);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void EnsureDoesContain_WithStringNotContainingSubstring_ThrowsValidationException()
    {
        // Arrange
        var value = "Hello World";
        var substring = "Universe";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureDoesContain(substring));
        Assert.That(exception!.Message, Does.Contain("String does not contain the specified substring"));
    }

    [Test]
    public void EnsureDoesContain_WithStringComparison_ReturnsString()
    {
        // Arrange
        var value = "Hello World";
        var substring = "world";

        // Act
        var result = value.EnsureDoesContain(substring, StringComparison.OrdinalIgnoreCase);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateDoesContain_WithStringContainingSubstring_ReturnsSuccess()
    {
        // Arrange
        var value = "Hello World";
        var substring = "World";

        // Act
        var result = value.ValidateDoesContain(substring, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesContain_WithStringNotContainingSubstring_ReturnsFailure()
    {
        // Arrange
        var value = "Hello World";
        var substring = "Universe";

        // Act
        var result = value.ValidateDoesContain(substring, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("String does not contain the specified substring"));
        Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        Assert.That(result.ValidationException!.Context.Context["value"], Is.EqualTo(value));
        Assert.That(result.ValidationException!.Context.Context["substring"], Is.EqualTo(substring));
    }

    [Test]
    public void ValidateDoesContain_WithStringComparison_ReturnsSuccess()
    {
        // Arrange
        var value = "Hello World";
        var substring = "world";

        // Act
        var result = value.ValidateDoesContain(substring, StringComparison.OrdinalIgnoreCase, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesContain_WithStringComparison_ReturnsFailure()
    {
        // Arrange
        var value = "Hello World";
        var substring = "world";

        // Act
        var result = value.ValidateDoesContain(substring, StringComparison.Ordinal, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("String does not contain the specified substring"));
        Assert.That(result.ValidationException!.Context.Context["comparisonType"], Is.EqualTo(StringComparison.Ordinal));
    }

    #endregion

    #region Generic Collection Tests

    [Test]
    public void CheckDoesContain_WithGenericCollectionContainingItem_ReturnsTrue()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var item = 3;

        // Act
        var result = value.CheckDoesContain(item);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContain_WithGenericCollectionNotContainingItem_ReturnsFalse()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var item = 6;

        // Act
        var result = value.CheckDoesContain(item);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContain_WithNullGenericCollection_ReturnsFalse()
    {
        // Arrange
        List<int>? value = null;
        var item = 1;

        // Act
        var result = value.CheckDoesContain(item);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void EnsureDoesContain_WithGenericCollectionContainingItem_ReturnsCollection()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var item = 3;

        // Act
        var result = value.EnsureDoesContain(item);

        // Assert
        Assert.That(result, Is.SameAs(value));
    }

    [Test]
    public void EnsureDoesContain_WithGenericCollectionNotContainingItem_ThrowsValidationException()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var item = 6;

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureDoesContain(item));
        Assert.That(exception!.Message, Does.Contain("Collection does not contain the specified item"));
    }

    [Test]
    public void ValidateDoesContain_WithGenericCollectionContainingItem_ReturnsSuccess()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var item = 3;

        // Act
        var result = value.ValidateDoesContain(item, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesContain_WithGenericCollectionNotContainingItem_ReturnsFailure()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var item = 6;

        // Act
        var result = value.ValidateDoesContain(item, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Collection does not contain the specified item"));
        Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        Assert.That(result.ValidationException!.Context.Context["value"], Is.SameAs(value));
        Assert.That(result.ValidationException!.Context.Context["item"], Is.EqualTo(item));
    }

    #endregion

    #region Non-Generic Collection Tests

    [Test]
    public void CheckDoesContain_WithNonGenericCollectionContainingItem_ReturnsTrue()
    {
        // Arrange
        var value = new ArrayList { 1, "test", 3.14 };
        var item = "test";

        // Act
        var result = value.CheckDoesContain(item);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContain_WithNonGenericCollectionNotContainingItem_ReturnsFalse()
    {
        // Arrange
        var value = new ArrayList { 1, "test", 3.14 };
        var item = "missing";

        // Act
        var result = value.CheckDoesContain(item);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContain_WithNullNonGenericCollection_ReturnsFalse()
    {
        // Arrange
        ArrayList? value = null;
        var item = "test";

        // Act
        var result = value.CheckDoesContain(item);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void EnsureDoesContain_WithNonGenericCollectionContainingItem_ReturnsCollection()
    {
        // Arrange
        var value = new ArrayList { 1, "test", 3.14 };
        var item = "test";

        // Act
        var result = value.EnsureDoesContain(item);

        // Assert
        Assert.That(result, Is.SameAs(value));
    }

    [Test]
    public void EnsureDoesContain_WithNonGenericCollectionNotContainingItem_ThrowsValidationException()
    {
        // Arrange
        var value = new ArrayList { 1, "test", 3.14 };
        var item = "missing";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureDoesContain(item));
        Assert.That(exception!.Message, Does.Contain("Collection does not contain the specified item"));
    }

    [Test]
    public void ValidateDoesContain_WithNonGenericCollectionContainingItem_ReturnsSuccess()
    {
        // Arrange
        var value = new ArrayList { 1, "test", 3.14 };
        var item = "test";

        // Act
        var result = value.ValidateDoesContain(item, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesContain_WithNonGenericCollectionNotContainingItem_ReturnsFailure()
    {
        // Arrange
        var value = new ArrayList { 1, "test", 3.14 };
        var item = "missing";

        // Act
        var result = value.ValidateDoesContain(item, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Collection does not contain the specified item"));
        Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        Assert.That(result.ValidationException!.Context.Context["value"], Is.SameAs(value));
        Assert.That(result.ValidationException!.Context.Context["item"], Is.EqualTo(item));
    }

    #endregion

    #region Span Tests

    [Test]
    public void CheckDoesContain_WithReadOnlySpanContainingItem_ReturnsTrue()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new ReadOnlySpan<int>(array);
        var item = 3;

        // Act
        var result = value.CheckDoesContain(item);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContain_WithReadOnlySpanNotContainingItem_ReturnsFalse()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new ReadOnlySpan<int>(array);
        var item = 6;

        // Act
        var result = value.CheckDoesContain(item);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContain_WithSpanContainingItem_ReturnsTrue()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new Span<int>(array);
        var item = 3;

        // Act
        var result = value.CheckDoesContain(item);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContain_WithSpanNotContainingItem_ReturnsFalse()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new Span<int>(array);
        var item = 6;

        // Act
        var result = value.CheckDoesContain(item);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void EnsureDoesContain_WithReadOnlySpanContainingItem_ReturnsSpan()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new ReadOnlySpan<int>(array);
        var item = 3;

        // Act & Assert
        // ReadOnlySpan is a ref struct, so we can't use it in lambda expressions
        // Just verify the method doesn't throw
        value.EnsureDoesContain(item);
        Assert.Pass();
    }

    [Test]
    public void EnsureDoesContain_WithReadOnlySpanNotContainingItem_ThrowsValidationException()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new ReadOnlySpan<int>(array);
        var item = 6;

        // Act & Assert
        ValidationException? exception = null;
        try
        {
            value.EnsureDoesContain(item);
        }
        catch (ValidationException ex)
        {
            exception = ex;
        }
        
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.Message, Does.Contain("Span does not contain the specified item"));
    }

    [Test]
    public void EnsureDoesContain_WithSpanContainingItem_ReturnsSpan()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new Span<int>(array);
        var item = 3;

        // Act & Assert
        // Span is a ref struct, so we can't use it in lambda expressions
        // Just verify the method doesn't throw
        value.EnsureDoesContain(item);
        Assert.Pass();
    }

    [Test]
    public void EnsureDoesContain_WithSpanNotContainingItem_ThrowsValidationException()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new Span<int>(array);
        var item = 6;

        // Act & Assert
        ValidationException? exception = null;
        try
        {
            value.EnsureDoesContain(item);
        }
        catch (ValidationException ex)
        {
            exception = ex;
        }
        
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.Message, Does.Contain("Span does not contain the specified item"));
    }

    [Test]
    public void ValidateDoesContain_WithReadOnlySpanContainingItem_ReturnsSuccess()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new ReadOnlySpan<int>(array);
        var item = 3;

        // Act
        var result = value.ValidateDoesContain(item, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesContain_WithReadOnlySpanNotContainingItem_ReturnsFailure()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new ReadOnlySpan<int>(array);
        var item = 6;

        // Act
        var result = value.ValidateDoesContain(item, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Span does not contain the specified item"));
        Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        Assert.That(result.ValidationException!.Context.Context["item"], Is.EqualTo(item));
    }

    [Test]
    public void ValidateDoesContain_WithSpanContainingItem_ReturnsSuccess()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new Span<int>(array);
        var item = 3;

        // Act
        var result = value.ValidateDoesContain(item, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesContain_WithSpanNotContainingItem_ReturnsFailure()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new Span<int>(array);
        var item = 6;

        // Act
        var result = value.ValidateDoesContain(item, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Span does not contain the specified item"));
        Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        Assert.That(result.ValidationException!.Context.Context["item"], Is.EqualTo(item));
    }

    #endregion

    #region Memory Tests

    [Test]
    public void CheckDoesContain_WithMemoryContainingItem_ReturnsTrue()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new Memory<int>(array);
        var item = 3;

        // Act
        var result = value.CheckDoesContain(item);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContain_WithMemoryNotContainingItem_ReturnsFalse()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new Memory<int>(array);
        var item = 6;

        // Act
        var result = value.CheckDoesContain(item);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void EnsureDoesContain_WithMemoryContainingItem_ReturnsMemory()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new Memory<int>(array);
        var item = 3;

        // Act
        var result = value.EnsureDoesContain(item);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void EnsureDoesContain_WithMemoryNotContainingItem_ThrowsValidationException()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new Memory<int>(array);
        var item = 6;

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureDoesContain(item));
        Assert.That(exception!.Message, Does.Contain("Memory does not contain the specified item"));
    }

    [Test]
    public void ValidateDoesContain_WithMemoryContainingItem_ReturnsSuccess()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new Memory<int>(array);
        var item = 3;

        // Act
        var result = value.ValidateDoesContain(item, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesContain_WithMemoryNotContainingItem_ReturnsFailure()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new Memory<int>(array);
        var item = 6;

        // Act
        var result = value.ValidateDoesContain(item, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Memory does not contain the specified item"));
        Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        Assert.That(result.ValidationException!.Context.Context["item"], Is.EqualTo(item));
    }

    #endregion

    #region StringBuilder Tests

    [Test]
    public void CheckDoesContain_WithStringBuilderContainingSubstring_ReturnsTrue()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substring = "World";

        // Act
        var result = value.CheckDoesContain(substring);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContain_WithStringBuilderNotContainingSubstring_ReturnsFalse()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substring = "Universe";

        // Act
        var result = value.CheckDoesContain(substring);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContain_WithNullStringBuilder_ReturnsFalse()
    {
        // Arrange
        StringBuilder? value = null;
        var substring = "test";

        // Act
        var result = value.CheckDoesContain(substring);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContain_WithNullSubstringForStringBuilder_ReturnsFalse()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        string? substring = null;

        // Act
        var result = value.CheckDoesContain(substring);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void EnsureDoesContain_WithStringBuilderContainingSubstring_ReturnsStringBuilder()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substring = "World";

        // Act
        var result = value.EnsureDoesContain(substring);

        // Assert
        Assert.That(result, Is.SameAs(value));
    }

    [Test]
    public void EnsureDoesContain_WithStringBuilderNotContainingSubstring_ThrowsValidationException()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substring = "Universe";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureDoesContain(substring));
        Assert.That(exception!.Message, Does.Contain("StringBuilder does not contain the specified substring"));
    }

    [Test]
    public void ValidateDoesContain_WithStringBuilderContainingSubstring_ReturnsSuccess()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substring = "World";

        // Act
        var result = value.ValidateDoesContain(substring, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesContain_WithStringBuilderNotContainingSubstring_ReturnsFailure()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substring = "Universe";

        // Act
        var result = value.ValidateDoesContain(substring, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("StringBuilder does not contain the specified substring"));
        Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        Assert.That(result.ValidationException!.Context.Context["value"], Is.SameAs(value));
        Assert.That(result.ValidationException!.Context.Context["substring"], Is.EqualTo(substring));
    }

    #endregion
}
