using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;
using Validations.Net.OLD.Validation;

namespace Validations.Net.OLD.Validators;

/// <summary>
/// Validates that an object instance passes all ValidationAttributes declared on its type
/// (class-level, property-level, and field-level), including nested and collection validation.
/// </summary>
public static class IsValid
{
    public const string ValidatorName = "IsValid";
    public const string DefaultValidationFailureMessage = "Object failed validation";

    /// <summary>
    /// Checks whether the given instance passes all of its ValidationAttributes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValid<T>(this T? value)
    {
        if (value is null) return false;
        var result = ValidationRunner.Validate(value);
        return result.IsValid;
    }

    /// <summary>
    /// Validates the given instance against all of its ValidationAttributes and returns
    /// an aggregate result containing all failures.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValid<T>(this T? value,
        IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (value is null)
        {
            var builder = ValidationResult.CreateBuilder();
            var failResult = ValidationResult.CreateFromValidationFailure(
                ValidatorName, "Instance is null", parameterName, blackboard,
                new List<(string key, object? value)> { ("value", null) });
            builder.AddFailure("", failResult);
            return builder.Build();
        }

        return ValidationRunner.Validate(value, blackboard);
    }

    /// <summary>
    /// Ensures the given instance passes all of its ValidationAttributes,
    /// throwing a ValidationException if any fail.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? EnsureIsValid<T>(this T? value,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = value.ValidateIsValid(blackboard, parameterName);
        if (!result.IsValid)
        {
            var failures = result.Failures;
            if (failures.Count > 0 && failures[0].ValidationException is { } validationEx)
                throw validationEx;

            throw ValidationException.Create(ValidatorName, validationFailureMessage, parameterName, blackboard,
                new List<(string key, object? value)> { ("value", value), ("failureCount", failures.Count) });
        }

        return value;
    }

    /// <summary>
    /// Asynchronously checks whether the given instance passes all of its ValidationAttributes.
    /// </summary>
    public static async Task<bool> CheckIsValidAsync<T>(this T? value,
        CancellationToken cancellationToken = default)
    {
        if (value is null) return false;
        var result = await AsyncValidationRunner.ValidateAsync(value, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        return result.IsValid;
    }

    /// <summary>
    /// Asynchronously validates the given instance against all of its ValidationAttributes
    /// and returns an aggregate result containing all failures.
    /// </summary>
    public static async Task<ValidationResult> ValidateIsValidAsync<T>(this T? value,
        IBlackboard? blackboard = null, CancellationToken cancellationToken = default)
    {
        if (value is null)
        {
            var builder = ValidationResult.CreateBuilder();
            var failResult = ValidationResult.CreateFromValidationFailure(
                ValidatorName, "Instance is null", null, blackboard,
                new List<(string key, object? value)> { ("value", null) });
            builder.AddFailure("", failResult);
            return builder.Build();
        }

        return await AsyncValidationRunner.ValidateAsync(value, blackboard, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Asynchronously ensures the given instance passes all of its ValidationAttributes,
    /// throwing a <see cref="ValidationException"/> if any fail.
    /// </summary>
    public static async Task<T?> EnsureIsValidAsync<T>(this T? value,
        IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        CancellationToken cancellationToken = default)
    {
        var result = await value.ValidateIsValidAsync(blackboard, cancellationToken)
            .ConfigureAwait(false);
        if (!result.IsValid)
        {
            var failures = result.Failures;
            if (failures.Count > 0 && failures[0].ValidationException is { } validationEx)
                throw validationEx;

            throw ValidationException.Create(ValidatorName, validationFailureMessage, null, blackboard,
                new List<(string key, object? value)> { ("value", value), ("failureCount", failures.Count) });
        }

        return value;
    }
}

public sealed class ValidValidator : IValidator
{
    public static readonly ValidValidator Instance = new();
    public string Name => IsValid.ValidatorName;
    public string DefaultFailureMessage => IsValid.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
        => value.ValidateIsValid(blackboard, memberName);
}
