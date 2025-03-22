using Avalonia.Layout;

namespace OpenTracker.Models.Requirements.ItemsPanelOrientation
{
    /// <summary>
    ///     This interface contains items panel orientation requirement data.
    /// </summary>
    public interface IItemsPanelOrientationRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new items panel orientation requirements.
        /// </summary>
        /// <param name="expectedValue">
        ///     The expected orientation value.
        /// </param>
        /// <returns>
        ///     A new items panel orientation requirements.
        /// </returns>
        delegate IItemsPanelOrientationRequirement Factory(Orientation expectedValue);
    }
}