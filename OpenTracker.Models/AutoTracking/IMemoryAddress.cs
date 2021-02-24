using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking
{
    public interface IMemoryAddress : INotifyPropertyChanged
    {
        byte Value { get; set; }

        delegate IMemoryAddress Factory();

        void Reset();
    }
}