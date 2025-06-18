using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;
using System.Collections.Generic;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestDoesContain
{
    [Test]
    public void CheckDoesContain_WithStringContainingSubstring_ReturnsTrue()
    {
        var value = "Hello World";
        var subString = "World";
        var result = value.CheckDoesContain(subString);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContain_WithStringNotContainingSubstring_ReturnsFalse()
    {
        var value = "Hello World";
        var subString = "Universe";
        var result = value.CheckDoesContain(subString);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContain_WithNullString_ReturnsFalse()
    {
        string? value = null;
        var subString = "test";
        var result = value.CheckDoesContain(subString);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContain_WithStringContainingSubstringInRange_ReturnsTrue()
    {
        var value = "Hello World";
        var subString = "World";
        var result = value.CheckDoesContain(subString, 6, 5);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContain_WithStringNotContainingSubstringInRange_ReturnsFalse()
    {
        var value = "Hello World";
        var subString = "Hello";
        var result = value.CheckDoesContain(subString, 6, 5);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContain_WithStringContainingCharacter_ReturnsTrue()
    {
        var value = "Hello World";
        var character = 'W';
        var result = value.CheckDoesContain(character);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContain_WithStringNotContainingCharacter_ReturnsFalse()
    {
        var value = "Hello World";
        var character = 'X';
        var result = value.CheckDoesContain(character);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContain_WithStringContainingCharacterInRange_ReturnsTrue()
    {
        var value = "Hello World";
        var character = 'W';
        var result = value.CheckDoesContain(character, 6, 5);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContain_WithStringNotContainingCharacterInRange_ReturnsFalse()
    {
        var value = "Hello World";
        var character = 'H';
        var result = value.CheckDoesContain(character, 6, 5);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContain_WithCollectionContainingItem_ReturnsTrue()
    {
        var collection = new List<int> { 1, 2, 3 };
        var item = 2;
        var result = collection.CheckDoesContain(item);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContain_WithCollectionNotContainingItem_ReturnsFalse()
    {
        var collection = new List<int> { 1, 2, 3 };
        var item = 4;
        var result = collection.CheckDoesContain(item);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContain_WithNullCollection_ReturnsFalse()
    {
        ICollection<int>? collection = null;
        var item = 1;
        var result = collection.CheckDoesContain(item);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContain_WithCollectionContainingMatchingItem_ReturnsTrue()
    {
        var collection = new List<string> { "test", "other" };
        var result = collection.CheckDoesContain(x => x.StartsWith("t"));
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContain_WithCollectionNotContainingMatchingItem_ReturnsFalse()
    {
        var collection = new List<string> { "test", "other" };
        var result = collection.CheckDoesContain(x => x.StartsWith("x"));
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateDoesContain_WithStringContainingSubstring_ReturnsString()
    {
        var value = "Hello World";
        var subString = "World";
        var propertyName = "TestProperty";
        var result = value.ValidateDoesContain(subString, propertyName);
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateDoesContain_WithStringNotContainingSubstring_ThrowsValidationException()
    {
        var value = "Hello World";
        var subString = "Universe";
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateDoesContain(subString, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("DoesContain"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must contain {subString}."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["subString"], Is.EqualTo(subString));
        });
    }

    [Test]
    public void ValidateDoesContain_WithStringContainingSubstringInRange_ReturnsString()
    {
        var value = "Hello World";
        var subString = "World";
        var propertyName = "TestProperty";
        var result = value.ValidateDoesContain(subString, propertyName, 6, 5);
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateDoesContain_WithStringNotContainingSubstringInRange_ThrowsValidationException()
    {
        var value = "Hello World";
        var subString = "Hello";
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateDoesContain(subString, propertyName, 6, 5));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("DoesContain"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must contain \"{subString}\" (in substring from 6 for 5 characters)."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["subString"], Is.EqualTo(subString));
            Assert.That(exception.ExceptionContext.Context["startIndex"], Is.EqualTo(6));
            Assert.That(exception.ExceptionContext.Context["count"], Is.EqualTo(5));
        });
    }

    [Test]
    public void ValidateDoesContain_WithStringContainingCharacter_ReturnsString()
    {
        var value = "Hello World";
        var character = 'W';
        var propertyName = "TestProperty";
        var result = value.ValidateDoesContain(character, propertyName);
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateDoesContain_WithStringNotContainingCharacter_ThrowsValidationException()
    {
        var value = "Hello World";
        var character = 'X';
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateDoesContain(character, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("DoesContain"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must contain '{character}'."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["character"], Is.EqualTo(character));
        });
    }

    [Test]
    public void ValidateDoesContain_WithCollectionContainingItem_ReturnsCollection()
    {
        var collection = new List<int> { 1, 2, 3 };
        var item = 2;
        var propertyName = "TestProperty";
        var result = collection.ValidateDoesContain(item, propertyName);
        Assert.That(result, Is.EqualTo(collection));
    }

    [Test]
    public void ValidateDoesContain_WithCollectionNotContainingItem_ThrowsValidationException()
    {
        var collection = new List<int> { 1, 2, 3 };
        var item = 4;
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => collection.ValidateDoesContain(item, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("DoesContain"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must contain item '{item}'."));
            Assert.That(exception.ExceptionContext.Context["collection"], Is.EqualTo(collection));
            Assert.That(exception.ExceptionContext.Context["item"], Is.EqualTo(item));
        });
    }

    [Test]
    public void ValidateDoesContain_WithCollectionContainingMatchingItem_ReturnsCollection()
    {
        var collection = new List<string> { "test", "other" };
        var propertyName = "TestProperty";
        var result = collection.ValidateDoesContain(x => x.StartsWith("t"), propertyName);
        Assert.That(result, Is.EqualTo(collection));
    }

    [Test]
    public void ValidateDoesContain_WithCollectionNotContainingMatchingItem_ThrowsValidationException()
    {
        var collection = new List<string> { "test", "other" };
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => collection.ValidateDoesContain(x => x.StartsWith("x"), propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("DoesContain"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} collection must contain at least one item matching the predicate."));
            Assert.That(exception.ExceptionContext.Context["collection"], Is.EqualTo(collection));
        });
    }

    [Test]
    public void ValidateDoesContain_WithBlackboard_PreservesBlackboardInException()
    {
        var value = "Hello World";
        var subString = "Universe";
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();
        var exception = Assert.Throws<ValidationException>(() => value.ValidateDoesContain(subString, propertyName, StringComparison.Ordinal, blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 
