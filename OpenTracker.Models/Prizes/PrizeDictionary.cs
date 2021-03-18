using System.Collections.Generic;
using OpenTracker.Models.Items;
using OpenTracker.Utils;

namespace OpenTracker.Models.Prizes
{
    /// <summary>
    /// This class contains the dictionary container for prize data.
    /// </summary>
    public class PrizeDictionary : LazyDictionary<PrizeType, IItem>, IPrizeDictionary
    {
        private readonly IItem.Factory _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        /// An Autofac factory for creating prize items.
        /// </param>
        public PrizeDictionary(IItem.Factory factory) : base(new Dictionary<PrizeType, IItem>())
        {
            _factory = factory;
        }

        protected override IItem Create(PrizeType key)
        {
            return _factory(0, null);
        }
    }
}
