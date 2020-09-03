using OpenTracker.Models.AutoTracking.AutotrackValues;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This is the class for creating items.
    /// </summary>
    public static class ItemFactory
    {
        /// <summary>
        /// Returns the starting amount of the item.
        /// </summary>
        /// <param name="type">
        /// The type of item.
        /// </param>
        /// <returns>
        /// A 32-bit integer representing the starting amount of the item.
        /// </returns>
        private static int GetItemStarting(ItemType type)
        {
            return type == ItemType.Sword ? 1 : 0;
        }

        /// <summary>
        /// Returns the maximum amount of the item.
        /// </summary>
        /// <param name="type">
        /// The type of item.
        /// </param>
        /// <returns>
        /// A 32-bit integer representing the maximum amount of the item.
        /// </returns>
        private static int? GetItemMaximum(ItemType type)
        {
            switch (type)
            {
                case ItemType.Sword:
                    {
                        return 5;
                    }
                case ItemType.Shield:
                case ItemType.BombosDungeons:
                case ItemType.EtherDungeons:
                case ItemType.QuakeDungeons:
                case ItemType.SWSmallKey:
                case ItemType.MMSmallKey:
                    {
                        return 3;
                    }
                case ItemType.Mail:
                case ItemType.Arrows:
                case ItemType.Mushroom:
                case ItemType.Gloves:
                case ItemType.ATSmallKey:
                case ItemType.IPSmallKey:
                    {
                        return 2;
                    }
                case ItemType.Aga1:
                case ItemType.Bow:
                case ItemType.Boomerang:
                case ItemType.RedBoomerang:
                case ItemType.Hookshot:
                case ItemType.Bomb:
                case ItemType.BigBomb:
                case ItemType.Powder:
                case ItemType.MagicBat:
                case ItemType.Boots:
                case ItemType.FireRod:
                case ItemType.IceRod:
                case ItemType.Bombos:
                case ItemType.Ether:
                case ItemType.Quake:
                case ItemType.Lamp:
                case ItemType.Hammer:
                case ItemType.Flute:
                case ItemType.FluteActivated:
                case ItemType.Net:
                case ItemType.Book:
                case ItemType.Shovel:
                case ItemType.Flippers:
                case ItemType.CaneOfSomaria:
                case ItemType.CaneOfByrna:
                case ItemType.Cape:
                case ItemType.Mirror:
                case ItemType.HalfMagic:
                case ItemType.MoonPearl:
                case ItemType.Aga2:
                case ItemType.HCSmallKey:
                case ItemType.DPSmallKey:
                case ItemType.ToHSmallKey:
                case ItemType.SPSmallKey:
                case ItemType.TTSmallKey:
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
                    {
                        return 1;
                    }
                case ItemType.TowerCrystals:
                case ItemType.GanonCrystals:
                    {
                        return 7;
                    }
                case ItemType.SmallKey:
                    {
                        return 29;
                    }
                case ItemType.Bottle:
                case ItemType.TRSmallKey:
                case ItemType.GTSmallKey:
                    {
                        return 4;
                    }
                case ItemType.PoDSmallKey:
                    {
                        return 6;
                    }
            }

            return null;
        }

        /// <summary>
        /// Returns a finished item.
        /// </summary>
        /// <param name="type">
        /// The item type.
        /// </param>
        /// <returns>
        /// A finished item.
        /// </returns>
        public static IItem GetItem(ItemType type)
        {
            int? maximum = GetItemMaximum(type);

            if (maximum.HasValue)
            {
                return new CappedItem(
                    GetItemStarting(type), maximum.Value,
                    AutoTrackValueFactory.GetItemAutoTrackValue(type));
            }

            return new Item(
                GetItemStarting(type), AutoTrackValueFactory.GetItemAutoTrackValue(type));
        }
    }
}
