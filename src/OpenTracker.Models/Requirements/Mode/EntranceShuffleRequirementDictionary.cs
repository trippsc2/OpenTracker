using System.Collections.Generic;
using OpenTracker.Models.Modes;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.Mode;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for
/// <see cref="EntranceShuffleRequirement"/> objects indexed by <see cref="EntranceShuffle"/>.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class EntranceShuffleRequirementDictionary : LazyDictionary<EntranceShuffle, IRequirement>,
    IEntranceShuffleRequirementDictionary
{
    private readonly EntranceShuffleRequirement.Factory _factory;
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="EntranceShuffleRequirement"/> objects.
    /// </param>
    public EntranceShuffleRequirementDictionary(EntranceShuffleRequirement.Factory factory)
        : base(new Dictionary<EntranceShuffle, IRequirement>())
    {
        _factory = factory;
    }

    protected override IRequirement Create(EntranceShuffle key)
    {
        return _factory(key);
    }
}