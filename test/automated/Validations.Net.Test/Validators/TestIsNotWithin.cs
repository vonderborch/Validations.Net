using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;
using System.Collections.Generic;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestIsNotWithin
{
    [Test]
    public void CheckIsNotWithin_WithValueNotInArray_ReturnsTrue()
    {
        var value = 4;
        var options = new[] { 1, 2, 3 };
        var result = value.CheckIsNotWithin(options);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotWithin_WithValueInArray_ReturnsFalse()
    {
        var value = 2;
        var options = new[] { 1, 2, 3 };
        var result = value.CheckIsNotWithin(options);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotWithin_WithNullValue_ReturnsTrue()
    {
        string? value = null;
        var options = new[] { "1", "2", "3" };
        var result = value.CheckIsNotWithin(options);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotWithin_WithValueNotInCollection_ReturnsTrue()
    {
        var value = "notInList";
        var options = new List<string> { "test", "other" };
        var result = value.CheckIsNotWithin(options);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotWithin_WithValueInCollection_ReturnsFalse()
    {
        var value = "test";
        var options = new List<string> { "test", "other" };
        var result = value.CheckIsNotWithin(options);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotWithin_WithNullValueAndCollection_ReturnsTrue()
    {
        string? value = null;
        var options = new List<string> { "test", "other" };
        var result = value.CheckIsNotWithin(options);
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateIsNotWithin_WithValueNotInArray_ReturnsValue()
    {
        var value = 4;
        var options = new[] { 1, 2, 3 };
        var propertyName = "TestProperty";
        var result = value.ValidateIsNotWithin(propertyName, null, options);
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsNotWithin_WithValueInArray_ThrowsValidationException()
    {
        var value = 2;
        var options = new[] { 1, 2, 3 };
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotWithin(propertyName, null, options));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNotWithin"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not be one of the specified values."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["options"], Is.EqualTo(options));
        });
    }

    [Test]
    public void ValidateIsNotWithin_WithNullValueAndArray_ReturnsNull()
    {
        string? value = null;
        var options = new[] { "1", "2", "3" };
        var propertyName = "TestProperty";
        var result = value.ValidateIsNotWithin(propertyName, null, options);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void ValidateIsNotWithin_WithValueNotInCollection_ReturnsValue()
    {
        var value = "notInList";
        var options = new List<string> { "test", "other" };
        var propertyName = "TestProperty";
        var result = value.ValidateIsNotWithin(options, propertyName);
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsNotWithin_WithValueInCollection_ThrowsValidationException()
    {
        var value = "test";
        var options = new List<string> { "test", "other" };
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotWithin(options, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNotWithin"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not be one of the specified values."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["options"], Is.EqualTo(options));
        });
    }

    [Test]
    public void ValidateIsNotWithin_WithNullValueAndCollection_ReturnsNull()
    {
        string? value = null;
        var options = new List<string> { "test", "other" };
        var propertyName = "TestProperty";
        var result = value.ValidateIsNotWithin(options, propertyName);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void ValidateIsNotWithin_WithBlackboard_PreservesBlackboardInException()
    {
        var value = 2;
        var options = new[] { 1, 2, 3 };
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotWithin(propertyName, blackboard, options));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 
