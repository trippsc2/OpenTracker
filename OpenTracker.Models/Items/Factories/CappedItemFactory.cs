using System;
using OpenTracker.Models.AutoTracking.Values;

namespace OpenTracker.Models.Items.Factories
{
    /// <summary>
    /// This class contains the creation logic for <see cref="ICappedItem"/> objects.
    /// </summary>
    public class CappedItemFactory : ICappedItemFactory
    {
        private readonly ICappedItem.Factory _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        ///     An Autofac factory for creating <see cref="ICappedItem"/> objects.
        /// </param>
        public CappedItemFactory(ICappedItem.Factory factory)
        {
            _factory = factory;
        }

        public IItem GetItem(ItemType type, int starting, IAutoTrackValue? autoTrackValue)
        {
            return _factory(starting, GetItemMaximum(type), autoTrackValue);
        }
        
        /// <summary>
        /// Returns the item maximum.
        /// </summary>
        /// <param name="type">
        ///     The <see cref="ItemType"/>.
        /// </param>
        /// <returns>
        ///     A <see cref="int"/> representing the item maximum.
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
                    return 3;
                case ItemType.Mail:
                case ItemType.Arrows:
                case ItemType.Mushroom:
                case ItemType.Gloves:
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
                    return 4;
            }

            throw new ArgumentOutOfRangeException(nameof(type));
        }
    }
}