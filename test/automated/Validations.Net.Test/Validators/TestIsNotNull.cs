using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestIsNotNull
{
    [Test]
    public void CheckIsNotNull_WithNonNullValue_ReturnsTrue()
    {
        var value = "test";
        var result = value.CheckIsNotNull();
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotNull_WithNullValue_ReturnsFalse()
    {
        string? value = null;
        var result = value.CheckIsNotNull();
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateIsNotNull_WithNonNullValue_ReturnsValue()
    {
        var value = "test";
        var propertyName = "TestProperty";
        var result = value.ValidateIsNotNull(propertyName);
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsNotNull_WithNullValue_ThrowsValidationException()
    {
        string? value = null;
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotNull(propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNotNull"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not be null."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
        });
    }

    [Test]
    public void ValidateIsNotNull_WithBlackboard_PreservesBlackboardInException()
    {
        string? value = null;
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotNull(propertyName, blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 