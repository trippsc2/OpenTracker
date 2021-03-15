using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Requirements;
using System;
using System.ComponentModel;
using ReactiveUI;

namespace OpenTracker.Models.KeyDoors
{
    /// <summary>
    /// This interface contains key door data.
    /// </summary>
    public interface IKeyDoor : IReactiveObject
    {
        AccessibilityLevel Accessibility { get; }
        bool Unlocked { get; set; }
        IRequirement Requirement { get; }

        delegate IKeyDoor Factory(IMutableDungeon dungeonData);
    }
}