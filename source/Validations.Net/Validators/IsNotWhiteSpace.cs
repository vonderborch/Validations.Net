using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class IsNotWhiteSpace
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotWhiteSpace(this string? value)
    {
        return !string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value);
    }

    public static string ValidateIsNotWhiteSpace(this string? value, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsNotWhiteSpace())
        {
            throw new ValidationException("IsNotWhiteSpace", propertyName, $"{propertyName} must not be whitespace.", blackboard, new Dictionary<string, object?>
            {
                { "value", value }
            });
        }

        return value!;
    }
}
