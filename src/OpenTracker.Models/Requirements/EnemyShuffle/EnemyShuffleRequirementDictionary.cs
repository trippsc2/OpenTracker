using System.Collections.Generic;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.EnemyShuffle;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for
/// <see cref="EnemyShuffleRequirement"/> objects indexed by <see cref="bool"/>.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class EnemyShuffleRequirementDictionary : LazyDictionary<bool, IRequirement>,
    IEnemyShuffleRequirementDictionary
{
    private readonly EnemyShuffleRequirement.Factory _factory;
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="EnemyShuffleRequirement"/> objects.
    /// </param>
    public EnemyShuffleRequirementDictionary(EnemyShuffleRequirement.Factory factory)
        : base(new Dictionary<bool, IRequirement>())
    {
        _factory = factory;
    }

    protected override IRequirement Create(bool key)
    {
        return _factory(key);
    }
}