using System.Collections.Generic;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.Boss
{
    /// <summary>
    /// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="IBossRequirement"/>
    /// objects indexed by <see cref="BossPlacementID"/>.
    /// </summary>
    public class BossRequirementDictionary : LazyDictionary<BossPlacementID, IRequirement>, IBossRequirementDictionary
    {
        private readonly IBossPlacementDictionary _bossPlacements;

        private readonly IBossRequirement.Factory _factory;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bossPlacements">
        ///     The <see cref="IBossPlacementDictionary"/>.
        /// </param>
        /// <param name="factory">
        ///     An Autofac factory for creating new <see cref="IBossRequirement"/> objects.
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