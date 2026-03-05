using Validations.Net;
using Validations.Net.OLD;
using Validations.Net.OLD.Predicates;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

/// <summary>
/// Unit tests for the AgainstPredicate validator class.
/// Tests the CheckAgainstPredicate, ValidateAgainstPredicate, and EnsureAgainstPredicate methods.
/// </summary>
[TestFixture]
public class AgainstPredicateTests
{
    private class InstancePredicateHolder
    {
        [PredicateRegistration("InstanceIsPositive")]
        public bool InstanceIsPositive(int value) => value > 0;
    }

    #region CheckAgainstPredicate Tests (Func overload)

    [Test]
    public void CheckAgainstPredicate_WithMatchingPredicate_ReturnsTrue()
    {
        // Arrange
        int? value = 5;
        Func<int?, bool> predicate = x => x > 0;

        // Act
        var result = value.CheckAgainstPredicate(predicate);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckAgainstPredicate_WithNonMatchingPredicate_ReturnsFalse()
    {
        // Arrange
        int? value = -5;
        Func<int?, bool> predicate = x => x > 0;

        // Act
        var result = value.CheckAgainstPredicate(predicate);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckAgainstPredicate_WithNullValue_WorksCorrectly()
    {
        // Arrange
        string? value = null;
        Func<string?, bool> predicate = x => x == null;

        // Act
        var result = value.CheckAgainstPredicate(predicate);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckAgainstPredicate_WithStringPredicate_WorksCorrectly()
    {
        // Arrange
        string value = "test";
        Func<string?, bool> predicate = x => x != null && x.Length > 2;

        // Act
        var result = value.CheckAgainstPredicate(predicate);

        // Assert
        Assert.That(result, Is.True);
    }

    #endregion

    #region CheckAgainstPredicate Tests (PredicateManager overload)

    [Test]
    public void CheckAgainstPredicate_WithInvalidPredicateName_ReturnsFalse()
    {
        // Arrange
        int? value = 5;

        // Act
        var result = value.CheckAgainstPredicate("NonExistentPredicate");

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void PredicateManager_Clear_RemovesCachedInstancePredicateRegistrations()
    {
        PredicateManager.Clear();
        var holder = new InstancePredicateHolder();

        var firstResult = 5.CheckAgainstPredicate("InstanceIsPositive", predicateInstance: holder);
        Assert.That(firstResult, Is.True);

        var field = typeof(PredicateManager).GetField("InstancePredicates",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        Assert.That(field, Is.Not.Null);

        var beforeClear = (System.Collections.Concurrent.ConcurrentDictionary<Type, System.Collections.Concurrent.ConcurrentDictionary<string, PredicateInfo>>)field!.GetValue(null)!;
        Assert.That(beforeClear.Count, Is.GreaterThan(0));

        PredicateManager.Clear();

        var afterClear = (System.Collections.Concurrent.ConcurrentDictionary<Type, System.Collections.Concurrent.ConcurrentDictionary<string, PredicateInfo>>)field.GetValue(null)!;
        Assert.That(afterClear.Count, Is.EqualTo(0));
    }

    #endregion

    #region ValidateAgainstPredicate Tests (Func overload)

    [Test]
    public void ValidateAgainstPredicate_WithMatchingPredicate_ReturnsValidResult()
    {
        // Arrange
        int? value = 10;
        Func<int?, bool> predicate = x => x >= 10;

        // Act
        var result = value.ValidateAgainstPredicate(predicate);

        // Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.ValidationException, Is.Null);
    }

    [Test]
    public void ValidateAgainstPredicate_WithNonMatchingPredicate_ReturnsInvalidResult()
    {
        // Arrange
        int? value = 5;
        Func<int?, bool> predicate = x => x >= 10;

        // Act
        var result = value.ValidateAgainstPredicate(predicate);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain(AgainstPredicate.ValidatorName));
        Assert.That(result.ValidationException.Message, Does.Contain(AgainstPredicate.DefaultValidationFailureMessage));
    }

    [Test]
    public void ValidateAgainstPredicate_WithCustomMessage_UsesCustomMessage()
    {
        // Arrange
        int? value = 5;
        Func<int?, bool> predicate = x => x >= 10;
        string customMessage = "Value must be at least 10";

        // Act
        var result = value.ValidateAgainstPredicate(predicate, validationFailureMessage: customMessage);

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain(customMessage));
    }

    [Test]
    public void ValidateAgainstPredicate_WithParameterName_IncludesParameterName()
    {
        // Arrange
        int? value = 5;
        Func<int?, bool> predicate = x => x >= 10;

        // Act
        var result = value.ValidateAgainstPredicate(predicate, parameterName: "testValue");

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
        Assert.That(result.ValidationException!.Message, Does.Contain("testValue"));
    }

    #endregion

    #region ValidateAgainstPredicate Tests (PredicateManager overload)

    [Test]
    public void ValidateAgainstPredicate_WithInvalidPredicateName_ReturnsInvalidResult()
    {
        // Arrange
        int? value = 5;

        // Act
        var result = value.ValidateAgainstPredicate("NonExistentPredicate");

        // Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
    }

    #endregion

    #region EnsureAgainstPredicate Tests (Func overload)

    [Test]
    public void EnsureAgainstPredicate_WithMatchingPredicate_DoesNotThrow()
    {
        // Arrange
        int? value = 10;
        Func<int?, bool> predicate = x => x >= 10;

        // Act & Assert
        Assert.DoesNotThrow(() => value.EnsureAgainstPredicate(predicate));
    }

    [Test]
    public void EnsureAgainstPredicate_WithMatchingPredicate_ReturnsValue()
    {
        // Arrange
        int? value = 10;
        Func<int?, bool> predicate = x => x >= 10;

        // Act
        var result = value.EnsureAgainstPredicate(predicate);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void EnsureAgainstPredicate_WithNonMatchingPredicate_ThrowsValidationException()
    {
        // Arrange
        int? value = 5;
        Func<int?, bool> predicate = x => x >= 10;

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.EnsureAgainstPredicate(predicate));
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.Message, Does.Contain(AgainstPredicate.ValidatorName));
        Assert.That(exception.Message, Does.Contain(AgainstPredicate.DefaultValidationFailureMessage));
    }

    [Test]
    public void EnsureAgainstPredicate_WithCustomMessage_ThrowsWithCustomMessage()
    {
        // Arrange
        int? value = 5;
        Func<int?, bool> predicate = x => x >= 10;
        string customMessage = "Value must be at least 10";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => 
            value.EnsureAgainstPredicate(predicate, validationFailureMessage: customMessage));
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.Message, Does.Contain(customMessage));
    }

