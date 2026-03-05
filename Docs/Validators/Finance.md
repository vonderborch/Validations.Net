# Finance

Validators for IBAN and BIC/SWIFT code formats.

## Validators

### IsValidIban

**Description:** Validates that a string represents a valid IBAN format. Basic format: strip spaces, length 15–34, first 2 letters (country), next 2 digits (check), remainder alphanumeric.

**Methods:**
- `CheckIsValidIban` → `bool`
- `ValidateIsValidIban` → `ValidationResult`
- `EnsureIsValidIban` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |

**Matching Attribute:** `[ValidateIsValidIban]`

---

### IsValidBicSwift

**Description:** Validates that a string represents a valid BIC/SWIFT code. Format: 8 or 11 chars, first 4 letters, next 2 letters (country), next 2 alphanumeric (location), optional 3 alphanumeric (branch).

**Methods:**
- `CheckIsValidBicSwift` → `bool`
- `ValidateIsValidBicSwift` → `ValidationResult`
- `EnsureIsValidBicSwift` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |

**Matching Attribute:** `[ValidateIsValidBicSwift]`
