using OpenTracker.Models.Enums;
using System.ComponentModel;

namespace OpenTracker.Models.Interfaces
{
    public interface ISection : INotifyPropertyChanged
    {
        string Name { get; }
        AccessibilityLevel Accessibility { get; }
        Mode RequiredMode { get; }

        bool IsAvailable();
        void Clear();
    }
}
