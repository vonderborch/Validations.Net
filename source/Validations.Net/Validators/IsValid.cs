using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;
using Validations.Net.ValidationAttributes.Helpers;

namespace Validations.Net.Validators;

/// <summary>
///     Provides extension methods for validating objects against their validation attributes.
/// </summary>
public static class IsValid
{
    /// <summary>
    ///     Checks if the reference type instance is valid according to its validation attributes.
    /// </summary>
    /// <typeparam name="T">The type of the instance to validate, which must be a reference type.</typeparam>
    /// <param name="instance">The instance to validate.</param>
    /// <param name="includePrivateFields">Whether to include private fields in validation.</param>
    /// <param name="includePrivateProperties">Whether to include private properties in validation.</param>
    /// <returns>True if the instance is valid; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValid<T>(this T? instance, bool includePrivateFields = false,
        bool includePrivateProperties = false) where T : class
    {
        TypeValidationInfo validators = ValidatorCache.GetValidatorsForInstance(instance);
        var result = validators.CheckInstance(instance, includePrivateFields, includePrivateProperties);
        return result;
    }

    /// <summary>
    ///     Checks if the nullable value type instance is valid according to its validation attributes.
    /// </summary>
    /// <typeparam name="T">The type of the instance to validate, which must be a value type.</typeparam>
    /// <param name="instance">The nullable value type instance to validate.</param>
    /// <param name="includePrivateFields">Whether to include private fields in validation.</param>
    /// <param name="includePrivateProperties">Whether to include private properties in validation.</param>
    /// <returns>True if the instance is valid; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValid<T>(this T? instance, bool includePrivateFields = false,
        bool includePrivateProperties = false) where T : struct
    {
        TypeValidationInfo validators = ValidatorCache.GetValidatorsForInstance(instance);
        var result = validators.CheckInstance(instance, includePrivateFields, includePrivateProperties);
        return result;
    }

    /// <summary>
    ///     Checks if the non-nullable value type instance is valid according to its validation attributes.
    /// </summary>
    /// <typeparam name="T">The type of the instance to validate, which must be a value type.</typeparam>
    /// <param name="instance">The non-nullable value type instance to validate.</param>
    /// <param name="includePrivateFields">Whether to include private fields in validation.</param>
    /// <param name="includePrivateProperties">Whether to include private properties in validation.</param>
    /// <param name="_">Default parameter used to distinguish from other overloads.</param>
    /// <returns>True if the instance is valid; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValid<T>(this T instance, bool includePrivateFields = false,
        bool includePrivateProperties = false, T _ = default) where T : struct
    {
        TypeValidationInfo validators = ValidatorCache.GetValidatorsForInstance(instance);
        var result = validators.CheckInstance(instance, includePrivateFields, includePrivateProperties);
        return result;
    }

    /// <summary>
    ///     Ensures the reference type instance is valid against its validation attributes and throws an exception if invalid.
    /// </summary>
    /// <typeparam name="T">The type of the instance to validate, which must be a reference type.</typeparam>
    /// <param name="instance">The instance to validate.</param>
    /// <param name="instanceName">The name of the instance for error reporting.</param>
    /// <param name="includePrivateFields">Whether to include private fields in validation.</param>
    /// <param name="includePrivateProperties">Whether to include private properties in validation.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context information.</param>
    /// <returns>The validated instance if valid.</returns>
    /// <exception cref="ValidationException">Thrown when the instance fails validation.</exception>
    public static T EnsureIsValid<T>(this T? instance, string instanceName, bool includePrivateFields = false,
        bool includePrivateProperties = false, IBlackboard? blackboard = null) where T : class
    {
        ValidationResult result =
            instance.ValidateIsValid(instanceName, includePrivateFields, includePrivateProperties, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return instance!;
    }

    /// <summary>
    ///     Ensures the nullable value type instance is valid against its validation attributes and throws an exception if
    ///     invalid.
    /// </summary>
    /// <typeparam name="T">The type of the instance to validate, which must be a value type.</typeparam>
    /// <param name="instance">The nullable value type instance to validate.</param>
    /// <param name="instanceName">The name of the instance for error reporting.</param>
    /// <param name="includePrivateFields">Whether to include private fields in validation.</param>
    /// <param name="includePrivateProperties">Whether to include private properties in validation.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context information.</param>
    /// <returns>The underlying value of the nullable instance if valid.</returns>
    /// <exception cref="ValidationException">Thrown when the instance fails validation.</exception>
    public static T EnsureIsValid<T>(this T? instance, string instanceName, bool includePrivateFields = false,
        bool includePrivateProperties = false, IBlackboard? blackboard = null) where T : struct
    {
        ValidationResult result =
            instance.ValidateIsValid(instanceName, includePrivateFields, includePrivateProperties, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return instance.Value;
    }

    /// <summary>
    ///     Ensures the non-nullable value type instance is valid against its validation attributes and throws an exception if
    ///     invalid.
    /// </summary>
    /// <typeparam name="T">The type of the instance to validate, which must be a value type.</typeparam>
    /// <param name="instance">The non-nullable value type instance to validate.</param>
    /// <param name="instanceName">The name of the instance for error reporting.</param>
    /// <param name="includePrivateFields">Whether to include private fields in validation.</param>
    /// <param name="includePrivateProperties">Whether to include private properties in validation.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context information.</param>
    /// <param name="_">Default parameter used to distinguish from other overloads.</param>
    /// <returns>The validated instance if valid.</returns>
    /// <exception cref="ValidationException">Thrown when the instance fails validation.</exception>
    public static T EnsureIsValid<T>(this T instance, string instanceName, bool includePrivateFields = false,
        bool includePrivateProperties = false, IBlackboard? blackboard = null, T _ = default) where T : struct
    {
        ValidationResult result = instance.ValidateIsValid(instanceName, includePrivateFields, includePrivateProperties,
            blackboard, _);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return instance;
    }

    /// <summary>
    ///     Validates the reference type instance against its validation attributes.
    ///     Returns a ValidationResult indicating success or failure.
    /// </summary>
    /// <typeparam name="T">The type of the instance to validate, which must be a reference type.</typeparam>
    /// <param name="instance">The instance to validate.</param>
    /// <param name="instanceName">The name of the instance for error reporting.</param>
    /// <param name="includePrivateFields">Whether to include private fields in validation.</param>
    /// <param name="includePrivateProperties">Whether to include private properties in validation.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context information.</param>
    /// <returns>A ValidationResult indicating whether the validation passed or failed.</returns>
    public static ValidationResult ValidateIsValid<T>(this T? instance, string instanceName,
        bool includePrivateFields = false,
        bool includePrivateProperties = false, IBlackboard? blackboard = null) where T : class
    {
        if (instance.CheckIsValid(includePrivateFields, includePrivateProperties))
        {
            return new ValidationResult();
        }

        TypeValidationInfo validators = ValidatorCache.GetValidatorsForInstance(instance);
        Dictionary<string, Exception> exceptions =
            validators.ValidateInstance(instance, includePrivateFields, includePrivateProperties, blackboard);

        return new ValidationResult(new ValidationException(
            "IsValid",
            instanceName,
            $"{typeof(T).Name} instance `{instanceName}` is not valid.",
            blackboard,
            new Dictionary<string, object?> { { "instance", instance }, { "validationExceptions", exceptions } }
        ));
    }

    /// <summary>
    ///     Validates the nullable value type instance against its validation attributes.
    ///     Returns a ValidationResult indicating success or failure.
    /// </summary>
    /// <typeparam name="T">The type of the instance to validate, which must be a value type.</typeparam>
    /// <param name="instance">The nullable value type instance to validate.</param>
    /// <param name="instanceName">The name of the instance for error reporting.</param>
    /// <param name="includePrivateFields">Whether to include private fields in validation.</param>
    /// <param name="includePrivateProperties">Whether to include private properties in validation.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context information.</param>
    /// <returns>A ValidationResult indicating whether the validation passed or failed.</returns>
    public static ValidationResult ValidateIsValid<T>(this T? instance, string instanceName,
        bool includePrivateFields = false,
        bool includePrivateProperties = false, IBlackboard? blackboard = null) where T : struct
    {
        if (instance.CheckIsValid(includePrivateFields, includePrivateProperties))
        {
            return new ValidationResult();
        }

        TypeValidationInfo validators = ValidatorCache.GetValidatorsForInstance(instance);
        Dictionary<string, Exception> exceptions =
            validators.ValidateInstance(instance, includePrivateFields, includePrivateProperties, blackboard);

        return new ValidationResult(new ValidationException(
            "IsValid",
            instanceName,
            $"{typeof(T).Name} instance `{instanceName}` is not valid.",
            blackboard,
            new Dictionary<string, object?> { { "instance", instance }, { "validationExceptions", exceptions } }
        ));
    }

    /// <summary>
    ///     Validates the non-nullable value type instance against its validation attributes.
    ///     Returns a ValidationResult indicating success or failure.
    /// </summary>
    /// <typeparam name="T">The type of the instance to validate, which must be a value type.</typeparam>
    /// <param name="instance">The non-nullable value type instance to validate.</param>
    /// <param name="instanceName">The name of the instance for error reporting.</param>
    /// <param name="includePrivateFields">Whether to include private fields in validation.</param>
    /// <param name="includePrivateProperties">Whether to include private properties in validation.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context information.</param>
    /// <param name="_">Default parameter used to distinguish from other overloads.</param>
    /// <returns>A ValidationResult indicating whether the validation passed or failed.</returns>
    public static ValidationResult ValidateIsValid<T>(this T instance, string instanceName,
        bool includePrivateFields = false,
        bool includePrivateProperties = false, IBlackboard? blackboard = null, T _ = default) where T : struct
    {
        if (instance.CheckIsValid(includePrivateFields, includePrivateProperties, _))
        {
            return new ValidationResult();
        }

        TypeValidationInfo validators = ValidatorCache.GetValidatorsForInstance(instance);
        Dictionary<string, Exception> exceptions =
            validators.ValidateInstance(instance, includePrivateFields, includePrivateProperties, blackboard);

        return new ValidationResult(new ValidationException(
            "IsValid",
            instanceName,
            $"{typeof(T).Name} instance `{instanceName}` is not valid.",
            blackboard,
            new Dictionary<string, object?> { { "instance", instance }, { "validationExceptions", exceptions } }
        ));
    }
}
