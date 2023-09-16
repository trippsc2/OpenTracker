using System.Collections.Generic;
using OpenTracker.Models.SequenceBreaks;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.SequenceBreak;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for
/// <see cref="ISequenceBreakRequirement"/> objects indexed by <see cref="SequenceBreakType"/>.
/// </summary>
public class SequenceBreakRequirementDictionary : LazyDictionary<SequenceBreakType, IRequirement>,
    ISequenceBreakRequirementDictionary
{
    private readonly ISequenceBreakDictionary _sequenceBreaks;

    private readonly ISequenceBreakRequirement.Factory _factory;
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="sequenceBreaks">
    ///     The <see cref="ISequenceBreakDictionary"/>.
    /// </param>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="ISequenceBreakRequirement"/> objects.
    /// </param>
    public SequenceBreakRequirementDictionary(
        ISequenceBreakDictionary sequenceBreaks, ISequenceBreakRequirement.Factory factory)
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