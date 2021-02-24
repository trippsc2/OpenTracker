using OpenTracker.Models.Dungeons;
using OpenTracker.Utils;
using System;
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
        private readonly Lazy<IKeyDoorFactory> _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dungeonData">
        /// The mutable dungeon data parent class.
        /// </param>
        public KeyDoorDictionary(IKeyDoorFactory.Factory factory, IMutableDungeon dungeonData)
            : base(new Dictionary<KeyDoorID, IKeyDoor>())
        {
            _dungeonData = dungeonData;
            _factory = new Lazy<IKeyDoorFactory>(() => factory());
        }

        protected override IKeyDoor Create(KeyDoorID key)
        {
            return _factory.Value.GetKeyDoor(_dungeonData);
        }
    }
}
