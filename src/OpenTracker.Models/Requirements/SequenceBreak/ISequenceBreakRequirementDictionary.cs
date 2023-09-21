using System.Collections.Generic;
using OpenTracker.Models.SequenceBreaks;

namespace OpenTracker.Models.Requirements.SequenceBreak;

/// <summary>
/// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for
/// <see cref="SequenceBreakRequirement"/> objects indexed by <see cref="SequenceBreakType"/>.
/// </summary>
public interface ISequenceBreakRequirementDictionary : IDictionary<SequenceBreakType, IRequirement>
{
}