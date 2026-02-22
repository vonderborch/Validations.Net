using System.Numerics;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is NaN.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsNaNAttribute() : ValidationAttribute(IsNaN.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is null) return ValidationResult.CreateFromValidationSuccess();

        bool isNaN = value switch
        {
            double v => v.CheckIsNaN(),
            float v => v.CheckIsNaN(),
            Half v => v.CheckIsNaN(),
            decimal v => v.CheckIsNaN(),
            int v => v.CheckIsNaN(),
            long v => v.CheckIsNaN(),
            short v => v.CheckIsNaN(),
            byte v => v.CheckIsNaN(),
            uint v => v.CheckIsNaN(),
            ulong v => v.CheckIsNaN(),
            ushort v => v.CheckIsNaN(),
            sbyte v => v.CheckIsNaN(),
            nint v => v.CheckIsNaN(),
            nuint v => v.CheckIsNaN(),
            Int128 v => v.CheckIsNaN(),
            UInt128 v => v.CheckIsNaN(),
            BigInteger v => v.CheckIsNaN(),
            _ => false
        };

        if (isNaN) return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsNaN.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
