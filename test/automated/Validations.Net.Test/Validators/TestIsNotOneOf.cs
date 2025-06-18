using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;
using System.Collections.Generic;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestIsNotOneOf
{
    [Test]
    public void CheckIsNotOneOf_WithValueNotInArray_ReturnsTrue()
    {
        var value = 4;
        var options = new[] { 1, 2, 3 };
        var result = value.CheckIsNotOneOf(options);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotOneOf_WithValueInArray_ReturnsFalse()
    {
        var value = 2;
        var options = new[] { 1, 2, 3 };
        var result = value.CheckIsNotOneOf(options);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotOneOf_WithNullValue_ReturnsTrue()
    {
        string? value = null;
        var options = new[] { "1", "2", "3" };
        var result = value.CheckIsNotOneOf(options);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotOneOf_WithValueNotInCollection_ReturnsTrue()
    {
        var value = "notInList";
        var options = new List<string> { "test", "other" };
        var result = value.CheckIsNotOneOf(options);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotOneOf_WithValueInCollection_ReturnsFalse()
    {
        var value = "test";
        var options = new List<string> { "test", "other" };
        var result = value.CheckIsNotOneOf(options);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotOneOf_WithNullValueAndCollection_ReturnsTrue()
    {
        string? value = null;
        var options = new List<string> { "test", "other" };
        var result = value.CheckIsNotOneOf(options);
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateIsNotOneOf_WithValueNotInArray_ReturnsValue()
    {
        var value = 4;
        var options = new[] { 1, 2, 3 };
        var propertyName = "TestProperty";
        var result = value.ValidateIsNotOneOf(propertyName, null, options);
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsNotOneOf_WithValueInArray_ThrowsValidationException()
    {
        var value = 2;
        var options = new[] { 1, 2, 3 };
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotOneOf(propertyName, null, options));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNotOneOf"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not be one of the specified values."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["options"], Is.EqualTo(options));
        });
    }

    [Test]
    public void ValidateIsNotOneOf_WithNullValueAndArray_ReturnsNull()
    {
        string? value = null;
        var options = new[] { "1", "2", "3" };
        var propertyName = "TestProperty";
        var result = value.ValidateIsNotOneOf(propertyName, null, options);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void ValidateIsNotOneOf_WithValueNotInCollection_ReturnsValue()
    {
        var value = "notInList";
        var options = new List<string> { "test", "other" };
        var propertyName = "TestProperty";
        var result = value.ValidateIsNotOneOf(options, propertyName);
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsNotOneOf_WithValueInCollection_ThrowsValidationException()
    {
        var value = "test";
        var options = new List<string> { "test", "other" };
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotOneOf(options, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNotOneOf"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not be one of the specified values."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["options"], Is.EqualTo(options));
        });
    }

    [Test]
    public void ValidateIsNotOneOf_WithNullValueAndCollection_ReturnsNull()
    {
        string? value = null;
        var options = new List<string> { "test", "other" };
        var propertyName = "TestProperty";
        var result = value.ValidateIsNotOneOf(options, propertyName);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void ValidateIsNotOneOf_WithBlackboard_PreservesBlackboardInException()
    {
        var value = 2;
        var options = new[] { 1, 2, 3 };
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotOneOf(propertyName, blackboard, options));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 
