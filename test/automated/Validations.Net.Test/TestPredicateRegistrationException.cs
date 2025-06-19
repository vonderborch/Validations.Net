using NUnit.Framework;
using System;
using System.Reflection;
using Validations.Net.ValidationAttributes.Helpers;

namespace Validations.Net.Test;

[TestFixture]
public class TestPredicateRegistrationException
{
    [Test]
    public void Constructor_InitializesProperties_Correctly()
    {
        // Arrange
        var methodInfo = typeof(string).GetMethod("IsNullOrEmpty");
        var predicateInfo = new PredicateInfo("TestPredicate", null, true, PredicateType.StaticMethod, methodInfo, typeof(string));
        var inputType = typeof(int);
        var innerException = new ArgumentException("Test inner exception");

        // Act
        var exception = new PredicateRegistrationException(predicateInfo, inputType, innerException);

        // Assert
        Assert.That(exception.PredicateInfo, Is.EqualTo(predicateInfo));
        Assert.That(exception.InputType, Is.EqualTo(inputType));
        Assert.That(exception.InnerException, Is.EqualTo(innerException));
        Assert.That(exception.Message, Is.Not.Null);
        Assert.That(exception.Message, Is.Not.Empty);
    }

    [Test]
    public void InnerException_Property_ReturnsCorrectValue()
    {
        // Arrange
        var methodInfo = typeof(string).GetMethod("IsNullOrEmpty");
        var predicateInfo = new PredicateInfo("TestPredicate", null, true, PredicateType.StaticMethod, methodInfo, typeof(string));
        var inputType = typeof(int);
        var innerException = new InvalidOperationException("Test inner exception");

        // Act
        var exception = new PredicateRegistrationException(predicateInfo, inputType, innerException);

        // Assert
        Assert.That(exception.InnerException, Is.SameAs(innerException));
        Assert.That(exception.InnerException, Is.InstanceOf<InvalidOperationException>());
    }

    [Test]
    public void PredicateInfo_Property_ReturnsCorrectValue()
    {
        // Arrange
        var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        var predicateInfo = new PredicateInfo("ContainsPredicate", "StringGroup", false, PredicateType.Method, methodInfo, typeof(string));
        var inputType = typeof(string);
        var innerException = new Exception("Test exception");

        // Act
        var exception = new PredicateRegistrationException(predicateInfo, inputType, innerException);

        // Assert
        Assert.That(exception.PredicateInfo, Is.EqualTo(predicateInfo));
        Assert.That(exception.PredicateInfo.Name, Is.EqualTo("ContainsPredicate"));
        Assert.That(exception.PredicateInfo.Group, Is.EqualTo("StringGroup"));
        Assert.That(exception.PredicateInfo.Type, Is.EqualTo(PredicateType.Method));
    }

    [Test]
    public void InputType_Property_ReturnsCorrectValue()
    {
        // Arrange
        var methodInfo = typeof(int).GetMethod("ToString", Type.EmptyTypes);
        var predicateInfo = new PredicateInfo("TestPredicate", null, true, PredicateType.Method, methodInfo, typeof(int));
        var inputType = typeof(double);
        var innerException = new Exception("Test exception");

        // Act
        var exception = new PredicateRegistrationException(predicateInfo, inputType, innerException);

        // Assert
        Assert.That(exception.InputType, Is.EqualTo(typeof(double)));
    }

    [Test]
    public void Exception_CanBeCaughtAndExamined()
    {
        // Arrange
        var methodInfo = typeof(string).GetMethod("IsNullOrEmpty");
        var predicateInfo = new PredicateInfo("TestPredicate", null, true, PredicateType.StaticMethod, methodInfo, typeof(string));
        var inputType = typeof(int);
        var innerException = new ArgumentException("Test inner exception");

        // Act & Assert
        try
        {
            throw new PredicateRegistrationException(predicateInfo, inputType, innerException);
        }
        catch (PredicateRegistrationException ex)
        {
            Assert.That(ex.PredicateInfo, Is.EqualTo(predicateInfo));
            Assert.That(ex.InputType, Is.EqualTo(inputType));
            Assert.That(ex.InnerException, Is.EqualTo(innerException));
        }
    }
}
