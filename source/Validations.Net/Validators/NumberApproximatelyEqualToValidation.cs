using System.Numerics;

namespace Validations.Net.Validators;

public static class NumberApproximatelyEqualToValidation
{
    public static bool ApproximatelyEqualTo<T>(T value, T other, T tolerance) where T : IFloatingPoint<T>
    {
        tolerance.ValidateIsGreaterThanOrEqualTo(T.Zero, nameof(tolerance));

        return T.Abs(value - other) <= tolerance;
    }
    
    public static bool CheckApproximatelyEqualTo<T>(this T value, T other, T tolerance) where T : IFloatingPoint<T>
    {
        return ApproximatelyEqualTo(value, other, tolerance);
    }
    
    public static T IsApproximatelyEqualToValidation<T>(T value, T other, T tolerance, string variableName, object? blackboard = null) where T : IFloatingPoint<T>
    {
        if (!value.CheckApproximatelyEqualTo(other, tolerance))
        {
            throw new ValidationException(variableName, $"{variableName} must be approximately equal to {other} within a tolerance of {tolerance}.", "IsApproximatelyEqualTo", blackboard, exceptionContext: new Dictionary<string, object?>
            {
                { "value", value },
                { "other", other },
                { "tolerance", tolerance }
            });
        }

        return value;
    }
    
    public static T ValidateIsApproximatelyEqualTo<T>(this T value, T other, T tolerance, string variableName, object? blackboard = null) where T : IFloatingPoint<T>
    {
        return IsApproximatelyEqualToValidation(value, other, tolerance, variableName, blackboard);
    }
}
