using SimpleBlackboard.Net;
using Validations.Net.OLD;
using Validations.Net.OLD.ValidationSets;
using Validations.Net.OLD.ValidationSets.BuilderExtensions;
using Validations.Net.OLD.Validator;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class AsyncFluentApiTests
{
    #region Test Models

    private class Order
    {
        public string? CustomerId { get; set; }
        public string? Email { get; set; }
        public decimal TotalCost { get; set; }
        public List<string> Tags { get; set; } = new();
    }

    private class Parent
    {
        public string? Name { get; set; }
        public Child? Child { get; set; }
    }

    private class Child
    {
        public string? Value { get; set; }
    }

    #endregion

    #region Async Validator Helpers

    private sealed class AsyncContainsValidator : IAsyncValidator
    {
        private readonly string _substring;
        public AsyncContainsValidator(string substring) => _substring = substring;

        public async Task<ValidationResult> ValidateAsync(object? value, string? memberName = null,
            IBlackboard? blackboard = null, CancellationToken cancellationToken = default)
        {
            await Task.Delay(1, cancellationToken);
            if (value is string s && s.Contains(_substring, StringComparison.Ordinal))
                return ValidationResult.CreateFromValidationSuccess();
            return ValidationResult.CreateFromValidationFailure("AsyncContains",
                $"Value must contain '{_substring}'", memberName, blackboard, [("value", value)]);
        }
    }

    private static Task<bool> IsNonEmptyAsync(string? value, CancellationToken ct)
    {
        return Task.FromResult(!string.IsNullOrEmpty(value));
    }

    private static async Task<bool> SimulateDbCheckAsync(string? value, CancellationToken ct)
    {
        await Task.Delay(1, ct);
        return value != "taken";
    }

    #endregion

    #region MemberRule.ApplyAsync Tests

    [Test]
    public async Task ApplyAsync_WithPassingValidator_Succeeds()
    {
        var validator = OLD.Validator.Validator.Create<Order>();
        validator.For(o => o.Email!).ApplyAsync(new AsyncContainsValidator("@"), "AsyncContains").End();

        var order = new Order { Email = "test@example.com" };
        var result = await validator.ValidateAsync(order);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public async Task ApplyAsync_WithFailingValidator_ReportsFailure()
    {
        var validator = OLD.Validator.Validator.Create<Order>();
        validator.For(o => o.Email!).ApplyAsync(new AsyncContainsValidator("@"), "AsyncContains").End();

        var order = new Order { Email = "invalid-email" };
        var result = await validator.ValidateAsync(order);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures.Any(f => f.MemberPath == "Email"), Is.True);
    }

    [Test]
    public async Task ApplyAsync_WithMessageOverride_UsesCustomMessage()
    {
        var validator = OLD.Validator.Validator.Create<Order>();
        validator.For(o => o.Email!)
            .ApplyAsync(new AsyncContainsValidator("@"), "AsyncContains", message: "Email must have @")
            .End();

        var order = new Order { Email = "invalid" };
        var result = await validator.ValidateAsync(order);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures[0].ExceptionMessage, Does.Contain("Email must have @"));
    }

    [Test]
    public void ApplyAsync_SyncExecution_ThrowsNotSupported()
    {
        var validator = OLD.Validator.Validator.Create<Order>();
        validator.For(o => o.Email!).ApplyAsync(new AsyncContainsValidator("@"), "AsyncContains").End();

        var order = new Order { Email = "test@example.com" };
        Assert.Throws<NotSupportedException>(() => validator.Validate(order));
    }

    #endregion

    #region MemberRule.ApplyAsync ForEach Tests

    [Test]
    public async Task ApplyAsync_ForEach_WithPassingValues_Succeeds()
    {
        var validator = OLD.Validator.Validator.Create<Order>();
        validator.ForEach(o => o.Tags)
            .ApplyAsync(new AsyncContainsValidator("valid"), "AsyncContains")
            .End();

        var order = new Order { Tags = ["valid-tag1", "valid-tag2"] };
        var result = await validator.ValidateAsync(order);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public async Task ApplyAsync_ForEach_WithFailingValue_ReportsIndexedPath()
    {
        var validator = OLD.Validator.Validator.Create<Order>();
        validator.ForEach(o => o.Tags)
            .ApplyAsync(new AsyncContainsValidator("valid"), "AsyncContains")
            .End();

        var order = new Order { Tags = ["valid-tag", "bad-tag"] };
        var result = await validator.ValidateAsync(order);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures.Any(f => f.MemberPath == "Tags[1]"), Is.True);
    }

    #endregion

    #region MemberRule.MustAsync Tests

    [Test]
    public async Task MustAsync_WithPassingPredicate_Succeeds()
    {
        var validator = OLD.Validator.Validator.Create<Order>();
        validator.For(o => o.CustomerId)
            .MustAsync(IsNonEmptyAsync, "Customer ID is required")
            .End();

        var order = new Order { CustomerId = "cust-123" };
        var result = await validator.ValidateAsync(order);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public async Task MustAsync_WithFailingPredicate_ReportsFailure()
    {
        var validator = OLD.Validator.Validator.Create<Order>();
        validator.For(o => o.CustomerId)
            .MustAsync(IsNonEmptyAsync, "Customer ID is required")
            .End();

        var order = new Order { CustomerId = null };
        var result = await validator.ValidateAsync(order);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures[0].MemberPath, Is.EqualTo("CustomerId"));
    }

    [Test]
    public async Task MustAsync_WithAsyncDbCheck_Works()
    {
        var validator = OLD.Validator.Validator.Create<Order>();
        validator.For(o => o.Email)
            .MustAsync(SimulateDbCheckAsync, "Email is already taken")
            .End();

        var order = new Order { Email = "taken" };
        var result = await validator.ValidateAsync(order);
        Assert.That(result.IsValid, Is.False);

        order.Email = "available@example.com";
        result = await validator.ValidateAsync(order);
        Assert.That(result.IsValid, Is.True);
    }

    #endregion

    #region MemberRule.MustAsync ForEach Tests

    [Test]
    public async Task MustAsync_ForEach_WithPassingValues_Succeeds()
    {
        var validator = OLD.Validator.Validator.Create<Order>();
        validator.ForEach(o => o.Tags)
            .MustAsync(IsNonEmptyAsync, "Tag must not be empty")
            .End();

        var order = new Order { Tags = ["tag1", "tag2"] };
        var result = await validator.ValidateAsync(order);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public async Task MustAsync_ForEach_WithFailingValue_ReportsIndexedPath()
    {
        var validator = OLD.Validator.Validator.Create<Order>();
        validator.ForEach(o => o.Tags)
            .MustAsync(IsNonEmptyAsync, "Tag must not be empty")
            .End();

        var order = new Order { Tags = ["tag1", "", "tag3"] };
        var result = await validator.ValidateAsync(order);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures.Any(f => f.MemberPath == "Tags[1]"), Is.True);
    }

    #endregion

    #region MemberRule.UseValidatorAsync Tests

    private class ChildValidator : AbstractValidator<Child>
    {
        public ChildValidator()
        {
            For(c => c.Value)
                .MustAsync(IsNonEmptyAsync, "Value is required");
        }
    }

    [Test]
    public async Task UseValidatorAsync_WithAbstractValidator_Succeeds()
    {
        var childValidator = new ChildValidator();
        var validator = OLD.Validator.Validator.Create<Parent>();
        validator.For(p => p.Child!)
            .UseValidatorAsync(childValidator)
            .End();

        var parent = new Parent { Child = new Child { Value = "hello" } };
        var result = await validator.ValidateAsync(parent);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public async Task UseValidatorAsync_WithAbstractValidator_ReportsPrefixedPath()
    {
        var childValidator = new ChildValidator();
        var validator = OLD.Validator.Validator.Create<Parent>();
        validator.For(p => p.Child!)
            .UseValidatorAsync(childValidator)
            .End();

        var parent = new Parent { Child = new Child { Value = "" } };
        var result = await validator.ValidateAsync(parent);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures.Any(f => f.MemberPath!.StartsWith("Child")), Is.True);
    }

    [Test]
    public async Task UseValidatorAsync_WithFluentValidator_Works()
    {
        var childValidator = OLD.Validator.Validator.Create<Child>();
        childValidator.For(c => c.Value)
            .MustAsync(IsNonEmptyAsync, "Value is required")
            .End();

        var validator = OLD.Validator.Validator.Create<Parent>();
        validator.For(p => p.Child!)
            .UseValidatorAsync(childValidator)
            .End();

        var parent = new Parent { Child = new Child { Value = "hello" } };
        var result = await validator.ValidateAsync(parent);
        Assert.That(result.IsValid, Is.True);

        parent.Child!.Value = "";
        result = await validator.ValidateAsync(parent);
        Assert.That(result.IsValid, Is.False);
    }

    #endregion

    #region Validator<T>.MustAsync Tests

    [Test]
    public async Task Validator_MustAsync_WithPassingPredicate_Succeeds()
    {
        var validator = OLD.Validator.Validator.Create<Order>();
        validator.MustAsync(async (o, ct) =>
        {
            await Task.Delay(1, ct);
            return o.TotalCost > 0;
        }, "Total cost must be positive");

        var order = new Order { TotalCost = 10m };
        var result = await validator.ValidateAsync(order);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public async Task Validator_MustAsync_WithFailingPredicate_ReportsFailure()
    {
        var validator = OLD.Validator.Validator.Create<Order>();
        validator.MustAsync(async (o, ct) =>
        {
            await Task.Delay(1, ct);
            return o.TotalCost > 0;
        }, "Total cost must be positive");

        var order = new Order { TotalCost = 0m };
        var result = await validator.ValidateAsync(order);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validator_MustAsync_SyncExecution_ThrowsNotSupported()
    {
        var validator = OLD.Validator.Validator.Create<Order>();
        validator.MustAsync(async (o, ct) =>
        {
            await Task.Delay(1, ct);
            return o.TotalCost > 0;
        }, "Total cost must be positive");

        var order = new Order { TotalCost = 10m };
        Assert.Throws<NotSupportedException>(() => validator.Validate(order));
    }

    #endregion

    #region AbstractValidator MustAsync Tests

    private class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            For(o => o.CustomerId).NotNull();
            For(o => o.Email)
                .MustAsync(SimulateDbCheckAsync, "Email is already taken");
            MustAsync(async (o, ct) =>
            {
                await Task.Delay(1, ct);
                return o.TotalCost > 0;
            }, "Total cost must be positive");
        }
    }

    [Test]
    public async Task AbstractValidator_MixedSyncAndAsyncRules_Works()
    {
        var validator = new OrderValidator();
        var order = new Order
        {
            CustomerId = "cust-1",
            Email = "new@example.com",
            TotalCost = 10m
        };
        var result = await validator.ValidateAsync(order);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public async Task AbstractValidator_AsyncRuleFails_ReportsFailure()
    {
        var validator = new OrderValidator();
        var order = new Order
        {
            CustomerId = "cust-1",
            Email = "taken",
            TotalCost = 10m
        };
        var result = await validator.ValidateAsync(order);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public async Task AbstractValidator_SyncRuleFails_StillReported()
    {
        var validator = new OrderValidator();
        var order = new Order
        {
            CustomerId = null,
            Email = "new@example.com",
            TotalCost = 10m
        };
        var result = await validator.ValidateAsync(order);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures.Any(f => f.MemberPath == "CustomerId"), Is.True);
    }

    [Test]
    public async Task AbstractValidator_CheckAsync_Works()
    {
        var validator = new OrderValidator();
        var valid = new Order { CustomerId = "c", Email = "new@x.com", TotalCost = 1m };
        var invalid = new Order { CustomerId = null, Email = "taken", TotalCost = 0m };

        Assert.That(await validator.CheckAsync(valid), Is.True);
        Assert.That(await validator.CheckAsync(invalid), Is.False);
    }

    [Test]
    public void AbstractValidator_EnsureAsync_WithInvalidObject_Throws()
    {
        var validator = new OrderValidator();
        var order = new Order { CustomerId = null, Email = "taken", TotalCost = 0m };
        Assert.That(async () => await validator.EnsureAsync(order), Throws.InstanceOf<ValidationException>());
    }

    #endregion

    #region ValidationSetBuilder Async Tests

    [Test]
    public async Task ValidationSetBuilder_AddAsync_Works()
    {
        var set = ValidationSet.For<Order>()
            .AddAsync(async (value, bb, ct) =>
            {
                await Task.Delay(1, ct);
                if (value.TotalCost > 0)
                    return ValidationResult.CreateFromValidationSuccess();
                return ValidationResult.CreateFromValidationFailure("AsyncStep", "Bad total", null, bb,
                    [("value", value.TotalCost)]);
            })
            .Build();

        Assert.That((await set.ExecuteAsync(new Order { TotalCost = 10m })).IsValid, Is.True);
        Assert.That((await set.ExecuteAsync(new Order { TotalCost = 0m })).IsValid, Is.False);
    }

    [Test]
    public async Task ValidationSetBuilder_AddCheckAsync_Works()
    {
        var set = ValidationSet.For<Order>()
            .AddCheckAsync(async (o, ct) =>
            {
                await Task.Delay(1, ct);
                return o.Email != "taken";
            }, "Email is taken")
            .Build();

        Assert.That(await set.CheckAsync(new Order { Email = "ok@x.com" }), Is.True);
        Assert.That(await set.CheckAsync(new Order { Email = "taken" }), Is.False);
    }

    [Test]
    public async Task ValidationSetBuilder_AddValidationAsync_Works()
    {
        var asyncValidator = new AsyncContainsValidator("@");
        var set = ValidationSet.For<Order>()
            .AddValidationAsync(o => o.Email!, asyncValidator, "AsyncContains")
            .Build();

        Assert.That((await set.ExecuteAsync(new Order { Email = "a@b.com" })).IsValid, Is.True);
        Assert.That((await set.ExecuteAsync(new Order { Email = "invalid" })).IsValid, Is.False);
    }

    [Test]
    public async Task ValidationSetBuilder_MixedSyncAndAsync_Works()
    {
        var set = ValidationSet.For<Order>()
            .AddIsNotNull()
            .AddCheckAsync(async (o, ct) =>
            {
                await Task.Delay(1, ct);
                return o.TotalCost > 0;
            }, "Total must be positive")
            .Build();

        var result = await set.ExecuteAsync(new Order { TotalCost = 10m });
        Assert.That(result.IsValid, Is.True);

        result = await set.ExecuteAsync(new Order { TotalCost = 0m });
        Assert.That(result.IsValid, Is.False);
    }

    #endregion

    #region CancellationToken Propagation Tests

    [Test]
    public void MustAsync_WithCancelledToken_ThrowsOperationCancelled()
    {
        var validator = OLD.Validator.Validator.Create<Order>();
        validator.MustAsync(async (o, ct) =>
        {
            await Task.Delay(5000, ct);
            return true;
        }, "Slow check");

        var cts = new CancellationTokenSource();
        cts.Cancel();
        Assert.ThrowsAsync<TaskCanceledException>(
            async () => await validator.ValidateAsync(new Order(), cancellationToken: cts.Token));
    }

    [Test]
    public void ApplyAsync_WithCancelledToken_ThrowsOperationCancelled()
    {
        var validator = OLD.Validator.Validator.Create<Order>();
        validator.For(o => o.Email!)
            .ApplyAsync(new AsyncContainsValidator("@"), "AsyncContains")
            .End();

        var cts = new CancellationTokenSource();
        cts.Cancel();
        Assert.ThrowsAsync<TaskCanceledException>(
            async () => await validator.ValidateAsync(new Order { Email = "test" }, cancellationToken: cts.Token));
    }

    #endregion

    #region Mixed Sync + Async Chain Tests

    [Test]
    public async Task MemberRule_MixedSyncAndAsyncChain_AllEvaluatedAsync()
    {
        var validator = OLD.Validator.Validator.Create<Order>();
        validator.For(o => o.Email)
            .NotNull()
            .MustAsync(SimulateDbCheckAsync, "Email is already taken")
            .End();

        var order = new Order { Email = "available@x.com" };
        var result = await validator.ValidateAsync(order);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public async Task MemberRule_SyncFailsBeforeAsync_ReportsOnlySyncFailure()
    {
        var validator = OLD.Validator.Validator.Create<Order>();
        validator.For(o => o.Email)
            .NotNull()
            .MustAsync(SimulateDbCheckAsync, "Email is already taken")
            .End();

        var order = new Order { Email = null };
        var result = await validator.ValidateAsync(order);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.GreaterThanOrEqualTo(1));
    }

    #endregion
}
