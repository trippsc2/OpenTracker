using System.ComponentModel;

namespace OpenTracker.Models.Markings
{
    /// <summary>
    /// This interface contains marking data.
    /// </summary>
    public interface IMarking : INotifyPropertyChanging, INotifyPropertyChanged
    {
        MarkType Mark { get; set; }

        delegate IMarking Factory();
    }
}