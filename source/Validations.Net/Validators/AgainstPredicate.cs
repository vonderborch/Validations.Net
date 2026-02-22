using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;
using Validations.Net.Predicates;

namespace Validations.Net.Validators;

/// <summary>
/// Provides methods for validating if a value meets a predicate.
/// </summary>
public static class AgainstPredicate
{
    /// <summary>
    /// Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "AgainstPredicate";

    /// <summary>
    /// Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must meet the predicate";

    /// <summary>
    /// Checks if the value meets the predicate.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="predicate">The predicate to check against.</param>
    /// <returns>True if the value meets the predicate, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckAgainstPredicate<T>(this T? value, Func<T?, bool> predicate)
    {
        var result = predicate(value);
        return result;
    }

    /// <summary>
    /// Checks if the value meets the predicate.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="predicateName">The name of the predicate to check.</param>
    /// <param name="predicateGroup">The group of the predicate to check.</param>
    /// <param name="predicateInstance">The instance of the predicate to check.</param>
    /// <returns>True if the value meets the predicate, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckAgainstPredicate<T>(this T? value, string predicateName, string? predicateGroup = null,
        object? predicateInstance = null)
    {
        predicateInstance ??= value;
        try
        {
            var predicate = PredicateManager.GetPredicate<T>(predicateName, predicateGroup, predicateInstance);
            if (predicate == null)
            {
                return false;
            }
            var result = predicate(value!);
            return result;
        }
        catch (PredicateException)
        {
            return false;
        }
        catch (PredicateRegistrationException)
        {
            return false;
        }
    }

    /// <summary>
    /// Ensures that the value meets the predicate.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="predicate">The predicate to check against.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <returns>The value if it meets the predicate, otherwise throws a <see cref="ValidationException" />.</returns>
    /// <exception cref="ValidationException">Thrown when the value does not meet the predicate.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? EnsureAgainstPredicate<T>(this T? value, Func<T?, bool> predicate,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateAgainstPredicate(predicate, blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException
                  ?? validationResult.PredicateException
                  ?? (Exception)ValidationException.Create(ValidatorName, validationFailureMessage, parameterName, blackboard,
                      new List<(string key, object? value)> { ("value", value) });
        }

        return value;
    }

    /// <summary>
    /// Ensures that the value meets the predicate.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="predicateName">The name of the predicate to check.</param>
    /// <param name="predicateGroup">The group of the predicate to check.</param>
    /// <param name="predicateInstance">The instance of the predicate to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <returns>The value if it meets the predicate, otherwise throws a <see cref="ValidationException" />.</returns>
    /// <exception cref="ValidationException">Thrown when the value does not meet the predicate.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? EnsureAgainstPredicate<T>(this T? value, string predicateName, string? predicateGroup = null,
        object? predicateInstance = null, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var validationResult = value.ValidateAgainstPredicate(predicateName, predicateGroup, predicateInstance,
            blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException
                  ?? validationResult.PredicateException
                  ?? (Exception)ValidationException.Create(ValidatorName, validationFailureMessage, parameterName, blackboard,
                      new List<(string key, object? value)> { ("value", value), ("predicateName", predicateName), ("predicateGroup", predicateGroup) });
        }

        return value;
    }

    /// <summary>
    /// Validates if the value meets the predicate.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="predicate">The predicate to check against.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the result of the validation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateAgainstPredicate<T>(this T? value, Func<T?, bool> predicate,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckAgainstPredicate(predicate))
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage,
                parameterName, blackboard, [("value", value)]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Validates if the value meets the predicate.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="predicateName">The name of the predicate to check.</param>
    /// <param name="predicateGroup">The group of the predicate to check.</param>
    /// <param name="predicateInstance">The instance of the predicate to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter to check.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the result of the validation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateAgainstPredicate<T>(this T? value, string predicateName,
        string? predicateGroup = null, object? predicateInstance = null, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!value.CheckAgainstPredicate(predicateName, predicateGroup, predicateInstance))
        {
            var result = ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage,
                parameterName, blackboard,
                [
                    ("value", value), ("predicateName", predicateName), ("predicateGroup", predicateGroup),
                    ("predicateInstance", predicateInstance)
                ]);
            return result;
        }

        return ValidationResult.CreateFromValidationSuccess();
    }
}

