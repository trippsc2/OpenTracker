using System.ComponentModel;

namespace OpenTracker.Models.Sections
{
    public interface IMarkableSection : INotifyPropertyChanging, ISection
    {
        MarkingType? Marking { get; set; }
    }
}
