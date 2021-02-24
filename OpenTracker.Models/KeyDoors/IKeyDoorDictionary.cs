using OpenTracker.Models.Dungeons;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.KeyDoors
{
    public interface IKeyDoorDictionary : IDictionary<KeyDoorID, IKeyDoor>,
        ICollection<KeyValuePair<KeyDoorID, IKeyDoor>>
    {
        event EventHandler<KeyValuePair<KeyDoorID, IKeyDoor>> ItemCreated;

        delegate IKeyDoorDictionary Factory(IMutableDungeon dungeonData);
    }
}