using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class IsNotElementOfValidator : IValidator
{
    public object?[] DisallowedValues { get; }

    public IsNotElementOfValidator(params object?[] disallowedValues)
    {
        DisallowedValues = disallowedValues;
    }

    public string Name => IsNotElementOf.ValidatorName;
    public string DefaultFailureMessage => IsNotElementOf.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (DisallowedValues.Length == 0)
            return ValidationResult.CreateFromValidationSuccess();

        if (value is string str && DisallowedValues.All(v => v is string))
        {
            var stringValues = DisallowedValues.Cast<string>().ToArray();
            if (str.CheckIsNotElementOf(stringValues))
                return ValidationResult.CreateFromValidationSuccess();
        }
        else
        {
            if (((object?)value).CheckIsNotElementOf(DisallowedValues))
                return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("disallowedValues", DisallowedValues)]);
    }
}
