using OpenTracker.Models.Enums;
using System.ComponentModel;

namespace OpenTracker.Models.Interfaces
{
    public interface ISection : INotifyPropertyChanged
    {
        string Name { get; }
        bool HasMarking { get; }
        MarkingType? Marking { get; set; }
        AccessibilityLevel Accessibility { get; }
        Mode RequiredMode { get; }

        bool IsAvailable();
        void Clear();
    }
}
