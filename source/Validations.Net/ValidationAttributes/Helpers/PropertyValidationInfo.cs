using System.Reflection;

namespace Validations.Net.ValidationAttributes.Helpers;

/// <summary>
///     Represents information about a property that has validation attributes applied to it.
///     This struct stores the metadata needed for property-level validation processing.
/// </summary>
/// <param name="Property">The property information.</param>
/// <param name="IsPublic">Whether the property is public.</param>
/// <param name="Validators">The validation attributes applied to the property.</param>
public record struct PropertyValidationInfo(PropertyInfo Property, bool IsPublic, List<ValidationAttribute> Validators);