    [Test]
    public void EnsureAgainstPredicate_WithParameterName_ThrowsWithParameterName()
    {
        // Arrange
        int? value = 5;
        Func<int?, bool> predicate = x => x >= 10;
        string paramName = "testValue";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => 
            value.EnsureAgainstPredicate(predicate, parameterName: paramName));
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.Message, Does.Contain(paramName));
        Assert.That(exception.ParameterName, Is.EqualTo(paramName));
    }

    [Test]
    public void EnsureAgainstPredicate_ExceptionContainsValidator_IsCorrect()
    {
        // Arrange
        int? value = 5;
        Func<int?, bool> predicate = x => x >= 10;

        // Act
        var exception = Assert.Throws<ValidationException>(() => value.EnsureAgainstPredicate(predicate));

        // Assert
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.Validator, Is.EqualTo(AgainstPredicate.ValidatorName));
    }

    #endregion

    #region EnsureAgainstPredicate Tests (PredicateManager overload)

    [Test]
    public void EnsureAgainstPredicate_WithInvalidPredicateName_ThrowsValidationException()
    {
        // Arrange
        int? value = 5;

        // Act & Assert
        Assert.Throws<ValidationException>(() => value.EnsureAgainstPredicate("NonExistentPredicate"));
    }

    #endregion

    #region Edge Cases

    [Test]
    public void CheckAgainstPredicate_WithComplexPredicate_WorksCorrectly()
    {
        // Arrange
        var person = new { Name = "John", Age = 30 };
        Func<object?, bool> predicate = x => x is { } p && ((dynamic)p).Age >= 18;

        // Act
        var result = person.CheckAgainstPredicate(predicate);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckAgainstPredicate_WithZeroValue_WorksCorrectly()
    {
        // Arrange
        int? value = 0;
        Func<int?, bool> predicate = x => x == 0;

        // Act
        var result = value.CheckAgainstPredicate(predicate);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckAgainstPredicate_WithEmptyString_WorksCorrectly()
    {
        // Arrange
        string value = string.Empty;
        Func<string?, bool> predicate = x => x != null && x.Length == 0;

        // Act
        var result = value.CheckAgainstPredicate(predicate);

        // Assert
        Assert.That(result, Is.True);
    }

    #endregion
}




