using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;
using System.Collections.Generic;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestIsNotLength
{
    [Test]
    public void CheckIsNotLength_WithIncorrectCollectionLength_ReturnsTrue()
    {
        var collection = new List<int> { 1, 2, 3 };
        var length = 4;
        var result = collection.CheckIsNotLength(length);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotLength_WithCorrectCollectionLength_ReturnsFalse()
    {
        var collection = new List<int> { 1, 2, 3 };
        var length = 3;
        var result = collection.CheckIsNotLength(length);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotLength_WithNullCollection_ReturnsTrue()
    {
        ICollection<int>? collection = null;
        var length = 3;
        var result = collection.CheckIsNotLength(length);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotLength_WithIncorrectStringLength_ReturnsTrue()
    {
        var value = "test";
        var length = 5;
        var result = value.CheckIsNotLength(length);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotLength_WithCorrectStringLength_ReturnsFalse()
    {
        var value = "test";
        var length = 4;
        var result = value.CheckIsNotLength(length);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotLength_WithNullString_ReturnsTrue()
    {
        string? value = null;
        var length = 4;
        var result = value.CheckIsNotLength(length);
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateIsNotLength_WithIncorrectCollectionLength_ReturnsCollection()
    {
        var collection = new List<int> { 1, 2, 3 };
        var length = 4;
        var propertyName = "TestProperty";
        var result = collection.ValidateIsNotLength(length, propertyName);
        Assert.That(result, Is.EqualTo(collection));
    }

    [Test]
    public void ValidateIsNotLength_WithCorrectCollectionLength_ThrowsValidationException()
    {
        var collection = new List<int> { 1, 2, 3 };
        var length = 3;
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => collection.ValidateIsNotLength(length, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNotLength"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not have a length of {length}."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(collection));
            Assert.That(exception.ExceptionContext.Context["length"], Is.EqualTo(length));
        });
    }

    [Test]
    public void ValidateIsNotLength_WithNullCollection_ReturnsNull()
    {
        ICollection<int>? collection = null;
        var length = 3;
        var propertyName = "TestProperty";
        var result = collection.ValidateIsNotLength(length, propertyName);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void ValidateIsNotLength_WithIncorrectStringLength_ReturnsString()
    {
        var value = "test";
        var length = 5;
        var propertyName = "TestProperty";
        var result = value.ValidateIsNotLength(length, propertyName);
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsNotLength_WithCorrectStringLength_ThrowsValidationException()
    {
        var value = "test";
        var length = 4;
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotLength(length, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNotLength"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not have a length of {length}."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["length"], Is.EqualTo(length));
        });
    }

    [Test]
    public void ValidateIsNotLength_WithNullString_ReturnsNull()
    {
        string? value = null;
        var length = 4;
        var propertyName = "TestProperty";
        var result = value.ValidateIsNotLength(length, propertyName);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void ValidateIsNotLength_WithBlackboard_PreservesBlackboardInException()
    {
        var collection = new List<int> { 1, 2, 3 };
        var length = 3;
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();
        var exception = Assert.Throws<ValidationException>(() => collection.ValidateIsNotLength(length, propertyName, blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 