using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;
using System.Collections.Generic;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestDoesNotContainAny
{
    [Test]
    public void CheckDoesNotContainAny_WithStringNotContainingAnySubstring_ReturnsTrue()
    {
        var value = "Hello World";
        var subStrings = new List<string> { "Universe", "Test", "Example" };
        var result = value.CheckDoesNotContainAny(subStrings);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAny_WithStringContainingAnySubstring_ReturnsFalse()
    {
        var value = "Hello World";
        var subStrings = new List<string> { "Universe", "World", "Test" };
        var result = value.CheckDoesNotContainAny(subStrings);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContainAny_WithNullString_ReturnsFalse()
    {
        string? value = null;
        var subStrings = new List<string> { "test" };
        var result = value.CheckDoesNotContainAny(subStrings);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContainAny_WithStringNotContainingAnyCharacter_ReturnsTrue()
    {
        var value = "Hello World";
        var characters = new List<char> { 'X', 'Y', 'Z' };
        var result = value.CheckDoesNotContainAny(characters);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAny_WithStringContainingAnyCharacter_ReturnsFalse()
    {
        var value = "Hello World";
        var characters = new List<char> { 'X', 'W', 'Z' };
        var result = value.CheckDoesNotContainAny(characters);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContainAny_WithCollectionNotContainingAnyItem_ReturnsTrue()
    {
        var collection = new List<int> { 1, 2, 3 };
        var items = new List<int> { 4, 5, 6 };
        var result = collection.CheckDoesNotContainAny(items);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAny_WithCollectionContainingAnyItem_ReturnsFalse()
    {
        var collection = new List<int> { 1, 2, 3 };
        var items = new List<int> { 4, 2, 5 };
        var result = collection.CheckDoesNotContainAny(items);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContainAny_WithNullCollection_ReturnsFalse()
    {
        ICollection<int>? collection = null;
        var items = new List<int> { 1 };
        var result = collection.CheckDoesNotContainAny(items);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContainAny_WithCollectionNotContainingAnyMatchingItem_ReturnsTrue()
    {
        var collection = new List<string> { "test", "other" };
        var predicates = new List<Func<string, bool>>
        {
            x => x.StartsWith("x"),
            x => x.StartsWith("y"),
            x => x.StartsWith("z")
        };
        var result = collection.CheckDoesNotContainAny(predicates);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAny_WithCollectionContainingAnyMatchingItem_ReturnsFalse()
    {
        var collection = new List<string> { "test", "other" };
        var predicates = new List<Func<string, bool>>
        {
            x => x.StartsWith("x"),
            x => x.StartsWith("t"),
            x => x.StartsWith("o")
        };
        var result = collection.CheckDoesNotContainAny(predicates);
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateDoesNotContainAny_WithStringNotContainingAnySubstring_ReturnsString()
    {
        var value = "Hello World";
        var subStrings = new List<string> { "Universe", "Test", "Example" };
        var propertyName = "TestProperty";
        var result = value.ValidateDoesNotContainAny(subStrings, propertyName);
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateDoesNotContainAny_WithStringContainingAnySubstring_ThrowsValidationException()
    {
        var value = "Hello World";
        var subStrings = new List<string> { "Universe", "World", "Test" };
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateDoesNotContainAny(subStrings, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("DoesNotContainAny"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not contain any of the substrings."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["subStrings"], Is.EqualTo(subStrings));
        });
    }

    [Test]
    public void ValidateDoesNotContainAny_WithStringNotContainingAnyCharacter_ReturnsString()
    {
        var value = "Hello World";
        var characters = new List<char> { 'X', 'Y', 'Z' };
        var propertyName = "TestProperty";
        var result = value.ValidateDoesNotContainAny(characters, propertyName);
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateDoesNotContainAny_WithStringContainingAnyCharacter_ThrowsValidationException()
    {
        var value = "Hello World";
        var characters = new List<char> { 'X', 'W', 'Z' };
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateDoesNotContainAny(characters, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("DoesNotContainAny"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not contain any of the characters."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["characters"], Is.EqualTo(characters));
        });
    }

    [Test]
    public void ValidateDoesNotContainAny_WithCollectionNotContainingAnyItem_ReturnsCollection()
    {
        var collection = new List<int> { 1, 2, 3 };
        var items = new List<int> { 4, 5, 6 };
        var propertyName = "TestProperty";
        var result = collection.ValidateDoesNotContainAny(items, propertyName);
        Assert.That(result, Is.EqualTo(collection));
    }

    [Test]
    public void ValidateDoesNotContainAny_WithCollectionContainingAnyItem_ThrowsValidationException()
    {
        var collection = new List<int> { 1, 2, 3 };
        var items = new List<int> { 4, 2, 5 };
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => collection.ValidateDoesNotContainAny(items, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("DoesNotContainAny"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not contain any of the items."));
            Assert.That(exception.ExceptionContext.Context["collection"], Is.EqualTo(collection));
            Assert.That(exception.ExceptionContext.Context["items"], Is.EqualTo(items));
        });
    }

    [Test]
    public void ValidateDoesNotContainAny_WithCollectionNotContainingAnyMatchingItem_ReturnsCollection()
    {
        var collection = new List<string> { "test", "other" };
        var predicates = new List<Func<string, bool>>
        {
            x => x.StartsWith("x"),
            x => x.StartsWith("y"),
            x => x.StartsWith("z")
        };
        var propertyName = "TestProperty";
        var result = collection.ValidateDoesNotContainAny(predicates, propertyName);
        Assert.That(result, Is.EqualTo(collection));
    }

    [Test]
    public void ValidateDoesNotContainAny_WithCollectionContainingAnyMatchingItem_ThrowsValidationException()
    {
        var collection = new List<string> { "test", "other" };
        var predicates = new List<Func<string, bool>>
        {
            x => x.StartsWith("x"),
            x => x.StartsWith("t"),
            x => x.StartsWith("o")
        };
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => collection.ValidateDoesNotContainAny(predicates, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("DoesNotContainAny"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not contain any items matching any of the predicates."));
            Assert.That(exception.ExceptionContext.Context["collection"], Is.EqualTo(collection));
            Assert.That(exception.ExceptionContext.Context["predicates"], Is.EqualTo(predicates));
        });
    }

    [Test]
    public void ValidateDoesNotContainAny_WithBlackboard_PreservesBlackboardInException()
    {
        var value = "Hello World";
        var subStrings = new List<string> { "Universe", "World", "Test" };
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();
        var exception = Assert.Throws<ValidationException>(() => value.ValidateDoesNotContainAny(subStrings, propertyName, StringComparison.Ordinal, blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 