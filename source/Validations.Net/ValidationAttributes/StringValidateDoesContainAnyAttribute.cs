using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateDoesContainAnyAttribute : ValidationAttribute
{
    public ICollection<string>? Substrings { get; init; } = null;

    public ICollection<char>? Characters { get; init; } = null;
    
    public StringComparison Comparison { get; }

    public ValidateDoesContainAnyAttribute(StringComparison comparison, params string[] subStrings) : base("DoesContainAny")
    {
        Comparison = comparison;
        Substrings = subStrings;
    }

    public ValidateDoesContainAnyAttribute(StringComparison comparison, params char[] characters) : base("DoesContainAny")
    {
        Comparison = comparison;
        Characters = characters;
    }


    public override bool Check(object? value)
    {
        string str = GetCorrectType<string>(value, nameof(value));
        if (Characters is null)
        {
            return str.CheckDoesContainAny(Substrings!, Comparison);
        }
        else
        {
            return str.CheckDoesContainAny(Characters, Comparison);
        }
    }

    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        string str = GetCorrectType<string>(value, nameof(propertyName));
        if (Characters is null)
        { 
            str.ValidateDoesContainAny(Substrings!, propertyName, Comparison, blackboard);
        }
        else
        { 
            str.ValidateDoesContainAny(Characters, propertyName, Comparison, blackboard);
        }
    }
}
