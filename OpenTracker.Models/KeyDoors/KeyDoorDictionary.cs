using System;
using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Utils;

namespace OpenTracker.Models.KeyDoors
{
    /// <summary>
    /// This class contains the dictionary container for key door data.
    /// </summary>
    public class KeyDoorDictionary : LazyDictionary<KeyDoorID, IKeyDoor>,
        IKeyDoorDictionary
    {
        private readonly IMutableDungeon _dungeonData;
        private readonly Lazy<IKeyDoorFactory> _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        /// The key door factory.
        /// </param>
        /// <param name="dungeonData">
        /// The mutable dungeon data.
        /// </param>
        public KeyDoorDictionary(IKeyDoorFactory.Factory factory, IMutableDungeon dungeonData)
            : base(new Dictionary<KeyDoorID, IKeyDoor>())
        {
            _dungeonData = dungeonData;
            _factory = new Lazy<IKeyDoorFactory>(() => factory());
        }

        public void PopulateDoors(IList<KeyDoorID> keyDoors)
        {
            foreach (var keyDoor in keyDoors)
            {
                _ = this[keyDoor];
            }
        }

        protected override IKeyDoor Create(KeyDoorID key)
        {
            return _factory.Value.GetKeyDoor(_dungeonData);
        }
    }
}
