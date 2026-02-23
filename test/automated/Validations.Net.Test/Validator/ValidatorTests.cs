using Validations.Net.ValidationSets;

namespace Validations.Net.Test.Validator;

[TestFixture]
public class ValidatorTests
{
    private class Order
    {
        public string? CustomerId { get; set; }
        public string? DeliveryMethod { get; set; }
        public string? ShippingAddress { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int Quantity { get; set; }
        public decimal TotalCost { get; set; }
    }

    #region Basic For/End Chain

    [Test]
    public void For_NotNull_ValidValue_Passes()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .For(o => o.CustomerId).NotNull().End();

        Assert.That(validator.Check(new Order { CustomerId = "C1" }), Is.True);
    }

    [Test]
    public void For_NotNull_NullValue_Fails()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .For(o => o.CustomerId).NotNull().End();

        Assert.That(validator.Check(new Order { CustomerId = null }), Is.False);
    }

    [Test]
    public void For_MultipleMembers_AllValid_Passes()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .For(o => o.CustomerId).NotNull().NotEmpty().End()
            .For(o => o.Quantity).InRange(1, 1000).End();

        Assert.That(validator.Check(new Order { CustomerId = "C1", Quantity = 50 }), Is.True);
    }

    [Test]
    public void For_MultipleMembers_SomeFail_ReportsAllFailures()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .For(o => o.CustomerId).NotNull().End()
            .For(o => o.ShippingAddress).NotNull().End();

        var result = validator.Validate(new Order());
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.EqualTo(2));
    }

    [Test]
    public void For_ChainedValidators_AllApplied()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .For(o => o.CustomerId).NotNull().NotEmpty().End();

        Assert.That(validator.Check(new Order { CustomerId = "" }), Is.False);
    }

    #endregion

    #region MemberPath Tracking

    [Test]
    public void For_TracksMemberPath()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .For(o => o.CustomerId).NotNull().End();

        var result = validator.Validate(new Order());
        Assert.That(result.Failures, Has.Count.EqualTo(1));
        Assert.That(result.Failures[0].MemberPath, Is.EqualTo("CustomerId"));
    }

    #endregion

    #region Validate / Check / Ensure

    [Test]
    public void Validate_ReturnsFullResult()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .For(o => o.CustomerId).NotNull().End();

        var result = validator.Validate(new Order { CustomerId = "C1" });
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Check_ReturnsBool()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .For(o => o.Quantity).InRange(1, 100).End();

        Assert.That(validator.Check(new Order { Quantity = 50 }), Is.True);
        Assert.That(validator.Check(new Order { Quantity = 0 }), Is.False);
    }

    [Test]
    public void Ensure_ValidValue_ReturnsValue()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .For(o => o.CustomerId).NotNull().End();

        var order = new Order { CustomerId = "C1" };
        var result = validator.Ensure(order);
        Assert.That(result, Is.SameAs(order));
    }

    [Test]
    public void Ensure_InvalidValue_ThrowsValidationException()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .For(o => o.CustomerId).NotNull().End();

        Assert.Throws<ValidationException>(() => validator.Ensure(new Order()));
    }

    #endregion

    #region When Scope

    [Test]
    public void When_ConditionTrue_ExecutesRules()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .When(o => o.DeliveryMethod == "ship")
                .For(o => o.ShippingAddress).NotNullOrEmpty().End()
            .End();

        var result = validator.Validate(new Order { DeliveryMethod = "ship", ShippingAddress = null });
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void When_ConditionFalse_SkipsRules()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .When(o => o.DeliveryMethod == "ship")
                .For(o => o.ShippingAddress).NotNullOrEmpty().End()
            .End();

        var result = validator.Validate(new Order { DeliveryMethod = "pickup", ShippingAddress = null });
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void When_MultipleRulesInScope()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .When(o => o.DeliveryMethod == "ship")
                .For(o => o.ShippingAddress).NotNullOrEmpty().End()
                .For(o => o.CustomerId).NotNull().End()
            .End();

        var result = validator.Validate(new Order { DeliveryMethod = "ship" });
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.EqualTo(2));
    }

    #endregion

    #region Or Scope

    [Test]
    public void Or_AnyPasses_GroupPasses()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .Or()
                .For(o => o.Email).NotNull().End()
                .For(o => o.Phone).NotNull().End()
            .End();

        var result = validator.Validate(new Order { Email = "test@test.com", Phone = null });
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Or_AllFail_GroupFails()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .Or()
                .For(o => o.Email).NotNull().End()
                .For(o => o.Phone).NotNull().End()
            .End();

        var result = validator.Validate(new Order { Email = null, Phone = null });
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Or_SecondPasses_GroupPasses()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .Or()
                .For(o => o.Email).NotNull().End()
                .For(o => o.Phone).NotNull().End()
            .End();

        var result = validator.Validate(new Order { Email = null, Phone = "555-1234" });
        Assert.That(result.IsValid, Is.True);
    }

    #endregion

    #region And Scope

    [Test]
    public void And_AllPass_GroupPasses()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .And()
                .For(o => o.Email).NotNull().End()
                .For(o => o.Phone).NotNull().End()
            .End();

        var result = validator.Validate(new Order { Email = "test@test.com", Phone = "555" });
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void And_OneFails_GroupFails()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .And()
                .For(o => o.Email).NotNull().End()
                .For(o => o.Phone).NotNull().End()
            .End();

        var result = validator.Validate(new Order { Email = "test@test.com", Phone = null });
        Assert.That(result.IsValid, Is.False);
    }

    #endregion

    #region Mutable Extension

    [Test]
    public void Validator_IsMutable_CanAddRulesAfterCreation()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .For(o => o.CustomerId).NotNull().End();

        Assert.That(validator.Check(new Order { CustomerId = "C1", Quantity = 0 }), Is.True);

        validator.For(o => o.Quantity).InRange(1, 100).End();

        Assert.That(validator.Check(new Order { CustomerId = "C1", Quantity = 0 }), Is.False);
    }

    [Test]
    public void CacheInvalidation_NewRulesInvalidateCache()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .For(o => o.CustomerId).NotNull().End();

        var order = new Order { CustomerId = "C1", Quantity = 0 };
        Assert.That(validator.Check(order), Is.True);

        validator.For(o => o.Quantity).InRange(1, 100).End();
        Assert.That(validator.Check(order), Is.False);
    }

    #endregion

    #region Build

    [Test]
    public void Build_ProducesImmutableValidationSet()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .For(o => o.CustomerId).NotNull().End()
            .For(o => o.Quantity).InRange(1, 100).End();

        ValidationSet<Order> set = validator.Build();
        Assert.That(set.Check(new Order { CustomerId = "C1", Quantity = 50 }), Is.True);
        Assert.That(set.Check(new Order { CustomerId = null, Quantity = 0 }), Is.False);
    }

    #endregion

    #region Must (Custom Predicate)

    [Test]
    public void Must_PredicateTrue_Passes()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .For(o => o.CustomerId).Must(c => c != null && c.StartsWith("C"), "Must start with C").End();

        Assert.That(validator.Check(new Order { CustomerId = "C123" }), Is.True);
    }

    [Test]
    public void Must_PredicateFalse_Fails()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .For(o => o.CustomerId).Must(c => c != null && c.StartsWith("C"), "Must start with C").End();

        Assert.That(validator.Check(new Order { CustomerId = "X123" }), Is.False);
    }

    #endregion

    #region String Extension Methods

    [Test]
    public void StringExtensions_MinLength_Works()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .For(o => o.CustomerId).MinLength(3).End();

        Assert.That(validator.Check(new Order { CustomerId = "ABC" }), Is.True);
        Assert.That(validator.Check(new Order { CustomerId = "AB" }), Is.False);
    }

    [Test]
    public void StringExtensions_Match_Works()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .For(o => o.CustomerId).Match(@"^C\d+$").End();

        Assert.That(validator.Check(new Order { CustomerId = "C123" }), Is.True);
        Assert.That(validator.Check(new Order { CustomerId = "X123" }), Is.False);
    }

    #endregion

    #region Comparable Extension Methods

    [Test]
    public void ComparableExtensions_InRange_Works()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .For(o => o.Quantity).InRange(1, 100).End();

        Assert.That(validator.Check(new Order { Quantity = 50 }), Is.True);
        Assert.That(validator.Check(new Order { Quantity = 0 }), Is.False);
        Assert.That(validator.Check(new Order { Quantity = 101 }), Is.False);
    }

    [Test]
    public void ComparableExtensions_GreaterThan_Works()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .For(o => o.TotalCost).GreaterThan(0m).End();

        Assert.That(validator.Check(new Order { TotalCost = 10m }), Is.True);
        Assert.That(validator.Check(new Order { TotalCost = 0m }), Is.False);
    }

    #endregion

    #region AbstractValidator

    private class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            For(o => o.CustomerId).NotNull().NotEmpty();
            For(o => o.Quantity).InRange(1, 1000);
            When(o => o.DeliveryMethod == "ship")
                .For(o => o.ShippingAddress).NotNullOrEmpty().End()
            .End();
        }
    }

    [Test]
    public void AbstractValidator_ValidOrder_Passes()
    {
        var validator = new OrderValidator();
        Assert.That(validator.Check(new Order
        {
            CustomerId = "C1",
            Quantity = 50,
            DeliveryMethod = "pickup"
        }), Is.True);
    }

    [Test]
    public void AbstractValidator_InvalidOrder_Fails()
    {
        var validator = new OrderValidator();
        var result = validator.Validate(new Order());
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void AbstractValidator_ConditionalRule_Works()
    {
        var validator = new OrderValidator();
        Assert.That(validator.Check(new Order
        {
            CustomerId = "C1",
            Quantity = 50,
            DeliveryMethod = "ship",
            ShippingAddress = null
        }), Is.False);

        Assert.That(validator.Check(new Order
        {
            CustomerId = "C1",
            Quantity = 50,
            DeliveryMethod = "ship",
            ShippingAddress = "123 Main St"
        }), Is.True);
    }

    [Test]
    public void AbstractValidator_Ensure_ThrowsOnInvalid()
    {
        var validator = new OrderValidator();
        Assert.Throws<ValidationException>(() => validator.Ensure(new Order()));
    }

    [Test]
    public void AbstractValidator_Ensure_ReturnsOnValid()
    {
        var validator = new OrderValidator();
        var order = new Order { CustomerId = "C1", Quantity = 50 };
        Assert.That(validator.Ensure(order), Is.SameAs(order));
    }

    #endregion

    #region Combined Scenario (User's Original Example)

    [Test]
    public void FullExample_UserDesign_Works()
    {
        var validator = Validations.Net.Validator.Create<Order>()
            .For(o => o.CustomerId).NotNull().NotEmpty().End()
            .For(o => o.Quantity).InRange(1, 1000).End()
            .When(o => o.DeliveryMethod == "ship")
                .For(o => o.ShippingAddress).NotNullOrEmpty().End()
            .End()
            .Or()
                .For(o => o.Email).NotNull().End()
                .For(o => o.Phone).NotNull().End()
            .End();

        // Valid: everything provided with email
        Assert.That(validator.Check(new Order
        {
            CustomerId = "C123",
            Quantity = 50,
            DeliveryMethod = "pickup",
            Email = "test@test.com"
        }), Is.True);

        // Valid: ship with address, phone instead of email
        Assert.That(validator.Check(new Order
        {
            CustomerId = "C123",
            Quantity = 50,
            DeliveryMethod = "ship",
            ShippingAddress = "123 Main St",
            Phone = "555-1234"
        }), Is.True);

        // Invalid: missing CustomerId
        Assert.That(validator.Check(new Order
        {
            Quantity = 50,
            Email = "test@test.com"
        }), Is.False);

        // Invalid: ship without address
        Assert.That(validator.Check(new Order
        {
            CustomerId = "C123",
            Quantity = 50,
            DeliveryMethod = "ship",
            Email = "test@test.com"
        }), Is.False);

        // Invalid: no email or phone (Or group fails)
        Assert.That(validator.Check(new Order
        {
            CustomerId = "C123",
            Quantity = 50
        }), Is.False);

        // Mutable: add more rules later
        validator.For(o => o.TotalCost).GreaterThan(0m).End();
        Assert.That(validator.Check(new Order
        {
            CustomerId = "C123",
            Quantity = 50,
            Email = "test@test.com",
            TotalCost = 0m
        }), Is.False);
    }

    #endregion
}
