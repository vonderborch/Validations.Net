using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateDoesNotContainAttribute<T> : ValidationAttribute
{
    public T? Item { get; init; } = default;

    public Func<T, bool>? Predicate { get; init; } = null;
    
    public ValidateDoesNotContainAttribute(T item) : base("DoesNotContain")
    {
        Item = item;
    }

    public override bool Check(object? value)
    {
        ICollection<T> collection = GetCorrectType<ICollection<T>>(value, nameof(value));
        return Predicate is null ? collection.CheckDoesNotContain(Item) : collection.CheckDoesNotContain(Predicate);
    }

    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        ICollection<T> collection = GetCorrectType<ICollection<T>>(value, nameof(value));
        if (Predicate is null)
        {
            collection.ValidateDoesNotContain(Item, propertyName, blackboard);
        }
        else
        {
            collection.ValidateDoesNotContain(Predicate, propertyName, blackboard);
        }
    }
}
