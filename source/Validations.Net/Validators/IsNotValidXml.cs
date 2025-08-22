using System.Xml.Linq;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Validates that a value is NOT valid XML.
/// </summary>
public static class IsNotValidXml
{
    private const string ValidatorName = nameof(IsNotValidXml);

    /// <summary>
    ///     Checks if the specified string is NOT valid XML.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is NOT valid XML; otherwise, false.</returns>
    public static bool CheckIsNotValidXml(string? value)
    {
        return !IsValidXml.CheckIsValidXml(value);
    }

    /// <summary>
    ///     Checks if the specified string is NOT valid XML with specific options.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="options">The XML parsing options to use.</param>
    /// <returns>True if the string is NOT valid XML; otherwise, false.</returns>
    public static bool CheckIsNotValidXml(string? value, LoadOptions options)
    {
        return !IsValidXml.CheckIsValidXml(value, options);
    }

    /// <summary>
    ///     Checks if the specified string is NOT valid XML and represents a well-formed document.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is NOT valid XML document; otherwise, false.</returns>
    public static bool CheckIsNotValidXmlDocument(string? value)
    {
        return !IsValidXml.CheckIsValidXmlDocument(value);
    }

    /// <summary>
    ///     Checks if the specified string is NOT valid XML and represents a fragment.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is NOT valid XML fragment; otherwise, false.</returns>
    public static bool CheckIsNotValidXmlFragment(string? value)
    {
        return !IsValidXml.CheckIsValidXmlFragment(value);
    }

    /// <summary>
    ///     Checks if the specified string is NOT valid XML and has a specific root element name.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="rootElementName">The expected root element name.</param>
    /// <returns>True if the string is NOT valid XML with the specified root element; otherwise, false.</returns>
    public static bool CheckIsNotValidXmlWithRootElement(string? value, string rootElementName)
    {
        return !IsValidXml.CheckIsValidXmlWithRootElement(value, rootElementName);
    }

    /// <summary>
    ///     Ensures that the specified string is NOT valid XML.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is valid XML.</exception>
    public static void EnsureIsNotValidXml(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidXml(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is valid XML.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified string is NOT valid XML with specific options.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="options">The XML parsing options to use.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is valid XML.</exception>
    public static void EnsureIsNotValidXml(string? value, LoadOptions options, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidXml(value, options);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Options", options),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is valid XML with the specified options.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified string is NOT valid XML and represents a well-formed document.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is valid XML document.</exception>
    public static void EnsureIsNotValidXmlDocument(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidXmlDocument(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a valid XML document.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified string is NOT valid XML and represents a fragment.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is valid XML fragment.</exception>
    public static void EnsureIsNotValidXmlFragment(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidXmlFragment(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is a valid XML fragment.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the specified string is NOT valid XML and has a specific root element name.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="rootElementName">The expected root element name.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is valid XML with the specified root element.</exception>
    public static void EnsureIsNotValidXmlWithRootElement(string? value, string rootElementName, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidXmlWithRootElement(value, rootElementName);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("RootElementName", rootElementName),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is valid XML with root element '{rootElementName}'.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    ///     Validates that the specified string is NOT valid XML.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is NOT valid XML.</returns>
    public static ValidationResult ValidateIsNotValidXml(string? value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidXml(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is valid XML.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified string is NOT valid XML with specific options.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="options">The XML parsing options to use.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is NOT valid XML.</returns>
    public static ValidationResult ValidateIsNotValidXml(string? value, LoadOptions options, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidXml(value, options);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("Options", options),
            ("FieldName", fieldName)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is valid XML with the specified options.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified string is NOT valid XML and represents a well-formed document.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is NOT valid XML document.</returns>
    public static ValidationResult ValidateIsNotValidXmlDocument(string? value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidXmlDocument(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is a valid XML document.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified string is NOT valid XML and represents a fragment.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is NOT valid XML fragment.</returns>
    public static ValidationResult ValidateIsNotValidXmlFragment(string? value, string fieldName,
        IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidXmlFragment(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is a valid XML fragment.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the specified string is NOT valid XML and has a specific root element name.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="rootElementName">The expected root element name.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is NOT valid XML with the specified root element.</returns>
    public static ValidationResult ValidateIsNotValidXmlWithRootElement(string? value, string rootElementName,
        string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsNotValidXmlWithRootElement(value, rootElementName);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("RootElementName", rootElementName),
            ("FieldName", fieldName)
        };

        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value '{value}' is valid XML with root element '{rootElementName}'.",
            fieldName,
            blackboard,
            contextList);
    }
}
