using OpenTracker.Models.Dungeons;
using OpenTracker.Models.RequirementNodes;

namespace OpenTracker.Models.KeyDoors
{
    public interface IKeyDoorFactory
    {
        IKeyDoor GetKeyDoor(IMutableDungeon dungeonData);
        IRequirementNode GetKeyDoorNode(KeyDoorID id, IMutableDungeon dungeonData);

        delegate IKeyDoorFactory Factory();
    }
}