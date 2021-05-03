using OpenTracker.Models.SaveLoad;
using ReactiveUI;

namespace OpenTracker.Models.SequenceBreaks
{
    /// <summary>
    ///     This class contains sequence break data.
    /// </summary>
    public class SequenceBreak : ReactiveObject, ISequenceBreak
    {
        private bool _enabled;
        public bool Enabled
        {
            get => _enabled;
            set => this.RaiseAndSetIfChanged(ref _enabled, value);
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="starting">
        ///     A boolean representing the starting value of this sequence break.
        /// </param>
        public SequenceBreak(bool starting = true)
        {
            Enabled = starting;
        }

        /// <summary>
        ///     Returns a new sequence break save data instance for this sequence break.
        /// </summary>
        /// <returns>
        ///     A new sequence break save data instance.
        /// </returns>
        public SequenceBreakSaveData Save()
        {
            return new() {Enabled = Enabled};
        }

        /// <summary>
        ///     Loads sequence break save data.
        /// </summary>
        public void Load(SequenceBreakSaveData? saveData)
        {
            if (saveData is null)
            {
                return;
            }
            
            Enabled = saveData.Enabled;
        }
    }
}
