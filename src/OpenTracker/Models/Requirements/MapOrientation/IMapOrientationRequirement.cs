using Avalonia.Layout;

namespace OpenTracker.Models.Requirements.MapOrientation
{
    /// <summary>
    ///     This interface contains map orientation setting requirement data.
    /// </summary>
    public interface IMapOrientationRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new map orientation requirements.
        /// </summary>
        /// <param name="expectedValue">
        ///     The expected orientation value.
        /// </param>
        /// <returns>
        ///     A new map orientation requirement.
        /// </returns>
        delegate IMapOrientationRequirement Factory(Orientation? expectedValue);
    }
}