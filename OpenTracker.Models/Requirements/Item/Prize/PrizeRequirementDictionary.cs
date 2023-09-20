using System.Collections.Generic;
using OpenTracker.Models.Prizes;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.Item.Prize;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="IItemRequirement"/>
/// objects indexed by <see cref="PrizeType"/> and count.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class PrizeRequirementDictionary : LazyDictionary<(PrizeType type, int count), IRequirement>,
    IPrizeRequirementDictionary
{
    private readonly IPrizeDictionary _prizes;

    private readonly IItemRequirement.Factory _factory;
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="prizes">
    ///     The <see cref="IPrizeDictionary"/>.
    /// </param>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="IItemRequirement"/> objects.
    /// </param>
    public PrizeRequirementDictionary(IPrizeDictionary prizes, IItemRequirement.Factory factory)
        : base(new Dictionary<(PrizeType type, int count), IRequirement>())
    {
        _prizes = prizes;
            
        _factory = factory;
    }
        
    protected override IRequirement Create((PrizeType type, int count) key)
    {
        return _factory(_prizes[key.type], key.count);
    }
}