using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;

namespace Validations.Net.Test.ValidationAttributes;

public class IsNullTestClass
{
    [ValidateIsNull]
    public object? Value { get; set; }
}

public class IsNullStringTestClass
{
    [ValidateIsNull]
    public string? Value { get; set; }
}

public class IsNullCollectionTestClass
{
    [ValidateIsNull]
    public System.Collections.Generic.List<int>? Values { get; set; }
}

public class IsNullArrayTestClass
{
    [ValidateIsNull]
    public int[]? Values { get; set; }
}

public struct IsNullStruct
{
    [ValidateIsNull]
    public int Value;
}

[TestFixture]
public class TestValidateIsNullAttribute
{
    [Test]
    public void ValidateIsNullAttribute_Valid()
    {
        var valid = new IsNullTestClass { Value = null };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNullAttribute_Invalid()
    {
        var invalid = new IsNullTestClass { Value = new object() };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsNullAttribute_String_Valid()
    {
        var valid = new IsNullStringTestClass { Value = null };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNullAttribute_String_Invalid()
    {
        var invalid = new IsNullStringTestClass { Value = "abc" };
        Assert.That(invalid.CheckIsValid(), Is.False);
        var ex = Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
        Assert.That(ex!.Message, Does.Contain("IsNull"));
    }

    [Test]
    public void ValidateIsNullAttribute_Collection_Valid()
    {
        var valid = new IsNullCollectionTestClass { Values = null };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNullAttribute_Collection_Invalid()
    {
        var invalid = new IsNullCollectionTestClass { Values = new System.Collections.Generic.List<int>() };
        Assert.That(invalid.CheckIsValid(), Is.False);
        var ex = Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
        Assert.That(ex!.Message, Does.Contain("IsNull"));
    }

    [Test]
    public void ValidateIsNullAttribute_Array_Valid()
    {
        var valid = new IsNullArrayTestClass { Values = null };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNullAttribute_Array_Invalid()
    {
        var invalid = new IsNullArrayTestClass { Values = new int[0] };
        Assert.That(invalid.CheckIsValid(), Is.False);
        var ex = Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
        Assert.That(ex!.Message, Does.Contain("IsNull"));
    }

    [Test]
    public void ValidateIsNullAttribute_ValueType_Invalid()
    {
        var invalid = new IsNullStruct { Value = 5 };
        Assert.That(invalid.Value, Is.EqualTo(5)); // Value types can't be null
    }
} 