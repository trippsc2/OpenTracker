using System.ComponentModel;
using ReactiveUI;

namespace OpenTracker.Models.AutoTracking.Memory
{
    /// <summary>
    /// This class contains SNES memory flag data.
    /// </summary>
    public class MemoryFlag : ReactiveObject, IMemoryFlag
    {
        private readonly IMemoryAddress _memoryAddress;
        private readonly byte _flag;

        private bool? _status;
        public bool? Status
        {
            get => _status;
            private set => this.RaiseAndSetIfChanged(ref _status, value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="memoryAddress">
        ///     The <see cref="IMemoryAddress"/> containing the flag.
        /// </param>
        /// <param name="flag">
        ///     A <see cref="byte"/> representing the bitwise flag.
        /// </param>
        public MemoryFlag(IMemoryAddress memoryAddress, byte flag)
        {
            _memoryAddress = memoryAddress;
            _flag = flag;

            UpdateFlag();
            
            _memoryAddress.PropertyChanged += OnMemoryChanged;
        }

        /// <summary>
        /// Subscribes to the <see cref="IMemoryAddress.PropertyChanged"/> event.
        /// </summary>
        /// <param name="sender">
        ///     The object from which the event was sent.
        /// </param>
        /// <param name="e">
        ///     The event args.
        /// </param>
        private void OnMemoryChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMemoryAddress.Value))
            {
                UpdateFlag();
            }
        }

        /// <summary>
        /// Updates the flag status.
        /// </summary>
        private void UpdateFlag()
        {
            if (_memoryAddress.Value is null)
            {
                Status = null;
                return;
            }
            
            Status = (_memoryAddress.Value & _flag) != 0;
        }
    }
}
