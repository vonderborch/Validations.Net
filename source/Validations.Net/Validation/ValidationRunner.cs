using System.Collections;
using SimpleBlackboard.Net;

namespace Validations.Net.Validation;

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
    public static AggregateValidationResult Validate(object? instance, IBlackboard? blackboard = null)
    {
        if (instance is null)
        {
            var builder = AggregateValidationResult.CreateBuilder();
            var result = ValidationResult.CreateFromValidationFailure(
                "IsValid", "Instance is null", null, blackboard, new List<(string key, object? value)> { ("value", null) });
            builder.AddFailure("", result);
            return builder.Build();
        }

        var visited = new HashSet<object>(ReferenceEqualityComparer.Instance);
        return ValidateInstance(instance, blackboard, visited);
    }

    private static AggregateValidationResult ValidateInstance(
        object instance, IBlackboard? blackboard, HashSet<object> visited)
    {
        if (!instance.GetType().IsValueType && !visited.Add(instance))
            return AggregateValidationResult.Success;

        var typeInfo = TypeValidationInfo.GetForType(instance.GetType());
        var builder = AggregateValidationResult.CreateBuilder();

        // Run class-level attributes
        for (int i = 0; i < typeInfo.ClassAttributes.Length; i++)
        {
            var attr = typeInfo.ClassAttributes[i];
            var result = attr.Validate(instance, null, blackboard);
            if (!result.IsValid)
                builder.AddFailure("", result);
        }

        // Run member-level attributes
        for (int i = 0; i < typeInfo.Members.Length; i++)
        {
            var member = typeInfo.Members[i];
            object? memberValue;
            try
            {
                memberValue = member.GetValue(instance);
            }
            catch
            {
                continue;
            }

            // Run each validation attribute on this member
            for (int j = 0; j < member.Attributes.Length; j++)
            {
                var attr = member.Attributes[j];

                // Skip structural attributes - they don't validate the value directly
                if (attr.GetType().Name is "ValidateNestedAttribute" or "ValidateEachIsValidAttribute")
                    continue;

                var result = attr.Validate(memberValue, member.Name, blackboard);
                if (!result.IsValid)
                    builder.AddFailure(member.Name, result);
            }

            // Handle nested object validation
            if (member.IsNested && memberValue is not null)
            {
                var nestedResult = ValidateInstance(memberValue, blackboard, visited);
                if (!nestedResult.IsValid)
                    builder.AddFailures(member.Name, nestedResult);
            }

            // Handle collection item validation
            if (member.IsCollection && memberValue is IEnumerable enumerable)
            {
                int index = 0;
                foreach (var item in enumerable)
                {
                    if (item is not null)
                    {
                        var itemResult = ValidateInstance(item, blackboard, visited);
                        if (!itemResult.IsValid)
                            builder.AddFailures($"{member.Name}[{index}]", itemResult);
                    }
                    index++;
                }
            }
        }

        return builder.Build();
    }
}
