using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;
using System.Collections.Generic;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestDoesContainAny
{
    [Test]
    public void CheckDoesContainAny_WithStringContainingAnySubstring_ReturnsTrue()
    {
        var value = "Hello World";
        var subStrings = new List<string> { "Universe", "World", "Test" };
        var result = value.CheckDoesContainAny(subStrings);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAny_WithStringNotContainingAnySubstring_ReturnsFalse()
    {
        var value = "Hello World";
        var subStrings = new List<string> { "Universe", "Test", "Example" };
        var result = value.CheckDoesContainAny(subStrings);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAny_WithNullString_ReturnsFalse()
    {
        string? value = null;
        var subStrings = new List<string> { "test" };
        var result = value.CheckDoesContainAny(subStrings);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAny_WithStringContainingAnyCharacter_ReturnsTrue()
    {
        var value = "Hello World";
        var characters = new List<char> { 'X', 'W', 'Z' };
        var result = value.CheckDoesContainAny(characters);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAny_WithStringNotContainingAnyCharacter_ReturnsFalse()
    {
        var value = "Hello World";
        var characters = new List<char> { 'X', 'Y', 'Z' };
        var result = value.CheckDoesContainAny(characters);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAny_WithCollectionContainingAnyItem_ReturnsTrue()
    {
        var collection = new List<int> { 1, 2, 3 };
        var items = new List<int> { 4, 2, 5 };
        var result = collection.CheckDoesContainAny(items);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAny_WithCollectionNotContainingAnyItem_ReturnsFalse()
    {
        var collection = new List<int> { 1, 2, 3 };
        var items = new List<int> { 4, 5, 6 };
        var result = collection.CheckDoesContainAny(items);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAny_WithNullCollection_ReturnsFalse()
    {
        ICollection<int>? collection = null;
        var items = new List<int> { 1 };
        var result = collection.CheckDoesContainAny(items);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAny_WithCollectionContainingAnyMatchingItem_ReturnsTrue()
    {
        var collection = new List<string> { "test", "other" };
        var predicates = new List<Func<string, bool>>
        {
            x => x.StartsWith("x"),
            x => x.StartsWith("t"),
            x => x.StartsWith("o")
        };
        var result = collection.CheckDoesContainAny(predicates);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAny_WithCollectionNotContainingAnyMatchingItem_ReturnsFalse()
    {
        var collection = new List<string> { "test", "other" };
        var predicates = new List<Func<string, bool>>
        {
            x => x.StartsWith("x"),
            x => x.StartsWith("y"),
            x => x.StartsWith("z")
        };
        var result = collection.CheckDoesContainAny(predicates);
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateDoesContainAny_WithStringContainingAnySubstring_ReturnsString()
    {
        var value = "Hello World";
        var subStrings = new List<string> { "Universe", "World", "Test" };
        var propertyName = "TestProperty";
        var result = value.ValidateDoesContainAny(subStrings, propertyName);
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateDoesContainAny_WithStringNotContainingAnySubstring_ThrowsValidationException()
    {
        var value = "Hello World";
        var subStrings = new List<string> { "Universe", "Test", "Example" };
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateDoesContainAny(subStrings, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("DoesContainAny"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must contain any of the substrings."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["subStrings"], Is.EqualTo(subStrings));
        });
    }

    [Test]
    public void ValidateDoesContainAny_WithStringContainingAnyCharacter_ReturnsString()
    {
        var value = "Hello World";
        var characters = new List<char> { 'X', 'W', 'Z' };
        var propertyName = "TestProperty";
        var result = value.ValidateDoesContainAny(characters, propertyName);
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateDoesContainAny_WithStringNotContainingAnyCharacter_ThrowsValidationException()
    {
        var value = "Hello World";
        var characters = new List<char> { 'X', 'Y', 'Z' };
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateDoesContainAny(characters, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("DoesContainAny"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must contain any of the characters."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["characters"], Is.EqualTo(characters));
        });
    }

    [Test]
    public void ValidateDoesContainAny_WithCollectionContainingAnyItem_ReturnsCollection()
    {
        var collection = new List<int> { 1, 2, 3 };
        var items = new List<int> { 4, 2, 5 };
        var propertyName = "TestProperty";
        var result = collection.ValidateDoesContainAny(items, propertyName);
        Assert.That(result, Is.EqualTo(collection));
    }

    [Test]
    public void ValidateDoesContainAny_WithCollectionNotContainingAnyItem_ThrowsValidationException()
    {
        var collection = new List<int> { 1, 2, 3 };
        var items = new List<int> { 4, 5, 6 };
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => collection.ValidateDoesContainAny(items, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("DoesContainAny"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must contain any of the items."));
            Assert.That(exception.ExceptionContext.Context["collection"], Is.EqualTo(collection));
            Assert.That(exception.ExceptionContext.Context["items"], Is.EqualTo(items));
        });
    }

    [Test]
    public void ValidateDoesContainAny_WithCollectionContainingAnyMatchingItem_ReturnsCollection()
    {
        var collection = new List<string> { "test", "other" };
        var predicates = new List<Func<string, bool>>
        {
            x => x.StartsWith("x"),
            x => x.StartsWith("t"),
            x => x.StartsWith("o")
        };
        var propertyName = "TestProperty";
        var result = collection.ValidateDoesContainAny(predicates, propertyName);
        Assert.That(result, Is.EqualTo(collection));
    }

    [Test]
    public void ValidateDoesContainAny_WithCollectionNotContainingAnyMatchingItem_ThrowsValidationException()
    {
        var collection = new List<string> { "test", "other" };
        var predicates = new List<Func<string, bool>>
        {
            x => x.StartsWith("x"),
            x => x.StartsWith("y"),
            x => x.StartsWith("z")
        };
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => collection.ValidateDoesContainAny(predicates, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("DoesContainAny"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must contain at least one item matching any of the predicates."));
            Assert.That(exception.ExceptionContext.Context["collection"], Is.EqualTo(collection));
            Assert.That(exception.ExceptionContext.Context["predicates"], Is.EqualTo(predicates));
        });
    }

    [Test]
    public void ValidateDoesContainAny_WithBlackboard_PreservesBlackboardInException()
    {
        var value = "Hello World";
        var subStrings = new List<string> { "Universe", "Test", "Example" };
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();
        var exception = Assert.Throws<ValidationException>(() => value.ValidateDoesContainAny(subStrings, propertyName, StringComparison.Ordinal, blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 