using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Requirements;
using System.ComponentModel;

namespace OpenTracker.Models.KeyDoors
{
    /// <summary>
    /// This is the interface for a key door.
    /// </summary>
    public interface IKeyDoor : INotifyPropertyChanged
    {
        AccessibilityLevel Accessibility { get; }
        bool Unlocked { get; set; }
        IRequirement Requirement { get; }
    }
}