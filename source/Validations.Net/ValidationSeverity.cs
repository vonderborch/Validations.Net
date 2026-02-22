namespace Validations.Net;

/// <summary>
/// Indicates how a validation failure should be treated.
/// </summary>
public enum ValidationSeverity
{
    /// <summary>
    /// The failure is a blocking error. The overall result will be marked as invalid.
    /// </summary>
    Error,

    /// <summary>
    /// The failure is a non-blocking warning. The overall result remains valid,
    /// but the warning is still accessible for inspection.
    /// </summary>
    Warning
}
