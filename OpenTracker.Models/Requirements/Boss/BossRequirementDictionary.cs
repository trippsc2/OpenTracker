using System.Collections.Generic;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.Boss
{
    /// <summary>
    ///     This class contains the dictionary container for boss requirements.
    /// </summary>
    public class BossRequirementDictionary : LazyDictionary<BossPlacementID, IRequirement>, IBossRequirementDictionary
    {
        private readonly IBossPlacementDictionary _bossPlacements;

        private readonly IBossRequirement.Factory _factory;
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="bossPlacements">
        ///     The boss placement dictionary.
        /// </param>
        /// <param name="factory">
        ///     An Autofac factory for creating new boss requirements.
        /// </param>
        public BossRequirementDictionary(IBossPlacementDictionary bossPlacements, IBossRequirement.Factory factory)
            : base(new Dictionary<BossPlacementID, IRequirement>())
        {
            _bossPlacements = bossPlacements;
            
            _factory = factory;
        }

        protected override IRequirement Create(BossPlacementID key)
        {
            return _factory(_bossPlacements[key]);
        }
    }
}