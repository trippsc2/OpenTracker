using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using ReactiveUI;

namespace OpenTracker.Models.Dungeons.KeyDoors
{
    /// <summary>
    ///     This interface contains key door data.
    /// </summary>
    public interface IKeyDoor : IReactiveObject
    {
        /// <summary>
        ///     The accessibility level of the key door.
        /// </summary>
        AccessibilityLevel Accessibility { get; }
        
        /// <summary>
        ///     A boolean representing whether the key door is unlocked.
        /// </summary>
        bool Unlocked { get; set; }
        
        /// <summary>
        ///     The requirement for this key door.
        /// </summary>
        IRequirement Requirement { get; }

        /// <summary>
        ///     A factory for creating key doors.
        /// </summary>
        /// <param name="node">
        ///     The node to which the key door belongs.
        /// </param>
        /// <returns>
        ///     A new key door.
        /// </returns>
        delegate IKeyDoor Factory(INode node);
    }
}