import re

def update_validator_with_params(filepath):
    """Update validators that have params arrays in their signatures"""
    
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()
    
    # Pattern 1: Validate methods with params at the end
    # public static ValidationResult ValidateSomething(..., IBlackboard? blackboard = null,
    #     [CallerArgumentExpression(nameof(value))] string? parameterName = null, params Type[] items)
    pattern1 = r'(public static ValidationResult Validate\w+\([^)]+IBlackboard\? blackboard = null,)\s+(\[CallerArgumentExpression[^\]]+\][^,]+,)\s+(params )'
    replacement1 = r'\1\n        string validationFailureMessage = DefaultValidationFailureMessage,\n        \2\n        \3'
    content = re.sub(pattern1, replacement1, content)
    
    # Pattern 2: Ensure methods with params at the end
    pattern2 = r'(public static [^{]+Ensure\w+\([^)]+IBlackboard\? blackboard = null,)\s+(\[CallerArgumentExpression[^\]]+\][^,]+,)\s+(params )'
    replacement2 = r'\1\n        string validationFailureMessage = DefaultValidationFailureMessage,\n        \2\n        \3'
    content = re.sub(pattern2, replacement2, content)
    
    # Pattern 3: Update Validate calls in Ensure methods
    # Handle calls with params: .ValidateSomething(blackboard, parameterName, items)
    content = re.sub(r'(\.Validate\w+\(blackboard,) parameterName,', r'\1 validationFailureMessage, parameterName,', content)
    
    # Pattern 4: Replace hardcoded messages in ValidationResult.CreateFromValidationFailure
    # Match the entire CreateFromValidationFailure call and replace the message parameter
    pattern4 = r'(ValidationResult\.CreateFromValidationFailure\(\s*ValidatorName,\s*)("[^"]+"|[^\n,]+),(\s*parameterName)'
    replacement4 = r'\1validationFailureMessage,\3'
    content = re.sub(pattern4, replacement4, content)
    
    with open(filepath, 'w', encoding='utf-8', newline='') as f:
        f.write(content)

# Update the two complex validators
print("Updating DoesContainAny.cs...")
update_validator_with_params("source/Validations.Net/Validators/DoesContainAny.cs")
print("[OK] Updated DoesContainAny.cs")

print("Updating DoesNotContainAny.cs...")
update_validator_with_params("source/Validations.Net/Validators/DoesNotContainAny.cs")
print("[OK] Updated DoesNotContainAny.cs")

print("\n[OK] Complex validators updated!")





