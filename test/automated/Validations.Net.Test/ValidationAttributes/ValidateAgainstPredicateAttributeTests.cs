using Validations.Net.Predicates;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateAgainstPredicateAttributeTests
{
    private static class PredicateFixture
    {
        [ValidationPredicate("IsEven")]
        public static bool IsEven(int value) => value % 2 == 0;

        [ValidationPredicate("HasLengthAtLeast3", "Strings")]
        public static bool HasLengthAtLeast3(string? value) => value is { Length: >= 3 };
    }

    [SetUp]
    public void SetUp()
    {
        PredicateManager.Clear();
    }

    [Test]
    public void Validate_WithRegisteredPredicateAndMatchingValue_ReturnsSuccess()
    {
        var attr = new ValidateAgainstPredicateAttribute("IsEven");
        var result = attr.Validate(4);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithRegisteredPredicateAndNonMatchingValue_ReturnsFailure()
    {
        var attr = new ValidateAgainstPredicateAttribute("IsEven");
        var result = attr.Validate(3);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithPredicateGroup_ResolvesGroupedPredicate()
    {
        var attr = new ValidateAgainstPredicateAttribute("HasLengthAtLeast3")
        {
            PredicateGroup = "Strings"
        };

        var result = attr.Validate("abcd");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateAgainstPredicateAttribute("IsEven")
        {
            Message = "Value must be even"
        };

        var result = attr.Validate(3);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Value must be even"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateAgainstPredicateAttribute("IsEven");
        var result = attr.Validate(3, "MyProperty");
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
