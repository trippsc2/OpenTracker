using OpenTracker.Models.SequenceBreaks;

namespace OpenTracker.Models.Requirements.SequenceBreak
{
    /// <summary>
    /// This interface contains <see cref="ISequenceBreak"/> <see cref="IRequirement"/> data.
    /// </summary>
    public interface ISequenceBreakRequirement : IRequirement
    {
        /// <summary>
        /// A factory for creating new <see cref="ISequenceBreakRequirement"/> objects.
        /// </summary>
        /// <param name="sequenceBreak">
        ///     The <see cref="ISequenceBreak"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="ISequenceBreakRequirement"/> object.
        /// </returns>
        delegate ISequenceBreakRequirement Factory(ISequenceBreak sequenceBreak);
    }
}