using SimpleBlackboard.Net;
using Validations.Net.ValidationAttributes.Helpers;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
///     Attribute that validates if a string does not contain a specified substring or character.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateDoesNotContainAttribute : ValidationAttribute
{
    private readonly StringContainAttributeMode _mode;

    /// <summary>
    ///     Initializes a new instance of the ValidateDoesNotContainAttribute class with a substring, start index, and optional
    ///     count.
    /// </summary>
    /// <param name="subString">The substring to search for.</param>
    /// <param name="startIndex">The starting index for the search.</param>
    /// <param name="count">The number of characters to search, or null to search until the end of the string.</param>
    /// <param name="comparison">The string comparison type to use. Default is Ordinal.</param>
    public ValidateDoesNotContainAttribute(string subString, int startIndex, int count = -1,
        StringComparison comparison = StringComparison.Ordinal) : base("DoesNotContain")
    {
        this.SubString = subString;
        this.StartIndex = startIndex;
        this.Count = count > -1 ? count : null;
        this.Comparison = comparison;
        this._mode = StringContainAttributeMode.SubstringString;
    }

    /// <summary>
    ///     Initializes a new instance of the ValidateDoesNotContainAttribute class with a substring.
    /// </summary>
    /// <param name="subString">The substring to search for.</param>
    /// <param name="comparison">The string comparison type to use. Default is Ordinal.</param>
    public ValidateDoesNotContainAttribute(string subString, StringComparison comparison = StringComparison.Ordinal) :
        base("DoesNotContain")
    {
        this.SubString = subString;
        this.Comparison = comparison;
        this._mode = StringContainAttributeMode.String;
    }

    /// <summary>
    ///     Initializes a new instance of the ValidateDoesNotContainAttribute class with a character, start index, and optional
    ///     count.
    /// </summary>
    /// <param name="character">The character to search for.</param>
    /// <param name="startIndex">The starting index for the search.</param>
    /// <param name="count">The number of characters to search, or null to search until the end of the string.</param>
    /// <param name="comparison">The string comparison type to use. Default is Ordinal.</param>
    public ValidateDoesNotContainAttribute(char character, int startIndex, int? count = null,
        StringComparison comparison = StringComparison.Ordinal) : base("DoesNotContain")
    {
        this.Character = character;
        this.StartIndex = startIndex;
        this.Count = count;
        this.Comparison = comparison;
        this._mode = StringContainAttributeMode.SubstringCharacter;
    }

    /// <summary>
    ///     Initializes a new instance of the ValidateDoesNotContainAttribute class with a character.
    /// </summary>
    /// <param name="character">The character to search for.</param>
    /// <param name="comparison">The string comparison type to use. Default is Ordinal.</param>
    public ValidateDoesNotContainAttribute(char character, StringComparison comparison = StringComparison.Ordinal) :
        base("DoesNotContain")
    {
        this.Character = character;
        this.Comparison = comparison;
        this._mode = StringContainAttributeMode.Character;
    }

    /// <summary>
    ///     Gets the string comparison type to use.
    /// </summary>
    public StringComparison Comparison { get; }

    /// <summary>
    ///     Gets the starting index for the search.
    /// </summary>
    public int StartIndex { get; }

    /// <summary>
    ///     Gets the character to search for.
    /// </summary>
    public char? Character { get; init; }

    /// <summary>
    ///     Gets the number of characters to search, or null to search until the end of the string.
    /// </summary>
    public int? Count { get; init; }

    /// <summary>
    ///     Gets the substring to search for.
    /// </summary>
    public string? SubString { get; init; }

    /// <summary>
    /// Checks whether the specified value does not contain a defined substring, character, or pattern based on the attribute's configuration.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="instance">The instance containing the value being validated.</param>
    /// <returns>True if the value does not contain the specified substring, character, or pattern; otherwise, false.</returns>
    public override bool Check(object? value, object? instance)
    {
        TypeInfo<string> typedValue = GetCorrectType<string>(value, nameof(value), instance);
        if (!typedValue.IsCorrectType)
        {
            return false;
        }

        return this._mode switch
        {
            StringContainAttributeMode.Character => typedValue.ConvertedValue.CheckDoesNotContain(this.Character!.Value,
                this.Comparison),
            StringContainAttributeMode.SubstringCharacter => typedValue.ConvertedValue.CheckDoesNotContain(
                this.Character!.Value, this.StartIndex, this.Count, this.Comparison),
            StringContainAttributeMode.String => typedValue.ConvertedValue.CheckDoesNotContain(this.SubString!,
                this.Comparison),
            StringContainAttributeMode.SubstringString => typedValue.ConvertedValue.CheckDoesNotContain(this.SubString!,
                this.StartIndex, this.Count, this.Comparison),
            _ => false
        };
    }

    /// <summary>
    /// Validates the specified value against the validation rules defined by this attribute.
    /// </summary>
    /// <param name="value">The value of the field or property being validated.</param>
    /// <param name="instance">The object instance containing the property or field being validated.</param>
    /// <param name="propertyName">The name of the property or field being validated.</param>
    /// <param name="blackboard">An optional blackboard instance providing additional context for validation.</param>
    /// <returns>A ValidationResult object indicating the result of the validation.</returns>
    public override ValidationResult Validate(object? value, object? instance, string propertyName,
        IBlackboard? blackboard = null)
    {
        TypeInfo<string> typedValue = GetCorrectType<string>(value, nameof(value), instance, propertyName, blackboard);
        if (!typedValue.IsCorrectType)
        {
            return new ValidationResult(typedValue.Exception!);
        }

        return this._mode switch
        {
            StringContainAttributeMode.Character => typedValue.ConvertedValue.ValidateDoesNotContain(this.Character!.Value,
                propertyName, this.Comparison, blackboard),
            StringContainAttributeMode.SubstringCharacter => typedValue.ConvertedValue.ValidateDoesNotContain(
                this.Character!.Value, propertyName, this.StartIndex, this.Count, this.Comparison, blackboard),
            StringContainAttributeMode.String => typedValue.ConvertedValue.ValidateDoesNotContain(this.SubString!,
                propertyName, this.Comparison, blackboard),
            StringContainAttributeMode.SubstringString => typedValue.ConvertedValue.ValidateDoesNotContain(this.SubString!,
                propertyName, this.StartIndex, this.Count, this.Comparison, blackboard),
            _ => new ValidationResult(new ValidationException(ValidatorName, propertyName, "Invalid mode", blackboard))
        };
    }
}
