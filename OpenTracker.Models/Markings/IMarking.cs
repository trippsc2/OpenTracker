using System.ComponentModel;

namespace OpenTracker.Models.Markings
{
    public interface IMarking : INotifyPropertyChanging, INotifyPropertyChanged
    {
        MarkType? Mark { get; set; }
    }
}