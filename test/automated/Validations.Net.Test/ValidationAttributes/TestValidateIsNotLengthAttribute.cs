using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;
using System.Collections.Generic;

namespace Validations.Net.Test.ValidationAttributes;

public class IsNotLengthTestClass
{
    [ValidateIsNotLengthAttribute(2)]
    public List<object>? Values { get; set; }
}

public class IsNotLengthStringTestClass
{
    [ValidateIsNotLengthAttribute(3)]
    public string? Value { get; set; }
}

[TestFixture]
public class TestValidateIsNotLengthAttribute
{
    [Test]
    public void ValidateIsNotLengthAttribute_Valid()
    {
        var valid = new IsNotLengthTestClass { Values = new List<object> { 1 } };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNotLengthAttribute_Invalid()
    {
        var invalid = new IsNotLengthTestClass { Values = new List<object> { 1, 2 } };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsNotLengthAttribute_String_Valid()
    {
        var valid = new IsNotLengthStringTestClass { Value = "ab" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNotLengthAttribute_String_Invalid()
    {
        var invalid = new IsNotLengthStringTestClass { Value = "abc" };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsNotLengthAttribute_Empty_Valid()
    {
        var valid = new IsNotLengthTestClass { Values = new List<object>() };
        var attr = new ValidateIsNotLengthAttribute(1);
        Assert.That(attr.Check(valid.Values, null), Is.True);
    }

    [Test]
    public void ValidateIsNotLengthAttribute_Null_Valid()
    {
        var valid = new IsNotLengthTestClass { Values = null };
        Assert.That(valid.CheckIsValid(), Is.True);
        var attr = new ValidateIsNotLengthAttribute(2);
        Assert.That(attr.Check(null, null), Is.True);
    }

    [Test]
    public void ValidateIsNotLengthAttribute_InvalidType_Throws()
    {
        var attr = new ValidateIsNotLengthAttribute(2);
        var ex = Assert.Throws<ValidationException>(() => attr.Check(123, null));
        Assert.That(ex!.Message, Does.Contain("must be of type"));
    }

    [Test]
    public void ValidateIsNotLengthAttribute_NegativeLength_Throws()
    {
        Assert.Throws<ValidationException>(() => new ValidateIsNotLengthAttribute(-1));
    }
} 
