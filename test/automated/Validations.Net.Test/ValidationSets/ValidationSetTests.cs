using Validations.Net.OLD;
using Validations.Net.OLD.ValidationSets;
using Validations.Net.OLD.ValidationSets.BuilderExtensions;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationSets;

[TestFixture]
public class ValidationSetTests
{
    private class Order
    {
        public string? CustomerId { get; set; }
        public string? DeliveryMethod { get; set; }
        public string? ShippingAddress { get; set; }
        public int Quantity { get; set; }
    }

    [ValidateIsNotNull]
    private class DecoratedModel
    {
        [ValidateIsNotNull]
        public string? Name { get; set; }
    }

    private class MultiFailureDecoratedModel
    {
        [ValidateIsNotNull]
        public string? Name { get; set; }

        [ValidateIsNotNull]
        public string? Email { get; set; }
    }

    #region Execute Tests

    [Test]
    public void Execute_WithAllPassingSteps_ReturnsIsValid()
    {
        var set = ValidationSet.For<Order>()
            .AddIsNotNull()
            .AddIsNotNullOrEmpty(o => o.CustomerId)
            .Build();
        var order = new Order { CustomerId = "C123" };
        var result = set.Execute(order);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Execute_WithFailingStep_ReturnsAllFailures()
    {
        var set = ValidationSet.For<Order>()
            .AddIsNotNullOrEmpty(o => o.CustomerId)
            .AddIsNotNullOrEmpty(o => o.ShippingAddress)
            .Build();
        var order = new Order { CustomerId = null, ShippingAddress = null };
        var result = set.Execute(order);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.EqualTo(2));
    }

    [Test]
    public void Execute_WithCustomStep_Works()
    {
        var set = ValidationSet.For<Order>()
            .Add((order, _) => order.Quantity > 0
                ? ValidationResult.CreateFromValidationSuccess()
                : ValidationResult.CreateFromValidationFailure("Quantity", "Must be positive", null, null,
                    new List<(string key, object? value)> { ("value", order.Quantity) }))
            .Build();
        var result = set.Execute(new Order { Quantity = 0 });
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Execute_WithSelectorBasedConvenienceStep_UsesMemberPath()
    {
        var set = ValidationSet.For<Order>()
            .AddIsNotNullOrEmpty(o => o.CustomerId)
            .Build();

        var result = set.Execute(new Order { CustomerId = null });

        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.EqualTo(1));
        Assert.That(result.Failures[0].MemberPath, Is.EqualTo("CustomerId"));
    }

    [Test]
    public void Execute_WithCustomStepParameterName_UsesMemberPath()
    {
        var set = ValidationSet.For<Order>()
            .Add((order, _) =>
                ValidationResult.CreateFromValidationFailure(
                    "CustomValidator",
                    "Quantity must be positive",
                    "Quantity",
                    null,
                    new List<(string key, object? value)> { ("value", order.Quantity) }))
            .Build();

        var result = set.Execute(new Order { Quantity = 0 });

        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.EqualTo(1));
        Assert.That(result.Failures[0].MemberPath, Is.EqualTo("Quantity"));
    }

    #endregion

    #region Check Tests

    [Test]
    public void Check_WithAllPassing_ReturnsTrue()
    {
        var set = ValidationSet.For<Order>()
            .AddIsNotNull()
            .AddIsNotNullOrEmpty(o => o.CustomerId)
            .Build();
        Assert.That(set.Check(new Order { CustomerId = "C1" }), Is.True);
    }

    [Test]
    public void Check_WithFailure_ReturnsFalse()
    {
        var set = ValidationSet.For<Order>()
            .AddIsNotNullOrEmpty(o => o.CustomerId)
            .Build();
        Assert.That(set.Check(new Order { CustomerId = null }), Is.False);
    }

    #endregion

    #region Ensure Tests

    [Test]
    public void Ensure_WithAllPassing_ReturnsValue()
    {
        var set = ValidationSet.For<Order>()
            .AddIsNotNull()
            .Build();
        var order = new Order();
        var result = set.Ensure(order);
        Assert.That(result, Is.SameAs(order));
    }

    [Test]
    public void Ensure_WithFailure_ThrowsValidationException()
    {
        var set = ValidationSet.For<Order>()
            .AddIsNotNullOrEmpty(o => o.CustomerId)
            .Build();
        Assert.Throws<ValidationException>(() => set.Ensure(new Order()));
    }

    #endregion

    #region When (Conditional) Tests

    [Test]
    public void When_ConditionTrue_ExecutesStep()
    {
        var set = ValidationSet.For<Order>()
            .When(o => o.DeliveryMethod == "ship")
            .AddIsNotNullOrEmpty(o => o.ShippingAddress)
            .Build();
        var result = set.Execute(new Order { DeliveryMethod = "ship", ShippingAddress = null });
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void When_ConditionFalse_SkipsStep()
    {
        var set = ValidationSet.For<Order>()
            .When(o => o.DeliveryMethod == "ship")
            .AddIsNotNullOrEmpty(o => o.ShippingAddress)
            .Build();
        var result = set.Execute(new Order { DeliveryMethod = "pickup", ShippingAddress = null });
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void EndWhen_ResumesUnconditionalSteps()
    {
        var set = ValidationSet.For<Order>()
            .When(o => false)
            .AddIsNotNullOrEmpty(o => o.CustomerId)
            .EndWhen()
            .AddIsNotNullOrEmpty(o => o.ShippingAddress)
            .Build();
        var result = set.Execute(new Order { CustomerId = null, ShippingAddress = null });
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.EqualTo(1));
    }

    #endregion

    #region AddFromType Tests

    [Test]
    public void AddFromType_WithValidModel_Passes()
    {
        var set = ValidationSet.For<DecoratedModel>()
            .AddFromType()
            .Build();
        var result = set.Execute(new DecoratedModel { Name = "John" });
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void AddFromType_WithInvalidModel_Fails()
    {
        var set = ValidationSet.For<DecoratedModel>()
            .AddFromType()
            .Build();
        var result = set.Execute(new DecoratedModel { Name = null });
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void AddFromType_WithMultipleAttributeFailures_ReportsAllFailures()
    {
        var set = ValidationSet.For<MultiFailureDecoratedModel>()
            .AddFromType()
            .Build();

        var result = set.Execute(new MultiFailureDecoratedModel { Name = null, Email = null });

        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.EqualTo(2));
        Assert.That(result.Failures.Any(f => f.MemberPath == "Name"), Is.True);
        Assert.That(result.Failures.Any(f => f.MemberPath == "Email"), Is.True);
    }

    #endregion

    #region IsInRange Tests

    [Test]
    public void AddIsInRange_WithValueInRange_Passes()
    {
        var set = ValidationSet.For<Order>()
            .AddIsInRange(o => o.Quantity, 1, 100)
            .Build();
        Assert.That(set.Check(new Order { Quantity = 50 }), Is.True);
    }

    [Test]
    public void AddIsInRange_WithValueOutOfRange_Fails()
    {
        var set = ValidationSet.For<Order>()
            .AddIsInRange(o => o.Quantity, 1, 100)
            .Build();
        Assert.That(set.Check(new Order { Quantity = 0 }), Is.False);
    }

    [Test]
    public void AddIsInRange_WithNullSelectedValue_ReturnsFailureWithoutThrowing()
    {
        var set = ValidationSet.For<Order>()
            .AddIsInRange(o => o.CustomerId!, "a", "z")
            .Build();

        Assert.DoesNotThrow(() =>
        {
            var result = set.Execute(new Order { CustomerId = null });
            Assert.That(result.IsValid, Is.False);
        });
    }

    #endregion
}
