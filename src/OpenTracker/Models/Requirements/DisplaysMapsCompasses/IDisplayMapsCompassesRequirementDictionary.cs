using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.DisplaysMapsCompasses;

/// <summary>
///     This interface contains the dictionary container for display maps and compasses requirements.
/// </summary>
public interface IDisplayMapsCompassesRequirementDictionary : IDictionary<bool, IRequirement>
{
}