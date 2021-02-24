using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking
{
    public interface IMemoryFlag : INotifyPropertyChanged
    {
        bool Status { get; }

        delegate IMemoryFlag Factory(IMemoryAddress memoryAddress, byte flag);
    }
}