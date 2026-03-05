using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// Base class for all validator-backed attributes. Concrete attributes extend this with a one-line
/// primary constructor that passes the appropriate IValidator instance.
/// Handles the Message override and delegates all validation to the IValidator.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public abstract class ValidatorAttribute(IValidator validator) : ValidationAttribute(validator.Name)
{
    protected IValidator Validator { get; } = validator;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        var result = this.Validator.Validate(value, memberName, blackboard);
        if (!result.IsValid && this.Message is not null)
            return ValidationResult.CreateFromValidationFailure(this.Name, this.Message, memberName, blackboard,
                [("value", value)]);
        return result;
    }

    public override async Task<ValidationResult> ValidateAsync(object? value, string? memberName = null,
        IBlackboard? blackboard = null, CancellationToken cancellationToken = default)
    {
        var result = this.Validator is IAsyncValidator asyncValidator
            ? await asyncValidator.ValidateAsync(value, memberName, blackboard, cancellationToken)
                .ConfigureAwait(false)
            : this.Validator.Validate(value, memberName, blackboard);

        if (!result.IsValid && this.Message is not null)
            return ValidationResult.CreateFromValidationFailure(this.Name, this.Message, memberName, blackboard,
                [("value", value)]);
        return result;
    }
}
