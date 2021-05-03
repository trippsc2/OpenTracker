using System.Collections.Generic;
using OpenTracker.Models.SequenceBreaks;

namespace OpenTracker.Models.Requirements.SequenceBreak
{
    /// <summary>
    ///     This interface contains the dictionary container for sequence break requirements.
    /// </summary>
    public interface ISequenceBreakRequirementDictionary : IDictionary<SequenceBreakType, IRequirement>
    {
    }
}