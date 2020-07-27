using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking
{
    /// <summary>
    /// This is the class for representing a SNES memory address.
    /// </summary>
    public class MemoryAddress : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private byte _value;
        public byte Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }
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
        /// Resets the memory address to its starting value.
        /// </summary>
        public void Reset()
        {
            _value = 0;
        }
    }
}
