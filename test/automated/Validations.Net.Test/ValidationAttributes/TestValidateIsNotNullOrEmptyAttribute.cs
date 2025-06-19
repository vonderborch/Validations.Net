using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;
using System.Collections.Generic;

namespace Validations.Net.Test.ValidationAttributes;

public class NotNullOrEmptyStringTestClass
{
    [ValidateIsNotNullOrEmpty<string>]
    public string? Value { get; set; }
}

public class NotNullOrEmptyCollectionTestClass
{
    [ValidateIsNotNullOrEmpty<int>]
    public List<int>? Values { get; set; }
}

public class NotNullOrEmptyArrayTestClass
{
    [ValidateIsNotNullOrEmpty<int>]
    public int[]? Values { get; set; }
}

[TestFixture]
public class TestValidateIsNotNullOrEmptyAttribute
{
    [Test]
    public void ValidateIsNotNullOrEmptyAttribute_String_Valid()
    {
        var valid = new NotNullOrEmptyStringTestClass { Value = "abc" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNotNullOrEmptyAttribute_String_Invalid()
    {
        var invalid = new NotNullOrEmptyStringTestClass { Value = "" };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsNotNullOrEmptyAttribute_Collection_Valid()
    {
        var valid = new NotNullOrEmptyCollectionTestClass { Values = new List<int> { 1 } };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNotNullOrEmptyAttribute_Collection_Invalid()
    {
        var invalid = new NotNullOrEmptyCollectionTestClass { Values = new List<int>() };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsNotNullOrEmptyAttribute_String_Null()
    {
        var nullVal = new NotNullOrEmptyStringTestClass { Value = null };
        Assert.That(nullVal.CheckIsValid(), Is.False);
        var ex = Assert.Throws<ValidationException>(() => nullVal.ValidateIsValid(nameof(nullVal)));
        Assert.That(ex!.Message, Does.Contain("IsNotNullOrEmpty"));
    }

    [Test]
    public void ValidateIsNotNullOrEmptyAttribute_String_Whitespace()
    {
        var ws = new NotNullOrEmptyStringTestClass { Value = "   " };
        Assert.That(ws.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNotNullOrEmptyAttribute_Array_Valid()
    {
        var valid = new NotNullOrEmptyArrayTestClass { Values = new[] { 1 } };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNotNullOrEmptyAttribute_Array_Invalid()
    {
        var invalid = new NotNullOrEmptyArrayTestClass { Values = new int[0] };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsNotNullOrEmptyAttribute_Collection_Null()
    {
        var nullVal = new NotNullOrEmptyCollectionTestClass { Values = null };
        Assert.That(nullVal.CheckIsValid(), Is.False);
        var ex = Assert.Throws<ValidationException>(() => nullVal.ValidateIsValid(nameof(nullVal)));
        Assert.That(ex!.Message, Does.Contain("IsNotNullOrEmpty"));
    }

    [Test]
    public void ValidateIsNotNullOrEmptyAttribute_Array_Null()
    {
        var nullVal = new NotNullOrEmptyArrayTestClass { Values = null };
        Assert.That(nullVal.CheckIsValid(), Is.False);
        var ex = Assert.Throws<ValidationException>(() => nullVal.ValidateIsValid(nameof(nullVal)));
        Assert.That(ex!.Message, Does.Contain("IsNotNullOrEmpty"));
    }

    [Test]
    public void ValidateIsNotNullOrEmptyAttribute_InvalidType_Throws()
    {
        var attr = new ValidateIsNotNullOrEmptyAttribute<int>();
        var ex = Assert.Throws<ValidationException>(() => attr.Check(123, null));
        Assert.That(ex!.Message, Does.Contain("IsNotNullOrEmpty"));
    }
} 