namespace Validations.Net.OLD.ValidationAttributes.Helpers;

/// <summary>
/// Specifies the different modes for checking containment of characters or substrings
/// within a string during validation.
/// </summary>
public enum StringContainAttributeMode
{
    /// <summary>
    /// Represents a validation mode that checks if a specified character is present
    /// within an entire string, without considering specific indices or ranges.
    /// </summary>
    Character,

    /// <summary>
    /// Represents a validation mode that verifies if a specified character is present
    /// within a substring of a string, defined by a starting index and optional count.
    /// </summary>
    SubstringCharacter,

    /// <summary>
    /// Represents a validation mode that checks if a string contains another string entirely,
    /// without any additional constraints such as position or range.
    /// </summary>
    String,

    /// <summary>
    /// Represents a validation mode where a specific substring is searched for within a string,
    /// starting at a specified index and optionally within a defined range of characters.
    /// </summary>
    SubstringString,
}
