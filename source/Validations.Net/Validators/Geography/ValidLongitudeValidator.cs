using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.Geography;

public sealed class ValidLongitudeValidator : IValidator
{
    public static readonly ValidLongitudeValidator Instance = new();
    public string Name => IsValidLongitude.ValidatorName;
    public string DefaultFailureMessage => IsValidLongitude.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is double d)
            return d.ValidateIsValidLongitude(blackboard, DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(Name, "Value is not a double", memberName, blackboard,
            [("value", value)]);
    }
}
