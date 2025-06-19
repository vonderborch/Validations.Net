using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;
using System;

namespace Validations.Net.Test.ValidationAttributes;

public class StringDoesNotContainAnySubstringsTestClass
{
    [ValidateDoesNotContainAnyAttribute(StringComparison.Ordinal, "foo", "bar")]
    public string? Value { get; set; }
}

public class StringDoesNotContainAnyCharsTestClass
{
    [ValidateDoesNotContainAnyAttribute(StringComparison.Ordinal, 'x', 'y')]
    public string? Value { get; set; }
}

[TestFixture]
public class TestStringValidateDoesNotContainAnyAttribute
{
    [Test]
    public void ValidateDoesNotContainAnyAttribute_Substrings_Valid()
    {
        var valid = new StringDoesNotContainAnySubstringsTestClass { Value = "bazqux" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAnyAttribute_Substrings_Invalid()
    {
        var invalid = new StringDoesNotContainAnySubstringsTestClass { Value = "foobar" };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateDoesNotContainAnyAttribute_Chars_Valid()
    {
        var valid = new StringDoesNotContainAnyCharsTestClass { Value = "hello" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAnyAttribute_Chars_Invalid()
    {
        var invalid = new StringDoesNotContainAnyCharsTestClass { Value = "xylophone" };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateDoesNotContainAnyAttribute_Empty_Valid()
    {
        var valid = new StringDoesNotContainAnySubstringsTestClass { Value = string.Empty };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAnyAttribute_Null_Valid()
    {
        var attr = new ValidateDoesNotContainAnyAttribute(StringComparison.Ordinal, "foo", "bar");
        Assert.That(attr.Check(null, null), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAnyAttribute_InvalidType_Throws()
    {
        var attr = new ValidateDoesNotContainAnyAttribute(StringComparison.Ordinal, "foo");
        var ex = Assert.Throws<ValidationException>(() => attr.Check(123, null));
        Assert.That(ex!.Message, Does.Contain("DoesNotContainAny"));
    }

    [Test]
    public void ValidateDoesNotContainAnyAttribute_Comparison_Valid()
    {
        var attr = new ValidateDoesNotContainAnyAttribute(StringComparison.OrdinalIgnoreCase, "FOO", "BAR");
        Assert.That(attr.Check("bazqux", null), Is.True);
    }
} 