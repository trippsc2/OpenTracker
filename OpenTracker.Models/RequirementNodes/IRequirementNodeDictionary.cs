using System;
using System.Collections.Generic;

namespace OpenTracker.Models.RequirementNodes
{
    /// <summary>
    /// This interface contains the dictionary container for requirement node data.
    /// </summary>
    public interface IRequirementNodeDictionary : IDictionary<RequirementNodeID, IRequirementNode>
    {
        event EventHandler<KeyValuePair<RequirementNodeID, IRequirementNode>>? ItemCreated;
    }
}