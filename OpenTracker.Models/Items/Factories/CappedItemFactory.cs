using System;
using OpenTracker.Models.AutoTracking.Values;

namespace OpenTracker.Models.Items.Factories
{
    /// <summary>
    ///     This class contains the creation logic for items with maximum values.
    /// </summary>
    public class CappedItemFactory : ICappedItemFactory
    {
        private readonly ICappedItem.Factory _factory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="factory">
        ///     An Autofac factory for creating items with maximum values.
        /// </param>
        public CappedItemFactory(ICappedItem.Factory factory)
        {
            _factory = factory;
        }

        /// <summary>
        ///     Returns a new item.
        /// </summary>
        /// <param name="type">
        ///     The item type.
        /// </param>
        /// <param name="starting">
        ///     A 32-bit signed integer representing the starting value.
        /// </param>
        /// <param name="autoTrackValue">
        ///     The auto-track value for the item.
        /// </param>
        /// <returns>
        ///     A new item.
        /// </returns>
        public IItem GetItem(ItemType type, int starting, IAutoTrackValue? autoTrackValue)
        {
            return _factory(starting, GetItemMaximum(type), autoTrackValue);
        }
        
        /// <summary>
        ///     Returns the maximum amount of the item.
        /// </summary>
        /// <param name="type">
        ///     The type of item.
        /// </param>
        /// <returns>
        ///     A 32-bit integer representing the maximum amount of the item.
        /// </returns>
        private static int GetItemMaximum(ItemType type)
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

            throw new ArgumentOutOfRangeException(nameof(type));
        }
    }
}