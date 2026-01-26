using System.Collections.Generic;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.KeyDropShuffle;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for
/// <see cref="IKeyDropShuffleRequirement"/> objects indexed by <see cref="bool"/>.
/// </summary>
public class KeyDropShuffleRequirementDictionary : LazyDictionary<bool, IRequirement>, IKeyDropShuffleRequirementDictionary
{
    private readonly IKeyDropShuffleRequirement.Factory _factory;
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="IKeyDropShuffleRequirement"/> objects.
    /// </param>
    public KeyDropShuffleRequirementDictionary(IKeyDropShuffleRequirement.Factory factory)
        : base(new Dictionary<bool, IRequirement>())
    {
        _factory = factory;
    }

    protected override IRequirement Create(bool key)
    {
        return _factory(key);
    }
}