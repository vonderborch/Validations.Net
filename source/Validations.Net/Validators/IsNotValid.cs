using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;
using Validations.Net.ValidationAttributes.Helpers;

namespace Validations.Net.Validators;

public static class IsNotValid
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValid<T>(this T? instance, bool includePrivateFields = false, bool includePrivateProperties = false) where T : class
    {
        TypeValidationInfo validators = ValidatorCache.GetValidatorsForInstance(instance);
        bool result = validators.CheckInstance(instance, includePrivateFields, includePrivateProperties);
        return !result;
    }

    public static T ValidateIsNotValid<T>(this T? instance, string instanceName, bool includePrivateFields = false, bool includePrivateProperties = false, Blackboard? blackboard = null) where T : class
    {
        TypeValidationInfo validators = ValidatorCache.GetValidatorsForInstance(instance);
        Dictionary<string, Exception> exceptions =
            validators.ValidateInstance(instance, includePrivateFields, includePrivateProperties);
        if (exceptions.Count == 0)
        {
            throw new ValidationException(
                "IsNotValid",
                instanceName,
                $"{typeof(T).Name} instance `{instanceName}` is not invalid.",
                blackboard,
                new Dictionary<string, object?> { { "instance", instance } }
            );
        }

        return instance!;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValid<T>(this T? instance, bool includePrivateFields = false, bool includePrivateProperties = false) where T : struct
    {
        TypeValidationInfo validators = ValidatorCache.GetValidatorsForInstance(instance);
        bool result = validators.CheckInstance(instance, includePrivateFields, includePrivateProperties);
        return !result;
    }

    public static T ValidateIsNotValid<T>(this T? instance, string instanceName, bool includePrivateFields = false, bool includePrivateProperties = false, Blackboard? blackboard = null) where T : struct
    {
        TypeValidationInfo validators = ValidatorCache.GetValidatorsForInstance(instance);
        Dictionary<string, Exception> exceptions =
            validators.ValidateInstance(instance, includePrivateFields, includePrivateProperties);
        if (exceptions.Count == 0)
        {
            throw new ValidationException(
                "IsNotValid",
                instanceName,
                $"{typeof(T).Name} instance `{instanceName}` is not invalid.",
                blackboard,
                new Dictionary<string, object?> { { "instance", instance } }
            );
        }

        return instance.Value;
    }
}
