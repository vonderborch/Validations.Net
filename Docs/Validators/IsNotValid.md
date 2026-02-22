# IsNotValid

Validates that an object instance fails at least one `ValidationAttribute` declared on its type. The logical inverse of IsValid.

## Validators

### IsNotValid

**Description:** Checks whether the given instance fails at least one of its ValidationAttributes. Null returns valid (considered "not valid" in the sense of passing all validations).

**Type constraints:** Any class or struct

**Methods:**
- `CheckIsNotValid` → `bool`
- `ValidateIsNotValid` → `ValidationResult` (success when at least one attribute fails; failure when all pass)
- `EnsureIsNotValid` → `T?` (throws when all validations pass)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | T? | The instance to validate |
| blackboard | IBlackboard? | Optional blackboard for additional context |
| validationFailureMessage | string | Custom failure message (Validate/Ensure) |
| parameterName | string? | Caller argument expression (auto-captured) |

**Example:**
```csharp
[ValidateIsNotNull]
public class Person
{
    [ValidateIsEquals("John")]
    public string Name { get; set; }
    
    [ValidateIsGreaterThan(18)]
    public int Age { get; set; }
}

Person person = new() { Name = "Jane", Age = 17 }; // Fails Name and Age

// Check
bool result = person.CheckIsNotValid(); // true if at least one validation fails

// Validate
var validationResult = person.ValidateIsNotValid();

// Ensure (throws when all validations pass)
var safe = person.EnsureIsNotValid();
```

**ValidationAttributes:** n/a (IsNotValid validates against attributes on the type)
