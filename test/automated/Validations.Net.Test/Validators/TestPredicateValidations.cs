using NUnit.Framework;
using System;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestPredicateValidations
{
    [Test]
    public void CheckAgainstPredicate_WithTrueCondition_ReturnsTrue()
    {
        // Arrange
        int value = 5;

        // Act
        bool result = value.CheckAgainstPredicate(x => x > 0);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckAgainstPredicate_WithFalseCondition_ReturnsFalse()
    {
        // Arrange
        int value = 5;
        Func<int, bool> predicate = x => x > 10;

        // Act
        bool result = value.CheckAgainstPredicate(predicate);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckAgainstPredicate_WithNullPredicate_ThrowsValidationException()
    {
        // Arrange
        string value = "test";
        Func<string, bool> predicate = null;

        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => value.CheckAgainstPredicate(predicate));
        Assert.That(ex.Message, Does.Contain("can not be null"));
        Assert.That(ex.ParameterName, Is.EqualTo("predicate"));
    }

    [Test]
    public void ValidateAgainstPredicate_WithTrueCondition_ReturnsOriginalValue()
    {
        // Arrange
        int value = 5;
        Func<int, bool> predicate = x => x > 0;

        // Act
        var result = value.ValidateAgainstPredicate(predicate, "testValue");

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateAgainstPredicate_WithFalseCondition_ThrowsValidationException()
    {
        // Arrange
        int value = 5;
        Func<int, bool> predicate = x => x > 10;

        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => value.ValidateAgainstPredicate(predicate, "testValue"));
        Assert.That(ex.Message, Does.Contain("failed predicate validation"));
        Assert.That(ex.ParameterName, Is.EqualTo("testValue"));
        Assert.That(ex.Validation, Is.EqualTo("PredicateValidation"));
    }

    [Test]
    public void ValidateAgainstPredicate_WithBlackboard_IncludesBlackboardInException()
    {
        // Arrange
        int value = 5;
        Func<int, bool> predicate = x => x > 10;
        var blackboard = new { Info = "Additional context" };

        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => value.ValidateAgainstPredicate(predicate, "testValue", blackboard));
        Assert.That(ex.Blackboard, Is.EqualTo(blackboard));
    }

    [Test]
    public void ValidateAgainstPredicate_WithExceptionContext_IncludesValueInExceptionContext()
    {
        // Arrange
        int value = 5;
        Func<int, bool> predicate = x => x > 10;

        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => value.ValidateAgainstPredicate(predicate, "testValue"));
        var exceptionContext = ex.ExceptionContext as Dictionary<string, object?>;

        Assert.That(exceptionContext, Is.Not.Null);
        Assert.That(exceptionContext.ContainsKey("value"), Is.True);
        Assert.That(exceptionContext["value"], Is.EqualTo(value));
    }

    [Test]
    public void ValidateAgainstPredicate_WithComplexPredicate_WorksAsExpected()
    {
        // Arrange
        string value = "valid123";
        Func<string, bool> predicate = s => s.Length > 5 && s.Any(char.IsDigit) && s.Any(char.IsLetter);

        // Act
        var result = value.ValidateAgainstPredicate(predicate, "password");

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateAgainstPredicate_WithComplexPredicateFailing_ThrowsValidationException()
    {
        // Arrange
        string value = "weak";
        Func<string, bool> predicate = s => s.Length > 5 && s.Any(char.IsDigit) && s.Any(char.IsLetter);

        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => value.ValidateAgainstPredicate(predicate, "password"));
        Assert.That(ex.Message, Does.Contain("failed predicate validation"));
    }
}
