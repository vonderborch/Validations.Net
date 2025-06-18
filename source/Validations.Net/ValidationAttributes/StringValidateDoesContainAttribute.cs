using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateDoesContainAttribute : ValidationAttribute
{
    public string? SubString { get; init; } = null;

    public char? Character { get; init; } = null;

    public int StartIndex { get; } = 0;

    public int? Count { get; init; } = null;
    
    public StringComparison Comparison { get; }

    private string mode = "";
    
    public ValidateDoesContainAttribute(string subString, int startIndex, int? count = null,
        StringComparison comparison = StringComparison.Ordinal) : base("DoesContain")
    {
        SubString = subString;
        StartIndex = startIndex;
        Count = count;
        Comparison = comparison;
        mode = "SubstringString";
    }

    public ValidateDoesContainAttribute(string subString, StringComparison comparison = StringComparison.Ordinal) : base("DoesContain")
    {
        SubString = subString;
        Comparison = comparison;
        mode = "String";
    }
    
    public ValidateDoesContainAttribute(char character, int startIndex, int? count = null,
        StringComparison comparison = StringComparison.Ordinal) : base("DoesContain")
    {
        Character = character;
        StartIndex = startIndex;
        Count = count;
        Comparison = comparison;
        mode = "SubstringCharacter";
    }

    public ValidateDoesContainAttribute(char character, StringComparison comparison = StringComparison.Ordinal) : base("DoesContain")
    {
        Character = character;
        Comparison = comparison;
        mode = "Character";
    }

    public override bool Check(object? value)
    {
        string str = GetCorrectType<string>(value, nameof(value));
        switch (mode)
        {
            case "Character":
                return str.CheckDoesContain(Character.Value, Comparison);
            case "SubstringCharacter":
                return str.CheckDoesContain(Character.Value, StartIndex, Count, Comparison);
            case "String":
                return str.CheckDoesContain(SubString, Comparison);
            case "SubstringString":
                return str.CheckDoesContain(SubString, StartIndex, Count, Comparison);
            default:
                throw new NotImplementedException();
        }
    }

    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        string str = GetCorrectType<string>(value, nameof(value));
        switch (mode)
        {
            case "Character": 
                str.ValidateDoesContain(Character.Value, propertyName, Comparison, blackboard);
                break;
            case "SubstringCharacter": 
                str.ValidateDoesContain(Character.Value, propertyName, StartIndex, Count, Comparison, blackboard);
                break;
            case "String": 
                str.ValidateDoesContain(SubString, propertyName, Comparison, blackboard);
                break;
            case "SubstringString": 
                str.ValidateDoesContain(SubString, propertyName, StartIndex, Count, Comparison, blackboard);
                break;
            default:
                throw new NotImplementedException();
        }
    }
}
