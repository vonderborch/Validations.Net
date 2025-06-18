using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Attribute that validates if a string contains a specified substring or character.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateDoesContainAttribute : ValidationAttribute
{
    /// <summary>
    /// Gets the substring to search for.
    /// </summary>
    public string? SubString { get; init; } = null;

    /// <summary>
    /// Gets the character to search for.
    /// </summary>
    public char? Character { get; init; } = null;

    /// <summary>
    /// Gets the starting index for the search.
    /// </summary>
    public int StartIndex { get; } = 0;

    /// <summary>
    /// Gets the number of characters to search, or null to search until the end of the string.
    /// </summary>
    public int? Count { get; init; } = null;
    
    /// <summary>
    /// Gets the string comparison type to use.
    /// </summary>
    public StringComparison Comparison { get; }

    private string mode = "";
    
    /// <summary>
    /// Initializes a new instance of the ValidateDoesContainAttribute class with a substring, start index, and optional count.
    /// </summary>
    /// <param name="subString">The substring to search for.</param>
    /// <param name="startIndex">The starting index for the search.</param>
    /// <param name="count">The number of characters to search, or null to search until the end of the string.</param>
    /// <param name="comparison">The string comparison type to use. Default is Ordinal.</param>
    public ValidateDoesContainAttribute(string subString, int startIndex, int? count = null,
        StringComparison comparison = StringComparison.Ordinal) : base("DoesContain")
    {
        SubString = subString;
        StartIndex = startIndex;
        Count = count;
        Comparison = comparison;
        mode = "SubstringString";
    }

    /// <summary>
    /// Initializes a new instance of the ValidateDoesContainAttribute class with a substring.
    /// </summary>
    /// <param name="subString">The substring to search for.</param>
    /// <param name="comparison">The string comparison type to use. Default is Ordinal.</param>
    public ValidateDoesContainAttribute(string subString, StringComparison comparison = StringComparison.Ordinal) : base("DoesContain")
    {
        SubString = subString;
        Comparison = comparison;
        mode = "String";
    }
    
    /// <summary>
    /// Initializes a new instance of the ValidateDoesContainAttribute class with a character, start index, and optional count.
    /// </summary>
    /// <param name="character">The character to search for.</param>
    /// <param name="startIndex">The starting index for the search.</param>
    /// <param name="count">The number of characters to search, or null to search until the end of the string.</param>
    /// <param name="comparison">The string comparison type to use. Default is Ordinal.</param>
    public ValidateDoesContainAttribute(char character, int startIndex, int? count = null,
        StringComparison comparison = StringComparison.Ordinal) : base("DoesContain")
    {
        Character = character;
        StartIndex = startIndex;
        Count = count;
        Comparison = comparison;
        mode = "SubstringCharacter";
    }

    /// <summary>
    /// Initializes a new instance of the ValidateDoesContainAttribute class with a character.
    /// </summary>
    /// <param name="character">The character to search for.</param>
    /// <param name="comparison">The string comparison type to use. Default is Ordinal.</param>
    public ValidateDoesContainAttribute(char character, StringComparison comparison = StringComparison.Ordinal) : base("DoesContain")
    {
        Character = character;
        Comparison = comparison;
        mode = "Character";
    }

    /// <summary>
    /// Checks if the provided value contains the specified substring or character.
    /// </summary>
    /// <param name="value">The value to check. Must be a string.</param>
    /// <returns>True if the value contains the specified substring or character, false otherwise.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a string.</exception>
    public override bool Check(object? value)
    {
        string str = GetCorrectType<string>(value, nameof(value));
        switch (mode)
        {
            case "Character":
                return str.CheckDoesContain(Character.Value, Comparison);
            case "SubstringCharacter":
                return str.CheckDoesContain(Character.Value, StartIndex, Count, Comparison);
            case "String":
                return str.CheckDoesContain(SubString, Comparison);
            case "SubstringString":
                return str.CheckDoesContain(SubString, StartIndex, Count, Comparison);
            default:
                throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Validates if the provided value contains the specified substring or character and throws a ValidationException if it does not.
    /// </summary>
    /// <param name="value">The value to validate. Must be a string.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context.</param>
    /// <exception cref="ValidationException">Thrown when the value does not contain the specified substring or character, or when the value is not a string.</exception>
    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        string str = GetCorrectType<string>(value, nameof(value));
        switch (mode)
        {
            case "Character": 
                str.ValidateDoesContain(Character.Value, propertyName, Comparison, blackboard);
                break;
            case "SubstringCharacter": 
                str.ValidateDoesContain(Character.Value, propertyName, StartIndex, Count, Comparison, blackboard);
                break;
            case "String": 
                str.ValidateDoesContain(SubString, propertyName, Comparison, blackboard);
                break;
            case "SubstringString": 
                str.ValidateDoesContain(SubString, propertyName, StartIndex, Count, Comparison, blackboard);
                break;
            default:
                throw new NotImplementedException();
        }
    }
}
