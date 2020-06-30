using OpenTracker.Models.Enums;
using System.ComponentModel;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This is the interface for item/entrance sections of a location.
    /// </summary>
    public interface ISection : INotifyPropertyChanging, INotifyPropertyChanged
    {
        string Name { get; }
        bool HasMarking { get; }
        ModeRequirement ModeRequirement { get; }
        AccessibilityLevel Accessibility { get; }
        bool UserManipulated { get; set; }
        MarkingType? Marking { get; set; }
        int Available { get; set; }

        bool IsAvailable();
        void Clear(bool force);
        void Reset();
    }
}
