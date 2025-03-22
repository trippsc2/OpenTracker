using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.TakeAnyLocations
{
    /// <summary>
    /// This interface contains the <see cref="IMode.TakeAnyLocations"/> <see cref="IRequirement"/> data.
    /// </summary>
    public interface ITakeAnyLocationsRequirement : IRequirement
    {
        /// <summary>
        /// A factory for creating new <see cref="ITakeAnyLocationsRequirement"/> objects.
        /// </summary>
        /// <param name="expectedValue">
        ///     A <see cref="bool"/> expected <see cref="IMode.TakeAnyLocations"/> value.
        /// </param>
        /// <returns>
        ///     A new <see cref="ITakeAnyLocationsRequirement"/> object.
        /// </returns>
        delegate ITakeAnyLocationsRequirement Factory(bool expectedValue);
    }
}