using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Nodes;

/// <summary>
/// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="INode"/> objects
/// indexed by <see cref="OverworldNodeID"/>.
/// </summary>
public interface IOverworldNodeDictionary : IDictionary<OverworldNodeID, INode>
{
    /// <summary>
    /// An event indicating that a new <see cref="INode"/> object was created.
    /// </summary>
    event EventHandler<KeyValuePair<OverworldNodeID, INode>>? ItemCreated;
}