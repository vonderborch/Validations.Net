using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public static class AgainstPredicate
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckAgainstPredicate<T>(this T value, Func<T, bool> predicate)
    {
        predicate.ValidateIsNotNull(nameof(predicate));
        return predicate(value);
    }
    
    public static T ValidateAgainstPredicate<T>(this T value, Func<T, bool> predicate, string variableName,
        Blackboard? blackboard = null)
    {
        if (!value.CheckAgainstPredicate(predicate))
        {
            
            throw new ValidationException("AgainstPredicate", variableName, $"{variableName} failed predicate validation.",
                blackboard, new Dictionary<string, object?>
                {
                    { "value", value }
                });
        }

        return value;
    }
}
