using System.Numerics;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is odd.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsOddAttribute() : ValidationAttribute(IsOdd.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is null) return ValidationResult.CreateFromValidationSuccess();

        bool isOdd = value switch
        {
            int v => v.CheckIsOdd(),
            long v => v.CheckIsOdd(),
            short v => v.CheckIsOdd(),
            byte v => v.CheckIsOdd(),
            uint v => v.CheckIsOdd(),
            ulong v => v.CheckIsOdd(),
            ushort v => v.CheckIsOdd(),
            sbyte v => v.CheckIsOdd(),
            nint v => v.CheckIsOdd(),
            nuint v => v.CheckIsOdd(),
            Int128 v => v.CheckIsOdd(),
            UInt128 v => v.CheckIsOdd(),
            BigInteger v => v.CheckIsOdd(),
            double v => v.CheckIsOdd(),
            float v => v.CheckIsOdd(),
            Half v => v.CheckIsOdd(),
            decimal v => v.CheckIsOdd(),
            _ => false
        };

        if (isOdd) return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsOdd.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            [("value", value)]);
    }
}
