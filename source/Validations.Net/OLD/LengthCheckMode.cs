namespace Validations.Net.OLD;

/// <summary>
/// Specifies the modes for validating the length of a given value against a reference value.
/// </summary>
public enum LengthCheckMode
{
    /// <summary>
    /// Represents a validation mode where the target length must exactly match
    /// the specified reference length.
    /// </summary>
    ExactLength,

    /// <summary>
    /// Represents a validation mode where the target length must be strictly less than
    /// the specified reference length.
    /// </summary>
    LessThan,

    /// <summary>
    /// Represents a validation mode where the target length must be less than
    /// or equal to the specified reference length.
    /// </summary>
    LessThanOrEqual,

    /// <summary>
    /// Represents a length check mode where the validation ensures that the actual length
    /// is greater than the specified value.
    /// </summary>
    GreaterThan,

    /// <summary>
    /// Represents a length check mode where the validation ensures that the actual length
    /// is greater than or equal to the specified value.
    /// </summary>
    GreaterThanOrEqual
}
