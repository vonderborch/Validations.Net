using System.Collections;
using SimpleBlackboard.Net;
using Validations.Net.OLD.Validators;

namespace Validations.Net.OLD.Validation;

/// <summary>
/// Internal runner that executes all validation attributes on an object instance,
/// supporting nested objects and collection items with member path tracking.
/// </summary>
internal static class ValidationRunner
{
    /// <summary>
    /// Validates all attributes on the given instance, recursing into nested objects
    /// and collection items as directed by structural attributes.
    /// </summary>
    public static ValidationResult Validate(object? instance, IBlackboard? blackboard = null)
    {
        if (instance is null)
        {
            var builder = ValidationResult.CreateBuilder();
            var result = ValidationResult.CreateFromValidationFailure(
                "IsValid", "Instance is null", null, blackboard, new List<(string key, object? value)> { ("value", null) });
            builder.AddFailure("", result);
            return builder.Build();
        }

        var visited = new HashSet<object>(ReferenceEqualityComparer.Instance);
        return ValidateInstance(instance, blackboard, visited);
    }

    private static ValidationResult ValidateInstance(
        object instance, IBlackboard? blackboard, HashSet<object> visited)
    {
        if (!instance.GetType().IsValueType && !visited.Add(instance))
            return ValidationResult.CreateFromValidationSuccess();

        var typeInfo = TypeValidationInfo.GetForType(instance.GetType());
        var builder = ValidationResult.CreateBuilder();

        for (int i = 0; i < typeInfo.ClassAttributes.Length; i++)
        {
            var attr = typeInfo.ClassAttributes[i];
            var result = attr.Validate(instance, null, blackboard);
            if (!result.IsValid)
                AddByAttributeSeverity(builder, "", attr, result);
        }

        for (int i = 0; i < typeInfo.Members.Length; i++)
        {
            var member = typeInfo.Members[i];
            object? memberValue;
            try
            {
                memberValue = member.GetValue(instance);
            }
            catch (Exception ex)
            {
                var failure = ValidationResult.CreateFromValidationFailure(
                    IsValid.ValidatorName,
                    $"Failed to read member '{member.Name}' during validation",
                    member.Name,
                    blackboard,
                    new List<(string key, object? value)>
                    {
                        ("memberName", member.Name),
                        ("exceptionType", ex.GetType().FullName),
                        ("exceptionMessage", ex.Message)
                    });
                builder.AddFailure(member.Name, failure);
                continue;
            }

            for (int j = 0; j < member.Attributes.Length; j++)
            {
                var attr = member.Attributes[j];

                if (attr is ValidateNestedAttribute or ValidateEachIsValidAttribute)
                    continue;

                var result = attr.Validate(memberValue, member.Name, blackboard);
                if (!result.IsValid)
                    AddByAttributeSeverity(builder, member.Name, attr, result);
            }

            if (member.IsNested && memberValue is not null)
            {
                var nestedResult = ValidateInstance(memberValue, blackboard, visited);
                builder.AddFailures(member.Name, nestedResult);
                builder.AddWarnings(member.Name, nestedResult);
            }

            if (member.IsCollection && memberValue is IEnumerable enumerable)
            {
                int index = 0;
                foreach (var item in enumerable)
                {
                    if (item is not null)
                    {
                        var itemResult = ValidateInstance(item, blackboard, visited);
                        var itemPath = $"{member.Name}[{index}]";
                        builder.AddFailures(itemPath, itemResult);
                        builder.AddWarnings(itemPath, itemResult);
                    }
                    index++;
                }
            }
        }

        return builder.Build();
    }

    private static void AddByAttributeSeverity(
        ValidationResult.Builder builder, string memberPath, ValidationAttribute attr, ValidationResult result)
    {
        if (attr.Severity == ValidationSeverity.Warning)
            builder.AddWarning(memberPath, result);
        else
            builder.AddFailure(memberPath, result);
    }
}
