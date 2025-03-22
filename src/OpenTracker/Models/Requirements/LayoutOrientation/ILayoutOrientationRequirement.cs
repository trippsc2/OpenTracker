using Avalonia.Layout;

namespace OpenTracker.Models.Requirements.LayoutOrientation
{
    /// <summary>
    ///     This interface contains layout orientation setting requirement data.
    /// </summary>
    public interface ILayoutOrientationRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new layout orientation requirements.
        /// </summary>
        /// <param name="expectedValue">
        ///     The expected orientation value.
        /// </param>
        /// <returns>
        ///     A new layout orientation requirement.
        /// </returns>
        delegate ILayoutOrientationRequirement Factory(Orientation? expectedValue);
    }
}