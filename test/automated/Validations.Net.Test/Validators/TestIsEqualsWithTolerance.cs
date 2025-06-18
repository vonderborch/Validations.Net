using NUnit.Framework;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestIsEqualsWithTolerance
{
    [Test]
    public void CheckIsEqualsWithTolerance_WithEqualValues_ReturnsTrue()
    {
        // Arrange
        double value = 5.0;
        double compareTo = 5.0;
        double tolerance = 0.1;

        // Act
        var result = value.CheckIsEqualsWithTolerance(compareTo, tolerance);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsEqualsWithTolerance_WithValuesWithinTolerance_ReturnsTrue()
    {
        // Arrange
        double value = 5.0;
        double compareTo = 5.05;
        double tolerance = 0.1;

        // Act
        var result = value.CheckIsEqualsWithTolerance(compareTo, tolerance);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsEqualsWithTolerance_WithValuesOutsideTolerance_ReturnsFalse()
    {
        // Arrange
        double value = 5.0;
        double compareTo = 5.2;
        double tolerance = 0.1;

        // Act
        var result = value.CheckIsEqualsWithTolerance(compareTo, tolerance);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsEqualsWithTolerance_WithDifferentNumericTypes_WorksCorrectly()
    {
        // Arrange
        float value = 5.0f;
        float compareTo = 5.05f;
        float tolerance = 0.1f;

        // Act
        var result = value.CheckIsEqualsWithTolerance(compareTo, tolerance);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateIsEqualsWithTolerance_WithValuesWithinTolerance_ReturnsValue()
    {
        // Arrange
        double value = 5.0;
        double compareTo = 5.05;
        double tolerance = 0.1;
        var propertyName = "TestProperty";

        // Act
        var result = value.ValidateIsEqualsWithTolerance(compareTo, tolerance, propertyName);

        // Assert
        Assert.That(result, Is.EqualTo(value));
    }

    [Test]
    public void ValidateIsEqualsWithTolerance_WithValuesOutsideTolerance_ThrowsValidationException()
    {
        // Arrange
        double value = 5.0;
        double compareTo = 5.2;
        double tolerance = 0.1;
        var propertyName = "TestProperty";

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsEqualsWithTolerance(compareTo, tolerance, propertyName));
        Assert.Multiple(() =>
        {
            Assert.That(exception!.Validator, Is.EqualTo("IsEqualsWithTolerance"));
            Assert.That(exception.ParameterName, Is.EqualTo(propertyName));
            Assert.That(exception.Message, Is.EqualTo($"{propertyName} must be approximately equal to {compareTo} within a tolerance of {tolerance}."));
            Assert.That(exception.ExceptionContext.Context["value"], Is.EqualTo(value));
            Assert.That(exception.ExceptionContext.Context["compareTo"], Is.EqualTo(compareTo));
            Assert.That(exception.ExceptionContext.Context["tolerance"], Is.EqualTo(tolerance));
        });
    }

    [Test]
    public void ValidateIsEqualsWithTolerance_WithBlackboard_PreservesBlackboardInException()
    {
        // Arrange
        double value = 5.0;
        double compareTo = 5.2;
        double tolerance = 0.1;
        var propertyName = "TestProperty";
        var blackboard = new ValidationExceptionContext();

        // Act & Assert
        var exception = Assert.Throws<ValidationException>(() => value.ValidateIsEqualsWithTolerance(compareTo, tolerance, propertyName, blackboard));
        Assert.That(exception!.Blackboard, Is.SameAs(blackboard));
    }
} 