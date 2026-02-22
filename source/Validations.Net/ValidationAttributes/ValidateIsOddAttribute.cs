using System.Numerics;
using SimpleBlackboard.Net;
using Validations.Net.Helpers;
using Validations.Net.Validators;
using static Validations.Net.Helpers.BinaryIntegerHelper;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is odd.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsOddAttribute() : ValidationAttribute(IsOdd.ValidatorName)
{
    private readonly struct IsOddOp : IBinaryIntegerOperation<bool?>
    {
        public bool? Execute<T>(T value) where T : IBinaryInteger<T> => value.CheckIsOdd();
    }

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is null) return ValidationResult.CreateFromValidationSuccess();

        bool? isOdd = BinaryIntegerHelper.Dispatch(value, new IsOddOp(), (bool?)null);
        if (isOdd == true) return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsOdd.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            [("value", value)]);
    }
}
