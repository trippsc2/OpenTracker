using OpenTracker.Models.Dungeons;
using OpenTracker.Utils;
using System.Collections.Generic;

namespace OpenTracker.Models.KeyDoors
{
    /// <summary>
    /// This is the dictionary container for key doors.
    /// </summary>
    public class KeyDoorDictionary : LazyDictionary<KeyDoorID, IKeyDoor>,
        IKeyDoorDictionary
    {
        private readonly IMutableDungeon _dungeonData;
        private readonly IKeyDoorFactory _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dungeonData">
        /// The mutable dungeon data parent class.
        /// </param>
        public KeyDoorDictionary(IKeyDoorFactory factory, IMutableDungeon dungeonData)
            : base(new Dictionary<KeyDoorID, IKeyDoor>())
        {
            _dungeonData = dungeonData;
            _factory = factory;
        }

        protected override IKeyDoor Create(KeyDoorID key)
        {
            return _factory.GetKeyDoor(_dungeonData);
        }
    }
}
