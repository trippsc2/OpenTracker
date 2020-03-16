using OpenTracker.Models.Enums;
using System.Collections.Generic;

namespace OpenTracker.Models
{
    public class ItemDictionary : Dictionary<ItemType, Item>
    {
        private readonly Mode _mode;
        public ItemDictionary(Mode mode, int size) : base(size)
        {
            _mode = mode;
        }

        public bool Has(ItemType type, int atLeast = 1)
        {
            switch (type)
            {
                case ItemType.Mushroom:
                    return this[type].Current == 1;
                case ItemType.TowerCrystals:
                case ItemType.GanonCrystals:
                    return this[type].Current + this[ItemType.Crystal].Current + this[ItemType.RedCrystal].Current >= 7;
                case ItemType.Sword when atLeast == 0:
                    return this[type].Current == 0;
                case ItemType.HCSmallKey:
                case ItemType.DPSmallKey:
                case ItemType.ToHSmallKey:
                case ItemType.ATSmallKey:
                case ItemType.PoDSmallKey:
                case ItemType.SPSmallKey:
                case ItemType.SWSmallKey:
                case ItemType.TTSmallKey:
                case ItemType.IPSmallKey:
                case ItemType.MMSmallKey:
                case ItemType.TRSmallKey:
                case ItemType.GTSmallKey:
                    return _mode.DungeonItemShuffle < DungeonItemShuffle.MapsCompassesSmallKeys || this[type].Current >= atLeast;
                case ItemType.EPBigKey:
                case ItemType.DPBigKey:
                case ItemType.ToHBigKey:
                case ItemType.PoDBigKey:
                case ItemType.SPBigKey:
                case ItemType.SWBigKey:
                case ItemType.TTBigKey:
                case ItemType.IPBigKey:
                case ItemType.MMBigKey:
                case ItemType.TRBigKey:
                case ItemType.GTBigKey:
                    return _mode.DungeonItemShuffle < DungeonItemShuffle.Keysanity || this[type].Current >= atLeast;
                default:
                    return this[type].Current >= atLeast;
            }
        }

        public bool Swordless()
        {
            return this[ItemType.Sword].Current == 0;
        }
    }
}
