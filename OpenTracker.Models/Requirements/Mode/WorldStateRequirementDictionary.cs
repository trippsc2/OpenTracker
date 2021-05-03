using System.Collections.Generic;
using OpenTracker.Models.Modes;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.Mode
{
    /// <summary>
    ///     This class contains the dictionary container for world state requirements.
    /// </summary>
    public class WorldStateRequirementDictionary : LazyDictionary<WorldState, IRequirement>, IWorldStateRequirementDictionary
    {
        private readonly IWorldStateRequirement.Factory _factory;
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="factory">
        ///     An Autofac factory for creating new world state requirements.
        /// </param>
        public WorldStateRequirementDictionary(IWorldStateRequirement.Factory factory)
            : base(new Dictionary<WorldState, IRequirement>())
        {
            _factory = factory;
        }

        protected override IRequirement Create(WorldState key)
        {
            return _factory(key);
        }
    }
}