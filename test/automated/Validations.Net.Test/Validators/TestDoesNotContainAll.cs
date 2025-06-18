using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;
using System.Collections.Generic;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestDoesNotContainAll
{
    [Test]
    public void CheckDoesNotContainAll_WithStringNotContainingAllSubstrings_ReturnsTrue()
    {
        var value = "Hello World";
        var subStrings = new List<string> { "Hello", "Universe" };
        var result = value.CheckDoesNotContainAll(subStrings);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_WithStringContainingAllSubstrings_ReturnsFalse()
    {
        var value = "Hello World";
        var subStrings = new List<string> { "Hello", "World" };
        var result = value.CheckDoesNotContainAll(subStrings);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContainAll_WithNullString_ReturnsFalse()
    {
        string? value = null;
        var subStrings = new List<string> { "test" };
        var result = value.CheckDoesNotContainAll(subStrings);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContainAll_WithStringNotContainingAllCharacters_ReturnsTrue()
    {
        var value = "Hello World";
        var characters = new List<char> { 'H', 'X', 'o' };
        var result = value.CheckDoesNotContainAll(characters);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_WithStringContainingAllCharacters_ReturnsFalse()
    {
        var value = "Hello World";
        var characters = new List<char> { 'H', 'W', 'o' };
        var result = value.CheckDoesNotContainAll(characters);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContainAll_WithCollectionNotContainingAllItems_ReturnsTrue()
    {
        var collection = new List<int> { 1, 2, 3 };
        var items = new List<int> { 1, 4 };
        var result = collection.CheckDoesNotContainAll(items);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_WithCollectionContainingAllItems_ReturnsFalse()
    {
        var collection = new List<int> { 1, 2, 3 };
        var items = new List<int> { 1, 2 };
        var result = collection.CheckDoesNotContainAll(items);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContainAll_WithNullCollection_ReturnsFalse()
    {
        ICollection<int>? collection = null;
        var items = new List<int> { 1 };
        var result = collection.CheckDoesNotContainAll(items);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckDoesNotContainAll_WithCollectionNotContainingAllMatchingItems_ReturnsTrue()
    {
        var collection = new List<string> { "test", "other" };
        var predicates = new List<Func<string, bool>>
        {
            x => x.Length > 0,
            x => x.StartsWith("x")
        };
        var result = collection.CheckDoesNotContainAll(predicates);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckDoesNotContainAll_WithCollectionContainingAllMatchingItems_ReturnsFalse()
    {
        var collection = new List<string> { "test", "other" };
        var predicates = new List<Func<string, bool>>
        {
            x => x.Length > 0,
            x => x.Contains("t") || x.Contains("o")
        };
        var result = collection.CheckDoesNotContainAll(predicates);
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateDoesNotContainAll_WithStringNotContainingAllSubstrings_ReturnsString()
    {
        var value = "Hello World";
        var subStrings = new List<string> { "Hello", "Universe" };
        var propertyName = "TestProperty";
        var result = value.ValidateDoesNotContainAll(subStrings, propertyName);
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateDoesNotContainAll_WithStringContainingAllSubstrings_ThrowsValidationException()
    {
        var value = "Hello World";
        var subStrings = new List<string> { "Hello", "World" };
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateDoesNotContainAll(subStrings, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("DoesNotContainAll"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not contain any of the substrings."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["subStrings"], Is.EqualTo(subStrings));
        });
    }

    [Test]
    public void ValidateDoesNotContainAll_WithStringNotContainingAllCharacters_ReturnsString()
    {
        var value = "Hello World";
        var characters = new List<char> { 'H', 'X', 'o' };
        var propertyName = "TestProperty";
        var result = value.ValidateDoesNotContainAll(characters, propertyName);
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateDoesNotContainAll_WithStringContainingAllCharacters_ThrowsValidationException()
    {
        var value = "Hello World";
        var characters = new List<char> { 'H', 'W', 'o' };
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateDoesNotContainAll(characters, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("DoesNotContainAll"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not contain any of the characters."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["characters"], Is.EqualTo(characters));
        });
    }

    [Test]
    public void ValidateDoesNotContainAll_WithCollectionNotContainingAllItems_ReturnsCollection()
    {
        var collection = new List<int> { 1, 2, 3 };
        var items = new List<int> { 1, 4 };
        var propertyName = "TestProperty";
        var result = collection.ValidateDoesNotContainAll(items, propertyName);
        Assert.That(result, Is.EqualTo(collection));
    }

    [Test]
    public void ValidateDoesNotContainAll_WithCollectionContainingAllItems_ThrowsValidationException()
    {
        var collection = new List<int> { 1, 2, 3 };
        var items = new List<int> { 1, 2 };
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => collection.ValidateDoesNotContainAll(items, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("DoesNotContainAll"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not contain any of the items."));
            Assert.That(exception.ExceptionContext.Context["collection"], Is.EqualTo(collection));
            Assert.That(exception.ExceptionContext.Context["items"], Is.EqualTo(items));
        });
    }

    [Test]
    public void ValidateDoesNotContainAll_WithCollectionNotContainingAllMatchingItems_ReturnsCollection()
    {
        var collection = new List<string> { "test", "other" };
        var predicates = new List<Func<string, bool>>
        {
            x => x.Length > 0,
            x => x.StartsWith("x")
        };
        var propertyName = "TestProperty";
        var result = collection.ValidateDoesNotContainAll(predicates, propertyName);
        Assert.That(result, Is.EqualTo(collection));
    }

    [Test]
    public void ValidateDoesNotContainAll_WithCollectionContainingAllMatchingItems_ThrowsValidationException()
    {
        var collection = new List<string> { "test", "other" };
        var predicates = new List<Func<string, bool>>
        {
            x => x.Length > 0,
            x => x.Contains("t") || x.Contains("o")
        };
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => collection.ValidateDoesNotContainAll(predicates, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("DoesNotContainAll"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"No item in {propertyName} may match any of the predicates."));
            Assert.That(exception.ExceptionContext.Context["collection"], Is.EqualTo(collection));
            Assert.That(exception.ExceptionContext.Context["predicates"], Is.EqualTo(predicates));
        });
    }

    [Test]
    public void ValidateDoesNotContainAll_WithBlackboard_PreservesBlackboardInException()
    {
        var value = "Hello World";
        var subStrings = new List<string> { "Hello", "World" };
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();
        var exception = Assert.Throws<ValidationException>(() => value.ValidateDoesNotContainAll(subStrings, propertyName, StringComparison.Ordinal, blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 