using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;
using Validations.Net.ValidationAttributes.Helpers;

namespace Validations.Net.Validators;

public static class IsValid
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValid<T>(this T? instance, bool includePrivateFields = false, bool includePrivateProperties = false) where T : class
    {
        TypeValidationInfo validators = ValidatorCache.GetValidatorsForInstance(instance);
        bool result = validators.CheckInstance(instance, includePrivateFields, includePrivateProperties);
        return result;
    }

    public static T ValidateIsValid<T>(this T? instance, string instanceName, bool includePrivateFields = false, bool includePrivateProperties = false, Blackboard? blackboard = null) where T : class
    {
        TypeValidationInfo validators = ValidatorCache.GetValidatorsForInstance(instance);
        Dictionary<string, Exception> exceptions =
            validators.ValidateInstance(instance, includePrivateFields, includePrivateProperties);
        if (exceptions.Count > 0)
        {
            throw new ValidationException(
                "IsValid",
                instanceName,
                $"{typeof(T).Name} instance `{instanceName}` is not valid.",
                blackboard,
                new Dictionary<string, object?> { { "instance", instance }, { "validationExceptions", exceptions } }
            );
        }

        return instance!;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValid<T>(this T? instance, bool includePrivateFields = false, bool includePrivateProperties = false) where T : struct
    {
        TypeValidationInfo validators = ValidatorCache.GetValidatorsForInstance(instance);
        bool result = validators.CheckInstance(instance, includePrivateFields, includePrivateProperties);
        return result;
    }

    public static T ValidateIsValid<T>(this T? instance, string instanceName, bool includePrivateFields = false, bool includePrivateProperties = false, Blackboard? blackboard = null) where T : struct
    {
        TypeValidationInfo validators = ValidatorCache.GetValidatorsForInstance(instance);
        Dictionary<string, Exception> exceptions =
            validators.ValidateInstance(instance, includePrivateFields, includePrivateProperties);
        if (exceptions.Count > 0)
        {
            throw new ValidationException(
                "IsValid",
                instanceName,
                $"{typeof(T).Name} instance `{instanceName}` is not valid.",
                blackboard,
                new Dictionary<string, object?> { { "instance", instance }, { "validationExceptions", exceptions } }
            );
        }

        return instance.Value;
    }
}
