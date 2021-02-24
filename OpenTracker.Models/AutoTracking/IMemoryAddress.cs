using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking
{
    /// <summary>
    /// This is the interface for representing a SNES memory address.
    /// </summary>
    public interface IMemoryAddress : INotifyPropertyChanged
    {
        byte Value { get; set; }

        delegate IMemoryAddress Factory();

        void Reset();
    }
}