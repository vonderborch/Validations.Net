using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;
using System.Collections.Generic;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestIsLength
{
    [Test]
    public void CheckIsLength_WithCorrectCollectionLength_ReturnsTrue()
    {
        var collection = new List<int> { 1, 2, 3 };
        var length = 3;
        var result = collection.CheckIsLength(length);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsLength_WithIncorrectCollectionLength_ReturnsFalse()
    {
        var collection = new List<int> { 1, 2, 3 };
        var length = 4;
        var result = collection.CheckIsLength(length);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsLength_WithNullCollection_ReturnsFalse()
    {
        ICollection<int>? collection = null;
        var length = 3;
        var result = collection.CheckIsLength(length);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsLength_WithCorrectStringLength_ReturnsTrue()
    {
        var value = "test";
        var length = 4;
        var result = value.CheckIsLength(length);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsLength_WithIncorrectStringLength_ReturnsFalse()
    {
        var value = "test";
        var length = 5;
        var result = value.CheckIsLength(length);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsLength_WithNullString_ReturnsFalse()
    {
        string? value = null;
        var length = 4;
        var result = value.CheckIsLength(length);
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateIsLength_WithCorrectCollectionLength_ReturnsCollection()
    {
        var collection = new List<int> { 1, 2, 3 };
        var length = 3;
        var propertyName = "TestProperty";
        var result = collection.ValidateIsLength(length, propertyName);
        Assert.That(result, Is.EqualTo(collection));
    }

    [Test]
    public void ValidateIsLength_WithIncorrectCollectionLength_ThrowsValidationException()
    {
        var collection = new List<int> { 1, 2, 3 };
        var length = 4;
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => collection.ValidateIsLength(length, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsLength"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must have a length of {length}."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(collection));
            Assert.That(exception.ExceptionContext.Context["length"], Is.EqualTo(length));
        });
    }

    [Test]
    public void ValidateIsLength_WithNullCollection_ThrowsValidationException()
    {
        ICollection<int>? collection = null;
        var length = 3;
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => collection.ValidateIsLength(length, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsLength"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must have a length of {length}."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.Null);
            Assert.That(exception.ExceptionContext.Context["length"], Is.EqualTo(length));
        });
    }

    [Test]
    public void ValidateIsLength_WithCorrectStringLength_ReturnsString()
    {
        var value = "test";
        var length = 4;
        var propertyName = "TestProperty";
        var result = value.ValidateIsLength(length, propertyName);
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsLength_WithIncorrectStringLength_ThrowsValidationException()
    {
        var value = "test";
        var length = 5;
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsLength(length, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsLength"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must have a length of {length}."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["length"], Is.EqualTo(length));
        });
    }

    [Test]
    public void ValidateIsLength_WithNullString_ThrowsValidationException()
    {
        string? value = null;
        var length = 4;
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsLength(length, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsLength"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must have a length of {length}."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.Null);
            Assert.That(exception.ExceptionContext.Context["length"], Is.EqualTo(length));
        });
    }

    [Test]
    public void ValidateIsLength_WithBlackboard_PreservesBlackboardInException()
    {
        var collection = new List<int> { 1, 2, 3 };
        var length = 4;
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();
        var exception = Assert.Throws<ValidationException>(() => collection.ValidateIsLength(length, propertyName, blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 