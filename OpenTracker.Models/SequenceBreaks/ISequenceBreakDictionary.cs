using OpenTracker.Models.SaveLoad;
using System.Collections.Generic;

namespace OpenTracker.Models.SequenceBreaks
{
    /// <summary>
    /// This interface contains the dictionary container for sequence breaks.
    /// </summary>
    public interface ISequenceBreakDictionary : IDictionary<SequenceBreakType, ISequenceBreak>,
        ICollection<KeyValuePair<SequenceBreakType, ISequenceBreak>>,
        ISaveable<Dictionary<SequenceBreakType, SequenceBreakSaveData>>
    {
    }
}