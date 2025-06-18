using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;
using System.Collections.Generic;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestDoesNotContain
{
    [Test]
    public void CheckDoesNotContain_WithStringNotContainingSubstring_ReturnsTrue()
    {
        var value = "Hello World";
        var subString = "Universe";
        var result = value.CheckDoesNotContain(subString);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContain_WithStringContainingSubstring_ReturnsFalse()
    {
        var value = "Hello World";
        var subString = "World";
        var result = value.CheckDoesNotContain(subString);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContain_WithNullString_ReturnsFalse()
    {
        string? value = null;
        var subString = "test";
        var result = value.CheckDoesNotContain(subString);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContain_WithStringNotContainingSubstringInRange_ReturnsTrue()
    {
        var value = "Hello World";
        var subString = "Hello";
        var result = value.CheckDoesNotContain(subString, 6, 5);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContain_WithStringContainingSubstringInRange_ReturnsFalse()
    {
        var value = "Hello World";
        var subString = "World";
        var result = value.CheckDoesNotContain(subString, 6, 5);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContain_WithStringNotContainingCharacter_ReturnsTrue()
    {
        var value = "Hello World";
        var character = 'X';
        var result = value.CheckDoesNotContain(character);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContain_WithStringContainingCharacter_ReturnsFalse()
    {
        var value = "Hello World";
        var character = 'W';
        var result = value.CheckDoesNotContain(character);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContain_WithStringNotContainingCharacterInRange_ReturnsTrue()
    {
        var value = "Hello World";
        var character = 'H';
        var result = value.CheckDoesNotContain(character, 6, 5);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContain_WithStringContainingCharacterInRange_ReturnsFalse()
    {
        var value = "Hello World";
        var character = 'W';
        var result = value.CheckDoesNotContain(character, 6, 5);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContain_WithCollectionNotContainingItem_ReturnsTrue()
    {
        var collection = new List<int> { 1, 2, 3 };
        var item = 4;
        var result = collection.CheckDoesNotContain(item);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContain_WithCollectionContainingItem_ReturnsFalse()
    {
        var collection = new List<int> { 1, 2, 3 };
        var item = 2;
        var result = collection.CheckDoesNotContain(item);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContain_WithNullCollection_ReturnsFalse()
    {
        ICollection<int>? collection = null;
        var item = 1;
        var result = collection.CheckDoesNotContain(item);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContain_WithCollectionNotContainingMatchingItem_ReturnsTrue()
    {
        var collection = new List<string> { "test", "other" };
        var result = collection.CheckDoesNotContain(x => x.StartsWith("x"));
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContain_WithCollectionContainingMatchingItem_ReturnsFalse()
    {
        var collection = new List<string> { "test", "other" };
        var result = collection.CheckDoesNotContain(x => x.StartsWith("t"));
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateDoesNotContain_WithStringNotContainingSubstring_ReturnsString()
    {
        var value = "Hello World";
        var subString = "Universe";
        var propertyName = "TestProperty";
        var result = value.ValidateDoesNotContain(subString, propertyName);
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateDoesNotContain_WithStringContainingSubstring_ThrowsValidationException()
    {
        var value = "Hello World";
        var subString = "World";
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateDoesNotContain(subString, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("DoesNotContain"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not contain {subString}."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["subString"], Is.EqualTo(subString));
        });
    }

    [Test]
    public void ValidateDoesNotContain_WithStringNotContainingSubstringInRange_ReturnsString()
    {
        var value = "Hello World";
        var subString = "Hello";
        var propertyName = "TestProperty";
        var result = value.ValidateDoesNotContain(subString, propertyName, 6, 5);
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateDoesNotContain_WithStringContainingSubstringInRange_ThrowsValidationException()
    {
        var value = "Hello World";
        var subString = "World";
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateDoesNotContain(subString, propertyName, 6, 5));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("DoesNotContain"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not contain \"{subString}\" (in substring from 6 for 5 characters)."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["subString"], Is.EqualTo(subString));
            Assert.That(exception.ExceptionContext.Context["startIndex"], Is.EqualTo(6));
            Assert.That(exception.ExceptionContext.Context["count"], Is.EqualTo(5));
        });
    }

    [Test]
    public void ValidateDoesNotContain_WithStringNotContainingCharacter_ReturnsString()
    {
        var value = "Hello World";
        var character = 'X';
        var propertyName = "TestProperty";
        var result = value.ValidateDoesNotContain(character, propertyName);
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateDoesNotContain_WithStringContainingCharacter_ThrowsValidationException()
    {
        var value = "Hello World";
        var character = 'W';
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateDoesNotContain(character, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("DoesNotContain"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not contain '{character}'."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["character"], Is.EqualTo(character));
        });
    }

    [Test]
    public void ValidateDoesNotContain_WithCollectionNotContainingItem_ReturnsCollection()
    {
        var collection = new List<int> { 1, 2, 3 };
        var item = 4;
        var propertyName = "TestProperty";
        var result = collection.ValidateDoesNotContain(item, propertyName);
        Assert.That(result, Is.EqualTo(collection));
    }

    [Test]
    public void ValidateDoesNotContain_WithCollectionContainingItem_ThrowsValidationException()
    {
        var collection = new List<int> { 1, 2, 3 };
        var item = 2;
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => collection.ValidateDoesNotContain(item, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("DoesNotContain"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not contain item '{item}'."));
            Assert.That(exception.ExceptionContext.Context["collection"], Is.EqualTo(collection));
            Assert.That(exception.ExceptionContext.Context["item"], Is.EqualTo(item));
        });
    }

    [Test]
    public void ValidateDoesNotContain_WithCollectionNotContainingMatchingItem_ReturnsCollection()
    {
        var collection = new List<string> { "test", "other" };
        var propertyName = "TestProperty";
        var result = collection.ValidateDoesNotContain(x => x.StartsWith("x"), propertyName);
        Assert.That(result, Is.EqualTo(collection));
    }

    [Test]
    public void ValidateDoesNotContain_WithCollectionContainingMatchingItem_ThrowsValidationException()
    {
        var collection = new List<string> { "test", "other" };
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => collection.ValidateDoesNotContain(x => x.StartsWith("t"), propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("DoesNotContain"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} collection must not contain any items matching the predicate."));
            Assert.That(exception.ExceptionContext.Context["collection"], Is.EqualTo(collection));
        });
    }

    [Test]
    public void ValidateDoesNotContain_WithBlackboard_PreservesBlackboardInException()
    {
        var value = "Hello World";
        var subString = "World";
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();
        var exception = Assert.Throws<ValidationException>(() => value.ValidateDoesNotContain(subString, propertyName, StringComparison.Ordinal, blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 
