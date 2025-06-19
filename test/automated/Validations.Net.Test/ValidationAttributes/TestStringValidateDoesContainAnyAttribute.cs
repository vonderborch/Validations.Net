using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;
using System;

namespace Validations.Net.Test.ValidationAttributes;

public class StringContainsAnySubstringsTestClass
{
    [ValidateDoesContainAnyAttribute(StringComparison.Ordinal, "foo", "bar")]
    public string? Value { get; set; }
}

public class StringContainsAnyCharsTestClass
{
    [ValidateDoesContainAnyAttribute(StringComparison.Ordinal, 'x', 'y')]
    public string? Value { get; set; }
}

[TestFixture]
public class TestStringValidateDoesContainAnyAttribute
{
    [Test]
    public void ValidateDoesContainAnyAttribute_Substrings_Valid()
    {
        var valid = new StringContainsAnySubstringsTestClass { Value = "bazfoo" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesContainAnyAttribute_Substrings_Invalid()
    {
        var invalid = new StringContainsAnySubstringsTestClass { Value = "bazqux" };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateDoesContainAnyAttribute_Chars_Valid()
    {
        var valid = new StringContainsAnyCharsTestClass { Value = "hello x" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesContainAnyAttribute_Chars_Invalid()
    {
        var invalid = new StringContainsAnyCharsTestClass { Value = "hello" };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateDoesContainAnyAttribute_Empty_Invalid()
    {
        var invalid = new StringContainsAnySubstringsTestClass { Value = string.Empty };
        Assert.That(invalid.CheckIsValid(), Is.False);
    }

    [Test]
    public void ValidateDoesContainAnyAttribute_Null_Invalid()
    {
        var attr = new ValidateDoesContainAnyAttribute(StringComparison.Ordinal, "foo", "bar");
        Assert.That(attr.Check(null, null), Is.False);
    }

    [Test]
    public void ValidateDoesContainAnyAttribute_InvalidType_Throws()
    {
        var attr = new ValidateDoesContainAnyAttribute(StringComparison.Ordinal, "foo");
        var ex = Assert.Throws<ValidationException>(() => attr.Check(123, null));
        Assert.That(ex!.Message, Does.Contain("must be of type"));
    }

    [Test]
    public void ValidateDoesContainAnyAttribute_Comparison_Valid()
    {
        var attr = new ValidateDoesContainAnyAttribute(StringComparison.OrdinalIgnoreCase, "FOO", "BAR");
        Assert.That(attr.Check("bazfoo", null), Is.True);
    }
} 
