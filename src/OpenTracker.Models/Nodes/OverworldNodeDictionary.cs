using System;
using System.Collections.Generic;
using OpenTracker.Models.Nodes.Factories;
using OpenTracker.Utils;

namespace OpenTracker.Models.Nodes;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="INode"/> objects
/// indexed by <see cref="OverworldNodeID"/>.
/// </summary>
public class OverworldNodeDictionary : LazyDictionary<OverworldNodeID, INode>,
    IOverworldNodeDictionary
{
    private readonly Lazy<IOverworldNodeFactory> _factory;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory">
    ///     An Autofac factory for creating the <see cref="IOverworldNodeFactory"/> object.
    /// </param>
    public OverworldNodeDictionary(IOverworldNodeFactory.Factory factory)
        : base(new Dictionary<OverworldNodeID, INode>())
    {
        _factory = new Lazy<IOverworldNodeFactory>(() => factory());
    }

    protected override INode Create(OverworldNodeID key)
    {
        return _factory.Value.GetOverworldNode(key);
    }
}