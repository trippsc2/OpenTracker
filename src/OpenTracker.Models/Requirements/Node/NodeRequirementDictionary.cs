using System.Collections.Generic;
using OpenTracker.Models.Nodes;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.Node;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="NodeRequirement"/>
/// objects indexed by <see cref="OverworldNodeID"/>.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class NodeRequirementDictionary : LazyDictionary<OverworldNodeID, IRequirement>, INodeRequirementDictionary
{
    private readonly IOverworldNodeDictionary _overworldNodes;
        
    private readonly NodeRequirement.Factory _factory;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="overworldNodes">
    ///     The <see cref="IOverworldNodeDictionary"/>.
    /// </param>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="NodeRequirement"/> objects.
    /// </param>
    public NodeRequirementDictionary(IOverworldNodeDictionary overworldNodes, NodeRequirement.Factory factory)
        : base(new Dictionary<OverworldNodeID, IRequirement>())
    {
        _overworldNodes = overworldNodes;
            
        _factory = factory;
    }

    protected override IRequirement Create(OverworldNodeID key)
    {
        return _factory(_overworldNodes[key]);
    }
}