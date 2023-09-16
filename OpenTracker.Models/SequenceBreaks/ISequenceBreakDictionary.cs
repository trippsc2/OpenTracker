using System.Collections.Generic;
using OpenTracker.Models.SaveLoad;

namespace OpenTracker.Models.SequenceBreaks;

/// <summary>
/// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="ISequenceBreak"/>
/// objects indexed by <see cref="SequenceBreakType"/>.
/// </summary>
public interface ISequenceBreakDictionary : IDictionary<SequenceBreakType, ISequenceBreak>,
    ISaveable<Dictionary<SequenceBreakType, SequenceBreakSaveData>>
{
}