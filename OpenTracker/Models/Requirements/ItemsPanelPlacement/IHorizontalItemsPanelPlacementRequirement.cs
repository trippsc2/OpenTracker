using Avalonia.Controls;

namespace OpenTracker.Models.Requirements.ItemsPanelPlacement
{
    /// <summary>
    ///     This interface contains horizontal items panel placement requirement data.
    /// </summary>
    public interface IHorizontalItemsPanelPlacementRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new horizontal items panel placement requirements.
        /// </summary>
        /// <param name="expectedValue">
        ///     The expected dock value.
        /// </param>
        /// <returns>
        ///     A new horizontal items panel placement requirement.
        /// </returns>
        delegate IHorizontalItemsPanelPlacementRequirement Factory(Dock expectedValue);
    }
}