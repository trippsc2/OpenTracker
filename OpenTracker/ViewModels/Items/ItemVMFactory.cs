using System;
using System.Collections.Generic;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Sections;
using OpenTracker.ViewModels.Items.Adapters;

namespace OpenTracker.ViewModels.Items
{
    /// <summary>
    /// This class contains the creation logic for large item controls.
    /// </summary>
    public class ItemVMFactory : IItemVMFactory
    {
        private readonly IItemDictionary _items;
        private readonly ILocationDictionary _locations;
        private readonly IRequirementDictionary _requirements;
        private readonly IItemVMDictionary _itemControls;

        private readonly ILargeItemVM.Factory _factory;
        private readonly IItemVM.Factory _itemFactory;
        private readonly CrystalRequirementAdapter.Factory _crystalFactory;
        private readonly ItemAdapter.Factory _adapterFactory;
        private readonly PairItemAdapter.Factory _pairFactory;
        private readonly StaticPrizeAdapter.Factory _prizeFactory;
        private readonly SmallKeyAdapter.Factory _smallKeyFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="items">
        /// The item dictionary.
        /// </param>
        /// <param name="locations">
        /// The location dictionary.
        /// </param>
        /// <param name="requirements">
        /// The requirement dictionary.
        /// </param>
        /// <param name="itemControls">
        /// The item control dictionary.
        /// </param>
        /// <param name="factory">
        /// An Autofac factory for creating large item controls.
        /// </param>
        /// <param name="itemFactory">
        /// An Autofac factory for creating item controls.
        /// </param>
        /// <param name="crystalFactory">
        /// An Autofac factory for creating crystal requirement item adapters.
        /// </param>
        /// <param name="adapterFactory">
        /// An Autofac factory for creating item adapters.
        /// </param>
        /// <param name="pairFactory">
        /// An Autofac factory for creating item pair adapters.
        /// </param>
        /// <param name="prizeFactory">
        /// An Autofac factory for creating prize adapters.
        /// </param>
        /// <param name="smallKeyFactory">
        /// An Autofac factory for creating small key adapters.
        /// </param>
        public ItemVMFactory(
            IItemDictionary items, ILocationDictionary locations, IRequirementDictionary requirements,
            IItemVMDictionary itemControls, ILargeItemVM.Factory factory, IItemVM.Factory itemFactory,
            CrystalRequirementAdapter.Factory crystalFactory, ItemAdapter.Factory adapterFactory,
            PairItemAdapter.Factory pairFactory, StaticPrizeAdapter.Factory prizeFactory,
            SmallKeyAdapter.Factory smallKeyFactory)
        {
            _items = items;
            _locations = locations;
            _requirements = requirements;
            _itemControls = itemControls;

            _factory = factory;
            _itemFactory = itemFactory;
            _crystalFactory = crystalFactory;
            _adapterFactory = adapterFactory;
            _pairFactory = pairFactory;
            _prizeFactory = prizeFactory;
            _smallKeyFactory = smallKeyFactory;
        }

        /// <summary>
        /// Creates a new basic large item control ViewModel instance representing the specified
        /// item type.
        /// </summary>
        /// <param name="type">
        /// The item type to be represented.
        /// </param>
        /// <returns>
        /// A new basic large item control ViewModel instance.
        /// </returns>
        private IItemVM GetLargeItemControlVM(LargeItemType type)
        {
            return _itemFactory(
                _adapterFactory(
                    _items[Enum.Parse<ItemType>(type.ToString())],
                    $"avares://OpenTracker/Assets/Images/Items/{type.ToString().ToLowerInvariant()}"),
                _requirements[RequirementType.NoRequirement]);
        }

        /// <summary>
        /// Creates a new prize large item control representing the specified item type.
        /// </summary>
        /// <param name="type">
        /// The item type to be represented.
        /// </param>
        /// <returns>
        /// A new prize large item control.
        /// </returns>
        private IItemVM GetPrizeLargeItemControlVM(LargeItemType type)
        {
            IPrizeSection section = type switch
            {
                LargeItemType.Aga1 =>
                    (IPrizeSection)_locations[LocationID.AgahnimTower].Sections[1],
                LargeItemType.Aga2 =>
                    (IPrizeSection)_locations[LocationID.GanonsTower].Sections[4],
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };

            return _itemFactory(
                _prizeFactory(
                    section,
                    $"avares://OpenTracker/Assets/Images/Prizes/{type.ToString().ToLowerInvariant()}"),
                _requirements[RequirementType.NoRequirement]);
        }

