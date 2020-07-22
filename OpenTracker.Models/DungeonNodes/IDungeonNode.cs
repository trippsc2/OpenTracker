using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.DungeonNodes
{
    /// <summary>
    /// This is the interface for a dungeon node.
    /// </summary>
    public interface IDungeonNode : INotifyPropertyChanged
    {
        AccessibilityLevel Accessibility { get; }
        int FreeKeysProvided { get; }
        DungeonNodeID ID { get; }

        AccessibilityLevel GetNodeAccessibility(List<DungeonNodeID> excludedNodes);
    }
}