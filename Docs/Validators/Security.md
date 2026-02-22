# Security

Validators for password strength.

## Validators

### IsSecurePassword

**Description:** Validates that a password meets security requirements: minimum length, uppercase, lowercase, digit, and special character (configurable).

**Methods:**
- `CheckIsSecurePassword` → `bool`
- `ValidateIsSecurePassword` → `ValidationResult`
- `EnsureIsSecurePassword` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The password to validate |
| minLength | int | Minimum length (default: 8) |
| requireUppercase | bool | Require at least one uppercase letter (default: true) |
| requireLowercase | bool | Require at least one lowercase letter (default: true) |
| requireDigit | bool | Require at least one digit (default: true) |
| requireSpecialChar | bool | Require at least one special character (default: true) |

**Matching Attribute:** `[ValidateIsSecurePassword]`
