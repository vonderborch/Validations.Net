# Identifiers

Validators for GUID format.

## Validators

### IsValidGuid

**Description:** Validates that a string represents a valid GUID.

**Methods:**
- `CheckIsValidGuid` → `bool`
- `ValidateIsValidGuid` → `ValidationResult`
- `EnsureIsValidGuid` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |

**Matching Attribute:** `[ValidateIsValidGuid]`

---

### IsNotValidGuid

**Description:** Validates that a string does not represent a valid GUID.

**Methods:**
- `CheckIsNotValidGuid` → `bool`
- `ValidateIsNotValidGuid` → `ValidationResult`
- `EnsureIsNotValidGuid` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |

**Matching Attribute:** `[ValidateIsNotValidGuid]`
