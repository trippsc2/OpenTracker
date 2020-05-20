using OpenTracker.Models.Enums;
using System.ComponentModel;

namespace OpenTracker.Models.Interfaces
{
    public interface ISection : INotifyPropertyChanging,INotifyPropertyChanged
    {
        string Name { get; }
        bool HasMarking { get; }
        Mode RequiredMode { get; }
        AccessibilityLevel Accessibility { get; }
        bool UserManipulated { get; set; }
        MarkingType? Marking { get; set; }
        int Available { get; set; }

        bool IsAvailable();
        void Clear(bool force);
        void Reset();
    }
}
