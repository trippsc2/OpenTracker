using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Sections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels.Items.Large
{
    /// <summary>
    /// This is the class containing creation logic for large item control ViewModel classes.
    /// </summary>
    public class LargeItemVMFactory : ILargeItemVMFactory
    {
        private readonly IItemDictionary _items;
        private readonly ILocationDictionary _locations;

        private readonly LargeItemVM.Factory _largeItemFactory;
        private readonly PrizeLargeItemVM.Factory _prizeFactory;
        private readonly CrystalRequirementLargeItemVM.Factory _crystalFactory;
        private readonly SmallKeyLargeItemVM.Factory _smallKeyFactory;
        private readonly PairLargeItemVM.Factory _pairFactory;

        public LargeItemVMFactory(
            IItemDictionary items, ILocationDictionary locations,
            LargeItemVM.Factory largeItemFactory, PrizeLargeItemVM.Factory prizeFactory,
            CrystalRequirementLargeItemVM.Factory crystalFactory,
            SmallKeyLargeItemVM.Factory smallKeyFactory, PairLargeItemVM.Factory pairFactory)
        {
            _items = items;
            _locations = locations;

            _largeItemFactory = largeItemFactory;
            _prizeFactory = prizeFactory;
            _crystalFactory = crystalFactory;
            _smallKeyFactory = smallKeyFactory;
            _pairFactory = pairFactory;
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
        private ILargeItemVMBase GetLargeItemControlVM(LargeItemType type)
        {
            return _largeItemFactory(
                _items[Enum.Parse<ItemType>(type.ToString())],
                $"avares://OpenTracker/Assets/Images/Items/{type.ToString().ToLowerInvariant()}");
        }

        /// <summary>
        /// Creates a new prize large item control ViewModel instance representing the specified
        /// item type.
        /// </summary>
        /// <param name="type">
        /// The item type to be represented.
        /// </param>
        /// <returns>
        /// A new prize large item control ViewModel instance.
        /// </returns>
        private ILargeItemVMBase GetPrizeLargeItemControlVM(LargeItemType type)
        {
            IPrizeSection section = type switch
            {
                LargeItemType.Aga1 =>
                    (IPrizeSection)_locations[LocationID.AgahnimTower].Sections[1],
                LargeItemType.Aga2 =>
                    (IPrizeSection)_locations[LocationID.GanonsTower].Sections[4],
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };

            return _prizeFactory(
                section,
                $"avares://OpenTracker/Assets/Images/Prizes/{type.ToString().ToLowerInvariant()}");
        }

        /// <summary>
        /// Creates a new crystal requirement large item control ViewModel instance representing
        /// the specified item type.
        /// </summary>
        /// <param name="type">
        /// The item type to be represented.
        /// </param>
        /// <returns>
        /// A new crystal requirement large item control ViewModel instance.
        /// </returns>
        private ILargeItemVMBase GetCrystalRequirementLargeItemVM(LargeItemType type)
        {
            return _crystalFactory(
                (ICrystalRequirementItem)_items[Enum.Parse<ItemType>(type.ToString())],
                $"avares://OpenTracker/Assets/Images/Items/{type.ToString().ToLowerInvariant()}.png");
        }

        /// <summary>
        /// Creates a new small key large item control ViewModel instance representing the
        /// specified item type.
        /// </summary>
        /// <param name="type">
        /// The item type to be represented.
        /// </param>
        /// <returns>
        /// A new small key large item control ViewModel instance.
        /// </returns>
        private ILargeItemVMBase GetSmallKeyLargeItemVM(LargeItemType type)
        {
            return _smallKeyFactory(
                _items[Enum.Parse<ItemType>(type.ToString())],
                $"avares://OpenTracker/Assets/Images/Items/{type.ToString().ToLowerInvariant()}");
        }

        /// <summary>
        /// Creates a new large item control ViewModel instance representing a pair of items
        /// starting at the specified item type.
        /// </summary>
        /// <param name="type">
        /// The first item type to be represented.
        /// </param>
        /// <returns>
        /// A new pair large item control ViewModel instance.
        /// </returns>
        private ILargeItemVMBase GetPairLargeItemVM(LargeItemType type)
        {
            var itemType = Enum.Parse<ItemType>(type.ToString());

            return _pairFactory(
                new IItem[]
                {
                    _items[itemType],
                    _items[itemType + 1]
                },
                $"avares://OpenTracker/Assets/Images/Items/{type.ToString().ToLowerInvariant()}");
        }

        /// <summary>
        /// Returns an observable collection of large item control ViewModel instances.
        /// </summary>
        /// <returns>
        /// An observable collection of large item control ViewModel instances.
        /// </returns>
        public List<ILargeItemVMBase> GetLargeItemControlVMs()
        {
            var largeItems = new List<ILargeItemVMBase>();

            for (int i = 0; i < Enum.GetValues(typeof(LargeItemType)).Length; i++)
            {
                switch ((LargeItemType)i)
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
                        {
                            largeItems.Add(GetLargeItemControlVM((LargeItemType)i));
                        }
                        break;
                    case LargeItemType.Aga1:
                    case LargeItemType.Aga2:
                        {
                            largeItems.Add(GetPrizeLargeItemControlVM((LargeItemType)i));
                        }
                        break;
                    case LargeItemType.TowerCrystals:
                    case LargeItemType.GanonCrystals:
                        {
                            largeItems.Add(GetCrystalRequirementLargeItemVM((LargeItemType)i));
                        }
                        break;
                    case LargeItemType.SmallKey:
                        {
                            largeItems.Add(GetSmallKeyLargeItemVM((LargeItemType)i));
                        }
                        break;
                    case LargeItemType.Bow:
                    case LargeItemType.Boomerang:
                    case LargeItemType.Bomb:
                    case LargeItemType.Powder:
                    case LargeItemType.Bombos:
                    case LargeItemType.Ether:
                    case LargeItemType.Quake:
                    case LargeItemType.Flute:
                        {
                            largeItems.Add(GetPairLargeItemVM((LargeItemType)i));
                        }
                        break;
                }
            }

            return largeItems;
        }
    }
}
