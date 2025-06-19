using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;
using Validations.Net.Test.ValidationAttributes;
using System.Collections.Generic;
using Validations.Net.ValidationAttributes.Helpers;

namespace Validations.Net.Test.ValidationAttributes;

public class AgainstPredicateIntTestClass
{
    [ValidateAgainstPredicateAttribute<int>("IsEven")]
    public int Value { get; set; }
}

public class AgainstPredicateStringTestClass
{
    [ValidateAgainstPredicateAttribute<string>("IsHello")]
    public string? Value { get; set; }
}

public class AgainstPredicateCustomTestClass
{
    [ValidateAgainstPredicateAttribute<CustomComparable>("IsSpecial")]
    public CustomComparable? Value { get; set; }
}

[TestFixture]
public class TestValidateAgainstPredicateAttribute
{
    [Test]
    public void ValidateAgainstPredicateAttribute_Int_Valid()
    {
        PredicateCache<int>.Cache.GlobalPredicates["IsEven"] = x => x % 2 == 0;
        var valid = new AgainstPredicateIntTestClass { Value = 2 };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateAgainstPredicateAttribute_Int_Invalid()
    {
        PredicateCache<int>.Cache.GlobalPredicates["IsEven"] = x => x % 2 == 0;
        var invalid = new AgainstPredicateIntTestClass { Value = 3 };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateAgainstPredicateAttribute_String_Valid()
    {
        PredicateCache<string>.Cache.GlobalPredicates["IsHello"] = s => s == "hello";
        var valid = new AgainstPredicateStringTestClass { Value = "hello" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateAgainstPredicateAttribute_String_Invalid()
    {
        PredicateCache<string>.Cache.GlobalPredicates["IsHello"] = s => s == "hello";
        var invalid = new AgainstPredicateStringTestClass { Value = "world" };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateAgainstPredicateAttribute_Custom_Valid()
    {
        PredicateCache<CustomComparable>.Cache.GlobalPredicates["IsSpecial"] = x => x.X == 42;
        var valid = new AgainstPredicateCustomTestClass { Value = new CustomComparable(42) };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateAgainstPredicateAttribute_Custom_Invalid()
    {
        PredicateCache<CustomComparable>.Cache.GlobalPredicates["IsSpecial"] = x => x.X == 42;
        var invalid = new AgainstPredicateCustomTestClass { Value = new CustomComparable(1) };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateAgainstPredicateAttribute_Null_Invalid()
    {
        PredicateCache<int>.Cache.GlobalPredicates["IsEven"] = x => x % 2 == 0;
        var attr = new ValidateAgainstPredicateAttribute<int>("IsEven");
        var ex = Assert.Throws<ValidationException>(() => attr.Check(null, null));
        Assert.That(ex!.Message, Does.Contain("AgainstPredicate"));
    }

    [Test]
    public void ValidateAgainstPredicateAttribute_InvalidType_Throws()
    {
        PredicateCache<int>.Cache.GlobalPredicates["IsEven"] = x => x % 2 == 0;
        var attr = new ValidateAgainstPredicateAttribute<int>("IsEven");
        var ex = Assert.Throws<ValidationException>(() => attr.Check("not an int", null));
        Assert.That(ex!.Message, Does.Contain("AgainstPredicate"));
    }

    [Test]
    public void ValidateAgainstPredicateAttribute_PredicateNotRegistered_Throws()
    {
        var attr = new ValidateAgainstPredicateAttribute<int>("NotRegistered");
        var ex = Assert.Throws<ValidationException>(() => attr.Check(1, null));
        Assert.That(ex!.Message, Does.Contain("AgainstPredicate"));
    }
} 
