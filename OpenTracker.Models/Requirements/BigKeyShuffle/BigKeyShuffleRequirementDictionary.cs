using System.Collections.Generic;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.BigKeyShuffle;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for
/// <see cref="IBigKeyShuffleRequirement"/> objects indexed by <see cref="bool"/>.
/// </summary>
public class BigKeyShuffleRequirementDictionary : LazyDictionary<bool, IRequirement>,
    IBigKeyShuffleRequirementDictionary
{
    private readonly IBigKeyShuffleRequirement.Factory _factory;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="IBigKeyShuffleRequirement"/> objects.
    /// </param>
    public BigKeyShuffleRequirementDictionary(IBigKeyShuffleRequirement.Factory factory)
        : base(new Dictionary<bool, IRequirement>())
    {
        _factory = factory;
    }

    protected override IRequirement Create(bool key)
    {
        return _factory(key);
    }
}