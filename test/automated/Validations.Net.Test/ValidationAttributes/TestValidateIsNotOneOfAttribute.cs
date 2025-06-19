using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;

namespace Validations.Net.Test.ValidationAttributes;

public class IsNotOneOfTestClass
{
    [ValidateIsNotOneOfAttribute<int>(1, 2, 3)]
    public int Value { get; set; }
}

public class IsNotOneOfStringTestClass
{
    [ValidateIsNotOneOfAttribute<string>("a", "b", "c")]
    public string? Value { get; set; }
}

public class IsNotOneOfEnumTestClass
{
    [ValidateIsNotOneOfAttribute<TestEnum>(TestEnum.A, TestEnum.B)]
    public TestEnum Value { get; set; }
}

[TestFixture]
public class TestValidateIsNotOneOfAttribute
{
    [Test]
    public void ValidateIsNotOneOfAttribute_Valid()
    {
        var valid = new IsNotOneOfTestClass { Value = 4 };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNotOneOfAttribute_Invalid()
    {
        var invalid = new IsNotOneOfTestClass { Value = 2 };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsNotOneOfAttribute_String_Valid()
    {
        var valid = new IsNotOneOfStringTestClass { Value = "z" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNotOneOfAttribute_String_Invalid()
    {
        var invalid = new IsNotOneOfStringTestClass { Value = "b" };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsNotOneOfAttribute_Enum_Valid()
    {
        var valid = new IsNotOneOfEnumTestClass { Value = TestEnum.C };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNotOneOfAttribute_Enum_Invalid()
    {
        var invalid = new IsNotOneOfEnumTestClass { Value = TestEnum.B };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsNotOneOfAttribute_Null_Valid()
    {
        var valid = new IsNotOneOfStringTestClass { Value = null };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNotOneOfAttribute_SingleOption_Valid()
    {
        var attr = new ValidateIsNotOneOfAttribute<int>(5);
        Assert.That(attr.Check(6, null), Is.True);
    }

    [Test]
    public void ValidateIsNotOneOfAttribute_SingleOption_Invalid()
    {
        var attr = new ValidateIsNotOneOfAttribute<int>(5);
        Assert.That(attr.Check(5, null), Is.False);
    }

    [Test]
    public void ValidateIsNotOneOfAttribute_CollectionConstructor_Valid()
    {
        var attr = new ValidateIsNotOneOfAttribute<int>(new List<int> { 1, 2, 3 });
        Assert.That(attr.Check(4, null), Is.True);
    }

    [Test]
    public void ValidateIsNotOneOfAttribute_EmptyOptions_Throws()
    {
        Assert.Throws<ValidationException>(() => new ValidateIsNotOneOfAttribute<int>());
        Assert.Throws<ValidationException>(() => new ValidateIsNotOneOfAttribute<int>(new List<int>()));
    }

    [Test]
    public void ValidateIsNotOneOfAttribute_InvalidType_Throws()
    {
        var attr = new ValidateIsNotOneOfAttribute<int>(1, 2, 3);
        var ex = Assert.Throws<ValidationException>(() => attr.Check("not an int", null));
        Assert.That(ex!.Message, Does.Contain("must be of type"));
    }
} 
