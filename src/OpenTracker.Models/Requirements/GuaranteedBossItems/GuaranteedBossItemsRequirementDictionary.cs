using System.Collections.Generic;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.GuaranteedBossItems;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for
/// <see cref="GuaranteedBossItemsRequirement"/> objects indexed by <see cref="bool"/>.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class GuaranteedBossItemsRequirementDictionary : LazyDictionary<bool, IRequirement>,
    IGuaranteedBossItemsRequirementDictionary
{
    private readonly GuaranteedBossItemsRequirement.Factory _factory;
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="GuaranteedBossItemsRequirement"/> objects.
    /// </param>
    public GuaranteedBossItemsRequirementDictionary(GuaranteedBossItemsRequirement.Factory factory)
        : base(new Dictionary<bool, IRequirement>())
    {
        _factory = factory;
    }

    protected override IRequirement Create(bool key)
    {
        return _factory(key);
    }
}