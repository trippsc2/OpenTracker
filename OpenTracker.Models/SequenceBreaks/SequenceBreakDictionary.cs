using OpenTracker.Models.SaveLoad;
using OpenTracker.Utils;
using System.Collections.Generic;

namespace OpenTracker.Models.SequenceBreaks
{
    /// <summary>
    /// This class contains the dictionary container for sequence breaks.
    /// </summary>
    public class SequenceBreakDictionary : LazyDictionary<SequenceBreakType, ISequenceBreak>,
        ISequenceBreakDictionary
    {
        private readonly ISequenceBreak.Factory _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        /// Factory for creating new sequence breaks.
        /// </param>
        public SequenceBreakDictionary(ISequenceBreak.Factory factory)
            : base(new Dictionary<SequenceBreakType, ISequenceBreak>())
        {
            _factory = factory;
        }

        protected override ISequenceBreak Create(SequenceBreakType key)
        {
            return _factory();
        }

        /// <summary>
        /// Returns a dictionary of sequence break save data.
        /// </summary>
        /// <returns>
        /// A dictionary of sequence break save data.
        /// </returns>
        public Dictionary<SequenceBreakType, SequenceBreakSaveData> Save()
        {
            Dictionary<SequenceBreakType, SequenceBreakSaveData> sequenceBreaks =
                new Dictionary<SequenceBreakType, SequenceBreakSaveData>();

            foreach (var type in Keys)
            {
                sequenceBreaks.Add(type, this[type].Save());
            }

            return sequenceBreaks;
        }

        /// <summary>
        /// Loads a dictionary of prize placement save data.
        /// </summary>
        public void Load(Dictionary<SequenceBreakType, SequenceBreakSaveData>? saveData)
        {
            if (saveData == null)
            {
                return;
            }

            foreach (var type in saveData.Keys)
            {
                this[type].Load(saveData[type]);
            }
        }
    }
}
