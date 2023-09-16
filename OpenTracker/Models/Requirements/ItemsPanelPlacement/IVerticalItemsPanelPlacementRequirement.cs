using Avalonia.Controls;

namespace OpenTracker.Models.Requirements.ItemsPanelPlacement;

/// <summary>
///     This interface contains vertical items panel placement requirement data.
/// </summary>
public interface IVerticalItemsPanelPlacementRequirement : IRequirement
{
    /// <summary>
    ///     A factory for creating new vertical items panel placement requirements.
    /// </summary>
    /// <param name="expectedValue">
    ///     The expected dock value.
    /// </param>
    /// <returns>
    ///     A new vertical items panel placement requirement.
    /// </returns>
    delegate IVerticalItemsPanelPlacementRequirement Factory(Dock expectedValue);
}