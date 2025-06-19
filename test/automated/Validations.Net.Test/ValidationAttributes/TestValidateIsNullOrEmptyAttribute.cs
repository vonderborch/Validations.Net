using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;
using System.Collections.Generic;

namespace Validations.Net.Test.ValidationAttributes;

public class IsNullOrEmptyStringTestClass
{
    [ValidateIsNullOrEmpty<string>]
    public string? Value { get; set; }
}

public class IsNullOrEmptyCollectionTestClass
{
    [ValidateIsNullOrEmpty<int>]
    public List<int>? Values { get; set; }
}

public class IsNullOrEmptyArrayTestClass
{
    [ValidateIsNullOrEmpty<int>]
    public int[]? Values { get; set; }
}

[TestFixture]
public class TestValidateIsNullOrEmptyAttribute
{
    [Test]
    public void ValidateIsNullOrEmptyAttribute_String_Valid()
    {
        var valid = new IsNullOrEmptyStringTestClass { Value = "" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNullOrEmptyAttribute_String_Invalid()
    {
        var invalid = new IsNullOrEmptyStringTestClass { Value = "abc" };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsNullOrEmptyAttribute_Collection_Valid()
    {
        var valid = new IsNullOrEmptyCollectionTestClass { Values = new List<int>() };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNullOrEmptyAttribute_Collection_Invalid()
    {
        var invalid = new IsNullOrEmptyCollectionTestClass { Values = new List<int> { 1 } };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsNullOrEmptyAttribute_String_Null()
    {
        var nullVal = new IsNullOrEmptyStringTestClass { Value = null };
        Assert.That(nullVal.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNullOrEmptyAttribute_String_Whitespace()
    {
        var ws = new IsNullOrEmptyStringTestClass { Value = "   " };
        Assert.That(ws.CheckIsValid(), Is.False);
    }

    [Test]
    public void ValidateIsNullOrEmptyAttribute_Array_Valid()
    {
        var valid = new IsNullOrEmptyArrayTestClass { Values = new int[0] };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNullOrEmptyAttribute_Array_Invalid()
    {
        var invalid = new IsNullOrEmptyArrayTestClass { Values = new[] { 1 } };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsNullOrEmptyAttribute_Collection_Null()
    {
        var nullVal = new IsNullOrEmptyCollectionTestClass { Values = null };
        Assert.That(nullVal.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNullOrEmptyAttribute_Array_Null()
    {
        var nullVal = new IsNullOrEmptyArrayTestClass { Values = null };
        Assert.That(nullVal.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNullOrEmptyAttribute_InvalidType_Throws()
    {
        var attr = new ValidateIsNullOrEmptyAttribute<int>();
        var ex = Assert.Throws<ValidationException>(() => attr.Check(123, null));
        Assert.That(ex!.Message, Does.Contain("must be of type"));
    }
} 
