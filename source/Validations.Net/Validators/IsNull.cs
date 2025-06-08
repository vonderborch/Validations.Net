using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class IsNull
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNull<T>(this T? value)
    {
        return value is null;
    }
    
    public static T ValidateIsNull<T>(this T? value, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsNull())
        {
            throw new ValidationException("IsNull", propertyName, $"{propertyName} must be null.", blackboard, new Dictionary<string, object?> { { "value", value } });
        }
        
        return value!;
    }
}
