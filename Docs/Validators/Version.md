# Version

Validators for semantic version format.

## Validators

### IsValidSemanticVersion

**Description:** Validates that a string represents a valid semantic version (major.minor.patch with optional -pre.release and +build.metadata).

**Methods:**
- `CheckIsValidSemanticVersion` → `bool`
- `ValidateIsValidSemanticVersion` → `ValidationResult`
- `EnsureIsValidSemanticVersion` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |

**Matching Attribute:** `[ValidateIsValidSemanticVersion]`
