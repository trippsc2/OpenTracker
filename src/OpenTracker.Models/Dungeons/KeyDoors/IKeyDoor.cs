using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Dungeons.KeyDoors;

/// <summary>
/// This interface contains key door data.
/// </summary>
public interface IKeyDoor : INotifyPropertyChanged
{
    /// <summary>
    /// The <see cref="AccessibilityLevel"/> of the key door.
    /// </summary>
    AccessibilityLevel Accessibility { get; }
        
    /// <summary>
    /// A <see cref="bool"/> representing whether the key door is unlocked.
    /// </summary>
    bool Unlocked { get; set; }
        
    /// <summary>
    /// The <see cref="IRequirement"/> for this key door to be unlocked.
    /// </summary>
    IRequirement Requirement { get; }

    /// <summary>
    /// A factory for creating new <see cref="IKeyDoor"/> objects.
    /// </summary>
    /// <param name="node">
    ///     The <see cref="INode"/> to which the key door belongs.
    /// </param>
    /// <returns>
    ///     A new <see cref="IKeyDoor"/> object.
    /// </returns>
    delegate IKeyDoor Factory(INode node);
}