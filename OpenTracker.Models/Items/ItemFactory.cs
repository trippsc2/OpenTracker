using System;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This class contains creation logic for item data.
    /// </summary>
    public class ItemFactory : IItemFactory
    {
        private readonly Lazy<IItemDictionary> _items;
        private readonly Lazy<IItemAutoTrackValueFactory> _autoTrackValueFactory;

        private readonly IItem.Factory _itemFactory;
        private readonly ICappedItem.Factory _cappedItemFactory;
        private readonly ICrystalRequirementItem.Factory _crystalRequirementFactory;
        private readonly IKeyItem.Factory _keyFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="items">
        /// An Autofac factory for creating the item dictionary.
        /// </param>
        /// <param name="autoTrackValueFactory">
        /// An Autofac factory for creating auto-track values.
        /// </param>
        /// <param name="itemFactory">
        /// An Autofac factory for creating items.
        /// </param>
        /// <param name="cappedItemFactory">
        /// An Autofac factory for creating items with maximums.
        /// </param>
        /// <param name="crystalRequirementFactory">
        /// An Autofac factory for creating crystal requirement items.
        /// </param>
        /// <param name="keyFactory">
        /// An Autofac factory for creating key items.
        /// </param>
        public ItemFactory(
            IItemDictionary.Factory items, IItemAutoTrackValueFactory.Factory autoTrackValueFactory,
            IItem.Factory itemFactory, ICappedItem.Factory cappedItemFactory,
            ICrystalRequirementItem.Factory crystalRequirementFactory, IKeyItem.Factory keyFactory)
        {
            _items = new Lazy<IItemDictionary>(items());
            _autoTrackValueFactory = new Lazy<IItemAutoTrackValueFactory>(autoTrackValueFactory());

            _itemFactory = itemFactory;
            _cappedItemFactory = cappedItemFactory;
            _crystalRequirementFactory = crystalRequirementFactory;
            _keyFactory = keyFactory;
        }

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
                    return 5;
                case ItemType.Shield:
                case ItemType.BombosDungeons:
                case ItemType.EtherDungeons:
                case ItemType.QuakeDungeons:
                case ItemType.SWSmallKey:
                case ItemType.MMSmallKey:
                    return 3;
                case ItemType.Mail:
                case ItemType.Arrows:
                case ItemType.Mushroom:
                case ItemType.Gloves:
                case ItemType.ATSmallKey:
                case ItemType.IPSmallKey:
                    return 2;
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
                case ItemType.HCMap:
                case ItemType.EPMap:
                case ItemType.DPMap:
                case ItemType.ToHMap:
                case ItemType.PoDMap:
                case ItemType.SPMap:
                case ItemType.SWMap:
                case ItemType.TTMap:
                case ItemType.IPMap:
                case ItemType.MMMap:
                case ItemType.TRMap:
                case ItemType.GTMap:
                case ItemType.EPCompass:
                case ItemType.DPCompass:
                case ItemType.ToHCompass:
                case ItemType.PoDCompass:
                case ItemType.SPCompass:
                case ItemType.SWCompass:
                case ItemType.TTCompass:
                case ItemType.IPCompass:
                case ItemType.MMCompass:
                case ItemType.TRCompass:
                case ItemType.GTCompass:
                    return 1;
                case ItemType.SmallKey:
                    return 29;
                case ItemType.Bottle:
                case ItemType.TRSmallKey:
                case ItemType.GTSmallKey:
                    return 4;
                case ItemType.PoDSmallKey:
                    return 6;
                case ItemType.HCBigKey:
                case ItemType.EPSmallKey:
                    return 0;
            }

            return null;
        }

        /// <summary>
        /// Returns the delta maximum when key drop shuffle is enabled.
        /// </summary>
        /// <param name="type">
        /// The type of item.
        /// </param>
        /// <returns>
        /// A 32-bit integer representing the delta maximum amount of the item.
        /// </returns>
        private static int? GetItemKeyDropMaximum(ItemType type)
        {
            switch (type)
            {
                case ItemType.HCSmallKey:
                case ItemType.ATSmallKey:
                case ItemType.DPSmallKey:
                    return 4;
                case ItemType.EPSmallKey:
                    return 3;
                case ItemType.PoDSmallKey:
                case ItemType.SPSmallKey:
                case ItemType.TTSmallKey:
                case ItemType.IPSmallKey:
                case ItemType.MMSmallKey:
                case ItemType.TRSmallKey:
                    return 6;
                case ItemType.SWSmallKey:
                    return 5;
                case ItemType.GTSmallKey:
                    return 8;
                case ItemType.HCBigKey:
                case ItemType.ToHSmallKey:
                    return 1;
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
        public IItem GetItem(ItemType type)
        {
            if (type == ItemType.TowerCrystals || type == ItemType.GanonCrystals)
            {
                return _crystalRequirementFactory();
            }

            var maximum = GetItemMaximum(type);

            if (maximum is null)
            {
                return _itemFactory(GetItemStarting(type), _autoTrackValueFactory.Value.GetAutoTrackValue(type));
            }
            
            var keyDropMaximum = GetItemKeyDropMaximum(type);

            if (keyDropMaximum is null)
            {
                return _cappedItemFactory(
                    GetItemStarting(type), maximum.Value, _autoTrackValueFactory.Value.GetAutoTrackValue(type));
            }
            
            return _keyFactory(
                _items.Value[ItemType.SmallKey], maximum.Value, keyDropMaximum.Value,
                _autoTrackValueFactory.Value.GetAutoTrackValue(type));
        }
    }
}