        /// <summary>
        /// Creates a new crystal requirement large item control representing the specified item type.
        /// </summary>
        /// <param name="type">
        /// The item type to be represented.
        /// </param>
        /// <returns>
        /// A new crystal requirement large item control.
        /// </returns>
        private IItemVM GetCrystalRequirementLargeItemVM(LargeItemType type)
        {
            return _itemFactory(_crystalFactory(
                    (ICrystalRequirementItem)_items[Enum.Parse<ItemType>(type.ToString())],
                    $"avares://OpenTracker/Assets/Images/Items/{type.ToString().ToLowerInvariant()}.png"),
                _requirements[RequirementType.NoRequirement]);
        }

        /// <summary>
        /// Creates a new small key large item control representing the specified item type.
        /// </summary>
        /// <param name="type">
        /// The item type to be represented.
        /// </param>
        /// <returns>
        /// A new small key large item control.
        /// </returns>
        private IItemVM GetSmallKeyLargeItemVM(LargeItemType type)
        {
            return _itemFactory(_smallKeyFactory(
                _items[Enum.Parse<ItemType>(type.ToString())]),
                _requirements[RequirementType.GenericKeys]);
        }

        /// <summary>
        /// Creates a new large item control representing a pair of items starting at the specified item type.
        /// </summary>
        /// <param name="type">
        /// The first item type to be represented.
        /// </param>
        /// <returns>
        /// A new pair large item control.
        /// </returns>
        private IItemVM GetPairLargeItemVM(LargeItemType type)
        {
            var itemType = Enum.Parse<ItemType>(type.ToString());

            return _itemFactory(_pairFactory(
                    new[]
                    {
                        _items[itemType],
                        _items[itemType + 1]
                    },
                    $"avares://OpenTracker/Assets/Images/Items/{type.ToString().ToLowerInvariant()}"),
                _requirements[RequirementType.NoRequirement]);
        }

        /// <summary>
        /// Creates a new large item control of the specified type.
        /// </summary>
        /// <param name="type">
        /// The item type to be represented.
        /// </param>
        /// <returns>
        /// A new large item control.
        /// </returns>
        public IItemVM GetLargeItemVM(LargeItemType type)
        {
            switch (type)
            {
                case LargeItemType.Sword:
                case LargeItemType.Shield:
                case LargeItemType.Mail:
                case LargeItemType.Hookshot:
                case LargeItemType.Mushroom:
                case LargeItemType.Boots:
                case LargeItemType.FireRod:
                case LargeItemType.IceRod:
                case LargeItemType.Gloves:
                case LargeItemType.Lamp:
                case LargeItemType.Hammer:
                case LargeItemType.Net:
                case LargeItemType.Book:
                case LargeItemType.Shovel:
                case LargeItemType.Flippers:
                case LargeItemType.Bottle:
                case LargeItemType.CaneOfSomaria:
                case LargeItemType.CaneOfByrna:
                case LargeItemType.Cape:
                case LargeItemType.Mirror:
                case LargeItemType.HalfMagic:
                case LargeItemType.MoonPearl:
                    return GetLargeItemControlVM(type);
                case LargeItemType.Aga1:
                case LargeItemType.Aga2:
                    return GetPrizeLargeItemControlVM(type);
                case LargeItemType.TowerCrystals:
                case LargeItemType.GanonCrystals:
                    return GetCrystalRequirementLargeItemVM(type);
                case LargeItemType.SmallKey:
                    return GetSmallKeyLargeItemVM(type);
                case LargeItemType.Bow:
                case LargeItemType.Boomerang:
                case LargeItemType.Bomb:
                case LargeItemType.Powder:
                case LargeItemType.Bombos:
                case LargeItemType.Ether:
                case LargeItemType.Quake:
                case LargeItemType.Flute:
                    return GetPairLargeItemVM(type);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        /// <summary>
        /// Returns an observable collection of large item control ViewModel instances.
        /// </summary>
        /// <returns>
        /// An observable collection of large item control ViewModel instances.
        /// </returns>
        public List<ILargeItemVM> GetLargeItemControlVMs()
        {
            var largeItems = new List<ILargeItemVM>();

            for (var i = 0; i < Enum.GetValues(typeof(LargeItemType)).Length; i++)
            {
                largeItems.Add(_factory(_itemControls[(LargeItemType)i]));
            }

            return largeItems;
        }
    }
}
