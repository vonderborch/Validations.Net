using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;
using Validations.Net.ValidationAttributes.Helpers;

namespace Validations.Net.Validators;

/// <summary>
/// Provides extension methods for validating that objects fail against their validation attributes.
/// This is the logical opposite of the IsValid class.
/// </summary>
public static class IsNotValid
{
    /// <summary>
    /// Checks if the reference type instance is invalid according to its validation attributes.
    /// </summary>
    /// <typeparam name="T">The type of the instance to validate, which must be a reference type.</typeparam>
    /// <param name="instance">The instance to validate.</param>
    /// <param name="includePrivateFields">Whether to include private fields in validation.</param>
    /// <param name="includePrivateProperties">Whether to include private properties in validation.</param>
    /// <returns>True if the instance is invalid; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValid<T>(this T? instance, bool includePrivateFields = false,
        bool includePrivateProperties = false) where T : class
    {
        TypeValidationInfo validators = ValidatorCache.GetValidatorsForInstance(instance);
        var result = validators.CheckInstance(instance, includePrivateFields, includePrivateProperties);
        return !result;
    }

    /// <summary>
    /// Checks if the nullable value type instance is invalid according to its validation attributes.
    /// </summary>
    /// <typeparam name="T">The type of the instance to validate, which must be a value type.</typeparam>
    /// <param name="instance">The nullable value type instance to validate.</param>
    /// <param name="includePrivateFields">Whether to include private fields in validation.</param>
    /// <param name="includePrivateProperties">Whether to include private properties in validation.</param>
    /// <returns>True if the instance is invalid; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValid<T>(this T? instance, bool includePrivateFields = false,
        bool includePrivateProperties = false) where T : struct
    {
        TypeValidationInfo validators = ValidatorCache.GetValidatorsForInstance(instance);
        var result = validators.CheckInstance(instance, includePrivateFields, includePrivateProperties);
        return !result;
    }

    /// <summary>
    /// Checks if the non-nullable value type instance is invalid according to its validation attributes.
    /// </summary>
    /// <typeparam name="T">The type of the instance to validate, which must be a value type.</typeparam>
    /// <param name="instance">The non-nullable value type instance to validate.</param>
    /// <param name="includePrivateFields">Whether to include private fields in validation.</param>
    /// <param name="includePrivateProperties">Whether to include private properties in validation.</param>
    /// <param name="_">Default parameter used to distinguish from other overloads.</param>
    /// <returns>True if the instance is invalid; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValid<T>(this T instance, bool includePrivateFields = false,
        bool includePrivateProperties = false, T _ = default) where T : struct
    {
        TypeValidationInfo validators = ValidatorCache.GetValidatorsForInstance(instance);
        var result = validators.CheckInstance(instance, includePrivateFields, includePrivateProperties);
        return !result;
    }

    /// <summary>
    /// Validates that the reference type instance fails against its validation attributes and throws an exception if it is valid.
    /// </summary>
    /// <typeparam name="T">The type of the instance to validate, which must be a reference type.</typeparam>
    /// <param name="instance">The instance to validate.</param>
    /// <param name="instanceName">The name of the instance for error reporting.</param>
    /// <param name="includePrivateFields">Whether to include private fields in validation.</param>
    /// <param name="includePrivateProperties">Whether to include private properties in validation.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context information.</param>
    /// <returns>The instance if it is invalid as expected.</returns>
    /// <exception cref="ValidationException">Thrown when the instance passes validation but was expected to fail.</exception>
    public static T ValidateIsNotValid<T>(this T? instance, string instanceName, bool includePrivateFields = false,
        bool includePrivateProperties = false, Blackboard? blackboard = null) where T : class
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

    /// <summary>
    /// Validates that the nullable value type instance fails against its validation attributes and throws an exception if it is valid.
    /// </summary>
    /// <typeparam name="T">The type of the instance to validate, which must be a value type.</typeparam>
    /// <param name="instance">The nullable value type instance to validate.</param>
    /// <param name="instanceName">The name of the instance for error reporting.</param>
    /// <param name="includePrivateFields">Whether to include private fields in validation.</param>
    /// <param name="includePrivateProperties">Whether to include private properties in validation.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context information.</param>
    /// <returns>The underlying value of the nullable instance if it is invalid as expected.</returns>
    /// <exception cref="ValidationException">Thrown when the instance passes validation but was expected to fail.</exception>
    public static T ValidateIsNotValid<T>(this T? instance, string instanceName, bool includePrivateFields = false,
        bool includePrivateProperties = false, Blackboard? blackboard = null) where T : struct
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

    /// <summary>
    /// Validates that the non-nullable value type instance fails against its validation attributes and throws an exception if it is valid.
    /// </summary>
    /// <typeparam name="T">The type of the instance to validate, which must be a value type.</typeparam>
    /// <param name="instance">The non-nullable value type instance to validate.</param>
    /// <param name="instanceName">The name of the instance for error reporting.</param>
    /// <param name="includePrivateFields">Whether to include private fields in validation.</param>
    /// <param name="includePrivateProperties">Whether to include private properties in validation.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context information.</param>
    /// <param name="_">Default parameter used to distinguish from other overloads.</param>
    /// <returns>The instance value if it is invalid as expected.</returns>
    /// <exception cref="ValidationException">Thrown when the instance passes validation but was expected to fail.</exception>
    public static T ValidateIsNotValid<T>(this T? instance, string instanceName, bool includePrivateFields = false,
        bool includePrivateProperties = false, Blackboard? blackboard = null, T _ = default) where T : struct
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
