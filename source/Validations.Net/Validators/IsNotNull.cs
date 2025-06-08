using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class IsNotNull
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotNull<T>(this T? value)
    {
        return value is not null;
    }
    
    public static T ValidateIsNotNull<T>(this T? value, string propertyName, Blackboard? blackboard = null)
    {
        if (!value.CheckIsNull())
        {
            throw new ValidationException("IsNotNull", propertyName, $"{propertyName} must be null.", blackboard, new Dictionary<string, object?> { { "value", value } });
        }
        
        return value!;
    }
}
