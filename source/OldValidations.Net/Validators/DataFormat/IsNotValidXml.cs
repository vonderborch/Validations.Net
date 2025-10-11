using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using SimpleBlackboard.Net;
using Validations.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods for checking if a value is not valid XML.
/// </summary>
public static class IsNotValidXml
{
    private const string ValidatorName = nameof(IsNotValidXml);

    #region Check Methods

    /// <summary>
    ///     Checks if the string is not valid XML.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is not valid XML; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidXml(string? value)
    {
        return !IsValidXml.CheckIsValidXml(value);
    }

    /// <summary>
    ///     Checks if the string is not a valid XML document.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is not a valid XML document; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidXmlDocument(string? value)
    {
        return !IsValidXml.CheckIsValidXmlDocument(value);
    }

    /// <summary>
    ///     Checks if the string is not a valid XML fragment.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is not a valid XML fragment; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidXmlFragment(string? value)
    {
        return !IsValidXml.CheckIsValidXmlFragment(value);
    }

    /// <summary>
    ///     Checks if the string is not valid XML with specific options.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="options">The XML parsing options to use.</param>
    /// <returns>True if the string is not valid XML; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidXml(string? value, LoadOptions options)
    {
        return !IsValidXml.CheckIsValidXml(value, options);
    }

    #endregion

    #region Validate Methods

    /// <summary>
    ///     Validates that the string is not valid XML.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidXml(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotValidXml(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must not be valid XML",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string is not a valid XML document.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidXmlDocument(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotValidXmlDocument(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must not be a valid XML document",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string is not a valid XML fragment.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidXmlFragment(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotValidXmlFragment(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must not be a valid XML fragment",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string is not valid XML with specific options.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="options">The XML parsing options to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidXml(string? value, LoadOptions options, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotValidXml(value, options))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("options", options)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must not be valid XML with the specified options",
            parameterName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that the string is not valid XML.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is not valid XML.</returns>
    /// <exception cref="ValidationException">Thrown when the value is valid XML.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotValidXml(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotValidXml(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string is not a valid XML document.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is not a valid XML document.</returns>
    /// <exception cref="ValidationException">Thrown when the value is a valid XML document.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotValidXmlDocument(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotValidXmlDocument(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string is not a valid XML fragment.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is not a valid XML fragment.</returns>
    /// <exception cref="ValidationException">Thrown when the value is a valid XML fragment.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotValidXmlFragment(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotValidXmlFragment(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string is not valid XML with specific options.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="options">The XML parsing options to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is not valid XML with the specified options.</returns>
    /// <exception cref="ValidationException">Thrown when the value is valid XML with the specified options.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotValidXml(string? value, LoadOptions options, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotValidXml(value, options, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    #endregion
}
