using System.Numerics;
using SimpleBlackboard.Net;
using Validations.Net.Helpers;
using Validations.Net.Validators;
using static Validations.Net.Helpers.BinaryIntegerHelper;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is divisible by the specified divisor.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsDivisibleByAttribute(long divisor) : ValidationAttribute(IsDivisibleBy.ValidatorName)
{
    public long Divisor { get; } = divisor;

    private readonly struct IsDivisibleByOp(long divisor) : IBinaryIntegerOperation<bool?>
    {
        public bool? Execute<T>(T value) where T : IBinaryInteger<T>
        {
            try
            {
                var typedDivisor = T.CreateChecked(divisor);
                return value.CheckIsDivisibleBy(typedDivisor);
            }
            catch (OverflowException)
            {
                return false;
            }
        }
    }

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is null)
            return ValidationResult.CreateFromValidationFailure(Name, Message ?? IsDivisibleBy.DefaultValidationFailureMessage, memberName, blackboard,
                [("value", null), ("divisor", Divisor)]);

        bool? isDivisible = BinaryIntegerHelper.Dispatch(value, new IsDivisibleByOp(Divisor), (bool?)null);
        if (isDivisible == true) return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsDivisibleBy.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            [("value", value), ("divisor", Divisor)]);
    }
}
