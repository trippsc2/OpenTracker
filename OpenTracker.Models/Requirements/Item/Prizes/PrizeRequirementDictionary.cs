using System.Collections.Generic;
using OpenTracker.Models.Prizes;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.Item.Prizes
{
    /// <summary>
    ///     This class contains the dictionary container for prize requirements.
    /// </summary>
    public class PrizeRequirementDictionary : LazyDictionary<(PrizeType type, int count), IRequirement>,
        IPrizeRequirementDictionary
    {
        private readonly IPrizeDictionary _prizes;

        private readonly IItemRequirement.Factory _factory;
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="prizes">
        ///     The prize dictionary.
        /// </param>
        /// <param name="factory">
        ///     An Autofac factory for creating new item requirements.
        /// </param>
        public PrizeRequirementDictionary(IPrizeDictionary prizes, IItemRequirement.Factory factory)
            : base(new Dictionary<(PrizeType type, int count), IRequirement>())
        {
            _prizes = prizes;
            
            _factory = factory;
        }
        
        protected override IRequirement Create((PrizeType type, int count) key)
        {
            return _factory(_prizes[key.type], key.count);
        }
    }
}