using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using SimpleBlackboard.Net;
using Validations.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods for checking if a value is valid XML.
/// </summary>
public static class IsValidXml
{
    private const string ValidatorName = nameof(IsValidXml);

    #region Check Methods

    /// <summary>
    ///     Checks if the string is valid XML.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is valid XML; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidXml(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        try
        {
            XDocument.Parse(value);
            return true;
        }
        catch (XmlException)
        {
            return false;
        }
    }

    /// <summary>
    ///     Checks if the string is a valid XML document.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a valid XML document; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidXmlDocument(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        try
        {
            var document = XDocument.Parse(value);
            return document.Root != null;
        }
        catch (XmlException)
        {
            return false;
        }
    }

    /// <summary>
    ///     Checks if the string is a valid XML fragment.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a valid XML fragment; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidXmlFragment(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        try
        {
            XElement.Parse(value);
            return true;
        }
        catch (XmlException)
        {
            return false;
        }
    }

    /// <summary>
    ///     Checks if the string is valid XML with specific options.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="options">The XML parsing options to use.</param>
    /// <returns>True if the string is valid XML; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidXml(string? value, LoadOptions options)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        try
        {
            XDocument.Parse(value, options);
            return true;
        }
        catch (XmlException)
        {
            return false;
        }
    }

    #endregion

    #region Validate Methods

    /// <summary>
    ///     Validates that the string is valid XML.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidXml(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidXml(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must be valid XML",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string is a valid XML document.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidXmlDocument(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidXmlDocument(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must be a valid XML document",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string is a valid XML fragment.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidXmlFragment(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidXmlFragment(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must be a valid XML fragment",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string is valid XML with specific options.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="options">The XML parsing options to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidXml(string? value, LoadOptions options, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidXml(value, options))
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
            "The value must be valid XML with the specified options",
            parameterName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that the string is valid XML.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is valid XML.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not valid XML.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidXml(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidXml(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string is a valid XML document.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is a valid XML document.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a valid XML document.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidXmlDocument(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidXmlDocument(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string is a valid XML fragment.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is a valid XML fragment.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a valid XML fragment.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidXmlFragment(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidXmlFragment(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string is valid XML with specific options.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="options">The XML parsing options to use.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is valid XML with the specified options.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not valid XML with the specified options.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidXml(string? value, LoadOptions options, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidXml(value, options, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    #endregion
}
