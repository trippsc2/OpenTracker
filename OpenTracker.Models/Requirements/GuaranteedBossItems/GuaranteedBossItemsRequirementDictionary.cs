using System.Collections.Generic;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.GuaranteedBossItems
{
    /// <summary>
    ///     This class contains the dictionary container for guaranteed boss items requirements.
    /// </summary>
    public class GuaranteedBossItemsRequirementDictionary : LazyDictionary<bool, IRequirement>, IGuaranteedBossItemsRequirementDictionary
    {
        private readonly IGuaranteedBossItemsRequirement.Factory _factory;
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="factory">
        ///     An Autofac factory for creating new guaranteed boss items requirements.
        /// </param>
        public GuaranteedBossItemsRequirementDictionary(IGuaranteedBossItemsRequirement.Factory factory)
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