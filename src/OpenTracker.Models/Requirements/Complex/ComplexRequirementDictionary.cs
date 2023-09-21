using System;
using System.Collections.Generic;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.Complex;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="IRequirement"/> objects
/// indexed by <see cref="ComplexRequirementType"/>.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class ComplexRequirementDictionary : LazyDictionary<ComplexRequirementType, IRequirement>,
    IComplexRequirementDictionary
{
    private readonly Lazy<IComplexRequirementFactory> _factory;
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory">
    ///     An Autofac factory for creating the <see cref="IComplexRequirementFactory"/> object.
    /// </param>
    public ComplexRequirementDictionary(IComplexRequirementFactory.Factory factory)
        : base(new Dictionary<ComplexRequirementType, IRequirement>())
    {
        _factory = new Lazy<IComplexRequirementFactory>(() => factory());
    }

    protected override IRequirement Create(ComplexRequirementType key)
    {
        return _factory.Value.GetComplexRequirement(key);
    }
}