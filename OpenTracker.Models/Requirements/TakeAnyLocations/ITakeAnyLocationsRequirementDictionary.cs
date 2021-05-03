using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.TakeAnyLocations
{
    /// <summary>
    ///     This interface contains the dictionary container for take any locations requirements.
    /// </summary>
    public interface ITakeAnyLocationsRequirementDictionary : IDictionary<bool, IRequirement>
    {
    }
}