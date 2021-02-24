using OpenTracker.Models.SaveLoad;
using System.Collections.Generic;

namespace OpenTracker.Models.SequenceBreaks
{
    public interface ISequenceBreakDictionary : IDictionary<SequenceBreakType, ISequenceBreak>,
        ICollection<KeyValuePair<SequenceBreakType, ISequenceBreak>>,
        ISaveable<Dictionary<SequenceBreakType, SequenceBreakSaveData>>
    {
    }
}