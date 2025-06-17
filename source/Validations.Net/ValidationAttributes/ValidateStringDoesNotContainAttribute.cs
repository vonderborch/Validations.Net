using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateDoesNotContainAttribute : ValidationAttribute
{
    public string? SubString { get; init; } = null;

    public char? Character { get; init; } = null;

    public int StartIndex { get; } = 0;

    public int? Count { get; init; } = null;
    
    public StringComparison Comparison { get; }

    private string mode = "";
    
    public ValidateDoesNotContainAttribute(string subString, int startIndex, int? count = null,
        StringComparison comparison = StringComparison.Ordinal) : base("DoesNotContain")
    {
        SubString = subString;
        StartIndex = startIndex;
        Count = count;
        Comparison = comparison;
        mode = "SubstringString";
    }

    public ValidateDoesNotContainAttribute(string subString, StringComparison comparison = StringComparison.Ordinal) : base("DoesNotContain")
    {
        SubString = subString;
        Comparison = comparison;
        mode = "String";
    }
    
    public ValidateDoesNotContainAttribute(char character, int startIndex, int? count = null,
        StringComparison comparison = StringComparison.Ordinal) : base("DoesNotContain")
    {
        Character = character;
        StartIndex = startIndex;
        Count = count;
        Comparison = comparison;
        mode = "SubstringCharacter";
    }

    public ValidateDoesNotContainAttribute(char character, StringComparison comparison = StringComparison.Ordinal) : base("DoesNotContain")
    {
        Character = character;
        Comparison = comparison;
        mode = "Character";
    }

    public override bool Check(object? value)
    {
        string obj = GetCorrectType<string>(value, nameof(value));
        switch (mode)
        {
            case "Character":
                return obj.CheckDoesNotContain(Character.Value, Comparison);
            case "SubstringCharacter":
                return obj.CheckDoesNotContain(Character.Value, StartIndex, Count, Comparison);
            case "String":
                return obj.CheckDoesNotContain(SubString, Comparison);
            case "SubstringString":
                return obj.CheckDoesNotContain(SubString, StartIndex, Count, Comparison);
            default:
                throw new NotImplementedException();
        }
    }

    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        string obj = GetCorrectType<string>(value, nameof(value));
        switch (mode)
        {
            case "Character": 
                obj.ValidateDoesNotContain(Character.Value, propertyName, Comparison, blackboard);
                break;
            case "SubstringCharacter": 
                obj.ValidateDoesNotContain(Character.Value, propertyName, StartIndex, Count, Comparison, blackboard);
                break;
            case "String": 
                obj.ValidateDoesNotContain(SubString, propertyName, Comparison, blackboard);
                break;
            case "SubstringString": 
                obj.ValidateDoesNotContain(SubString, propertyName, StartIndex, Count, Comparison, blackboard);
                break;
            default:
                throw new NotImplementedException();
        }
    }
}
