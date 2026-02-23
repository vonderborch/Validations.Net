using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class DoesContainAllValidator : IValidator
{
    public string[] Substrings { get; }

    public DoesContainAllValidator(params string[] substrings)
    {
        Substrings = substrings;
    }

    public string Name => DoesContainAll.ValidatorName;
    public string DefaultFailureMessage => DoesContainAll.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string str && str.CheckDoesContainAll(Substrings))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("substrings", Substrings)]);
    }
}
