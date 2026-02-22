# Identifiers

Validators for GUID format and general identifiers.

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

---

### IsValidId

**Description:** Validates that a string represents a valid identifier with configurable format constraints (length and character pattern).

**Target types:** `string?`

**Methods:**
- `CheckIsValidId` → `bool`
- `ValidateIsValidId` → `ValidationResult`
- `EnsureIsValidId` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |
| minLength | int | Minimum length (default: 1) |
| maxLength | int | Maximum length (default: 255) |
| allowedPattern | string? | Regex pattern for allowed characters (default: `^[a-zA-Z0-9_-]+$`) |

**Matching Attribute:** `[ValidateIsValidId(minLength, maxLength, allowedPattern)]`
