using System.Collections.Generic;
using OpenTracker.Models.Modes;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.Mode
{
    /// <summary>
    ///     This class contains the dictionary container for the item placement requirements.
    /// </summary>
    public class ItemPlacementRequirementDictionary : LazyDictionary<ItemPlacement, IRequirement>, IItemPlacementRequirementDictionary
    {
        private readonly IItemPlacementRequirement.Factory _factory;
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="factory">
        ///     An Autofac factory for creating new item placement requirements.
        /// </param>
        public ItemPlacementRequirementDictionary(IItemPlacementRequirement.Factory factory)
            : base(new Dictionary<ItemPlacement, IRequirement>())
        {
            _factory = factory;
        }

        protected override IRequirement Create(ItemPlacement key)
        {
            return _factory(key);
        }
    }
}