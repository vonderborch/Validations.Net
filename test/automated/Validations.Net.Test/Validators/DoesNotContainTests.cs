using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class DoesNotContainTests
{
    private Blackboard blackboard = null!;

    [SetUp]
    public void SetUp()
    {
        blackboard = new Blackboard();
    }

    #region String Tests

    [Test]
    public void CheckDoesNotContain_WithStringNotContainingSubstring_ReturnsTrue()
    {
        // Arrange
        var value = "Hello World";
        var substring = "Universe";

        // Act
        var result = value.CheckDoesNotContain(substring);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContain_WithStringContainingSubstring_ReturnsFalse()
    {
        // Arrange
        var value = "Hello World";
        var substring = "World";

        // Act
        var result = value.CheckDoesNotContain(substring);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContain_WithNullString_ReturnsTrue()
    {
        // Arrange
        string? value = null;
        var substring = "test";

        // Act
        var result = value.CheckDoesNotContain(substring);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContain_WithNullSubstring_ReturnsTrue()
    {
        // Arrange
        var value = "Hello World";
        string? substring = null;

        // Act
        var result = value.CheckDoesNotContain(substring);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContain_WithStringComparison_ReturnsTrue()
    {
        // Arrange
        var value = "Hello World";
        var substring = "world";

        // Act
        var result = value.CheckDoesNotContain(substring, StringComparison.Ordinal);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContain_WithStringComparison_ReturnsFalse()
    {
        // Arrange
        var value = "Hello World";
        var substring = "world";

        // Act
        var result = value.CheckDoesNotContain(substring, StringComparison.OrdinalIgnoreCase);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void EnsureDoesNotContain_WithStringNotContainingSubstring_ReturnsString()
    {
        // Arrange
        var value = "Hello World";
        var substring = "Universe";

        // Act
        var result = value.EnsureDoesNotContain(substring);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void EnsureDoesNotContain_WithStringContainingSubstring_ThrowsValidationException()
    {
        // Arrange
        var value = "Hello World";
        var substring = "World";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureDoesNotContain(substring));
        Assert.That(exception!.Message, Does.Contain("String contains the specified substring"));
    }

    [Test]
    public void EnsureDoesNotContain_WithStringComparison_ReturnsString()
    {
        // Arrange
        var value = "Hello World";
        var substring = "world";

        // Act
        var result = value.EnsureDoesNotContain(substring, StringComparison.Ordinal);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateDoesNotContain_WithStringNotContainingSubstring_ReturnsSuccess()
    {
        // Arrange
        var value = "Hello World";
        var substring = "Universe";

        // Act
        var result = value.ValidateDoesNotContain(substring, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesNotContain_WithStringContainingSubstring_ReturnsFailure()
    {
        // Arrange
        var value = "Hello World";
        var substring = "World";

        // Act
        var result = value.ValidateDoesNotContain(substring, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("String contains the specified substring"));
        Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        Assert.That(result.ValidationException!.Context.Context["value"], Is.EqualTo(value));
        Assert.That(result.ValidationException!.Context.Context["substring"], Is.EqualTo(substring));
    }

    [Test]
    public void ValidateDoesNotContain_WithStringComparison_ReturnsSuccess()
    {
        // Arrange
        var value = "Hello World";
        var substring = "world";

        // Act
        var result = value.ValidateDoesNotContain(substring, StringComparison.Ordinal, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesNotContain_WithStringComparison_ReturnsFailure()
    {
        // Arrange
        var value = "Hello World";
        var substring = "world";

        // Act
        var result = value.ValidateDoesNotContain(substring, StringComparison.OrdinalIgnoreCase, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("String contains the specified substring"));
        Assert.That(result.ValidationException!.Context.Context["comparisonType"], Is.EqualTo(StringComparison.OrdinalIgnoreCase));
    }

    #endregion

    #region Generic Collection Tests

    [Test]
    public void CheckDoesNotContain_WithGenericCollectionNotContainingItem_ReturnsTrue()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var item = 6;

        // Act
        var result = value.CheckDoesNotContain(item);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContain_WithGenericCollectionContainingItem_ReturnsFalse()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var item = 3;

        // Act
        var result = value.CheckDoesNotContain(item);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContain_WithNullGenericCollection_ReturnsTrue()
    {
        // Arrange
        List<int>? value = null;
        var item = 1;

        // Act
        var result = value.CheckDoesNotContain(item);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void EnsureDoesNotContain_WithGenericCollectionNotContainingItem_ReturnsCollection()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var item = 6;

        // Act
        var result = value.EnsureDoesNotContain(item);

        // Assert
        Assert.That(result, Is.SameAs(value));
    }

    [Test]
    public void EnsureDoesNotContain_WithGenericCollectionContainingItem_ThrowsValidationException()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var item = 3;

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureDoesNotContain(item));
        Assert.That(exception!.Message, Does.Contain("Collection contains the specified item"));
    }

    [Test]
    public void ValidateDoesNotContain_WithGenericCollectionNotContainingItem_ReturnsSuccess()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var item = 6;

        // Act
        var result = value.ValidateDoesNotContain(item, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesNotContain_WithGenericCollectionContainingItem_ReturnsFailure()
    {
        // Arrange
        var value = new List<int> { 1, 2, 3, 4, 5 };
        var item = 3;

        // Act
        var result = value.ValidateDoesNotContain(item, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Collection contains the specified item"));
        Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        Assert.That(result.ValidationException!.Context.Context["value"], Is.SameAs(value));
        Assert.That(result.ValidationException!.Context.Context["item"], Is.EqualTo(item));
    }

    #endregion

    #region Non-Generic Collection Tests

    [Test]
    public void CheckDoesNotContain_WithNonGenericCollectionNotContainingItem_ReturnsTrue()
    {
        // Arrange
        var value = new ArrayList { 1, "test", 3.14 };
        var item = "missing";

        // Act
        var result = value.CheckDoesNotContain(item);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContain_WithNonGenericCollectionContainingItem_ReturnsFalse()
    {
        // Arrange
        var value = new ArrayList { 1, "test", 3.14 };
        var item = "test";

        // Act
        var result = value.CheckDoesNotContain(item);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContain_WithNullNonGenericCollection_ReturnsTrue()
    {
        // Arrange
        ArrayList? value = null;
        var item = "test";

        // Act
        var result = value.CheckDoesNotContain(item);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void EnsureDoesNotContain_WithNonGenericCollectionNotContainingItem_ReturnsCollection()
    {
        // Arrange
        var value = new ArrayList { 1, "test", 3.14 };
        var item = "missing";

        // Act
        var result = value.EnsureDoesNotContain(item);

        // Assert
        Assert.That(result, Is.SameAs(value));
    }

    [Test]
    public void EnsureDoesNotContain_WithNonGenericCollectionContainingItem_ThrowsValidationException()
    {
        // Arrange
        var value = new ArrayList { 1, "test", 3.14 };
        var item = "test";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureDoesNotContain(item));
        Assert.That(exception!.Message, Does.Contain("Collection contains the specified item"));
    }

    [Test]
    public void ValidateDoesNotContain_WithNonGenericCollectionNotContainingItem_ReturnsSuccess()
    {
        // Arrange
        var value = new ArrayList { 1, "test", 3.14 };
        var item = "missing";

        // Act
        var result = value.ValidateDoesNotContain(item, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesNotContain_WithNonGenericCollectionContainingItem_ReturnsFailure()
    {
        // Arrange
        var value = new ArrayList { 1, "test", 3.14 };
        var item = "test";

        // Act
        var result = value.ValidateDoesNotContain(item, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Collection contains the specified item"));
        Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        Assert.That(result.ValidationException!.Context.Context["value"], Is.SameAs(value));
        Assert.That(result.ValidationException!.Context.Context["item"], Is.EqualTo(item));
    }

    #endregion

    #region Span Tests

    [Test]
    public void CheckDoesNotContain_WithReadOnlySpanNotContainingItem_ReturnsTrue()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new ReadOnlySpan<int>(array);
        var item = 6;

        // Act
        var result = value.CheckDoesNotContain(item);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContain_WithReadOnlySpanContainingItem_ReturnsFalse()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new ReadOnlySpan<int>(array);
        var item = 3;

        // Act
        var result = value.CheckDoesNotContain(item);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContain_WithSpanNotContainingItem_ReturnsTrue()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new Span<int>(array);
        var item = 6;

        // Act
        var result = value.CheckDoesNotContain(item);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContain_WithSpanContainingItem_ReturnsFalse()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new Span<int>(array);
        var item = 3;

        // Act
        var result = value.CheckDoesNotContain(item);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void EnsureDoesNotContain_WithReadOnlySpanNotContainingItem_ReturnsSpan()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new ReadOnlySpan<int>(array);
        var item = 6;

        // Act & Assert
        // ReadOnlySpan is a ref struct, so we can't use it in lambda expressions
        // Just verify the method doesn't throw
        value.EnsureDoesNotContain(item);
        Assert.Pass();
    }

    [Test]
    public void EnsureDoesNotContain_WithReadOnlySpanContainingItem_ThrowsValidationException()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new ReadOnlySpan<int>(array);
        var item = 3;

        // Act & Assert
        ValidationException? exception = null;
        try
        {
            value.EnsureDoesNotContain(item);
        }
        catch (ValidationException ex)
        {
            exception = ex;
        }
        
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.Message, Does.Contain("Span contains the specified item"));
    }

    [Test]
    public void EnsureDoesNotContain_WithSpanNotContainingItem_ReturnsSpan()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new Span<int>(array);
        var item = 6;

        // Act & Assert
        // Span is a ref struct, so we can't use it in lambda expressions
        // Just verify the method doesn't throw
        value.EnsureDoesNotContain(item);
        Assert.Pass();
    }

    [Test]
    public void EnsureDoesNotContain_WithSpanContainingItem_ThrowsValidationException()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new Span<int>(array);
        var item = 3;

        // Act & Assert
        ValidationException? exception = null;
        try
        {
            value.EnsureDoesNotContain(item);
        }
        catch (ValidationException ex)
        {
            exception = ex;
        }
        
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.Message, Does.Contain("Span contains the specified item"));
    }

    [Test]
    public void ValidateDoesNotContain_WithReadOnlySpanNotContainingItem_ReturnsSuccess()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new ReadOnlySpan<int>(array);
        var item = 6;

        // Act
        var result = value.ValidateDoesNotContain(item, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesNotContain_WithReadOnlySpanContainingItem_ReturnsFailure()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new ReadOnlySpan<int>(array);
        var item = 3;

        // Act
        var result = value.ValidateDoesNotContain(item, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Span contains the specified item"));
        Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        Assert.That(result.ValidationException!.Context.Context["item"], Is.EqualTo(item));
    }

    [Test]
    public void ValidateDoesNotContain_WithSpanNotContainingItem_ReturnsSuccess()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new Span<int>(array);
        var item = 6;

        // Act
        var result = value.ValidateDoesNotContain(item, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesNotContain_WithSpanContainingItem_ReturnsFailure()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new Span<int>(array);
        var item = 3;

        // Act
        var result = value.ValidateDoesNotContain(item, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Span contains the specified item"));
        Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        Assert.That(result.ValidationException!.Context.Context["item"], Is.EqualTo(item));
    }

    #endregion

    #region Memory Tests

    [Test]
    public void CheckDoesNotContain_WithMemoryNotContainingItem_ReturnsTrue()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new Memory<int>(array);
        var item = 6;

        // Act
        var result = value.CheckDoesNotContain(item);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContain_WithMemoryContainingItem_ReturnsFalse()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new Memory<int>(array);
        var item = 3;

        // Act
        var result = value.CheckDoesNotContain(item);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void EnsureDoesNotContain_WithMemoryNotContainingItem_ReturnsMemory()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new Memory<int>(array);
        var item = 6;

        // Act
        var result = value.EnsureDoesNotContain(item);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void EnsureDoesNotContain_WithMemoryContainingItem_ThrowsValidationException()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new Memory<int>(array);
        var item = 3;

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureDoesNotContain(item));
        Assert.That(exception!.Message, Does.Contain("Memory contains the specified item"));
    }

    [Test]
    public void ValidateDoesNotContain_WithMemoryNotContainingItem_ReturnsSuccess()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new Memory<int>(array);
        var item = 6;

        // Act
        var result = value.ValidateDoesNotContain(item, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesNotContain_WithMemoryContainingItem_ReturnsFailure()
    {
        // Arrange
        var array = new int[] { 1, 2, 3, 4, 5 };
        var value = new Memory<int>(array);
        var item = 3;

        // Act
        var result = value.ValidateDoesNotContain(item, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Memory contains the specified item"));
        Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        Assert.That(result.ValidationException!.Context.Context["item"], Is.EqualTo(item));
    }

    #endregion

    #region StringBuilder Tests

    [Test]
    public void CheckDoesNotContain_WithStringBuilderNotContainingSubstring_ReturnsTrue()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substring = "Universe";

        // Act
        var result = value.CheckDoesNotContain(substring);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContain_WithStringBuilderContainingSubstring_ReturnsFalse()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substring = "World";

        // Act
        var result = value.CheckDoesNotContain(substring);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContain_WithNullStringBuilder_ReturnsTrue()
    {
        // Arrange
        StringBuilder? value = null;
        var substring = "test";

        // Act
        var result = value.CheckDoesNotContain(substring);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContain_WithNullSubstringForStringBuilder_ReturnsTrue()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        string? substring = null;

        // Act
        var result = value.CheckDoesNotContain(substring);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void EnsureDoesNotContain_WithStringBuilderNotContainingSubstring_ReturnsStringBuilder()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substring = "Universe";

        // Act
        var result = value.EnsureDoesNotContain(substring);

        // Assert
        Assert.That(result, Is.SameAs(value));
    }

    [Test]
    public void EnsureDoesNotContain_WithStringBuilderContainingSubstring_ThrowsValidationException()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substring = "World";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureDoesNotContain(substring));
        Assert.That(exception!.Message, Does.Contain("StringBuilder contains the specified substring"));
    }

    [Test]
    public void ValidateDoesNotContain_WithStringBuilderNotContainingSubstring_ReturnsSuccess()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substring = "Universe";

        // Act
        var result = value.ValidateDoesNotContain(substring, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesNotContain_WithStringBuilderContainingSubstring_ReturnsFailure()
    {
        // Arrange
        var value = new StringBuilder("Hello World");
        var substring = "World";

        // Act
        var result = value.ValidateDoesNotContain(substring, blackboard);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("StringBuilder contains the specified substring"));
        Assert.That(result.ValidationException!.Blackboard, Is.SameAs(blackboard));
        Assert.That(result.ValidationException!.Context.Context["value"], Is.SameAs(value));
        Assert.That(result.ValidationException!.Context.Context["substring"], Is.EqualTo(substring));
    }

    #endregion
}
