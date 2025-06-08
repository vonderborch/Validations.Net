using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class IsNullOrWhiteSpace
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNullOrWhiteSpace(this string? value)
    {
        return string.IsNullOrWhiteSpace(value);
    }
    
    public static string ValidateIsNullOrWhiteSpace(this string? value, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsNullOrWhiteSpace())
        {
            throw new ValidationException("IsNullOrWhiteSpace", propertyName, $"{propertyName} must be null or whitespace.", blackboard, new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }
}
