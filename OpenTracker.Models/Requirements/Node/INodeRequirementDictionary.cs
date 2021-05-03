using System.Collections.Generic;
using OpenTracker.Models.Nodes;

namespace OpenTracker.Models.Requirements.Node
{
    /// <summary>
    ///     This interface contains the dictionary container for node requirements.
    /// </summary>
    public interface INodeRequirementDictionary : IDictionary<OverworldNodeID, IRequirement>
    {
    }
}