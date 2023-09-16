using Avalonia.Controls;

namespace OpenTracker.Models.Requirements.UIPanelPlacement;

/// <summary>
///     This interface contains horizontal UI panel placement requirement data.
/// </summary>
public interface IHorizontalUIPanelPlacementRequirement : IRequirement
{
    /// <summary>
    ///     A factory for creating new horizontal UI panel placement requirements.
    /// </summary>
    /// <param name="expectedValue">
    ///     The expected dock value.
    /// </param>
    /// <returns>
    ///     A new horizontal UI panel placement requirement.
    /// </returns>
    delegate IHorizontalUIPanelPlacementRequirement Factory(Dock expectedValue);
}