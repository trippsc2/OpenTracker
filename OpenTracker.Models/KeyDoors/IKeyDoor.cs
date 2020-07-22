using OpenTracker.Models.DungeonNodes;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.KeyDoors
{
    /// <summary>
    /// This is the interface for a key door.
    /// </summary>
    public interface IKeyDoor : INotifyPropertyChanged
    {
        AccessibilityLevel Accessibility { get; }
        KeyDoorID ID { get; }
        bool Unlocked { get; set; }

        AccessibilityLevel GetDoorAccessibility(List<DungeonNodeID> excludedNodes);
    }
}