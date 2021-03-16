using System.Collections.Generic;
using OpenTracker.Models.SaveLoad;

namespace OpenTracker.Models.SequenceBreaks
{
    /// <summary>
    /// This interface contains the dictionary container for sequence breaks.
    /// </summary>
    public interface ISequenceBreakDictionary : IDictionary<SequenceBreakType, ISequenceBreak>,
        ISaveable<Dictionary<SequenceBreakType, SequenceBreakSaveData>>
    {
    }
}