using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class DoesNotContainAnyValidator : IValidator
{
    public string[] Substrings { get; }

    public DoesNotContainAnyValidator(params string[] substrings)
    {
        Substrings = substrings;
    }

    public string Name => DoesNotContainAny.ValidatorName;
    public string DefaultFailureMessage => DoesNotContainAny.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string str && str.CheckDoesNotContainAny(Substrings))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("substrings", Substrings)]);
    }
}
