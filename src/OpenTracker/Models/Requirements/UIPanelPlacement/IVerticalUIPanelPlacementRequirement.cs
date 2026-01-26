using Avalonia.Controls;

namespace OpenTracker.Models.Requirements.UIPanelPlacement;

/// <summary>
///     This interface contains vertical UI panel placement requirement data.
/// </summary>
public interface IVerticalUIPanelPlacementRequirement : IRequirement
{
    /// <summary>
    ///     A factory for creating new vertical UI panel placement requirements.
    /// </summary>
    /// <param name="expectedValue">
    ///     The expected dock value.
    /// </param>
    /// <returns>
    ///     A new UI panel placement requirement.
    /// </returns>
    delegate IVerticalUIPanelPlacementRequirement Factory(Dock expectedValue);
}