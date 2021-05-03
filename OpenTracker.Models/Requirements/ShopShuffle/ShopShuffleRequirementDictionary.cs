using System.Collections.Generic;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.ShopShuffle
{
    /// <summary>
    ///     This class contains the dictionary container for shop shuffle requirements.
    /// </summary>
    public class ShopShuffleRequirementDictionary : LazyDictionary<bool, IRequirement>, IShopShuffleRequirementDictionary
    {
        private readonly IShopShuffleRequirement.Factory _factory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="factory">
        ///     An Autofac factory for creating new shop shuffle requirements.
        /// </param>
        public ShopShuffleRequirementDictionary(IShopShuffleRequirement.Factory factory)
            : base(new Dictionary<bool, IRequirement>())
        {
            _factory = factory;
        }

        protected override IRequirement Create(bool key)
        {
            return _factory(key);
        }
    }
}