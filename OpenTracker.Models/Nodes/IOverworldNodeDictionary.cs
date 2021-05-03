using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Nodes
{
    /// <summary>
    /// This interface contains the dictionary container for requirement node data.
    /// </summary>
    public interface IOverworldNodeDictionary : IDictionary<OverworldNodeID, INode>
    {
        event EventHandler<KeyValuePair<OverworldNodeID, INode>>? ItemCreated;
    }
}