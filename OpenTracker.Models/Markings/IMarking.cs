using System.ComponentModel;

namespace OpenTracker.Models.Markings
{
    public interface IMarking : INotifyPropertyChanging, INotifyPropertyChanged
    {
        MarkingType? Value { get; set; }
    }
}