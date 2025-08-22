using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Validations.Net;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Validates that a value is valid XML.
/// </summary>
public static class IsValidXml
{
    private const string ValidatorName = nameof(IsValidXml);

    /// <summary>
    /// Checks if the specified string is valid XML.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is valid XML; otherwise, false.</returns>
    public static bool CheckIsValidXml(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

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
    /// Checks if the specified string is valid XML with specific options.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="options">The XML parsing options to use.</param>
    /// <returns>True if the string is valid XML; otherwise, false.</returns>
    public static bool CheckIsValidXml(string? value, LoadOptions options)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

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

    /// <summary>
    /// Checks if the specified string is valid XML and represents a well-formed document.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is valid XML document; otherwise, false.</returns>
    public static bool CheckIsValidXmlDocument(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        try
        {
            var doc = XDocument.Parse(value);
            return doc.Root != null;
        }
        catch (XmlException)
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if the specified string is valid XML and represents a fragment.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is valid XML fragment; otherwise, false.</returns>
    public static bool CheckIsValidXmlFragment(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        try
        {
            // Try to parse as a fragment by wrapping it in a root element
            var wrappedXml = $"<root>{value}</root>";
            XDocument.Parse(wrappedXml);
            return true;
        }
        catch (XmlException)
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if the specified string is valid XML and has a specific root element name.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="rootElementName">The expected root element name.</param>
    /// <returns>True if the string is valid XML with the specified root element; otherwise, false.</returns>
    public static bool CheckIsValidXmlWithRootElement(string? value, string rootElementName)
    {
        if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(rootElementName))
            return false;

        try
        {
            var doc = XDocument.Parse(value);
            return doc.Root?.Name.LocalName == rootElementName;
        }
        catch (XmlException)
        {
            return false;
        }
    }

    /// <summary>
    /// Validates that the specified string is valid XML.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is valid XML.</returns>
    public static ValidationResult ValidateIsValidXml(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidXml(value);
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
            $"The value '{value}' is not valid XML.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified string is valid XML with specific options.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="options">The XML parsing options to use.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is valid XML.</returns>
    public static ValidationResult ValidateIsValidXml(string? value, LoadOptions options, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidXml(value, options);
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
            $"The value '{value}' is not valid XML with the specified options.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified string is valid XML and represents a well-formed document.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is valid XML document.</returns>
    public static ValidationResult ValidateIsValidXmlDocument(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidXmlDocument(value);
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
            $"The value '{value}' is not a valid XML document.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified string is valid XML and represents a fragment.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is valid XML fragment.</returns>
    public static ValidationResult ValidateIsValidXmlFragment(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidXmlFragment(value);
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
            $"The value '{value}' is not a valid XML fragment.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates that the specified string is valid XML and has a specific root element name.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="rootElementName">The expected root element name.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A validation result indicating whether the string is valid XML with the specified root element.</returns>
    public static ValidationResult ValidateIsValidXmlWithRootElement(string? value, string rootElementName, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidXmlWithRootElement(value, rootElementName);
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
            $"The value '{value}' is not valid XML with root element '{rootElementName}'.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Ensures that the specified string is valid XML.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not valid XML.</exception>
    public static void EnsureIsValidXml(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidXml(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not valid XML.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is valid XML with specific options.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="options">The XML parsing options to use.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not valid XML.</exception>
    public static void EnsureIsValidXml(string? value, LoadOptions options, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidXml(value, options);
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
                $"The value '{value}' is not valid XML with the specified options.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is valid XML and represents a well-formed document.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not valid XML document.</exception>
    public static void EnsureIsValidXmlDocument(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidXmlDocument(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not a valid XML document.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is valid XML and represents a fragment.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not valid XML fragment.</exception>
    public static void EnsureIsValidXmlFragment(string? value, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidXmlFragment(value);
        var contextList = new List<(string, object?)>
        {
            ("Value", value),
            ("FieldName", fieldName)
        };

        if (!isValid)
        {
            throw ValidationException.Create(
                ValidatorName,
                $"The value '{value}' is not a valid XML fragment.",
                fieldName,
                blackboard,
                contextList);
        }
    }

    /// <summary>
    /// Ensures that the specified string is valid XML and has a specific root element name.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="rootElementName">The expected root element name.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not valid XML with the specified root element.</exception>
    public static void EnsureIsValidXmlWithRootElement(string? value, string rootElementName, string fieldName, IBlackboard? blackboard = null)
    {
        var isValid = CheckIsValidXmlWithRootElement(value, rootElementName);
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
                $"The value '{value}' is not valid XML with root element '{rootElementName}'.",
                fieldName,
                blackboard,
                contextList);
        }
    }
}
