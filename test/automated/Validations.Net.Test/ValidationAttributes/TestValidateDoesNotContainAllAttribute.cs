using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;
using Validations.Net.Test.ValidationAttributes;
using System.Collections.Generic;
using Validations.Net.ValidationAttributes.Helpers;

namespace Validations.Net.Test.ValidationAttributes;

public class DoesNotContainAllIntTestClass
{
    [ValidateDoesNotContainAll<int>(1, 2)]
    public List<int> Values { get; set; } = new();
}

public class DoesNotContainAllStringTestClass
{
    [ValidateDoesNotContainAll<string>("foo", "bar")]
    public List<string> Values { get; set; } = new();
}

public class DoesNotContainAllCustomTestClass
{
    [ValidateDoesNotContainAll<CustomComparable>("IsSpecial")]
    public List<CustomComparable> Values { get; set; } = new();
}

[TestFixture]
public class TestValidateDoesNotContainAllAttribute
{
    [Test]
    public void ValidateDoesNotContainAllAttribute_Int_Valid()
    {
        var valid = new DoesNotContainAllIntTestClass { Values = new List<int> { 3, 4 } };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAllAttribute_Int_Invalid()
    {
        var invalid = new DoesNotContainAllIntTestClass { Values = new List<int> { 1, 2, 3 } };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateDoesNotContainAllAttribute_String_Valid()
    {
        var valid = new DoesNotContainAllStringTestClass { Values = new List<string> { "baz", "qux" } };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAllAttribute_String_Invalid()
    {
        var invalid = new DoesNotContainAllStringTestClass { Values = new List<string> { "foo", "bar", "baz" } };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateDoesNotContainAllAttribute_Null_Valid()
    {
        var attr = new ValidateDoesNotContainAllAttribute<int>(1, 2);
        Assert.That(attr.Check(null, null), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAllAttribute_Empty_Valid()
    {
        var valid = new DoesNotContainAllIntTestClass { Values = new List<int>() };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAllAttribute_AllMatch_Invalid()
    {
        var invalid = new DoesNotContainAllIntTestClass { Values = new List<int> { 1, 2 } };
        Assert.That(invalid.CheckIsValid(), Is.False);
    }

    [Test]
    public void ValidateDoesNotContainAllAttribute_SomeMatch_Valid()
    {
        var valid = new DoesNotContainAllIntTestClass { Values = new List<int> { 1, 3 } };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAllAttribute_InvalidType_Throws()
    {
        var attr = new ValidateDoesNotContainAllAttribute<int>(1, 2);
        var ex = Assert.Throws<ValidationException>(() => attr.Check("not a list", null));
        Assert.That(ex!.Message, Does.Contain("must be of type"));
    }

    [Test]
    public void ValidateDoesNotContainAllAttribute_Predicate_CustomClass_ValidAndInvalid()
    {
        PredicateCache<CustomComparable>.Cache.GlobalPredicates["IsSpecial"] = x => x.X == 42;
        var valid = new DoesNotContainAllCustomTestClass { Values = new List<CustomComparable> { new CustomComparable(1) } };
        var invalid = new DoesNotContainAllCustomTestClass { Values = new List<CustomComparable> { new CustomComparable(42) } };
        Assert.That(valid.CheckIsValid(), Is.True);
        Assert.That(invalid.CheckIsValid(), Is.False);
    }

    [Test]
    public void ValidateDoesNotContainAllAttribute_Predicate_NotRegistered_Throws()
    {
        var attr = new ValidateDoesNotContainAllAttribute<CustomComparable>("NotRegistered");
        var ex = Assert.Throws<ValidationException>(() => attr.Check(new List<CustomComparable> { new CustomComparable(1) }, null));
        Assert.That(ex!.Message, Does.Contain("DoesNotContainAll"));
    }
} 
