using OpenTracker.Models.SaveLoad;
using ReactiveUI;

namespace OpenTracker.Models.SequenceBreaks;

/// <summary>
/// This interface contains sequence break data.
/// </summary>
public interface ISequenceBreak : IReactiveObject, ISaveable<SequenceBreakSaveData>
{
    /// <summary>
    /// A <see cref="bool"/> representing whether the sequence break is enabled.
    /// </summary>
    bool Enabled { get; set; }

    /// <summary>
    /// A factory for creating new <see cref="ISequenceBreak"/> objects.
    /// </summary>
    /// <param name="starting">
    ///     A <see cref="bool"/> representing the starting <see cref="Enabled"/> value.
    /// </param>
    /// <returns>
    ///     A new <see cref="ISequenceBreak"/> object.
    /// </returns>
    delegate ISequenceBreak Factory(bool starting = true);
}