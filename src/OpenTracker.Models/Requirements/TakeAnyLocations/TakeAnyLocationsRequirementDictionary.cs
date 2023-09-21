using System.Collections.Generic;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.TakeAnyLocations;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for
/// <see cref="TakeAnyLocationsRequirement"/> objects indexed by <see cref="bool"/>.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class TakeAnyLocationsRequirementDictionary : LazyDictionary<bool, IRequirement>,
    ITakeAnyLocationsRequirementDictionary
{
    private readonly TakeAnyLocationsRequirement.Factory _factory;
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="TakeAnyLocationsRequirement"/> objects.
    /// </param>
    public TakeAnyLocationsRequirementDictionary(TakeAnyLocationsRequirement.Factory factory)
        : base(new Dictionary<bool, IRequirement>())
    {
        _factory = factory;
    }

    protected override IRequirement Create(bool key)
    {
        return _factory(key);
    }
}