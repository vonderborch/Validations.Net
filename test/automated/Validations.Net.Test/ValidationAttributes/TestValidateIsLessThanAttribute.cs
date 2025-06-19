using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;

namespace Validations.Net.Test.ValidationAttributes;

public class LessThanTestClass
{
    [ValidateIsLessThanAttribute<int>(5)]
    public int Value { get; set; }
}

[TestFixture]
public class TestValidateIsLessThanAttribute
{
    [Test]
    public void ValidateIsLessThanAttribute_Valid()
    {
        var valid = new LessThanTestClass { Value = 4 };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsLessThanAttribute_Invalid()
    {
        var invalid = new LessThanTestClass { Value = 5 };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }
} 