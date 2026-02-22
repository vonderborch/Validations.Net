# AgainstPredicate

Validates that a value meets a predicate, either a `Func<T?, bool>` or a registered predicate by name.

## Validators

### AgainstPredicate

**Description:** Tests the value against a specified predicate. Supports inline `Func<T?, bool>` or predicate lookup by name via `PredicateManager`.

**Type constraints:** Any

**Methods:**
- `CheckAgainstPredicate` → `bool` (overloads: `Func<T?, bool> predicate`, `string predicateName, string? predicateGroup, object? predicateInstance`)
- `ValidateAgainstPredicate` → `ValidationResult`
- `EnsureAgainstPredicate` → `T?` (throws on failure)

**Parameters (inline predicate):**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | T? | The value to validate |
| predicate | Func<T?, bool> | The predicate to test against |

**Parameters (registered predicate):**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | T? | The value to validate |
| predicateName | string | The name of the predicate |
| predicateGroup | string? | Optional group for the predicate |
| predicateInstance | object? | Instance context (defaults to value) |

**Example (inline):**
```csharp
// Check
bool result = 32.CheckAgainstPredicate(x => x > 31);

// Validate
var validationResult = 32.ValidateAgainstPredicate(x => x > 31);

// Ensure (throws on failure)
int safe = 32.EnsureAgainstPredicate(x => x > 31);
```

**Example (registered predicate):**
```csharp
public class Mock
{
    [ValidateAgainstPredicate("NameIsJohn")]
    public string Name { get; set; }
    
    [ValidationPredicate("NameIsJohn")]
    public static bool NameIsJohn(string name) => name == "John";
}
```

**Matching Attribute:** `[ValidateAgainstPredicate(predicateName)]` — predicates must be marked with `[ValidationPredicate(name)]` on methods, fields, or properties.
