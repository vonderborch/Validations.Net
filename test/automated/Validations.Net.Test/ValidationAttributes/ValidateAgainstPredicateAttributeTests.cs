namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateAgainstPredicateAttributeTests
{
    [Test]
    public void Validate_AlwaysReturnsFailure()
    {
        var attr = new ValidateAgainstPredicateAttribute("SomePredicate");
        var result = attr.Validate("any");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNull_ReturnsFailure()
    {
        var attr = new ValidateAgainstPredicateAttribute("TestPredicate");
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateAgainstPredicateAttribute("Pred") { Message = "Custom predicate error" };
        var result = attr.Validate(42);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Custom predicate error"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateAgainstPredicateAttribute("Pred");
        var result = attr.Validate("x", "MyProperty");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("MyProperty"));
    }

    [Test]
    public void Validate_RequiresPredicateName()
    {
        var attr = new ValidateAgainstPredicateAttribute("MyPredicate");
        Assert.That(attr.PredicateName, Is.EqualTo("MyPredicate"));
    }
}
