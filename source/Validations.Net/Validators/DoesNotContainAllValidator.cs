using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class DoesNotContainAllValidator : IValidator
{
    public string[] Substrings { get; }

    public DoesNotContainAllValidator(params string[] substrings)
    {
        Substrings = substrings;
    }

    public string Name => DoesNotContainAll.ValidatorName;
    public string DefaultFailureMessage => DoesNotContainAll.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string str && str.CheckDoesNotContainAll(Substrings))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("substrings", Substrings)]);
    }
}
