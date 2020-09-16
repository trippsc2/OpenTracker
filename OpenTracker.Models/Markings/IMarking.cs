using System.ComponentModel;

namespace OpenTracker.Models.Markings
{
    /// <summary>
    /// This is interface for marking data.
    /// </summary>
    public interface IMarking : INotifyPropertyChanging, INotifyPropertyChanged
    {
        MarkType Mark { get; set; }
    }
}