using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is divisible by the specified divisor.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsDivisibleByAttribute(long divisor) : ValidationAttribute(IsDivisibleBy.ValidatorName)
{
    public long Divisor { get; } = divisor;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is null) return ValidationResult.CreateFromValidationSuccess();
        try
        {
            dynamic dyn = value;
            var divisorTyped = Convert.ChangeType(Divisor, value.GetType());
            if (divisorTyped is null || (dynamic)divisorTyped == 0)
                return ValidationResult.CreateFromValidationFailure(Name, Message ?? IsDivisibleBy.DefaultValidationFailureMessage, memberName, blackboard,
                    new List<(string key, object? value)> { ("value", value), ("divisor", Divisor) });
            bool isDivisible = dyn % (dynamic)divisorTyped == (dynamic)Convert.ChangeType(0, value.GetType());
            if (isDivisible) return ValidationResult.CreateFromValidationSuccess();
        }
        catch { }

        var message = Message ?? IsDivisibleBy.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value), ("divisor", Divisor) });
    }
}
