using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;

namespace Validations.Net.Test.ValidationAttributes;

public class IsOneOfTestClass
{
    [ValidateIsOneOfAttribute<int>(1, 2, 3)]
    public int Value { get; set; }
}

public class IsOneOfStringTestClass
{
    [ValidateIsOneOfAttribute<string>("a", "b", "c")]
    public string? Value { get; set; }
}

public enum TestEnum { A, B, C }

public class IsOneOfEnumTestClass
{
    [ValidateIsOneOfAttribute<TestEnum>(TestEnum.A, TestEnum.B)]
    public TestEnum Value { get; set; }
}

[TestFixture]
public class TestValidateIsOneOfAttribute
{
    [Test]
    public void ValidateIsOneOfAttribute_Valid()
    {
        var valid = new IsOneOfTestClass { Value = 2 };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsOneOfAttribute_Invalid()
    {
        var invalid = new IsOneOfTestClass { Value = 4 };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsOneOfAttribute_String_Valid()
    {
        var valid = new IsOneOfStringTestClass { Value = "b" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsOneOfAttribute_String_Invalid()
    {
        var invalid = new IsOneOfStringTestClass { Value = "z" };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsOneOfAttribute_Enum_Valid()
    {
        var valid = new IsOneOfEnumTestClass { Value = TestEnum.B };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsOneOfAttribute_Enum_Invalid()
    {
        var invalid = new IsOneOfEnumTestClass { Value = TestEnum.C };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsOneOfAttribute_Null_Invalid()
    {
        var invalid = new IsOneOfStringTestClass { Value = null };
        Assert.That(invalid.CheckIsValid(), Is.False);
        var ex = Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
        Assert.That(ex!.Message, Does.Contain("IsOneOf"));
    }

    [Test]
    public void ValidateIsOneOfAttribute_SingleOption_Valid()
    {
        var attr = new ValidateIsOneOfAttribute<int>(5);
        Assert.That(attr.Check(5, null), Is.True);
    }

    [Test]
    public void ValidateIsOneOfAttribute_SingleOption_Invalid()
    {
        var attr = new ValidateIsOneOfAttribute<int>(5);
        Assert.That(attr.Check(6, null), Is.False);
    }

    [Test]
    public void ValidateIsOneOfAttribute_CollectionConstructor_Valid()
    {
        var attr = new ValidateIsOneOfAttribute<int>(new List<int> { 1, 2, 3 });
        Assert.That(attr.Check(2, null), Is.True);
    }

    [Test]
    public void ValidateIsOneOfAttribute_EmptyOptions_Throws()
    {
        Assert.Throws<ValidationException>(() => new ValidateIsOneOfAttribute<int>());
        Assert.Throws<ValidationException>(() => new ValidateIsOneOfAttribute<int>(new List<int>()));
    }

    [Test]
    public void ValidateIsOneOfAttribute_InvalidType_Throws()
    {
        var attr = new ValidateIsOneOfAttribute<int>(1, 2, 3);
        var ex = Assert.Throws<ValidationException>(() => attr.Check("not an int", null));
        Assert.That(ex!.Message, Does.Contain("must be of type"));
    }
} 
