using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;
using System;

namespace Validations.Net.Test.ValidationAttributes;

public class StringDoesNotContainSubstringTestClass
{
    [ValidateDoesNotContainAttribute("foo")]
    public string? Value { get; set; }
}

public class StringDoesNotContainCharTestClass
{
    [ValidateDoesNotContainAttribute('x')]
    public string? Value { get; set; }
}

public class StringDoesNotContainSubstringWithIndexTestClass
{
    [ValidateDoesNotContainAttribute("bar", 3)]
    public string? Value { get; set; }
}

[TestFixture]
public class TestStringValidateDoesNotContainAttribute
{
    [Test]
    public void ValidateDoesNotContainAttribute_Substring_Valid()
    {
        var valid = new StringDoesNotContainSubstringTestClass { Value = "bazqux" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAttribute_Substring_Invalid()
    {
        var invalid = new StringDoesNotContainSubstringTestClass { Value = "foobar" };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateDoesNotContainAttribute_Char_Valid()
    {
        var valid = new StringDoesNotContainCharTestClass { Value = "hello" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAttribute_Char_Invalid()
    {
        var invalid = new StringDoesNotContainCharTestClass { Value = "example" };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateDoesNotContainAttribute_SubstringWithIndex_Valid()
    {
        var valid = new StringDoesNotContainSubstringWithIndexTestClass { Value = "barfoo" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAttribute_SubstringWithIndex_Invalid()
    {
        var invalid = new StringDoesNotContainSubstringWithIndexTestClass { Value = "foo_bar" };
        Assert.That(invalid.CheckIsValid(), Is.False);
    }

    [Test]
    public void ValidateDoesNotContainAttribute_Empty_Valid()
    {
        var valid = new StringDoesNotContainSubstringTestClass { Value = string.Empty };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAttribute_Null_Valid()
    {
        var attr = new ValidateDoesNotContainAttribute("foo");
        Assert.That(attr.Check(null, null), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAttribute_InvalidType_Throws()
    {
        var attr = new ValidateDoesNotContainAttribute("foo");
        var ex = Assert.Throws<ValidationException>(() => attr.Check(123, null));
        Assert.That(ex!.Message, Does.Contain("must be of type"));
    }

    [Test]
    public void ValidateDoesNotContainAttribute_Comparison_Valid()
    {
        var attr = new ValidateDoesNotContainAttribute("FOO", StringComparison.OrdinalIgnoreCase);
        Assert.That(attr.Check("bar_baz", null), Is.True);
    }
} 
