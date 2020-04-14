using OpenTracker.Models.Enums;
using System.ComponentModel;

namespace OpenTracker.Models.Interfaces
{
    public interface ISection : INotifyPropertyChanged
    {
        string Name { get; }
        bool HasMarking { get; }
        Mode RequiredMode { get; }
        AccessibilityLevel Accessibility { get; }
        MarkingType? Marking { get; set; }
        int Available { get; set; }

        bool IsAvailable();
        void Clear();
        void Reset();
    }
}
