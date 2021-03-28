using System;
using System.Collections.Generic;
using OpenTracker.Utils;

namespace OpenTracker.Models.Dungeons
{
    /// <summary>
    /// This class contains the dictionary container for dungeon data.
    /// </summary>
    public class DungeonDictionary : LazyDictionary<DungeonID, IDungeon>, IDungeonDictionary
    {
        private readonly Lazy<IDungeonFactory> _factory;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        /// An Autofac factory for creating the dungeon factory.
        /// </param>
        public DungeonDictionary(IDungeonFactory.Factory factory) : base(new Dictionary<DungeonID, IDungeon>())
        {
            _factory = new Lazy<IDungeonFactory>(factory());
        }

        protected override IDungeon Create(DungeonID key)
        {
            return _factory.Value.GetDungeon(key);
        }
    }
}