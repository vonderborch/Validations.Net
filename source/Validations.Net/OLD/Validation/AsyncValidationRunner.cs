using System.Collections;
using SimpleBlackboard.Net;
using Validations.Net.OLD.Validators;

namespace Validations.Net.OLD.Validation;

/// <summary>
/// Async counterpart to <see cref="ValidationRunner"/>. Executes all validation attributes on an
/// object instance asynchronously, supporting nested objects and collection items with member path
/// tracking. Attributes that override <see cref="ValidationAttribute.ValidateAsync"/> are awaited;
/// synchronous attributes are wrapped in completed tasks by the base class default.
/// </summary>
internal static class AsyncValidationRunner
{
    /// <summary>
    /// Asynchronously validates all attributes on the given instance, recursing into nested objects
    /// and collection items as directed by structural attributes.
    /// </summary>
    public static async Task<ValidationResult> ValidateAsync(object? instance,
        IBlackboard? blackboard = null, CancellationToken cancellationToken = default)
    {
        if (instance is null)
        {
            var builder = ValidationResult.CreateBuilder();
            var result = ValidationResult.CreateFromValidationFailure(
                "IsValid", "Instance is null", null, blackboard,
                new List<(string key, object? value)> { ("value", null) });
            builder.AddFailure("", result);
            return builder.Build();
        }

        var visited = new HashSet<object>(ReferenceEqualityComparer.Instance);
        return await ValidateInstanceAsync(instance, blackboard, visited, cancellationToken)
            .ConfigureAwait(false);
    }

    private static async Task<ValidationResult> ValidateInstanceAsync(
        object instance, IBlackboard? blackboard, HashSet<object> visited,
        CancellationToken cancellationToken)
    {
        if (!instance.GetType().IsValueType && !visited.Add(instance))
            return ValidationResult.CreateFromValidationSuccess();

        var typeInfo = TypeValidationInfo.GetForType(instance.GetType());
        var builder = ValidationResult.CreateBuilder();

        for (int i = 0; i < typeInfo.ClassAttributes.Length; i++)
        {
            var attr = typeInfo.ClassAttributes[i];
            var result = await attr.ValidateAsync(instance, null, blackboard, cancellationToken)
                .ConfigureAwait(false);
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

                var result = await attr.ValidateAsync(memberValue, member.Name, blackboard, cancellationToken)
                    .ConfigureAwait(false);
                if (!result.IsValid)
                    AddByAttributeSeverity(builder, member.Name, attr, result);
            }

            if (member.IsNested && memberValue is not null)
            {
                var nestedResult = await ValidateInstanceAsync(memberValue, blackboard, visited, cancellationToken)
                    .ConfigureAwait(false);
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
                        var itemResult = await ValidateInstanceAsync(item, blackboard, visited, cancellationToken)
                            .ConfigureAwait(false);
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
