using System.Numerics;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates that the decorated member's value is divisible by the specified divisor.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateIsDivisibleByAttribute(long divisor) : ValidationAttribute(IsDivisibleBy.ValidatorName)
{
    public long Divisor { get; } = divisor;

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is null)
            return ValidationResult.CreateFromValidationFailure(Name, Message ?? IsDivisibleBy.DefaultValidationFailureMessage, memberName, blackboard,
                [("value", null), ("divisor", Divisor)]);

        bool isDivisible;
        try
        {
            isDivisible = value switch
            {
                int v => v.CheckIsDivisibleBy(checked((int)Divisor)),
                long v => v.CheckIsDivisibleBy(Divisor),
                short v => v.CheckIsDivisibleBy(checked((short)Divisor)),
                byte v => v.CheckIsDivisibleBy(checked((byte)Divisor)),
                uint v => v.CheckIsDivisibleBy(checked((uint)Divisor)),
                ulong v => v.CheckIsDivisibleBy(checked((ulong)Divisor)),
                ushort v => v.CheckIsDivisibleBy(checked((ushort)Divisor)),
                sbyte v => v.CheckIsDivisibleBy(checked((sbyte)Divisor)),
                nint v => v.CheckIsDivisibleBy(checked((nint)Divisor)),
                nuint v => v.CheckIsDivisibleBy(checked((nuint)Divisor)),
                Int128 v => v.CheckIsDivisibleBy((Int128)Divisor),
                UInt128 v => v.CheckIsDivisibleBy(checked((UInt128)Divisor)),
                BigInteger v => v.CheckIsDivisibleBy((BigInteger)Divisor),
                double v => v.CheckIsDivisibleBy((double)Divisor),
                float v => v.CheckIsDivisibleBy((float)Divisor),
                Half v => v.CheckIsDivisibleBy((Half)Divisor),
                decimal v => v.CheckIsDivisibleBy(Divisor),
                _ => false
            };
        }
        catch (OverflowException)
        {
            isDivisible = false;
        }

        if (isDivisible) return ValidationResult.CreateFromValidationSuccess();

        var message = Message ?? IsDivisibleBy.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            [("value", value), ("divisor", Divisor)]);
    }
}
