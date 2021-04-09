using System.Collections.Generic;
using OpenTracker.Models.Accessibility;

namespace OpenTracker.Models.Requirements.Static
{
    /// <summary>
    ///     This interface contains the dictionary container for static requirements.
    /// </summary>
    public interface IStaticRequirementDictionary : IDictionary<AccessibilityLevel, IRequirement>
    {
    }
}