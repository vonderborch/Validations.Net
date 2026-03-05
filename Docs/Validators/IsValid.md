# IsValid

Validates that an object instance passes all `ValidationAttributes` declared on its type (class-level, property-level, and field-level), including nested and collection validation.

## Validators

### IsValid

**Description:** Checks whether the given instance passes all of its ValidationAttributes. Treats null as invalid (`CheckIsValid` returns `false` for null values).

**Type constraints:** Any class or struct

**Methods:**
- `CheckIsValid` → `bool`
- `ValidateIsValid` → `AggregateValidationResult` (contains all failures)
- `EnsureIsValid` → `T?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | T? | The instance to validate |
| blackboard | IBlackboard? | Optional blackboard for additional context |
| validationFailureMessage | string | Custom failure message (Ensure only) |
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

Person person = new() { Name = "John", Age = 19 };

// Check
bool result = person.CheckIsValid();

// Validate (returns AggregateValidationResult with all failures)
var aggregateResult = person.ValidateIsValid();

// Ensure (throws on failure)
var safe = person.EnsureIsValid();
```

**ValidationAttributes:** n/a (IsValid validates against attributes on the type)
