using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Requirements;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.KeyDoors
{
    /// <summary>
    /// This is the interface for a key door.
    /// </summary>
    public interface IKeyDoor : INotifyPropertyChanged, IDisposable
    {
        AccessibilityLevel Accessibility { get; }
        bool Unlocked { get; set; }
        IRequirement Requirement { get; }

        delegate IKeyDoor Factory(IMutableDungeon dungeonData);
    }
}