# Geography

Validators for latitude and longitude coordinates.

## Validators

### IsValidLatitude

**Description:** Validates that a value is a valid latitude (-90 to 90).

**Target types:** `double`

**Methods:**
- `CheckIsValidLatitude` → `bool`
- `ValidateIsValidLatitude` → `ValidationResult`
- `EnsureIsValidLatitude` → `double` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | double | The latitude to validate |

**Matching Attribute:** `[ValidateIsValidLatitude]`

---

### IsValidLongitude

**Description:** Validates that a value is a valid longitude (-180 to 180).

**Target types:** `double`

**Methods:**
- `CheckIsValidLongitude` → `bool`
- `ValidateIsValidLongitude` → `ValidationResult`
- `EnsureIsValidLongitude` → `double` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | double | The longitude to validate |

**Matching Attribute:** `[ValidateIsValidLongitude]`
