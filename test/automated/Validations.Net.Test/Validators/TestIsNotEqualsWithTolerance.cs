using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestIsNotEqualsWithTolerance
{
    [Test]
    public void CheckIsNotEqualsWithTolerance_WithValuesOutsideTolerance_ReturnsTrue()
    {
        double value = 5.0;
        double compareTo = 5.2;
        double tolerance = 0.1;
        var result = value.CheckIsNotEqualsWithTolerance(compareTo, tolerance);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotEqualsWithTolerance_WithValuesWithinTolerance_ReturnsFalse()
    {
        double value = 5.0;
        double compareTo = 5.05;
        double tolerance = 0.1;
        var result = value.CheckIsNotEqualsWithTolerance(compareTo, tolerance);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotEqualsWithTolerance_WithEqualValues_ReturnsFalse()
    {
        double value = 5.0;
        double compareTo = 5.0;
        double tolerance = 0.1;
        var result = value.CheckIsNotEqualsWithTolerance(compareTo, tolerance);
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotEqualsWithTolerance_WithDifferentNumericTypes_WorksCorrectly()
    {
        float value = 5.0f;
        float compareTo = 5.2f;
        float tolerance = 0.1f;
        var result = value.CheckIsNotEqualsWithTolerance(compareTo, tolerance);
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateIsNotEqualsWithTolerance_WithValuesOutsideTolerance_ReturnsValue()
    {
        double value = 5.0;
        double compareTo = 5.2;
        double tolerance = 0.1;
        var propertyName = "TestProperty";
        var result = value.ValidateIsNotEqualsWithTolerance(compareTo, tolerance, propertyName);
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsNotEqualsWithTolerance_WithValuesWithinTolerance_ThrowsValidationException()
    {
        double value = 5.0;
        double compareTo = 5.05;
        double tolerance = 0.1;
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotEqualsWithTolerance(compareTo, tolerance, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNotEqualsWithTolerance"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not be approximately equal to {compareTo} within a tolerance of {tolerance}."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["compareTo"], Is.EqualTo(compareTo));
            Assert.That(exception.ExceptionContext.Context["tolerance"], Is.EqualTo(tolerance));
        });
    }

    [Test]
    public void ValidateIsNotEqualsWithTolerance_WithEqualValues_ThrowsValidationException()
    {
        double value = 5.0;
        double compareTo = 5.0;
        double tolerance = 0.1;
        var propertyName = "TestProperty";
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotEqualsWithTolerance(compareTo, tolerance, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsNotEqualsWithTolerance"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must not be approximately equal to {compareTo} within a tolerance of {tolerance}."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["compareTo"], Is.EqualTo(compareTo));
            Assert.That(exception.ExceptionContext.Context["tolerance"], Is.EqualTo(tolerance));
        });
    }

    [Test]
    public void ValidateIsNotEqualsWithTolerance_WithBlackboard_PreservesBlackboardInException()
    {
        double value = 5.0;
        double compareTo = 5.05;
        double tolerance = 0.1;
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsNotEqualsWithTolerance(compareTo, tolerance, propertyName, blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 