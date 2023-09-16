using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.AutoTracking.Memory
{
    /// <summary>
    /// This class contains SNES memory flag data.
    /// </summary>
    public class MemoryFlag : ReactiveObject, IMemoryFlag
    {
        private readonly byte _flag;

        private MemoryAddress MemoryAddress { get; }

        [ObservableAsProperty]
        public bool? Status { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="memoryAddress">
        ///     The <see cref="Memory.MemoryAddress"/> containing the flag.
        /// </param>
        /// <param name="flag">
        ///     A <see cref="byte"/> representing the bitwise flag.
        /// </param>
        public MemoryFlag(MemoryAddress memoryAddress, byte flag)
        {
            _flag = flag;
            
            MemoryAddress = memoryAddress;

            this.WhenAnyValue(x => x.MemoryAddress.Value)
                .Select(GetNewStatusFromAddressValue)
                .ToPropertyEx(this, x => x.Status);
        }

        private bool? GetNewStatusFromAddressValue(byte? addressValue)
        {
            if (addressValue is null)
            {
                return null;
            }
            
            return (addressValue & _flag) != 0;
        }
    }
}
