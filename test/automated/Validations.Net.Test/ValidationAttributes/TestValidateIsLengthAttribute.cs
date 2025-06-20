using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;
using System.Collections.Generic;

namespace Validations.Net.Test.ValidationAttributes;

public class IsLengthTestClass
{
    [ValidateIsLengthAttribute(2)]
    public List<object>? Values { get; set; }
}

public class IsLengthStringTestClass
{
    [ValidateIsLengthAttribute(3)]
    public string? Value { get; set; }
}

[TestFixture]
public class TestValidateIsLengthAttribute
{
    [Test]
    public void ValidateIsLengthAttribute_Valid()
    {
        var valid = new IsLengthTestClass { Values = new List<object> { 1, 2 } };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsLengthAttribute_Invalid()
    {
        var invalid = new IsLengthTestClass { Values = new List<object> { 1 } };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsLengthAttribute_String_Valid()
    {
        var valid = new IsLengthStringTestClass { Value = "abc" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsLengthAttribute_String_Invalid()
    {
        var invalid = new IsLengthStringTestClass { Value = "ab" };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsLengthAttribute_Empty_Valid()
    {
        var valid = new IsLengthTestClass { Values = new List<object>() };
        var attr = new ValidateIsLengthAttribute(0);
        Assert.That(attr.Check(valid.Values, null), Is.True);
    }

    [Test]
    public void ValidateIsLengthAttribute_Null_Invalid()
    {
        var invalid = new IsLengthTestClass { Values = null };
        Assert.That(invalid.CheckIsValid(), Is.False);
        var attr = new ValidateIsLengthAttribute(2);
        var ex = Assert.Throws<ValidationException>(() => attr.Validate(null, null, "mock"));
        Assert.That(ex!.Message, Does.Contain("must have a length of"));
    }

    [Test]
    public void ValidateIsLengthAttribute_InvalidType_Throws()
    {
        var attr = new ValidateIsLengthAttribute(2);
        var ex = Assert.Throws<ValidationException>(() => attr.Check(123, null));
        Assert.That(ex!.Message, Does.Contain("must be of type"));
    }

    [Test]
    public void ValidateIsLengthAttribute_NegativeLength_Throws()
    {
        Assert.Throws<ValidationException>(() => new ValidateIsLengthAttribute(-1));
    }
} 
