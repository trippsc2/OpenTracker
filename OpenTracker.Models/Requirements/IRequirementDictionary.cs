using System.Collections.Generic;

namespace OpenTracker.Models.Requirements
{
    public interface IRequirementDictionary : IDictionary<RequirementType, IRequirement>,
        ICollection<KeyValuePair<RequirementType, IRequirement>>
    {
    }
}