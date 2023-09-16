using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.AutoTracking.Memory
{
    /// <summary>
    /// This class contains SNES memory address data.
    /// </summary>
    public sealed class MemoryAddress : ReactiveObject
    {
        [Reactive]
        public byte? Value { get; set; }

        public void Reset()
        {
            Value = null;
        }
    }
}
