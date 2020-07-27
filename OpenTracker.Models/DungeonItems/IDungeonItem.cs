using OpenTracker.Models.AccessibilityLevels;
using System.ComponentModel;

namespace OpenTracker.Models.DungeonItems
{
    /// <summary>
    /// This is the interface for a dungeon item.
    /// </summary>
    public interface IDungeonItem : INotifyPropertyChanged
    {
        AccessibilityLevel Accessibility { get; }
    }
}