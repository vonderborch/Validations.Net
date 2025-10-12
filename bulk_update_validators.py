import re
import os

# Configuration for each validator
validators_config = {
    "IsNotZero.cs": "Parameter must not be zero",
    "IsEven.cs": "Parameter must be even",
    "IsOdd.cs": "Parameter must be odd",
    "IsPositive.cs": "Parameter must be positive",
    "IsNegative.cs": "Parameter must be negative",
    "IsDivisibleBy.cs": "Parameter must be divisible by the specified value",
    "IsNotDivisibleBy.cs": "Parameter must not be divisible by the specified value",
    "IsInRange.cs": "Parameter must be within the specified range",
    "IsNotInRange.cs": "Parameter must not be within the specified range",
    "IsPowerOf.cs": "Parameter must be a power of the specified value",
    "IsNotPowerOf.cs": "Parameter must not be a power of the specified value",
    "IsPerfectSquare.cs": "Parameter must be a perfect square",
    "IsNotPerfectSquare.cs": "Parameter must not be a perfect square",
    "IsOfType.cs": "Parameter must be of the specified type",
    "IsNotOfType.cs": "Parameter must not be of the specified type",
    "IsAssignableTo.cs": "Parameter must be assignable to the specified type",
    "IsNotAssignableTo.cs": "Parameter must not be assignable to the specified type",
    "IsInterfaceOf.cs": "Parameter must implement the specified interface",
    "IsLength.cs": "Parameter must have the specified length",
    "IsNotLength.cs": "Parameter must not have the specified length",
    "IsSorted.cs": "Parameter must be sorted",
    "IsNotSorted.cs": "Parameter must not be sorted",
    "DoesContainAll.cs": "Parameter must contain all specified items",
    "DoesContainAny.cs": "Parameter must contain any of the specified items",
    "DoesNotContain.cs": "Parameter must not contain the specified item",
    "DoesNotContainAll.cs": "Parameter must not contain all specified items",
    "DoesNotContainAny.cs": "Parameter must not contain any of the specified items",
}

base_path = "source/Validations.Net/Validators"

for filename, message in validators_config.items():
    filepath = os.path.join(base_path, filename)
    validator_name = filename.replace('.cs', '')
    
    if not os.path.exists(filepath):
        print(f"[WARNING] Skipping {filename} - file not found")
        continue
    
    print(f"Processing {filename}...")
    
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()
    
    # Skip if already has DefaultValidationFailureMessage
    if 'DefaultValidationFailureMessage' in content:
        print(f"  [OK] Already has DefaultValidationFailureMessage")
        continue
    
    # Step 1: Replace private const with public const and add DefaultValidationFailureMessage
    old_constant = f"    private const string ValidatorName = nameof({validator_name});"
    new_constant = f'''    /// <summary>
    /// Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "{validator_name}";

    /// <summary>
    /// Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "{message}";'''
    
    if old_constant in content:
        content = content.replace(old_constant, new_constant)
    elif f'    private const string ValidatorName = "{validator_name}";' in content:
        content = content.replace(f'    private const string ValidatorName = "{validator_name}";', new_constant)
    
    # Step 2: Update all Validate method signatures
    # Find and replace Validate method signatures that don't have validationFailureMessage
    pattern = r'(public static ValidationResult Validate\w+[^{]+IBlackboard\? blackboard = null,)\s+(\[CallerArgumentExpression)'
    replacement = r'\1\n        string validationFailureMessage = DefaultValidationFailureMessage,\n        \2'
    content = re.sub(pattern, replacement, content)
    
    # Step 3: Update all Ensure method signatures 
    pattern = r'(public static [^{]+Ensure\w+[^{]+IBlackboard\? blackboard = null,)\s+(\[CallerArgumentExpression)'
    replacement = r'\1\n        string validationFailureMessage = DefaultValidationFailureMessage,\n        \2'
    content = re.sub(pattern, replacement, content)
    
    # Step 4: Update Validate calls in Ensure methods to pass validationFailureMessage
    # Pattern: .ValidateSomething(params, blackboard, parameterName)
    content = re.sub(r'(\.Validate\w+\([^)]*blackboard,)\s+parameterName\)', r'\1 validationFailureMessage, parameterName)', content)
    content = re.sub(r'(= Validate\w+\([^)]*blackboard,)\s+parameterName\)', r'\1 validationFailureMessage, parameterName)', content)
    
    # Step 5: Replace hardcoded messages in CreateFromValidationFailure with validationFailureMessage
    # This is tricky because we need to preserve other parameters
    pattern = r'(ValidationResult\.CreateFromValidationFailure\(\s*ValidatorName,\s*)(\$?"[^"]*"[^,]*|[^,\n]+),(\s*parameterName)'
    replacement = r'\1validationFailureMessage,\3'
    content = re.sub(pattern, replacement, content)
    
    with open(filepath, 'w', encoding='utf-8', newline='') as f:
        f.write(content)
    
    print(f"  [OK] Updated {filename}")

print("\n[OK] All validators updated!")

