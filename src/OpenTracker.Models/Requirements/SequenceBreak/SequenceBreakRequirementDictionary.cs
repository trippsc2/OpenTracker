using System.Collections.Generic;
using OpenTracker.Models.SequenceBreaks;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.SequenceBreak;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for
/// <see cref="SequenceBreakRequirement"/> objects indexed by <see cref="SequenceBreakType"/>.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class SequenceBreakRequirementDictionary : LazyDictionary<SequenceBreakType, IRequirement>,
    ISequenceBreakRequirementDictionary
{
    private readonly ISequenceBreakDictionary _sequenceBreaks;

    private readonly SequenceBreakRequirement.Factory _factory;
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="sequenceBreaks">
    ///     The <see cref="ISequenceBreakDictionary"/>.
    /// </param>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="SequenceBreakRequirement"/> objects.
    /// </param>
    public SequenceBreakRequirementDictionary(
        ISequenceBreakDictionary sequenceBreaks, SequenceBreakRequirement.Factory factory)
        : base(new Dictionary<SequenceBreakType, IRequirement>())
    {
        _sequenceBreaks = sequenceBreaks;
        _factory = factory;
    }

    protected override IRequirement Create(SequenceBreakType key)
    {
        return _factory(_sequenceBreaks[key]);
    }
}