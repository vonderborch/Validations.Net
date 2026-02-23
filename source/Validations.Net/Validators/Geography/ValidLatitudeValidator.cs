using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.Geography;

public sealed class ValidLatitudeValidator : IValidator
{
    public static readonly ValidLatitudeValidator Instance = new();
    public string Name => IsValidLatitude.ValidatorName;
    public string DefaultFailureMessage => IsValidLatitude.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is double d)
            return d.ValidateIsValidLatitude(blackboard, DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(Name, "Value is not a double", memberName, blackboard,
            [("value", value)]);
    }
}
