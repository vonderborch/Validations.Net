using System.Numerics;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is finite (not NaN, not infinity).
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsFiniteAttribute() : ValidationAttribute(IsFinite.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is null) return ValidationResult.CreateFromValidationSuccess();

        bool isFinite = value switch
        {
            double v => v.CheckIsFinite(),
            float v => v.CheckIsFinite(),
            Half v => v.CheckIsFinite(),
            decimal v => v.CheckIsFinite(),
            int v => v.CheckIsFinite(),
            long v => v.CheckIsFinite(),
            short v => v.CheckIsFinite(),
            byte v => v.CheckIsFinite(),
            uint v => v.CheckIsFinite(),
            ulong v => v.CheckIsFinite(),
            ushort v => v.CheckIsFinite(),
            sbyte v => v.CheckIsFinite(),
            nint v => v.CheckIsFinite(),
            nuint v => v.CheckIsFinite(),
            Int128 v => v.CheckIsFinite(),
            UInt128 v => v.CheckIsFinite(),
            BigInteger v => v.CheckIsFinite(),
            _ => false
        };

        if (isFinite) return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsFinite.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
