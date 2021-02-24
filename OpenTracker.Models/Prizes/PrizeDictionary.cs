using OpenTracker.Models.SaveLoad;
using OpenTracker.Utils;
using System.Collections.Generic;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This is the class for the dictionary of prizes.
    /// </summary>
    public class PrizeDictionary : LazyDictionary<PrizeType, IItem>,
        IPrizeDictionary
    {
        private readonly Item.Factory _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        /// A factory that creates prize items.
        /// </param>
        public PrizeDictionary(Item.Factory factory)
            : base(new Dictionary<PrizeType, IItem>())
        {
            _factory = factory;
        }

        protected override IItem Create(PrizeType key)
        {
            return _factory(0, null);
        }

        /// <summary>
        /// Resets all contained items to their starting values.
        /// </summary>
        public void Reset()
        {
            foreach (IItem item in Values)
            {
                item.Reset();
            }
        }

        /// <summary>
        /// Returns a dictionary of item save data.
        /// </summary>
        /// <returns>
        /// A dictionary of item save data.
        /// </returns>
        public Dictionary<PrizeType, ItemSaveData> Save()
        {
            var items = new Dictionary<PrizeType, ItemSaveData>();

            foreach (var type in Keys)
            {
                items.Add(type, this[type].Save());
            }

            return items;
        }

        /// <summary>
        /// Loads a dictionary of item save data.
        /// </summary>
        public void Load(Dictionary<PrizeType, ItemSaveData>? saveData)
        {
            if (saveData == null)
            {
                return;
            }

            foreach (var item in saveData.Keys)
            {
                this[item].Load(saveData[item]);
            }
        }
    }
}
