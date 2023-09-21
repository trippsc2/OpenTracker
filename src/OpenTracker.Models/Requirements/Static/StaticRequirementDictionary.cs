using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.Static;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="StaticRequirement"/>
/// objects indexed by <see cref="AccessibilityLevel"/>.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class StaticRequirementDictionary : LazyDictionary<AccessibilityLevel, IRequirement>,
    IStaticRequirementDictionary
{
    private readonly StaticRequirement.Factory _factory;
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="StaticRequirement"/> objects.
    /// </param>
    public StaticRequirementDictionary(StaticRequirement.Factory factory)
        : base(new Dictionary<AccessibilityLevel, IRequirement>())
    {
        _factory = factory;
    }

    protected override IRequirement Create(AccessibilityLevel key)
    {
        return _factory(key);
    }
}