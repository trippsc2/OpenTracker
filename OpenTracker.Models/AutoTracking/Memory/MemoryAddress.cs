using ReactiveUI;

namespace OpenTracker.Models.AutoTracking.Memory
{
    /// <summary>
    /// This class contains SNES memory address data.
    /// </summary>
    public class MemoryAddress : ReactiveObject, IMemoryAddress
    {
        private byte? _value;
        public byte? Value
        {
            get => _value;
            set => this.RaiseAndSetIfChanged(ref _value, value);
        }

        /// <summary>
        /// Resets the memory address to its starting value.
        /// </summary>
        public void Reset()
        {
            _value = null;
        }
    }
}
