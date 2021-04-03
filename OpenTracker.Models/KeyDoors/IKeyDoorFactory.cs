using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Nodes;

namespace OpenTracker.Models.KeyDoors
{
    /// <summary>
    /// This interface contains creation logic for key door data.
    /// </summary>
    public interface IKeyDoorFactory
    {
        IKeyDoor GetKeyDoor(IMutableDungeon dungeonData);
        INode GetKeyDoorNode(KeyDoorID id, IMutableDungeon dungeonData);

        delegate IKeyDoorFactory Factory();
    }
}