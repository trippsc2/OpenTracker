using System;
using System.Collections.Generic;

namespace OpenTracker.Models.RequirementNodes
{
    public interface IRequirementNodeDictionary : IDictionary<RequirementNodeID, IRequirementNode>,
        ICollection<KeyValuePair<RequirementNodeID, IRequirementNode>>
    {
        event EventHandler<KeyValuePair<RequirementNodeID, IRequirementNode>>? ItemCreated;
    }
}