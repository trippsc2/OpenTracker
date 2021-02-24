using OpenTracker.Models.Dungeons;
using OpenTracker.Models.RequirementNodes;

namespace OpenTracker.Models.KeyDoors
{
    /// <summary>
    /// This interface contains creation logic for key door data.
    /// </summary>
    public interface IKeyDoorFactory
    {
        IKeyDoor GetKeyDoor(IMutableDungeon dungeonData);
        IRequirementNode GetKeyDoorNode(KeyDoorID id, IMutableDungeon dungeonData);

        delegate IKeyDoorFactory Factory();
    }
}