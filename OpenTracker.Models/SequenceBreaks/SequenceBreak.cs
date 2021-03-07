using OpenTracker.Models.SaveLoad;
using System.ComponentModel;

namespace OpenTracker.Models.SequenceBreaks
{
    /// <summary>
    /// This class contains sequence breaks.
    /// </summary>
    public class SequenceBreak : ISequenceBreak
    {
        private readonly bool _starting;

        public event PropertyChangedEventHandler? PropertyChanged;

        private bool _enabled = true;
        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    OnPropertyChanged(nameof(Enabled));
                }
            }
        }

        public delegate ISequenceBreak Factory(bool starting = true);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="starting">
        /// A boolean representing the starting value of this sequence break.
        /// </param>
        public SequenceBreak(bool starting = true)
        {
            _starting = starting;
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Resets the sequence break to its starting value.
        /// </summary>
        public void Reset()
        {
            Enabled = _starting;
        }

        /// <summary>
        /// Returns a new sequence break save data instance for this sequence break.
        /// </summary>
        /// <returns>
        /// A new sequence break save data instance.
        /// </returns>
        public SequenceBreakSaveData Save()
        {
            return new SequenceBreakSaveData()
            {
                Enabled = Enabled
            };
        }

        /// <summary>
        /// Loads sequence break save data.
        /// </summary>
        public void Load(SequenceBreakSaveData saveData)
        {
            Enabled = saveData.Enabled;
        }
    }
}
