using System.Collections.Generic;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.SmallKeyShuffle;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for
/// <see cref="ISmallKeyShuffleRequirement"/> objects indexed by <see cref="bool"/>.
/// </summary>
public class SmallKeyShuffleRequirementDictionary : LazyDictionary<bool, IRequirement>, ISmallKeyShuffleRequirementDictionary
{
    private readonly ISmallKeyShuffleRequirement.Factory _factory;
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="ISmallKeyShuffleRequirement"/> objects.
    /// </param>
    public SmallKeyShuffleRequirementDictionary(ISmallKeyShuffleRequirement.Factory factory)
        : base(new Dictionary<bool, IRequirement>())
    {
        _factory = factory;
    }

    protected override IRequirement Create(bool key)
    {
        return _factory(key);
    }
}