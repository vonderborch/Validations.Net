using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;

namespace Validations.Net.Test.ValidationAttributes;

public class NotInRangeTestClass
{
    [ValidateIsNotInRangeAttribute<int>(1, 10)]
    public int Value { get; set; }
}

[TestFixture]
public class TestValidateIsNotInRangeAttribute
{
    [Test]
    public void ValidateIsNotInRangeAttribute_Valid()
    {
        var valid = new NotInRangeTestClass { Value = 0 };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNotInRangeAttribute_Invalid()
    {
        var invalid = new NotInRangeTestClass { Value = 5 };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }
} 