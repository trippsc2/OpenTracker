using System.Collections.Generic;
using OpenTracker.Models.Modes;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.Mode;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for
/// <see cref="IWorldStateRequirement"/> objects indexed by <see cref="WorldState"/>.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class WorldStateRequirementDictionary : LazyDictionary<WorldState, IRequirement>,
    IWorldStateRequirementDictionary
{
    private readonly IWorldStateRequirement.Factory _factory;
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="IWorldStateRequirement"/> objects.
    /// </param>
    public WorldStateRequirementDictionary(IWorldStateRequirement.Factory factory)
        : base(new Dictionary<WorldState, IRequirement>())
    {
        _factory = factory;
    }

    protected override IRequirement Create(WorldState key)
    {
        return _factory(key);
    }
}