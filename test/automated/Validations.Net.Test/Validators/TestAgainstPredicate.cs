using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestAgainstPredicate
{
    [Test]
    public void CheckAgainstPredicate_WithSatisfyingPredicate_ReturnsTrue()
    {
        var value = 5;
        var result = value.CheckAgainstPredicate(x => x > 3);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckAgainstPredicate_WithUnsatisfyingPredicate_ReturnsFalse()
    {
        var value = 2;
        var result = value.CheckAgainstPredicate(x => x > 3);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckAgainstPredicate_WithNullPredicate_ThrowsArgumentNullException()
    {
        var value = 5;
        Assert.Throws<ValidationException>(() => value.CheckAgainstPredicate(null!));
    }

    [Test]
    public void CheckAgainstPredicate_WithComplexPredicate_ReturnsExpectedResult()
    {
        var value = "test";
        var result = value.CheckAgainstPredicate(x => x.Length > 3 && x.StartsWith("t"));
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateAgainstPredicate_WithSatisfyingPredicate_ReturnsValue()
    {
        var value = 5;
        var variableName = "TestVariable";
        var result = value.ValidateAgainstPredicate(x => x > 3, variableName);
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateAgainstPredicate_WithUnsatisfyingPredicate_ThrowsValidationException()
    {
        var value = 2;
        var variableName = "TestVariable";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateAgainstPredicate(x => x > 3, variableName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("AgainstPredicate"));
            Assert.That(exception.ParameterName, Is.EqualTo(variableName));
            Assert.That(exception.Message, Is.EqualTo($"{variableName} failed predicate validation."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
        });
    }

    [Test]
    public void ValidateAgainstPredicate_WithNullPredicate_ThrowsArgumentNullException()
    {
        var value = 5;
        var variableName = "TestVariable";
        Assert.Throws<ValidationException>(() => value.ValidateAgainstPredicate(null!, variableName));
    }

    [Test]
    public void ValidateAgainstPredicate_WithComplexPredicate_ReturnsValue()
    {
        var value = "test";
        var variableName = "TestVariable";
        var result = value.ValidateAgainstPredicate(x => x.Length > 3 && x.StartsWith("t"), variableName);
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateAgainstPredicate_WithBlackboard_PreservesBlackboardInException()
    {
        var value = 2;
        var variableName = "TestVariable";
        var blackboard = new ValidationExceptionContext();
        var exception = Assert.Throws<ValidationException>(() => value.ValidateAgainstPredicate(x => x > 3, variableName, blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 
