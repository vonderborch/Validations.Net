using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class DoesContainAnyValidator : IValidator
{
    public string[] Substrings { get; }

    public DoesContainAnyValidator(params string[] substrings)
    {
        Substrings = substrings;
    }

    public string Name => DoesContainAny.ValidatorName;
    public string DefaultFailureMessage => DoesContainAny.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string str && str.CheckDoesContainAny(Substrings))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("substrings", Substrings)]);
    }
}
