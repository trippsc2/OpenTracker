using System.Collections.Generic;
using OpenTracker.Models.Dungeons;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.Item.SmallKey
{
    /// <summary>
    ///     This class contains the dictionary container for small key requirements.
    /// </summary>
    public class SmallKeyRequirementDictionary : LazyDictionary<(DungeonID id, int count), IRequirement>,
        ISmallKeyRequirementDictionary
    {
        private readonly IDungeonDictionary _dungeons;

        private readonly ISmallKeyRequirement.Factory _factory;
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="dungeons">
        ///     The dungeon dictionary.
        /// </param>
        /// <param name="factory">
        ///     An Autofac factory for creating small key requirements.
        /// </param>
        public SmallKeyRequirementDictionary(IDungeonDictionary dungeons, ISmallKeyRequirement.Factory factory)
            : base(new Dictionary<(DungeonID id, int count), IRequirement>())
        {
            _dungeons = dungeons;
            
            _factory = factory;
        }

        protected override IRequirement Create((DungeonID id, int count) key)
        {
            return _factory(_dungeons[key.id].SmallKey, key.count);
        }
    }
}