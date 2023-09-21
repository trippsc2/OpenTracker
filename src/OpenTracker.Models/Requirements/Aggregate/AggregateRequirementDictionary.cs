using System.Collections.Generic;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.Aggregate;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for
/// <see cref="AggregateRequirement"/> objects indexed by <see cref="HashSet{T}"/> of <see cref="IRequirement"/>.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class AggregateRequirementDictionary
    : LazyDictionary<HashSet<IRequirement>, IRequirement>, IAggregateRequirementDictionary
{
    private readonly AggregateRequirement.Factory _factory;
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="AggregateRequirement"/> objects.
    /// </param>
    public AggregateRequirementDictionary(AggregateRequirement.Factory factory)
        : base(new Dictionary<HashSet<IRequirement>, IRequirement>(HashSet<IRequirement>.CreateSetComparer()))
    {
        _factory = factory;
    }

    protected override IRequirement Create(HashSet<IRequirement> key)
    {
        return _factory(key);
    }
}