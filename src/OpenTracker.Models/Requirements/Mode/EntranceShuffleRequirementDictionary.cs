using System.Collections.Generic;
using OpenTracker.Models.Modes;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.Mode;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for
/// <see cref="IEntranceShuffleRequirement"/> objects indexed by <see cref="EntranceShuffle"/>.
/// </summary>
public class EntranceShuffleRequirementDictionary : LazyDictionary<EntranceShuffle, IRequirement>,
    IEntranceShuffleRequirementDictionary
{
    private readonly IEntranceShuffleRequirement.Factory _factory;
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="IEntranceShuffleRequirement"/> objects.
    /// </param>
    public EntranceShuffleRequirementDictionary(IEntranceShuffleRequirement.Factory factory)
        : base(new Dictionary<EntranceShuffle, IRequirement>())
    {
        _factory = factory;
    }

    protected override IRequirement Create(EntranceShuffle key)
    {
        return _factory(key);
    }
}