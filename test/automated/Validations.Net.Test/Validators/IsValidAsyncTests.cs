using SimpleBlackboard.Net;
using Validations.Net.OLD;
using Validations.Net.OLD.ValidationSets;
using Validations.Net.OLD.ValidationSets.BuilderExtensions;
using Validations.Net.OLD.Validator;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsValidAsyncTests
{
    #region Test Models

    private class ValidPerson
    {
        [ValidateIsNotNull]
        public string Name { get; set; } = "John";

        [ValidateIsNotNull]
        public string Email { get; set; } = "john@example.com";
    }

    private class InvalidPerson
    {
        [ValidateIsNotNull]
        public string? Name { get; set; }

        [ValidateIsNotNull]
        public string? Email { get; set; }
    }

    private class NestedModel
    {
        [ValidateIsNotNull]
        public string Title { get; set; } = "Test";

        [ValidateNested]
        [ValidateIsNotNull]
        public ValidPerson? Person { get; set; } = new();
    }

    private class CollectionModel
    {
        [ValidateEachIsValid]
        [ValidateIsNotNull]
        public List<ValidPerson>? Items { get; set; } = new();
    }

    #endregion

    #region Async Validator Helpers

    private sealed class AlwaysFailAsyncValidator : IValidator, IAsyncValidator
    {
        public string Name => "AlwaysFailAsync";
        public string DefaultFailureMessage => "Async validation failed";

        public ValidationResult Validate(object? value, string? memberName = null,
            IBlackboard? blackboard = null)
            => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage,
                memberName, blackboard, [("value", value)]);

        public async Task<ValidationResult> ValidateAsync(object? value, string? memberName = null,
            IBlackboard? blackboard = null, CancellationToken cancellationToken = default)
        {
            await Task.Delay(1, cancellationToken);
            return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage,
                memberName, blackboard, [("value", value)]);
        }
    }

    private sealed class AlwaysPassAsyncValidator : IValidator, IAsyncValidator
    {
        public string Name => "AlwaysPassAsync";
        public string DefaultFailureMessage => "Should not fail";

        public ValidationResult Validate(object? value, string? memberName = null,
            IBlackboard? blackboard = null)
            => ValidationResult.CreateFromValidationSuccess();

        public async Task<ValidationResult> ValidateAsync(object? value, string? memberName = null,
            IBlackboard? blackboard = null, CancellationToken cancellationToken = default)
        {
            await Task.Delay(1, cancellationToken);
            return ValidationResult.CreateFromValidationSuccess();
        }
    }

    private sealed class AsyncOnlyValidator : IAsyncValidator
    {
        public async Task<ValidationResult> ValidateAsync(object? value, string? memberName = null,
            IBlackboard? blackboard = null, CancellationToken cancellationToken = default)
        {
            await Task.Delay(1, cancellationToken);
            if (value is string s && s.Contains("unique", StringComparison.Ordinal))
                return ValidationResult.CreateFromValidationSuccess();
            return ValidationResult.CreateFromValidationFailure("AsyncOnly", "Value is not unique",
                memberName, blackboard, [("value", value)]);
        }
    }

    private sealed class AsyncOnlyValidatorAttribute() : ValidatorAttribute(new AsyncOnlyBackedValidator());

    private sealed class AsyncOnlyBackedValidator : IValidator, IAsyncValidator
    {
        public string Name => "AsyncOnly";
        public string DefaultFailureMessage => "Value is not unique";

        public ValidationResult Validate(object? value, string? memberName = null,
            IBlackboard? blackboard = null)
            => throw new NotSupportedException("Use ValidateAsync");

        public async Task<ValidationResult> ValidateAsync(object? value, string? memberName = null,
            IBlackboard? blackboard = null, CancellationToken cancellationToken = default)
        {
            await Task.Delay(1, cancellationToken);
            if (value is string s && s.Contains("unique", StringComparison.Ordinal))
                return ValidationResult.CreateFromValidationSuccess();
            return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage,
                memberName, blackboard, [("value", value)]);
        }
    }

    private class ModelWithAsyncAttribute
    {
        [ValidateIsNotNull]
        public string Name { get; set; } = "John";

        [AsyncOnlyValidator]
        public string UniqueField { get; set; } = "unique-value";
    }

    #endregion

    #region CheckIsValidAsync Tests

    [Test]
    public async Task CheckIsValidAsync_WithNullValue_ReturnsFalse()
    {
        ValidPerson? person = null;
        var result = await person.CheckIsValidAsync();
        Assert.That(result, Is.False);
    }

    [Test]
    public async Task CheckIsValidAsync_WithValidObject_ReturnsTrue()
    {
        var person = new ValidPerson { Name = "John", Email = "john@example.com" };
        var result = await person.CheckIsValidAsync();
        Assert.That(result, Is.True);
    }

    [Test]
    public async Task CheckIsValidAsync_WithInvalidObject_ReturnsFalse()
    {
        var person = new InvalidPerson { Name = null, Email = null };
        var result = await person.CheckIsValidAsync();
        Assert.That(result, Is.False);
    }

    #endregion

    #region ValidateIsValidAsync Tests

    [Test]
    public async Task ValidateIsValidAsync_WithValidObject_ReturnsIsValid()
    {
        var person = new ValidPerson { Name = "John", Email = "john@example.com" };
        var result = await person.ValidateIsValidAsync();
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Failures, Is.Empty);
    }

    [Test]
    public async Task ValidateIsValidAsync_WithNullValue_ReturnsFailure()
    {
        ValidPerson? person = null;
        var result = await person.ValidateIsValidAsync();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.GreaterThan(0));
    }

    [Test]
    public async Task ValidateIsValidAsync_WithInvalidObject_ReturnsAllFailures()
    {
        var person = new InvalidPerson { Name = null, Email = null };
        var result = await person.ValidateIsValidAsync();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task ValidateIsValidAsync_WithPartiallyInvalid_ReturnsOnlyFailedMembers()
    {
        var person = new InvalidPerson { Name = "John", Email = null };
        var result = await person.ValidateIsValidAsync();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.EqualTo(1));
        Assert.That(result.Failures[0].MemberPath, Is.EqualTo("Email"));
    }

    #endregion

    #region EnsureIsValidAsync Tests

    [Test]
    public async Task EnsureIsValidAsync_WithValidObject_DoesNotThrow()
    {
        var person = new ValidPerson { Name = "John", Email = "john@example.com" };
        var returned = await person.EnsureIsValidAsync();
        Assert.That(returned, Is.SameAs(person));
    }

    [Test]
    public void EnsureIsValidAsync_WithInvalidObject_ThrowsValidationException()
    {
        var person = new InvalidPerson { Name = null, Email = null };
        Assert.ThrowsAsync<ValidationException>(async () => await person.EnsureIsValidAsync());
    }

    [Test]
    public void EnsureIsValidAsync_WithNullValue_ThrowsValidationException()
    {
        ValidPerson? person = null;
        Assert.ThrowsAsync<ValidationException>(async () => await person.EnsureIsValidAsync());
    }

    #endregion

    #region Nested Async Validation Tests

    [Test]
    public async Task ValidateIsValidAsync_WithValidNestedObject_Succeeds()
    {
        var model = new NestedModel
        {
            Title = "Test",
            Person = new ValidPerson { Name = "John", Email = "john@example.com" }
        };
        var result = await model.ValidateIsValidAsync();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public async Task ValidateIsValidAsync_WithInvalidNestedMember_ReportsNestedPath()
    {
        var model = new NestedModel
        {
            Title = "Test",
            Person = new ValidPerson { Name = null!, Email = "john@example.com" }
        };
        var result = await model.ValidateIsValidAsync();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures.Any(f => f.MemberPath == "Person.Name"), Is.True);
    }

    #endregion

    #region Collection Async Validation Tests

    [Test]
    public async Task ValidateIsValidAsync_WithValidCollection_Succeeds()
    {
        var model = new CollectionModel
        {
            Items = new List<ValidPerson>
            {
                new() { Name = "Alice", Email = "alice@example.com" },
                new() { Name = "Bob", Email = "bob@example.com" }
            }
        };
        var result = await model.ValidateIsValidAsync();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public async Task ValidateIsValidAsync_WithInvalidCollectionItem_ReportsIndexedPath()
    {
        var model = new CollectionModel
        {
            Items = new List<ValidPerson>
            {
                new() { Name = "Alice", Email = "alice@example.com" },
                new() { Name = null!, Email = "bob@example.com" }
            }
        };
        var result = await model.ValidateIsValidAsync();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures.Any(f => f.MemberPath == "Items[1].Name"), Is.True);
    }

    #endregion

    #region True Async Validator Tests

    [Test]
    public async Task ValidateIsValidAsync_WithAsyncAttribute_PassingValue_Succeeds()
    {
        var model = new ModelWithAsyncAttribute { Name = "John", UniqueField = "unique-value" };
        var result = await model.ValidateIsValidAsync();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public async Task ValidateIsValidAsync_WithAsyncAttribute_FailingValue_ReportsFailure()
    {
        var model = new ModelWithAsyncAttribute { Name = "John", UniqueField = "not-special" };
        var result = await model.ValidateIsValidAsync();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures.Any(f => f.MemberPath == "UniqueField"), Is.True);
    }

    [Test]
    public async Task ValidateIsValidAsync_MatchesSyncResult_ForSyncOnlyValidators()
    {
        var person = new InvalidPerson { Name = "John", Email = null };
        var syncResult = person.ValidateIsValid();
        var asyncResult = await person.ValidateIsValidAsync();

        Assert.That(asyncResult.IsValid, Is.EqualTo(syncResult.IsValid));
        Assert.That(asyncResult.Failures.Count, Is.EqualTo(syncResult.Failures.Count));
    }

    [Test]
    public async Task ValidatorAttribute_ValidateAsync_DetectsIAsyncValidator()
    {
        var attr = new AsyncOnlyValidatorAttribute();
        var result = await attr.ValidateAsync("unique-value", "TestMember");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public async Task ValidatorAttribute_ValidateAsync_FailsForNonUniqueValue()
    {
        var attr = new AsyncOnlyValidatorAttribute();
        var result = await attr.ValidateAsync("not-special", "TestMember");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public async Task ValidatorAttribute_ValidateAsync_AppliesMessageOverride()
    {
        var attr = new AsyncOnlyValidatorAttribute { Message = "Custom async message" };
        var result = await attr.ValidateAsync("not-special", "TestMember");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Custom async message"));
    }

    #endregion

    #region CancellationToken Tests

    [Test]
    public void CheckIsValidAsync_WithCancelledToken_ThrowsOperationCancelled()
    {
        var model = new ModelWithAsyncAttribute();
        var cts = new CancellationTokenSource();
        cts.Cancel();
        Assert.ThrowsAsync<TaskCanceledException>(
            async () => await model.CheckIsValidAsync(cts.Token));
    }

    #endregion

    #region ValidationSet Async Tests

    [Test]
    public async Task ValidationSet_ExecuteAsync_WithSyncSteps_MatchesSyncResult()
    {
        var set = ValidationSet.For<string?>()
            .AddIsNotNull()
            .Build();

        var syncResult = set.Execute("hello");
        var asyncResult = await set.ExecuteAsync("hello");
        Assert.That(asyncResult.IsValid, Is.EqualTo(syncResult.IsValid));
    }

    [Test]
    public async Task ValidationSet_ExecuteAsync_WithNullValue_ReportsFailure()
    {
        var set = ValidationSet.For<string?>()
            .AddIsNotNull()
            .Build();

        var result = await set.ExecuteAsync(null);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public async Task ValidationSet_CheckAsync_WithValidValue_ReturnsTrue()
    {
        var set = ValidationSet.For<string?>()
            .AddIsNotNull()
            .Build();

        var result = await set.CheckAsync("hello");
        Assert.That(result, Is.True);
    }

    [Test]
    public async Task ValidationSet_CheckAsync_WithInvalidValue_ReturnsFalse()
    {
        var set = ValidationSet.For<string?>()
            .AddIsNotNull()
            .Build();

        var result = await set.CheckAsync(null);
        Assert.That(result, Is.False);
    }

    [Test]
    public async Task ValidationSet_EnsureAsync_WithValidValue_ReturnsValue()
    {
        var set = ValidationSet.For<string?>()
            .AddIsNotNull()
            .Build();

        var result = await set.EnsureAsync("hello");
        Assert.That(result, Is.EqualTo("hello"));
    }

    [Test]
    public void ValidationSet_EnsureAsync_WithInvalidValue_Throws()
    {
        var set = ValidationSet.For<string?>()
            .AddIsNotNull()
            .Build();

        Assert.ThrowsAsync<ValidationException>(async () => await set.EnsureAsync(null));
    }

    #endregion

    #region Validator<T> Async Tests

    [Test]
    public async Task Validator_ValidateAsync_WithSyncRules_Works()
    {
        var validator = OLD.Validator.Validator.Create<string?>();
        validator.For(x => x!).NotNull().End();

        var result = await validator.ValidateAsync("hello");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public async Task Validator_CheckAsync_WithInvalidValue_ReturnsFalse()
    {
        var validator = OLD.Validator.Validator.Create<string?>();
        validator.For(x => x!).NotNull().End();

        var result = await validator.CheckAsync(null);
        Assert.That(result, Is.False);
    }

    [Test]
    public void Validator_EnsureAsync_WithInvalidValue_Throws()
    {
        var validator = OLD.Validator.Validator.Create<string?>();
        validator.For(x => x!).NotNull().End();

        Assert.ThrowsAsync<ValidationException>(async () => await validator.EnsureAsync(null));
    }

    #endregion

    #region AbstractValidator Async Tests

    private class PersonValidator : AbstractValidator<ValidPerson>
    {
        public PersonValidator()
        {
            For(p => p.Name).NotNull();
            For(p => p.Email).NotNull();
        }
    }

    [Test]
    public async Task AbstractValidator_ValidateAsync_WithValidObject_Succeeds()
    {
        var validator = new PersonValidator();
        var person = new ValidPerson { Name = "John", Email = "john@example.com" };
        var result = await validator.ValidateAsync(person);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public async Task AbstractValidator_ValidateAsync_WithInvalidObject_ReportsFailures()
    {
        var validator = new PersonValidator();
        var person = new ValidPerson { Name = null!, Email = null! };
        var result = await validator.ValidateAsync(person);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task AbstractValidator_CheckAsync_WithValidObject_ReturnsTrue()
    {
        var validator = new PersonValidator();
        var result = await validator.CheckAsync(new ValidPerson());
        Assert.That(result, Is.True);
    }

    [Test]
    public void AbstractValidator_EnsureAsync_WithInvalidObject_Throws()
    {
        var validator = new PersonValidator();
        Assert.That(
            async () => await validator.EnsureAsync(new ValidPerson { Name = null!, Email = null! }),
            Throws.InstanceOf<ValidationException>());
    }

    #endregion
}
