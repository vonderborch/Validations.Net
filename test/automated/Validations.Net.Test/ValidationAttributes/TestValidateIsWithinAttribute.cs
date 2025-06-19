using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;

namespace Validations.Net.Test.ValidationAttributes;

public class IsWithinTestClass
{
    [ValidateIsWithinAttribute<int>(1, 2, 3)]
    public int Value { get; set; }
}

public class IsWithinStringTestClass
{
    [ValidateIsWithinAttribute<string>("a", "b", "c")]
    public string? Value { get; set; }
}

public class IsWithinEnumTestClass
{
    [ValidateIsWithinAttribute<TestEnum>(TestEnum.A, TestEnum.B)]
    public TestEnum Value { get; set; }
}

[TestFixture]
public class TestValidateIsWithinAttribute
{
    [Test]
    public void ValidateIsWithinAttribute_Valid()
    {
        var valid = new IsWithinTestClass { Value = 2 };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsWithinAttribute_Invalid()
    {
        var invalid = new IsWithinTestClass { Value = 4 };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsWithinAttribute_String_Valid()
    {
        var valid = new IsWithinStringTestClass { Value = "b" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsWithinAttribute_String_Invalid()
    {
        var invalid = new IsWithinStringTestClass { Value = "z" };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsWithinAttribute_Enum_Valid()
    {
        var valid = new IsWithinEnumTestClass { Value = TestEnum.B };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsWithinAttribute_Enum_Invalid()
    {
        var invalid = new IsWithinEnumTestClass { Value = TestEnum.C };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsWithinAttribute_Null_Invalid()
    {
        var invalid = new IsWithinStringTestClass { Value = null };
        Assert.That(invalid.CheckIsValid(), Is.False);
        var ex = Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
        Assert.That(ex!.Message, Does.Contain("IsWithin"));
    }

    [Test]
    public void ValidateIsWithinAttribute_SingleOption_Valid()
    {
        var attr = new ValidateIsWithinAttribute<int>(5);
        Assert.That(attr.Check(5, null), Is.True);
    }

    [Test]
    public void ValidateIsWithinAttribute_SingleOption_Invalid()
    {
        var attr = new ValidateIsWithinAttribute<int>(5);
        Assert.That(attr.Check(6, null), Is.False);
    }

    [Test]
    public void ValidateIsWithinAttribute_CollectionConstructor_Valid()
    {
        var attr = new ValidateIsWithinAttribute<int>(new List<int> { 1, 2, 3 });
        Assert.That(attr.Check(2, null), Is.True);
    }

    [Test]
    public void ValidateIsWithinAttribute_EmptyOptions_Throws()
    {
        Assert.Throws<ValidationException>(() => new ValidateIsWithinAttribute<int>());
        Assert.Throws<ValidationException>(() => new ValidateIsWithinAttribute<int>(new List<int>()));
    }

    [Test]
    public void ValidateIsWithinAttribute_InvalidType_Throws()
    {
        var attr = new ValidateIsWithinAttribute<int>(1, 2, 3);
        var ex = Assert.Throws<ValidationException>(() => attr.Check("not an int", null));
        Assert.That(ex!.Message, Does.Contain("must be of type"));
    }
} 
