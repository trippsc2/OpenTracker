namespace OpenTracker.Models.Requirements.UIScale;

/// <summary>
///     This interface contains UI scale requirement data.
/// </summary>
public interface IUIScaleRequirement : IRequirement
{
    /// <summary>
    ///     A factory for creating new UI scale requirements.
    /// </summary>
    /// <param name="expectedValue">
    ///     The expected dock value.
    /// </param>
    /// <returns>
    ///     A new UI scale requirement.
    /// </returns>
    delegate IUIScaleRequirement Factory(double expectedValue);
}