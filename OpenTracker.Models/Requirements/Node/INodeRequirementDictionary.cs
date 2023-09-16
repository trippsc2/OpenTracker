using System.Collections.Generic;
using OpenTracker.Models.Nodes;

namespace OpenTracker.Models.Requirements.Node;

/// <summary>
/// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="INodeRequirement"/>
/// objects indexed by <see cref="OverworldNodeID"/>.
/// </summary>
public interface INodeRequirementDictionary : IDictionary<OverworldNodeID, IRequirement>
{
}