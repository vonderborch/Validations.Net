using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;

namespace Validations.Net.Test.ValidationAttributes;

public class NotNullTestClass
{
    [ValidateIsNotNull]
    public object? Value { get; set; }
}

public class NotNullStringTestClass
{
    [ValidateIsNotNull]
    public string? Value { get; set; }
}

public class NotNullCollectionTestClass
{
    [ValidateIsNotNull]
    public System.Collections.Generic.List<int>? Values { get; set; }
}

public class NotNullArrayTestClass
{
    [ValidateIsNotNull]
    public int[]? Values { get; set; }
}

public struct NotNullStruct
{
    [ValidateIsNotNull]
    public int Value;
}

[TestFixture]
public class TestValidateIsNotNullAttribute
{
    [Test]
    public void ValidateIsNotNullAttribute_Valid()
    {
        var valid = new NotNullTestClass { Value = new object() };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNotNullAttribute_Invalid()
    {
        var invalid = new NotNullTestClass { Value = null };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsNotNullAttribute_String_Valid()
    {
        var valid = new NotNullStringTestClass { Value = "abc" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNotNullAttribute_String_Invalid()
    {
        var invalid = new NotNullStringTestClass { Value = null };
        Assert.That(invalid.CheckIsValid(), Is.False);
        var ex = Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
        Assert.That(ex!.Message, Does.Contain("is not valid"));
    }

    [Test]
    public void ValidateIsNotNullAttribute_Collection_Valid()
    {
        var valid = new NotNullCollectionTestClass { Values = new System.Collections.Generic.List<int>() };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNotNullAttribute_Collection_Invalid()
    {
        var invalid = new NotNullCollectionTestClass { Values = null };
        Assert.That(invalid.CheckIsValid(), Is.False);
        var ex = Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
        Assert.That(ex!.Message, Does.Contain("is not valid"));
    }

    [Test]
    public void ValidateIsNotNullAttribute_Array_Valid()
    {
        var valid = new NotNullArrayTestClass { Values = new int[0] };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNotNullAttribute_Array_Invalid()
    {
        var invalid = new NotNullArrayTestClass { Values = null };
        Assert.That(invalid.CheckIsValid(), Is.False);
        var ex = Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
        Assert.That(ex!.Message, Does.Contain("is not valid"));
    }

    [Test]
    public void ValidateIsNotNullAttribute_ValueType_Valid()
    {
        var valid = new NotNullStruct { Value = 5 };
        Assert.That(valid.Value, Is.EqualTo(5)); // Value types can't be null
    }
} 
