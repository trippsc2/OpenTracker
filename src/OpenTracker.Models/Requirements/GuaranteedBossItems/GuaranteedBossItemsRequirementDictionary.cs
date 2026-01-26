using System.Collections.Generic;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.GuaranteedBossItems;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for
/// <see cref="IGuaranteedBossItemsRequirement"/> objects indexed by <see cref="bool"/>.
/// </summary>
public class GuaranteedBossItemsRequirementDictionary : LazyDictionary<bool, IRequirement>,
    IGuaranteedBossItemsRequirementDictionary
{
    private readonly IGuaranteedBossItemsRequirement.Factory _factory;
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="IGuaranteedBossItemsRequirement"/> objects.
    /// </param>
    public GuaranteedBossItemsRequirementDictionary(IGuaranteedBossItemsRequirement.Factory factory)
        : base(new Dictionary<bool, IRequirement>())
    {
        _factory = factory;
    }

    protected override IRequirement Create(bool key)
    {
        return _factory(key);
    }
}