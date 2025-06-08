using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class IsWhiteSpace
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsWhiteSpace(this string? value)
    {
        return !string.IsNullOrEmpty(value) && string.IsNullOrWhiteSpace(value);
    }
    
    public static string ValidateIsWhiteSpace(this string? value, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsWhiteSpace())
        {
            throw new ValidationException("ValidateIsWhiteSpace", propertyName, $"{propertyName} must be whitespace.", blackboard, new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }
}
