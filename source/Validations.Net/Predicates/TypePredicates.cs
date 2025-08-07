

public record struct TypePredicatesInfo
{
    public Type Type { get; }

    public IReadOnlyList<PredicateInfo> Predicates { get; }

    public TypePredicatesInfo(Type type)
    {
        this.Type = type;
        this.Predicates = GetPredicates(type);
    }

    private List<PredicateInfo> GetPredicates(Type type)
    {
        List<PredicateInfo> output = new();
        IEnumerable<(FieldInfo field, MethodInfo method, PredicateRegistrationAttribute attribute)> fieldsToAdd =
            GetValidFieldsInType(type);
    }
}