using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;
using System.Collections.Generic;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestIsWithin
{
    [Test]
    public void CheckIsWithin_WithValueInArray_ReturnsTrue()
    {
        var value = 2;
        var options = new[] { 1, 2, 3 };
        var result = value.CheckIsWithin(options);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsWithin_WithValueNotInArray_ReturnsFalse()
    {
        var value = 4;
        var options = new[] { 1, 2, 3 };
        var result = value.CheckIsWithin(options);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsWithin_WithNullValue_ReturnsFalse()
    {
        string? value = null;
        var options = new[] { "1", "2", "3" };
        var result = value.CheckIsWithin(options);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsWithin_WithValueInCollection_ReturnsTrue()
    {
        var value = "test";
        var options = new List<string> { "test", "other" };
        var result = value.CheckIsWithin(options);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsWithin_WithValueNotInCollection_ReturnsFalse()
    {
        var value = "notInList";
        var options = new List<string> { "test", "other" };
        var result = value.CheckIsWithin(options);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsWithin_WithNullValueAndCollection_ReturnsFalse()
    {
        string? value = null;
        var options = new List<string> { "test", "other" };
        var result = value.CheckIsWithin(options);
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateIsWithin_WithValueInArray_ReturnsValue()
    {
        var value = 2;
        var options = new[] { 1, 2, 3 };
        var propertyName = "TestProperty";
        var result = value.ValidateIsWithin(propertyName, null, options);
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsWithin_WithValueNotInArray_ThrowsValidationException()
    {
        var value = 4;
        var options = new[] { 1, 2, 3 };
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsWithin(propertyName, null, options));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsWithin"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must be one of the specified values."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["options"], Is.EqualTo(options));
        });
    }

    [Test]
    public void ValidateIsWithin_WithNullValueAndArray_ThrowsValidationException()
    {
        string? value = null;
        var options = new[] { "1", "2", "3" };
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsWithin(propertyName, null, options));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsWithin"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must be one of the specified values."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.Null);
            Assert.That(exception.ExceptionContext.Context["options"], Is.EqualTo(options));
        });
    }

    [Test]
    public void ValidateIsWithin_WithValueInCollection_ReturnsValue()
    {
        var value = "test";
        var options = new List<string> { "test", "other" };
        var propertyName = "TestProperty";
        var result = value.ValidateIsWithin(options, propertyName);
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsWithin_WithValueNotInCollection_ThrowsValidationException()
    {
        var value = "notInList";
        var options = new List<string> { "test", "other" };
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsWithin(options, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsWithin"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must be one of the specified values."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["options"], Is.EqualTo(options));
        });
    }

    [Test]
    public void ValidateIsWithin_WithNullValueAndCollection_ThrowsValidationException()
    {
        string? value = null;
        var options = new List<string> { "test", "other" };
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsWithin(options, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsWithin"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must be one of the specified values."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.Null);
            Assert.That(exception.ExceptionContext.Context["options"], Is.EqualTo(options));
        });
    }

    [Test]
    public void ValidateIsWithin_WithBlackboard_PreservesBlackboardInException()
    {
        var value = 4;
        var options = new[] { 1, 2, 3 };
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsWithin(propertyName, blackboard, options));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 
