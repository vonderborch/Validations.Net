using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;

namespace Validations.Net.Test.ValidationAttributes;

public class IsNotWithinTestClass
{
    [ValidateIsNotWithinAttribute<int>(1, 2, 3)]
    public int Value { get; set; }
}

public class IsNotWithinStringTestClass
{
    [ValidateIsNotWithinAttribute<string>("a", "b", "c")]
    public string? Value { get; set; }
}

public class IsNotWithinEnumTestClass
{
    [ValidateIsNotWithinAttribute<TestEnum>(TestEnum.A, TestEnum.B)]
    public TestEnum Value { get; set; }
}

[TestFixture]
public class TestValidateIsNotWithinAttribute
{
    [Test]
    public void ValidateIsNotWithinAttribute_Valid()
    {
        var valid = new IsNotWithinTestClass { Value = 4 };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNotWithinAttribute_Invalid()
    {
        var invalid = new IsNotWithinTestClass { Value = 2 };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsNotWithinAttribute_String_Valid()
    {
        var valid = new IsNotWithinStringTestClass { Value = "z" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNotWithinAttribute_String_Invalid()
    {
        var invalid = new IsNotWithinStringTestClass { Value = "b" };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsNotWithinAttribute_Enum_Valid()
    {
        var valid = new IsNotWithinEnumTestClass { Value = TestEnum.C };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNotWithinAttribute_Enum_Invalid()
    {
        var invalid = new IsNotWithinEnumTestClass { Value = TestEnum.B };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsNotWithinAttribute_Null_Valid()
    {
        var valid = new IsNotWithinStringTestClass { Value = null };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNotWithinAttribute_SingleOption_Valid()
    {
        var attr = new ValidateIsNotWithinAttribute<int>(5);
        Assert.That(attr.Check(6, null), Is.True);
    }

    [Test]
    public void ValidateIsNotWithinAttribute_SingleOption_Invalid()
    {
        var attr = new ValidateIsNotWithinAttribute<int>(5);
        Assert.That(attr.Check(5, null), Is.False);
    }

    [Test]
    public void ValidateIsNotWithinAttribute_CollectionConstructor_Valid()
    {
        var attr = new ValidateIsNotWithinAttribute<int>(new List<int> { 1, 2, 3 });
        Assert.That(attr.Check(4, null), Is.True);
    }

    [Test]
    public void ValidateIsNotWithinAttribute_EmptyOptions_Throws()
    {
        Assert.Throws<ValidationException>(() => new ValidateIsNotWithinAttribute<int>());
        Assert.Throws<ValidationException>(() => new ValidateIsNotWithinAttribute<int>(new List<int>()));
    }

    [Test]
    public void ValidateIsNotWithinAttribute_InvalidType_Throws()
    {
        var attr = new ValidateIsNotWithinAttribute<int>(1, 2, 3);
        var ex = Assert.Throws<ValidationException>(() => attr.Check("not an int", null));
        Assert.That(ex!.Message, Does.Contain("IsNotWithin"));
    }
} 