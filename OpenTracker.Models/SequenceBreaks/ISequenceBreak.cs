using OpenTracker.Models.SaveLoad;
using ReactiveUI;

namespace OpenTracker.Models.SequenceBreaks
{
    /// <summary>
    ///     This interface contains sequence break data.
    /// </summary>
    public interface ISequenceBreak : IReactiveObject, ISaveable<SequenceBreakSaveData>
    {
        /// <summary>
        ///     A boolean representing whether the sequence break is enabled.
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        ///     A factory for creating new sequence breaks.
        /// </summary>
        /// <param name="starting">
        ///     A boolean representing the starting value of this sequence break.
        /// </param>
        /// <returns>
        ///     A new sequence break.
        /// </returns>
        delegate ISequenceBreak Factory(bool starting = true);
    }
}