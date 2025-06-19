using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;
using System;

namespace Validations.Net.Test.ValidationAttributes;

public class StringContainsSubstringTestClass
{
    [ValidateDoesContainAttribute("foo")]
    public string? Value { get; set; }
}

public class StringContainsCharTestClass
{
    [ValidateDoesContainAttribute('x')]
    public string? Value { get; set; }
}

public class StringContainsSubstringWithIndexTestClass
{
    [ValidateDoesContainAttribute("bar", 3)]
    public string? Value { get; set; }
}

[TestFixture]
public class TestStringValidateDoesContainAttribute
{
    [Test]
    public void ValidateDoesContainAttribute_Substring_Valid()
    {
        var valid = new StringContainsSubstringTestClass { Value = "foobar" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesContainAttribute_Substring_Invalid()
    {
        var invalid = new StringContainsSubstringTestClass { Value = "bazqux" };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateDoesContainAttribute_Char_Valid()
    {
        var valid = new StringContainsCharTestClass { Value = "example" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesContainAttribute_Char_Invalid()
    {
        var invalid = new StringContainsCharTestClass { Value = "hello" };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateDoesContainAttribute_SubstringWithIndex_Valid()
    {
        var valid = new StringContainsSubstringWithIndexTestClass { Value = "foo_bar" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesContainAttribute_SubstringWithIndex_Invalid()
    {
        var invalid = new StringContainsSubstringWithIndexTestClass { Value = "barfoo" };
        Assert.That(invalid.CheckIsValid(), Is.False);
    }

    [Test]
    public void ValidateDoesContainAttribute_Empty_Invalid()
    {
        var invalid = new StringContainsSubstringTestClass { Value = string.Empty };
        Assert.That(invalid.CheckIsValid(), Is.False);
    }

    [Test]
    public void ValidateDoesContainAttribute_Null_Invalid()
    {
        var attr = new ValidateDoesContainAttribute("foo");
        var ex = Assert.Throws<ValidationException>(() => attr.Check(null, null));
        Assert.That(ex!.Message, Does.Contain("DoesContain"));
    }

    [Test]
    public void ValidateDoesContainAttribute_InvalidType_Throws()
    {
        var attr = new ValidateDoesContainAttribute("foo");
        var ex = Assert.Throws<ValidationException>(() => attr.Check(123, null));
        Assert.That(ex!.Message, Does.Contain("DoesContain"));
    }

    [Test]
    public void ValidateDoesContainAttribute_Comparison_Valid()
    {
        var attr = new ValidateDoesContainAttribute("FOO", StringComparison.OrdinalIgnoreCase);
        Assert.That(attr.Check("foo_bar", null), Is.True);
    }
} 