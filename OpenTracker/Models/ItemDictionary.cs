using OpenTracker.Enums;
using System.Collections.Generic;

namespace OpenTracker.Models
{
    public class ItemDictionary : Dictionary<ItemType, Item>
    {
        public ItemDictionary(int size) : base(size)
        {
        }

        public bool Has(ItemType type, int atLeast = 1)
        {
            if (type == ItemType.Mushroom)
                return this[type].Current == 1;

            if (type == ItemType.Sword && atLeast == 0)
                return this[type].Current == 0;

            return this[type].Current >= atLeast;
        }

        public bool Swordless()
        {
            return this[ItemType.Sword].Current == 0;
        }
    }
}
