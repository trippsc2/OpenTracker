using OpenTracker.Models.SequenceBreaks;

namespace OpenTracker.Models.Requirements.SequenceBreak
{
    /// <summary>
    ///     This interface contains sequence break requirement data.
    /// </summary>
    public interface ISequenceBreakRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new sequence break requirements.
        /// </summary>
        /// <param name="sequenceBreak">
        ///     The sequence break required.
        /// </param>
        /// <returns>
        ///     A new sequence break requirement.
        /// </returns>
        delegate ISequenceBreakRequirement Factory(ISequenceBreak sequenceBreak);
    }
}