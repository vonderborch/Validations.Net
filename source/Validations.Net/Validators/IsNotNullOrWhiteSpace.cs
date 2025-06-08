using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class IsNotNullOrWhiteSpace
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNullOrWhiteSpace(this string? value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }
    
    public static string ValidateIsNotNullOrWhiteSpace(this string? value, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsNotNullOrWhiteSpace())
        {
            throw new ValidationException("IsNotNullOrWhiteSpace", propertyName, $"{propertyName} must not be null or whitespace.", blackboard, new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }
}
