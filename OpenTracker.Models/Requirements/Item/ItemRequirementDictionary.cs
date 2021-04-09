using System.Collections.Generic;
using OpenTracker.Models.Items;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.Item
{
    /// <summary>
    ///     This class contains the dictionary container for item requirements.
    /// </summary>
    public class ItemRequirementDictionary : LazyDictionary<(ItemType type, int count), IRequirement>, IItemRequirementDictionary
    {
        private readonly IItemDictionary _items;

        private readonly IItemRequirement.Factory _factory;
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="items">
        ///     The items dictionary.
        /// </param>
        /// <param name="factory">
        ///     An Autofac factory for creating item requirements.
        /// </param>
        public ItemRequirementDictionary(IItemDictionary items, IItemRequirement.Factory factory)
            : base(new Dictionary<(ItemType type, int count), IRequirement>())
        {
            _items = items;
            
            _factory = factory;
        }

        protected override IRequirement Create((ItemType type, int count) key)
        {
            return _factory(_items[key.type], key.count);
        }
    }
}