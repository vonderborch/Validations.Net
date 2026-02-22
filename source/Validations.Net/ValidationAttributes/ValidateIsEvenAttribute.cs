using System.Numerics;
using SimpleBlackboard.Net;
using Validations.Net.Helpers;
using Validations.Net.Validators;
using static Validations.Net.Helpers.BinaryIntegerHelper;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is even.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsEvenAttribute() : ValidationAttribute(IsEven.ValidatorName)
{
    private readonly struct IsEvenOp : IBinaryIntegerOperation<bool?>
    {
        public bool? Execute<T>(T value) where T : IBinaryInteger<T> => value.CheckIsEven();
    }

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is null) return ValidationResult.CreateFromValidationSuccess();

        bool? isEven = BinaryIntegerHelper.Dispatch(value, new IsEvenOp(), (bool?)null);
        if (isEven == true) return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsEven.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            [("value", value)]);
    }
}
