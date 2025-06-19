using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;
using System;
using System.Collections.Generic;

namespace Validations.Net.Test.ValidationAttributes;

public class IsNotEqualsIntTestClass
{
    [ValidateIsNotEquals<int>(42)]
    public int Value { get; set; }
}

public class IsNotEqualsStringTestClass
{
    [ValidateIsNotEquals<string>("hello")]
    public string? Value { get; set; }
}

public class IsNotEqualsCustomTestClass
{
    public CustomComparable? Value { get; set; }
}

[TestFixture]
public class TestValidateIsNotEqualsAttribute
{
    [Test]
    public void ValidateIsNotEqualsAttribute_Int_Valid()
    {
        var valid = new IsNotEqualsIntTestClass { Value = 41 };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNotEqualsAttribute_Int_Invalid()
    {
        var invalid = new IsNotEqualsIntTestClass { Value = 42 };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsNotEqualsAttribute_String_Valid()
    {
        var valid = new IsNotEqualsStringTestClass { Value = "world" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNotEqualsAttribute_String_Invalid()
    {
        var invalid = new IsNotEqualsStringTestClass { Value = "hello" };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsNotEqualsAttribute_Custom_Valid()
    {
        var attr = new ValidateIsNotEqualsAttribute<CustomComparable>(new CustomComparable(5));
        Assert.That(attr.Check(new CustomComparable(6), null), Is.True);
    }

    [Test]
    public void ValidateIsNotEqualsAttribute_Custom_Invalid()
    {
        var attr = new ValidateIsNotEqualsAttribute<CustomComparable>(new CustomComparable(5));
        Assert.That(attr.Check(new CustomComparable(5), null), Is.False);
        Assert.Throws<ValidationException>(() => attr.Validate(new CustomComparable(5), null, "Value"));
    }

    [Test]
    public void ValidateIsNotEqualsAttribute_Null_Valid()
    {
        var valid = new IsNotEqualsStringTestClass { Value = null };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNotEqualsAttribute_InvalidType_Throws()
    {
        var attr = new ValidateIsNotEqualsAttribute<int>(5);
        var ex = Assert.Throws<ValidationException>(() => attr.Check("not an int", null));
        Assert.That(ex!.Message, Does.Contain("IsNotEquals"));
    }
} 