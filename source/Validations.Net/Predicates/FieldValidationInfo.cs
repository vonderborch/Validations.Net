using System.Reflection;

namespace Validations.Net.Predicates;

/// <summary>
///     Represents information about a field that has validation attributes applied to it.
///     This struct stores the metadata needed for field-level validation processing.
/// </summary>
/// <param name="Field">The field information.</param>
/// <param name="IsPublic">Whether the field is public.</param>
/// <param name="Validators">The validation attributes applied to the field.</param>
public record struct FieldValidationInfo(FieldInfo Field, bool IsPublic, List<ValidationAttribute> Validators);
