# Phone

Validators for phone number format.

## Validators

### IsValidPhoneNumber

**Description:** Validates that a string represents a valid phone number. Strips spaces, dashes, parens. Must start with optional '+', remaining all digits, length 7–15 after stripping.

**Methods:**
- `CheckIsValidPhoneNumber` → `bool`
- `ValidateIsValidPhoneNumber` → `ValidationResult`
- `EnsureIsValidPhoneNumber` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |

**Matching Attribute:** `[ValidateIsValidPhoneNumber]`
