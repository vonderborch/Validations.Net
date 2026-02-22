using System.Numerics;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is even.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsEvenAttribute() : ValidationAttribute(IsEven.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is null) return ValidationResult.CreateFromValidationSuccess();

        bool isEven = value switch
        {
            int v => v.CheckIsEven(),
            long v => v.CheckIsEven(),
            short v => v.CheckIsEven(),
            byte v => v.CheckIsEven(),
            uint v => v.CheckIsEven(),
            ulong v => v.CheckIsEven(),
            ushort v => v.CheckIsEven(),
            sbyte v => v.CheckIsEven(),
            nint v => v.CheckIsEven(),
            nuint v => v.CheckIsEven(),
            Int128 v => v.CheckIsEven(),
            UInt128 v => v.CheckIsEven(),
            BigInteger v => v.CheckIsEven(),
            double v => v.CheckIsEven(),
            float v => v.CheckIsEven(),
            Half v => v.CheckIsEven(),
            decimal v => v.CheckIsEven(),
            _ => false
        };

        if (isEven) return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsEven.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            [("value", value)]);
    }
}
