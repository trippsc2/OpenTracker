using OpenTracker.Models.Dungeons.KeyDoors;

namespace OpenTracker.Models.Requirements.KeyDoor
{
    /// <summary>
    /// This interface contains key door requirement data.
    /// </summary>
    public interface IKeyDoorRequirement : IRequirement
    {
        delegate IKeyDoorRequirement Factory(IKeyDoor keyDoor);
    }
}