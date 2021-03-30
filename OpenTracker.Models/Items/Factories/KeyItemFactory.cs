using System;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Items.Keys;

namespace OpenTracker.Models.Items.Factories
{
    /// <summary>
    ///     This class contains the creation logic for key items.
    /// </summary>
    public class KeyItemFactory : IKeyItemFactory
    {
        private readonly IItemDictionary _items;

        private readonly ISmallKeyItem.Factory _smallKeyFactory;
        private readonly IBigKeyItem.Factory _bigKeyFactory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="items">
        ///     The item dictionary.
        /// </param>
        /// <param name="smallKeyFactory">
        ///     An Autofac factory for creating small keys.
        /// </param>
        /// <param name="bigKeyFactory">
        ///     An Autofac factory for creating big keys.
        /// </param>
        public KeyItemFactory(
            IItemDictionary items, ISmallKeyItem.Factory smallKeyFactory, IBigKeyItem.Factory bigKeyFactory)
        {
            _items = items;
            
            _smallKeyFactory = smallKeyFactory;
            _bigKeyFactory = bigKeyFactory;
        }

        public IItem GetItem(ItemType type, IAutoTrackValue? autoTrackValue)
        {
            var nonKeyDropMaximum = GetItemNonKeyDropMaximum(type);
            var keyDropMaximum = GetItemKeyDropMaximum(type) ?? nonKeyDropMaximum;
            
            switch (type)
            {
                case ItemType.HCSmallKey:
                case ItemType.EPSmallKey:
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
                    return _smallKeyFactory(
                        _items[ItemType.SmallKey], nonKeyDropMaximum, keyDropMaximum, autoTrackValue);
                case ItemType.HCBigKey:
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
                    return _bigKeyFactory(nonKeyDropMaximum, keyDropMaximum, autoTrackValue);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        
        /// <summary>
        ///     Returns the maximum value when key drop shuffle is disabled.
        /// </summary>
        /// <param name="type">
        ///     The type of item.
        /// </param>
        /// <returns>
        ///     A 32-bit integer representing the maximum value.
        /// </returns>
        private static int GetItemNonKeyDropMaximum(ItemType type)
        {
            switch (type)
            {
                case ItemType.SWSmallKey:
                case ItemType.MMSmallKey:
                    return 3;
                case ItemType.ATSmallKey:
                case ItemType.IPSmallKey:
                    return 2;
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
                    return 1;
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

        /// <summary>
        ///     Returns the maximum value when key drop shuffle is enabled.
        /// </summary>
        /// <param name="type">
        ///     The type of item.
        /// </param>
        /// <returns>
        ///     A 32-bit integer representing the maximum value.
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
                    return 2;
                case ItemType.ToHSmallKey:
                case ItemType.HCBigKey:
                    return 1;
                case ItemType.PoDSmallKey:
                case ItemType.SPSmallKey:
                case ItemType.IPSmallKey:
                case ItemType.MMSmallKey:
                case ItemType.TRSmallKey:
                    return 6;
                case ItemType.SWSmallKey:
                    return 5;
                case ItemType.TTSmallKey:
                    return 3;
                case ItemType.GTSmallKey:
                    return 8;
            }

            return null;
        }

    }
}