using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public sealed class IsElementOfValidator : IValidator
{
    public object?[] AllowedValues { get; }

    public IsElementOfValidator(params object?[] allowedValues)
    {
        AllowedValues = allowedValues;
    }

    public string Name => IsElementOf.ValidatorName;
    public string DefaultFailureMessage => IsElementOf.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (AllowedValues.Length == 0)
            return Fail(value, memberName, blackboard);

        if (value is string str && AllowedValues.All(v => v is string))
        {
            var stringValues = AllowedValues.Cast<string>().ToArray();
            if (str.CheckIsElementOf(stringValues))
                return ValidationResult.CreateFromValidationSuccess();
        }
        else
        {
            if (((object?)value).CheckIsElementOf(AllowedValues))
                return ValidationResult.CreateFromValidationSuccess();
        }

        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
        => ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("allowedValues", AllowedValues)]);
}
