using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking
{
    /// <summary>
    /// This is the interface for representing a SNES memory flag.
    /// </summary>
    public interface IMemoryFlag : INotifyPropertyChanged
    {
        bool Status { get; }

        delegate IMemoryFlag Factory(IMemoryAddress memoryAddress, byte flag);
    }
}