using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;
using System.Collections.Generic;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestDoesContainAll
{
    [Test]
    public void CheckDoesContainAll_WithStringContainingAllSubstrings_ReturnsTrue()
    {
        var value = "Hello World";
        var subStrings = new List<string> { "Hello", "World" };
        var result = value.CheckDoesContainAll(subStrings);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAll_WithStringNotContainingAllSubstrings_ReturnsFalse()
    {
        var value = "Hello World";
        var subStrings = new List<string> { "Hello", "Universe" };
        var result = value.CheckDoesContainAll(subStrings);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAll_WithNullString_ReturnsFalse()
    {
        string? value = null;
        var subStrings = new List<string> { "test" };
        var result = value.CheckDoesContainAll(subStrings);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAll_WithStringContainingAllCharacters_ReturnsTrue()
    {
        var value = "Hello World";
        var characters = new List<char> { 'H', 'W', 'o' };
        var result = value.CheckDoesContainAll(characters);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAll_WithStringNotContainingAllCharacters_ReturnsFalse()
    {
        var value = "Hello World";
        var characters = new List<char> { 'H', 'X', 'o' };
        var result = value.CheckDoesContainAll(characters);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAll_WithCollectionContainingAllItems_ReturnsTrue()
    {
        var collection = new List<int> { 1, 2, 3 };
        var items = new List<int> { 1, 2 };
        var result = collection.CheckDoesContainAll(items);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAll_WithCollectionNotContainingAllItems_ReturnsFalse()
    {
        var collection = new List<int> { 1, 2, 3 };
        var items = new List<int> { 1, 4 };
        var result = collection.CheckDoesContainAll(items);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAll_WithNullCollection_ReturnsFalse()
    {
        ICollection<int>? collection = null;
        var items = new List<int> { 1 };
        var result = collection.CheckDoesContainAll(items);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesContainAll_WithCollectionContainingAllMatchingItems_ReturnsTrue()
    {
        var collection = new List<string> { "test", "other" };
        var predicates = new List<Func<string, bool>>
        {
            x => x.Length > 0,
            x => x.Contains("t") || x.Contains("o")
        };
        var result = collection.CheckDoesContainAll(predicates);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesContainAll_WithCollectionNotContainingAllMatchingItems_ReturnsFalse()
    {
        var collection = new List<string> { "test", "other" };
        var predicates = new List<Func<string, bool>>
        {
            x => x.Length > 0,
            x => x.StartsWith("x")
        };
        var result = collection.CheckDoesContainAll(predicates);
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateDoesContainAll_WithStringContainingAllSubstrings_ReturnsString()
    {
        var value = "Hello World";
        var subStrings = new List<string> { "Hello", "World" };
        var propertyName = "TestProperty";
        var result = value.ValidateDoesContainAll(subStrings, propertyName);
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateDoesContainAll_WithStringNotContainingAllSubstrings_ThrowsValidationException()
    {
        var value = "Hello World";
        var subStrings = new List<string> { "Hello", "Universe" };
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateDoesContainAll(subStrings, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("DoesContainAll"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must contain all of the substrings."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["subStrings"], Is.EqualTo(subStrings));
        });
    }

    [Test]
    public void ValidateDoesContainAll_WithStringContainingAllCharacters_ReturnsString()
    {
        var value = "Hello World";
        var characters = new List<char> { 'H', 'W', 'o' };
        var propertyName = "TestProperty";
        var result = value.ValidateDoesContainAll(characters, propertyName);
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateDoesContainAll_WithStringNotContainingAllCharacters_ThrowsValidationException()
    {
        var value = "Hello World";
        var characters = new List<char> { 'H', 'X', 'o' };
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateDoesContainAll(characters, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("DoesContainAll"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must contain all of the characters."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["characters"], Is.EqualTo(characters));
        });
    }

    [Test]
    public void ValidateDoesContainAll_WithCollectionContainingAllItems_ReturnsCollection()
    {
        var collection = new List<int> { 1, 2, 3 };
        var items = new List<int> { 1, 2 };
        var propertyName = "TestProperty";
        var result = collection.ValidateDoesContainAll(items, propertyName);
        Assert.That(result, Is.EqualTo(collection));
    }

    [Test]
    public void ValidateDoesContainAll_WithCollectionNotContainingAllItems_ThrowsValidationException()
    {
        var collection = new List<int> { 1, 2, 3 };
        var items = new List<int> { 1, 4 };
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => collection.ValidateDoesContainAll(items, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("DoesContainAll"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must contain all of the items."));
            Assert.That(exception.ExceptionContext.Context["collection"], Is.EqualTo(collection));
            Assert.That(exception.ExceptionContext.Context["items"], Is.EqualTo(items));
        });
    }

    [Test]
    public void ValidateDoesContainAll_WithCollectionContainingAllMatchingItems_ReturnsCollection()
    {
        var collection = new List<string> { "test", "other" };
        var predicates = new List<Func<string, bool>>
        {
            x => x.Length > 0,
            x => x.Contains("t") || x.Contains("o")
        };
        var propertyName = "TestProperty";
        var result = collection.ValidateDoesContainAll(predicates, propertyName);
        Assert.That(result, Is.EqualTo(collection));
    }

    [Test]
    public void ValidateDoesContainAll_WithCollectionNotContainingAllMatchingItems_ThrowsValidationException()
    {
        var collection = new List<string> { "test", "other" };
        var predicates = new List<Func<string, bool>>
        {
            x => x.Length > 0,
            x => x.StartsWith("x")
        };
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => collection.ValidateDoesContainAll(predicates, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("DoesContainAll"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"All items in {propertyName} must match all of the predicates."));
            Assert.That(exception.ExceptionContext.Context["collection"], Is.EqualTo(collection));
            Assert.That(exception.ExceptionContext.Context["predicates"], Is.EqualTo(predicates));
        });
    }

    [Test]
    public void ValidateDoesContainAll_WithBlackboard_PreservesBlackboardInException()
    {
        var value = "Hello World";
        var subStrings = new List<string> { "Hello", "Universe" };
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();
        var exception = Assert.Throws<ValidationException>(() => value.ValidateDoesContainAll(subStrings, propertyName, StringComparison.Ordinal, blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 