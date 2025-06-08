using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class RegisterAgainstPredicateValidationFunctionAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}

public class ValidateAgainstPredicateAttribute<T> : ValidationAttribute
{
    public ValidateAgainstPredicateAttribute(string validationFunctionRegistrationName) : base("AgainstPredicate")
    {
        validationFunctionRegistrationName.ValidateIsNotNull(nameof(validationFunctionRegistrationName));
        
        // Find a method/delegate with the specified name in all loaded assemblies
        var method = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .SelectMany(type => type.GetMethods())
            .FirstOrDefault(m => m.GetCustomAttributes(typeof(RegisterAgainstPredicateValidationFunctionAttribute), false)
                .Cast<RegisterAgainstPredicateValidationFunctionAttribute>()
                .Any(attr => attr.Name == validationFunctionRegistrationName));

        method.ValidateIsNotNull(nameof(method));
        Predicate = method!.CreateDelegate<Func<T?, bool>>();
    }
    
    public Func<T?, bool> Predicate { get; }
    
    public override bool Check(object? value)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        return typedValue.CheckAgainstPredicate(Predicate);
    }

    public override void Validate(object? value, string propertyName)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        typedValue.ValidateAgainstPredicate(Predicate, propertyName);
    }
}
