using Validations.Net.OLD.Validator;
using Validations.Net.OLD.Validator.Extensions;

namespace Validations.Net.Test.Validator;

[TestFixture]
public class ValidatorNewFeaturesTests
{
    #region Test Models

    private class Order
    {
        public string? CustomerId { get; set; }
        public string? ShippingAddress { get; set; }
        public decimal TotalCost { get; set; }
        public int Quantity { get; set; }
        public bool IsExpress { get; set; }
        public List<OrderItem> Items { get; set; } = new();
        public Address? Address { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    private class OrderItem
    {
        public string? ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    private class Address
    {
        public string? City { get; set; }
        public string? ZipCode { get; set; }
    }

    #endregion

    #region WithMessage

    [Test]
    public void WithMessage_OverridesDefaultErrorMessage()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .For(o => o.CustomerId).NotNull(message: "Customer ID is required").End();

        var result = validator.Validate(new Order());
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures[0].ExceptionMessage, Does.Contain("Customer ID is required"));
    }

    [Test]
    public void WithMessage_NullMessage_UsesDefault()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .For(o => o.CustomerId).NotNull().End();

        var result = validator.Validate(new Order());
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures[0].ExceptionMessage, Does.Not.Contain("Customer ID is required"));
    }

    [Test]
    public void WithMessage_OnSuccess_NoEffect()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .For(o => o.CustomerId).NotNull(message: "Custom message").End();

        var result = validator.Validate(new Order { CustomerId = "C1" });
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void WithMessage_OnExtensionMethod_Works()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .For(o => o.CustomerId).MinLength(5, message: "ID must be at least 5 characters").End();

        var result = validator.Validate(new Order { CustomerId = "AB" });
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures[0].ExceptionMessage, Does.Contain("ID must be at least 5 characters"));
    }

    #endregion

    #region ForEach (with member selector)

    [Test]
    public void ForEach_WithMemberSelector_ValidatesEachElement()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .ForEach(o => o.Items, i => i.ProductId).NotNull().End();

        var order = new Order
        {
            Items = new List<OrderItem>
            {
                new() { ProductId = "P1" },
                new() { ProductId = null },
                new() { ProductId = "P3" }
            }
        };

        var result = validator.Validate(order);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.EqualTo(1));
    }

    [Test]
    public void ForEach_AllValid_Passes()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .ForEach(o => o.Items, i => i.ProductId).NotNull().End();

        var order = new Order
        {
            Items = new List<OrderItem>
            {
                new() { ProductId = "P1" },
                new() { ProductId = "P2" }
            }
        };

        Assert.That(validator.Check(order), Is.True);
    }

    [Test]
    public void ForEach_EmptyCollection_Passes()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .ForEach(o => o.Items, i => i.ProductId).NotNull().End();

        Assert.That(validator.Check(new Order()), Is.True);
    }

    [Test]
    public void ForEach_MultipleFailures_ReportsAll()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .ForEach(o => o.Items, i => i.ProductId).NotNull().End();

        var order = new Order
        {
            Items = new List<OrderItem>
            {
                new() { ProductId = null },
                new() { ProductId = null },
                new() { ProductId = "P3" }
            }
        };

        var result = validator.Validate(order);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.EqualTo(2));
    }

    [Test]
    public void ForEach_TracksIndexedMemberPath()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .ForEach(o => o.Items, i => i.ProductId).NotNull().End();

        var order = new Order
        {
            Items = new List<OrderItem>
            {
                new() { ProductId = "P1" },
                new() { ProductId = null }
            }
        };

        var result = validator.Validate(order);
        Assert.That(result.Failures[0].MemberPath, Does.Contain("[1]"));
        Assert.That(result.Failures[0].MemberPath, Does.Contain("ProductId"));
    }

    [Test]
    public void ForEach_ChainedValidators_AllApplied()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .ForEach(o => o.Items, i => i.Quantity).GreaterThan(0).LessThan(1000).End();

        var order = new Order
        {
            Items = new List<OrderItem>
            {
                new() { Quantity = 5 },
                new() { Quantity = 0 },
                new() { Quantity = 1001 }
            }
        };

        var result = validator.Validate(order);
        Assert.That(result.IsValid, Is.False);
    }

    #endregion

    #region ForEach (element directly)

    [Test]
    public void ForEach_ElementDirectly_ValidatesEachElement()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .ForEach(o => o.Items).NotNull().End();

        var order = new Order
        {
            Items = new List<OrderItem>
            {
                new() { ProductId = "P1" },
                null!
            }
        };

        var result = validator.Validate(order);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.EqualTo(1));
    }

    #endregion

    #region ForEach with Must

    [Test]
    public void ForEach_Must_ValidatesEachElement()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .ForEach(o => o.Items, i => i.Price)
                .Must(p => p > 0, "Price must be positive")
            .End();

        var order = new Order
        {
            Items = new List<OrderItem>
            {
                new() { Price = 10m },
                new() { Price = 0m },
                new() { Price = 5m }
            }
        };

        var result = validator.Validate(order);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.EqualTo(1));
    }

    #endregion

    #region ForEach with WithMessage

    [Test]
    public void ForEach_WithMessage_OverridesMessage()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .ForEach(o => o.Items, i => i.ProductId).NotNull(message: "Product is required").End();

        var order = new Order
        {
            Items = new List<OrderItem> { new() { ProductId = null } }
        };

        var result = validator.Validate(order);
        Assert.That(result.Failures[0].ExceptionMessage, Does.Contain("Product is required"));
    }

    #endregion

    #region UseValidator

    private class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            For(a => a.City).NotNullOrEmpty();
            For(a => a.ZipCode).NotNullOrEmpty();
        }
    }

    [Test]
    public void UseValidator_AbstractValidator_ValidatesNestedObject()
    {
        var addressValidator = new AddressValidator();
        var validator = OLD.Validator.Validator.Create<Order>()
            .For(o => o.Address!).UseValidator(addressValidator).End();

        var order = new Order { Address = new Address { City = null, ZipCode = null } };
        var result = validator.Validate(order);

        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.EqualTo(2));
    }

    [Test]
    public void UseValidator_PrefixesMemberPaths()
    {
        var addressValidator = new AddressValidator();
        var validator = OLD.Validator.Validator.Create<Order>()
            .For(o => o.Address!).UseValidator(addressValidator).End();

        var order = new Order { Address = new Address { City = null, ZipCode = "12345" } };
        var result = validator.Validate(order);

        Assert.That(result.Failures[0].MemberPath, Does.Contain("Address"));
    }

    [Test]
    public void UseValidator_ValidNestedObject_Passes()
    {
        var addressValidator = new AddressValidator();
        var validator = OLD.Validator.Validator.Create<Order>()
            .For(o => o.Address!).UseValidator(addressValidator).End();

        var order = new Order { Address = new Address { City = "NYC", ZipCode = "10001" } };
        Assert.That(validator.Check(order), Is.True);
    }

    [Test]
    public void UseValidator_WithFluentValidator_Works()
    {
        var addressValidator = OLD.Validator.Validator.Create<Address>()
            .For(a => a.City).NotNullOrEmpty().End()
            .For(a => a.ZipCode).NotNullOrEmpty().End();

        var validator = OLD.Validator.Validator.Create<Order>()
            .For(o => o.Address!).UseValidator(addressValidator).End();

        var order = new Order { Address = new Address { City = null, ZipCode = null } };
        var result = validator.Validate(order);

        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.EqualTo(2));
    }

    [Test]
    public void UseValidator_CombinedWithOtherRules()
    {
        var addressValidator = new AddressValidator();
        var validator = OLD.Validator.Validator.Create<Order>()
            .For(o => o.CustomerId).NotNull().End()
            .For(o => o.Address!).UseValidator(addressValidator).End();

        var order = new Order
        {
            CustomerId = null,
            Address = new Address { City = null, ZipCode = "12345" }
        };
        var result = validator.Validate(order);

        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.EqualTo(2));
    }

    #endregion

    #region UseValidator with ForEach

    [Test]
    public void ForEach_UseValidator_ValidatesEachElement()
    {
        var itemValidator = OLD.Validator.Validator.Create<OrderItem>()
            .For(i => i.ProductId).NotNull().End()
            .For(i => i.Quantity).GreaterThan(0).End();

        var validator = OLD.Validator.Validator.Create<Order>()
            .ForEach(o => o.Items).UseValidator(itemValidator).End();

        var order = new Order
        {
            Items = new List<OrderItem>
            {
                new() { ProductId = "P1", Quantity = 5 },
                new() { ProductId = null, Quantity = 0 }
            }
        };

        var result = validator.Validate(order);
        Assert.That(result.IsValid, Is.False);
    }

    #endregion

    #region Unless

    [Test]
    public void Unless_ConditionFalse_ExecutesRules()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .Unless(o => o.IsExpress)
                .For(o => o.ShippingAddress).NotNullOrEmpty().End()
            .End();

        var result = validator.Validate(new Order { IsExpress = false, ShippingAddress = null });
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Unless_ConditionTrue_SkipsRules()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .Unless(o => o.IsExpress)
                .For(o => o.ShippingAddress).NotNullOrEmpty().End()
            .End();

        var result = validator.Validate(new Order { IsExpress = true, ShippingAddress = null });
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Unless_MultipleRulesInScope()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .Unless(o => o.IsExpress)
                .For(o => o.ShippingAddress).NotNullOrEmpty().End()
                .For(o => o.CustomerId).NotNull().End()
            .End();

        var result = validator.Validate(new Order { IsExpress = false });
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.EqualTo(2));
    }

    #endregion

    #region Cascade

    [Test]
    public void Cascade_FirstRuleFails_SkipsSubsequentRules()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .For(o => o.CustomerId).Cascade().NotNull().MinLength(5).End();

        var result = validator.Validate(new Order { CustomerId = null });
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.EqualTo(1));
    }

    [Test]
    public void Cascade_FirstRulePasses_ContinuesToSecond()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .For(o => o.CustomerId).Cascade().NotNull().MinLength(5).End();

        var result = validator.Validate(new Order { CustomerId = "AB" });
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.EqualTo(1));
    }

    [Test]
    public void Cascade_AllPass_Succeeds()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .For(o => o.CustomerId).Cascade().NotNull().MinLength(3).End();

        Assert.That(validator.Check(new Order { CustomerId = "C12345" }), Is.True);
    }

    [Test]
    public void NoCascade_AllFailuresReported()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .For(o => o.CustomerId).NotNull().MinLength(5).End();

        var result = validator.Validate(new Order { CustomerId = null });
        Assert.That(result.Failures, Has.Count.EqualTo(2));
    }

    #endregion

    #region Root-level Must

    [Test]
    public void Must_RootLevel_PredicateTrue_Passes()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .Must(o => o.StartDate < o.EndDate, "Start date must be before end date");

        Assert.That(validator.Check(new Order
        {
            StartDate = new DateTime(2024, 1, 1),
            EndDate = new DateTime(2024, 12, 31)
        }), Is.True);
    }

    [Test]
    public void Must_RootLevel_PredicateFalse_Fails()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .Must(o => o.StartDate < o.EndDate, "Start date must be before end date");

        var result = validator.Validate(new Order
        {
            StartDate = new DateTime(2024, 12, 31),
            EndDate = new DateTime(2024, 1, 1)
        });

        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures[0].ExceptionMessage, Does.Contain("Start date must be before end date"));
    }

    [Test]
    public void Must_RootLevel_CombinedWithMemberRules()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .For(o => o.CustomerId).NotNull().End()
            .Must(o => o.TotalCost > 0, "Total cost must be positive");

        var result = validator.Validate(new Order { CustomerId = null, TotalCost = 0 });
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.EqualTo(2));
    }

    [Test]
    public void Must_RootLevel_ReturnsValidatorForChaining()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .Must(o => o.TotalCost > 0, "Total cost must be positive")
            .For(o => o.CustomerId).NotNull().End();

        Assert.That(validator.Check(new Order { CustomerId = "C1", TotalCost = 10 }), Is.True);
    }

    #endregion

    #region AbstractValidator with new features

    private class FullOrderValidator : AbstractValidator<Order>
    {
        public FullOrderValidator()
        {
            For(o => o.CustomerId).NotNull().NotEmpty();
            ForEach(o => o.Items, i => i.ProductId).NotNull();
            ForEach(o => o.Items, i => i.Quantity).GreaterThan(0);
            Must(o => o.TotalCost >= 0, "Total cost cannot be negative");
            Unless(o => o.IsExpress)
                .For(o => o.ShippingAddress).NotNullOrEmpty().End()
            .End();
        }
    }

    [Test]
    public void AbstractValidator_WithNewFeatures_ValidOrder()
    {
        var validator = new FullOrderValidator();
        var order = new Order
        {
            CustomerId = "C1",
            TotalCost = 100,
            IsExpress = true,
            Items = new List<OrderItem>
            {
                new() { ProductId = "P1", Quantity = 2 },
                new() { ProductId = "P2", Quantity = 1 }
            }
        };
        Assert.That(validator.Check(order), Is.True);
    }

    [Test]
    public void AbstractValidator_WithNewFeatures_InvalidItems()
    {
        var validator = new FullOrderValidator();
        var order = new Order
        {
            CustomerId = "C1",
            TotalCost = 100,
            IsExpress = true,
            Items = new List<OrderItem>
            {
                new() { ProductId = null, Quantity = 0 }
            }
        };
        Assert.That(validator.Check(order), Is.False);
    }

    [Test]
    public void AbstractValidator_Unless_NotExpress_RequiresAddress()
    {
        var validator = new FullOrderValidator();
        var order = new Order
        {
            CustomerId = "C1",
            TotalCost = 100,
            IsExpress = false,
            ShippingAddress = null,
            Items = new List<OrderItem> { new() { ProductId = "P1", Quantity = 1 } }
        };
        Assert.That(validator.Check(order), Is.False);
    }

    [Test]
    public void AbstractValidator_RootMust_NegativeCost()
    {
        var validator = new FullOrderValidator();
        var order = new Order
        {
            CustomerId = "C1",
            TotalCost = -5,
            IsExpress = true,
            Items = new List<OrderItem> { new() { ProductId = "P1", Quantity = 1 } }
        };
        Assert.That(validator.Check(order), Is.False);
    }

    #endregion

    #region ForEach in WhenScope

    [Test]
    public void ForEach_InWhenScope_ConditionTrue_Validates()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .When(o => o.Items.Count > 0)
                .ForEach(o => o.Items, i => i.ProductId).NotNull().End()
            .End();

        var order = new Order
        {
            Items = new List<OrderItem> { new() { ProductId = null } }
        };

        Assert.That(validator.Check(order), Is.False);
    }

    [Test]
    public void ForEach_InWhenScope_ConditionFalse_Skips()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .When(o => o.Items.Count > 0)
                .ForEach(o => o.Items, i => i.ProductId).NotNull().End()
            .End();

        Assert.That(validator.Check(new Order()), Is.True);
    }

    #endregion

    #region Combined Integration Test

    [Test]
    public void FullIntegration_AllNewFeatures()
    {
        var addressValidator = new AddressValidator();

        var validator = OLD.Validator.Validator.Create<Order>()
            .For(o => o.CustomerId).Cascade().NotNull(message: "Customer is required").MinLength(3).End()
            .ForEach(o => o.Items, i => i.ProductId).NotNull(message: "Product required").End()
            .ForEach(o => o.Items, i => i.Quantity).GreaterThan(0, message: "Qty must be > 0").End()
            .Must(o => o.StartDate <= o.EndDate, "Invalid date range")
            .Unless(o => o.IsExpress)
                .For(o => o.ShippingAddress).NotNullOrEmpty().End()
                .For(o => o.Address!).UseValidator(addressValidator).End()
            .End();

        // Valid order
        var validOrder = new Order
        {
            CustomerId = "C001",
            StartDate = new DateTime(2024, 1, 1),
            EndDate = new DateTime(2024, 12, 31),
            IsExpress = true,
            Items = new List<OrderItem>
            {
                new() { ProductId = "P1", Quantity = 2, Price = 10m },
                new() { ProductId = "P2", Quantity = 1, Price = 5m }
            }
        };
        Assert.That(validator.Check(validOrder), Is.True);

        // Invalid: null CustomerId with cascade (stops at NotNull, doesn't check MinLength)
        var cascadeOrder = new Order
        {
            CustomerId = null,
            StartDate = new DateTime(2024, 1, 1),
            EndDate = new DateTime(2024, 12, 31),
            IsExpress = true,
            Items = new List<OrderItem>
            {
                new() { ProductId = "P1", Quantity = 1 }
            }
        };
        var cascadeResult = validator.Validate(cascadeOrder);
        Assert.That(cascadeResult.IsValid, Is.False);
        var customerFailures = cascadeResult.Failures
            .Where(f => f.MemberPath?.Contains("CustomerId") == true).ToList();
        Assert.That(customerFailures, Has.Count.EqualTo(1));
        Assert.That(customerFailures[0].ExceptionMessage, Does.Contain("Customer is required"));
    }

    #endregion

    #region For with when parameter

    [Test]
    public void For_When_ConditionTrue_ExecutesRules()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .For(o => o.ShippingAddress, when: o => o.IsExpress == false).NotNullOrEmpty().End();

        var result = validator.Validate(new Order { IsExpress = false, ShippingAddress = null });
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void For_When_ConditionFalse_SkipsRules()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .For(o => o.ShippingAddress, when: o => o.IsExpress == false).NotNullOrEmpty().End();

        var result = validator.Validate(new Order { IsExpress = true, ShippingAddress = null });
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void For_When_NullCondition_AlwaysExecutes()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .For(o => o.CustomerId, when: null).NotNull().End();

        Assert.That(validator.Check(new Order { CustomerId = null }), Is.False);
    }

    [Test]
    public void For_When_MultipleChainedRules_AllConditional()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .For(o => o.CustomerId, when: o => o.Quantity > 0).NotNull().MinLength(3).End();

        Assert.That(validator.Check(new Order { Quantity = 0, CustomerId = null }), Is.True);
        Assert.That(validator.Check(new Order { Quantity = 5, CustomerId = null }), Is.False);
        Assert.That(validator.Check(new Order { Quantity = 5, CustomerId = "AB" }), Is.False);
        Assert.That(validator.Check(new Order { Quantity = 5, CustomerId = "ABC" }), Is.True);
    }

    [Test]
    public void For_When_MixedConditionalAndUnconditional()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .For(o => o.CustomerId).NotNull().End()
            .For(o => o.ShippingAddress, when: o => !o.IsExpress).NotNullOrEmpty().End();

        Assert.That(validator.Check(new Order { CustomerId = "C1", IsExpress = true }), Is.True);
        Assert.That(validator.Check(new Order { CustomerId = "C1", IsExpress = false, ShippingAddress = null }), Is.False);
        Assert.That(validator.Check(new Order { CustomerId = "C1", IsExpress = false, ShippingAddress = "123 St" }), Is.True);
    }

    [Test]
    public void ForEach_When_ConditionTrue_Validates()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .ForEach(o => o.Items, i => i.ProductId, when: o => o.Items.Count > 0).NotNull().End();

        var order = new Order
        {
            Items = new List<OrderItem> { new() { ProductId = null } }
        };

        Assert.That(validator.Check(order), Is.False);
    }

    [Test]
    public void ForEach_When_ConditionFalse_Skips()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .ForEach(o => o.Items, i => i.ProductId, when: o => o.IsExpress).NotNull().End();

        var order = new Order
        {
            IsExpress = false,
            Items = new List<OrderItem> { new() { ProductId = null } }
        };

        Assert.That(validator.Check(order), Is.True);
    }

    [Test]
    public void AbstractValidator_For_When_Works()
    {
        var validator = new ConditionalOrderValidator();

        Assert.That(validator.Check(new Order { CustomerId = "C1", IsExpress = true }), Is.True);
        Assert.That(validator.Check(new Order { CustomerId = "C1", IsExpress = false, ShippingAddress = null }), Is.False);
        Assert.That(validator.Check(new Order { CustomerId = "C1", IsExpress = false, ShippingAddress = "123" }), Is.True);
    }

    private class ConditionalOrderValidator : AbstractValidator<Order>
    {
        public ConditionalOrderValidator()
        {
            For(o => o.CustomerId).NotNull();
            For(o => o.ShippingAddress, when: o => !o.IsExpress).NotNullOrEmpty();
        }
    }

    #endregion

    #region For with unless parameter

    [Test]
    public void For_Unless_ConditionTrue_SkipsRules()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .For(o => o.ShippingAddress, unless: o => o.IsExpress).NotNullOrEmpty().End();

        var result = validator.Validate(new Order { IsExpress = true, ShippingAddress = null });
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void For_Unless_ConditionFalse_ExecutesRules()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .For(o => o.ShippingAddress, unless: o => o.IsExpress).NotNullOrEmpty().End();

        var result = validator.Validate(new Order { IsExpress = false, ShippingAddress = null });
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void For_WhenAndUnless_BothApply()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .For(o => o.ShippingAddress,
                when: o => o.Quantity > 0,
                unless: o => o.IsExpress)
            .NotNullOrEmpty().End();

        // when=true, unless=false → executes
        Assert.That(validator.Check(new Order { Quantity = 5, IsExpress = false, ShippingAddress = null }), Is.False);
        // when=true, unless=true → skipped
        Assert.That(validator.Check(new Order { Quantity = 5, IsExpress = true, ShippingAddress = null }), Is.True);
        // when=false, unless=false → skipped
        Assert.That(validator.Check(new Order { Quantity = 0, IsExpress = false, ShippingAddress = null }), Is.True);
    }

    [Test]
    public void ForEach_Unless_ConditionTrue_Skips()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .ForEach(o => o.Items, i => i.ProductId, unless: o => o.IsExpress).NotNull().End();

        var order = new Order
        {
            IsExpress = true,
            Items = new List<OrderItem> { new() { ProductId = null } }
        };

        Assert.That(validator.Check(order), Is.True);
    }

    [Test]
    public void ForEach_Unless_ConditionFalse_Validates()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .ForEach(o => o.Items, i => i.ProductId, unless: o => o.IsExpress).NotNull().End();

        var order = new Order
        {
            IsExpress = false,
            Items = new List<OrderItem> { new() { ProductId = null } }
        };

        Assert.That(validator.Check(order), Is.False);
    }

    #endregion

    #region Otherwise

    [Test]
    public void Otherwise_WhenTrue_ExecutesWhenBranch()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .When(o => o.IsExpress)
                .For(o => o.CustomerId).NotNull().End()
            .Otherwise()
                .For(o => o.ShippingAddress).NotNullOrEmpty().End()
            .End();

        var result = validator.Validate(new Order { IsExpress = true, CustomerId = null });
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.EqualTo(1));
        Assert.That(result.Failures[0].MemberPath, Is.EqualTo("CustomerId"));
    }

    [Test]
    public void Otherwise_WhenFalse_ExecutesOtherwiseBranch()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .When(o => o.IsExpress)
                .For(o => o.CustomerId).NotNull().End()
            .Otherwise()
                .For(o => o.ShippingAddress).NotNullOrEmpty().End()
            .End();

        var result = validator.Validate(new Order { IsExpress = false, ShippingAddress = null });
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.EqualTo(1));
        Assert.That(result.Failures[0].MemberPath, Is.EqualTo("ShippingAddress"));
    }

    [Test]
    public void Otherwise_BothBranchesValid_Passes()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .When(o => o.IsExpress)
                .For(o => o.CustomerId).NotNull().End()
            .Otherwise()
                .For(o => o.ShippingAddress).NotNullOrEmpty().End()
            .End();

        Assert.That(validator.Check(new Order { IsExpress = true, CustomerId = "C1" }), Is.True);
        Assert.That(validator.Check(new Order { IsExpress = false, ShippingAddress = "123 St" }), Is.True);
    }

    [Test]
    public void Otherwise_MultipleRulesInBothBranches()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .When(o => o.IsExpress)
                .For(o => o.CustomerId).NotNull().End()
                .For(o => o.Quantity).GreaterThan(0).End()
            .Otherwise()
                .For(o => o.ShippingAddress).NotNullOrEmpty().End()
                .For(o => o.CustomerId).NotNull().End()
            .End();

        // Express with both missing
        var result1 = validator.Validate(new Order { IsExpress = true, CustomerId = null, Quantity = 0 });
        Assert.That(result1.Failures, Has.Count.EqualTo(2));

        // Non-express with both missing
        var result2 = validator.Validate(new Order { IsExpress = false, CustomerId = null, ShippingAddress = null });
        Assert.That(result2.Failures, Has.Count.EqualTo(2));
    }

    [Test]
    public void Otherwise_WithUnless_Works()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .Unless(o => o.IsExpress)
                .For(o => o.ShippingAddress).NotNullOrEmpty().End()
            .Otherwise()
                .For(o => o.CustomerId).NotNull().End()
            .End();

        // Unless fires when !IsExpress, so when IsExpress=false, ShippingAddress is required
        Assert.That(validator.Check(new Order { IsExpress = false, ShippingAddress = null }), Is.False);
        // Otherwise fires when IsExpress=true, so CustomerId is required
        Assert.That(validator.Check(new Order { IsExpress = true, CustomerId = null }), Is.False);
        Assert.That(validator.Check(new Order { IsExpress = true, CustomerId = "C1" }), Is.True);
    }

    [Test]
    public void Otherwise_CombinedWithOtherRules()
    {
        var validator = OLD.Validator.Validator.Create<Order>()
            .For(o => o.CustomerId).NotNull().End()
            .When(o => o.IsExpress)
                .For(o => o.Quantity).GreaterThan(0).End()
            .Otherwise()
                .For(o => o.ShippingAddress).NotNullOrEmpty().End()
            .End()
            .For(o => o.TotalCost).GreaterThan(0m).End();

        Assert.That(validator.Check(new Order
        {
            CustomerId = "C1", IsExpress = true, Quantity = 5, TotalCost = 100
        }), Is.True);

        Assert.That(validator.Check(new Order
        {
            CustomerId = "C1", IsExpress = false, ShippingAddress = "123 St", TotalCost = 100
        }), Is.True);
    }

    #endregion
}
