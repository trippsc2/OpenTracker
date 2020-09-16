using System;
using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking
{
    /// <summary>
    /// This is the class for representing a SNES memory flag.
    /// </summary>
    public class MemoryFlag : INotifyPropertyChanged
    {
        private readonly MemoryAddress _memoryAddress;
        private readonly byte _flag;

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _status;
        public bool Status
        {
            get => _status;
            private set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="memoryAddress">
        /// The memory address.
        /// </param>
        /// <param name="flag">
        /// The 8-bit bitwise flag.
        /// </param>
        public MemoryFlag(MemoryAddress memoryAddress, byte flag)
        {
            _memoryAddress = memoryAddress ??
                throw new ArgumentNullException(nameof(memoryAddress));
            _flag = flag;

            _memoryAddress.PropertyChanged += OnMemoryChanged;
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
        /// Subscribes to the PropertyChanged event on the MemoryAddress class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnMemoryChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MemoryAddress.Value))
            {
                UpdateFlag();
            }
        }

        /// <summary>
        /// Updates the flag status.
        /// </summary>
        private void UpdateFlag()
        {
            Status = (_memoryAddress.Value & _flag) != 0;
        }
    }
}
