using SimpleBlackboard.Net;
using Validations.Net.OLD.ValidationAttributes.Helpers;
using Validations.Net.OLD.Validators;

namespace Validations.Net.OLD.ValidationAttributes;

/// <summary>
///     Attribute that validates if a string contains a specified substring or character.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateDoesContainAttribute : ValidationAttribute
{
    private readonly StringContainAttributeMode _mode;

    /// <summary>
    ///     Initializes a new instance of the ValidateDoesContainAttribute class with a substring, start index, and optional
    ///     count.
    /// </summary>
    /// <param name="subString">The substring to search for.</param>
    /// <param name="startIndex">The starting index for the search.</param>
    /// <param name="count">The number of characters to search, or null to search until the end of the string.</param>
    /// <param name="comparison">The string comparison type to use. Default is Ordinal.</param>
    public ValidateDoesContainAttribute(string subString, int startIndex, int count = -1,
        StringComparison comparison = StringComparison.Ordinal) : base("DoesContain")
    {
        this.SubString = subString;
        this.StartIndex = startIndex;
        this.Count = count > -1 ? count : null;
        this.Comparison = comparison;
        this._mode = StringContainAttributeMode.SubstringString;
    }

    /// <summary>
    ///     Initializes a new instance of the ValidateDoesContainAttribute class with a substring.
    /// </summary>
    /// <param name="subString">The substring to search for.</param>
    /// <param name="comparison">The string comparison type to use. Default is Ordinal.</param>
    public ValidateDoesContainAttribute(string subString, StringComparison comparison = StringComparison.Ordinal) :
        base("DoesContain")
    {
        this.SubString = subString;
        this.Comparison = comparison;
        this._mode = StringContainAttributeMode.String;
    }

    /// <summary>
    ///     Initializes a new instance of the ValidateDoesContainAttribute class with a character, start index, and optional
    ///     count.
    /// </summary>
    /// <param name="character">The character to search for.</param>
    /// <param name="startIndex">The starting index for the search.</param>
    /// <param name="count">The number of characters to search, or null to search until the end of the string.</param>
    /// <param name="comparison">The string comparison type to use. Default is Ordinal.</param>
    public ValidateDoesContainAttribute(char character, int startIndex, int? count = null,
        StringComparison comparison = StringComparison.Ordinal) : base("DoesContain")
    {
        this.Character = character;
        this.StartIndex = startIndex;
        this.Count = count;
        this.Comparison = comparison;
        this._mode = StringContainAttributeMode.SubstringCharacter;
    }

    /// <summary>
    ///     Initializes a new instance of the ValidateDoesContainAttribute class with a character.
    /// </summary>
    /// <param name="character">The character to search for.</param>
    /// <param name="comparison">The string comparison type to use. Default is Ordinal.</param>
    public ValidateDoesContainAttribute(char character, StringComparison comparison = StringComparison.Ordinal) :
        base("DoesContain")
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
    /// Checks if the provided value meets the validation criteria defined by the instance of the attribute.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="instance">The instance owning the value being validated. Can be null.</param>
    /// <returns>Returns true if the value satisfies the validation rules; otherwise, false.</returns>
    public override bool Check(object? value, object? instance)
    {
        TypeInfo<string> typedValue = GetCorrectType<string>(value, nameof(value), instance);
        if (!typedValue.IsCorrectType)
        {
            return false;
        }

        return this._mode switch
        {
            StringContainAttributeMode.Character => typedValue.ConvertedValue.CheckDoesContain(this.Character!.Value,
                this.Comparison),
            StringContainAttributeMode.SubstringCharacter => typedValue.ConvertedValue.CheckDoesContain(
                this.Character!.Value, this.StartIndex, this.Count, this.Comparison),
            StringContainAttributeMode.String => typedValue.ConvertedValue.CheckDoesContain(this.SubString!,
                this.Comparison),
            StringContainAttributeMode.SubstringString => typedValue.ConvertedValue.CheckDoesContain(this.SubString!,
                this.StartIndex, this.Count, this.Comparison),
            _ => false
        };
    }

    /// <summary>
    /// Validates a specified value against the rules defined in the validation attribute implementation.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="instance">The object instance containing the property or field being validated.</param>
    /// <param name="propertyName">The name of the property or field being validated.</param>
    /// <param name="blackboard">An optional blackboard object for storing shared validation-related context or data.</param>
    /// <returns>A ValidationResult instance indicating the outcome of the validation.</returns>
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
            StringContainAttributeMode.Character => typedValue.ConvertedValue.ValidateDoesContain(this.Character!.Value,
                propertyName, this.Comparison, blackboard),
            StringContainAttributeMode.SubstringCharacter => typedValue.ConvertedValue.ValidateDoesContain(
                this.Character!.Value, propertyName, this.StartIndex, this.Count, this.Comparison, blackboard),
            StringContainAttributeMode.String => typedValue.ConvertedValue.ValidateDoesContain(this.SubString!,
                propertyName, this.Comparison, blackboard),
            StringContainAttributeMode.SubstringString => typedValue.ConvertedValue.ValidateDoesContain(this.SubString!,
                propertyName, this.StartIndex, this.Count, this.Comparison, blackboard),
            _ => new ValidationResult(new ValidationException(this.ValidatorName, propertyName, "Invalid mode", blackboard))
        };
    }
}
