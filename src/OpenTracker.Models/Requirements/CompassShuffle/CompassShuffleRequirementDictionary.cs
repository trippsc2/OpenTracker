using System.Collections.Generic;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.CompassShuffle;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for
/// <see cref="CompassShuffleRequirement"/> objects indexed by <see cref="bool"/>.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class CompassShuffleRequirementDictionary
    : LazyDictionary<bool, IRequirement>, ICompassShuffleRequirementDictionary
{
    private readonly CompassShuffleRequirement.Factory _factory;
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="CompassShuffleRequirement"/> objects.
    /// </param>
    public CompassShuffleRequirementDictionary(CompassShuffleRequirement.Factory factory)
        : base(new Dictionary<bool, IRequirement>())
    {
        _factory = factory;
    }

    protected override IRequirement Create(bool key)
    {
        return _factory(key);
    }
}