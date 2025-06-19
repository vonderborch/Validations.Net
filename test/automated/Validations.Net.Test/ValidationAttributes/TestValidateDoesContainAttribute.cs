using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;
using Validations.Net.ValidationAttributes.Helpers;
using System.Collections.Generic;
using Validations.Net.Test.ValidationAttributes;

namespace Validations.Net.Test.ValidationAttributes;

public class ContainsTestClass
{
    [ValidateDoesContain<int>(1)]
    public List<int> Values { get; set; } = new();
}

public class ContainsTestClassWithPredicate
{
    [ValidateDoesContain<int>("IsEven")]
    public List<int> Values { get; set; } = new();
}

public class ContainsStringTestClass
{
    [ValidateDoesContain<string>("foo")]
    public List<string> Values { get; set; } = new();
}

public class ContainsCustomTestClass
{
    [ValidateDoesContain<CustomComparable>("IsSpecial")]
    public List<CustomComparable> Values { get; set; } = new();
}

[TestFixture]
public class TestValidateDoesContainAttribute
{
    [Test]
    public void ValidateDoesContainAttribute_Valid()
    {
        var valid = new ContainsTestClass { Values = new List<int> { 1, 2 } };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesContainAttribute_Invalid()
    {
        var invalid = new ContainsTestClass { Values = new List<int> { 2, 3 } };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateDoesContainAttribute_Predicate_ValidAndInvalid()
    {
        // Register a predicate for demonstration
        PredicateCache<int>.Cache.GlobalPredicates["IsEven"] = x => x % 2 == 0;

        var valid = new ContainsTestClassWithPredicate { Values = new List<int> { 2, 4 } };
        var invalid = new ContainsTestClassWithPredicate { Values = new List<int> { 1, 3 } };
        Assert.That(valid.CheckIsValid(), Is.True);
        Assert.That(invalid.CheckIsValid(), Is.False);
    }

    [Test]
    public void ValidateDoesContainAttribute_String_Valid()
    {
        var valid = new ContainsStringTestClass { Values = new List<string> { "foo", "bar" } };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesContainAttribute_String_Invalid()
    {
        var invalid = new ContainsStringTestClass { Values = new List<string> { "bar", "baz" } };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateDoesContainAttribute_Null_Invalid()
    {
        var attr = new ValidateDoesContainAttribute<int>(1);
        var ex = Assert.Throws<ValidationException>(() => attr.Check(null, null));
        Assert.That(ex!.Message, Does.Contain("DoesContain"));
    }

    [Test]
    public void ValidateDoesContainAttribute_Empty_Invalid()
    {
        var invalid = new ContainsTestClass { Values = new List<int>() };
        Assert.That(invalid.CheckIsValid(), Is.False);
    }

    [Test]
    public void ValidateDoesContainAttribute_MultipleMatches_Valid()
    {
        var valid = new ContainsTestClass { Values = new List<int> { 1, 1, 2 } };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesContainAttribute_InvalidType_Throws()
    {
        var attr = new ValidateDoesContainAttribute<int>(1);
        var ex = Assert.Throws<ValidationException>(() => attr.Check("not a list", null));
        Assert.That(ex!.Message, Does.Contain("DoesContain"));
    }

    [Test]
    public void ValidateDoesContainAttribute_Predicate_CustomClass_ValidAndInvalid()
    {
        PredicateCache<CustomComparable>.Cache.GlobalPredicates["IsSpecial"] = x => x.X == 42;
        var valid = new ContainsCustomTestClass { Values = new List<CustomComparable> { new CustomComparable(42) } };
        var invalid = new ContainsCustomTestClass { Values = new List<CustomComparable> { new CustomComparable(1) } };
        Assert.That(valid.CheckIsValid(), Is.True);
        Assert.That(invalid.CheckIsValid(), Is.False);
    }

    [Test]
    public void ValidateDoesContainAttribute_Predicate_NotRegistered_Throws()
    {
        var attr = new ValidateDoesContainAttribute<int>("NotRegistered");
        var ex = Assert.Throws<ValidationException>(() => attr.Check(new List<int> { 1, 2 }, null));
        Assert.That(ex!.Message, Does.Contain("DoesContain"));
    }
} 