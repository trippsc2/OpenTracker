using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Sections;
using System;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels.Items.Large
{
    /// <summary>
    /// This is the class containing creation logic for large item control ViewModel classes.
    /// </summary>
    internal static class LargeItemControlVMFactory
    {
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
        private static LargeItemVMBase GetLargeItemControlVM(ItemType type)
        {
            return new LargeItemVM(
                $"avares://OpenTracker/Assets/Images/Items/{type.ToString().ToLowerInvariant()}",
                ItemDictionary.Instance[Enum.Parse<ItemType>(type.ToString())]);
        }

        /// <summary>
        /// Creates a new prize large item control ViewModel instance representing the specified
        /// item type.
        /// </summary>
        /// <param name="id">
        /// The location identity to be represented.
        /// </param>
        /// <returns>
        /// A new prize large item control ViewModel instance.
        /// </returns>
        private static LargeItemVMBase GetPrizeLargeItemControlVM(ItemType type)
        {
            IPrizeSection section = type switch
            {
                ItemType.Aga1 => (IPrizeSection)LocationDictionary
                    .Instance[LocationID.AgahnimTower].Sections[1],
                ItemType.Aga2 => (IPrizeSection)LocationDictionary
                    .Instance[LocationID.GanonsTower].Sections[4],
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };

            return new PrizeLargeItemVM(
                $"avares://OpenTracker/Assets/Images/Prizes/{type.ToString().ToLowerInvariant()}",
                section);
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
        private static LargeItemVMBase GetCrystalRequirementLargeItemVM(ItemType type)
        {
            return new CrystalRequirementLargeItemVM(
                $"avares://OpenTracker/Assets/Images/Items/{type.ToString().ToLowerInvariant()}.png",
                ItemDictionary.Instance[Enum.Parse<ItemType>(type.ToString())]);
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
        private static LargeItemVMBase GetSmallKeyLargeItemVM(ItemType type)
        {
            return new SmallKeyLargeItemVM(
                $"avares://OpenTracker/Assets/Images/Items/{type.ToString().ToLowerInvariant()}",
                ItemDictionary.Instance[Enum.Parse<ItemType>(type.ToString())]);
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
        private static LargeItemVMBase GetPairLargeItemVM(ItemType type)
        {
            return new PairLargeItemVM(
                $"avares://OpenTracker/Assets/Images/Items/{type.ToString().ToLowerInvariant()}",
                new IItem[]
                {
                    ItemDictionary.Instance[type],
                    ItemDictionary.Instance[type + 1]
                });
        }

        /// <summary>
        /// Returns an observable collection of large item control ViewModel instances.
        /// </summary>
        /// <returns>
        /// An observable collection of large item control ViewModel instances.
        /// </returns>
        internal static ObservableCollection<LargeItemVMBase> GetLargeItemControlVMs()
        {
            var largeItems = new ObservableCollection<LargeItemVMBase>();

            for (int i = 0; i < Enum.GetValues(typeof(ItemType)).Length; i++)
            {
                switch ((ItemType)i)
                {
                    case ItemType.Sword:
                    case ItemType.Shield:
                    case ItemType.Mail:
                    case ItemType.Hookshot:
                    case ItemType.Mushroom:
                    case ItemType.Boots:
                    case ItemType.FireRod:
                    case ItemType.IceRod:
                    case ItemType.Gloves:
                    case ItemType.Lamp:
                    case ItemType.Hammer:
                    case ItemType.Net:
                    case ItemType.Book:
                    case ItemType.Shovel:
                    case ItemType.Flippers:
                    case ItemType.Bottle:
                    case ItemType.CaneOfSomaria:
                    case ItemType.CaneOfByrna:
                    case ItemType.Cape:
                    case ItemType.Mirror:
                    case ItemType.HalfMagic:
                    case ItemType.MoonPearl:
                        {
                            largeItems.Add(GetLargeItemControlVM((ItemType)i));
                        }
                        break;
                    case ItemType.Aga1:
                    case ItemType.Aga2:
                        {
                            largeItems.Add(GetPrizeLargeItemControlVM((ItemType)i));
                        }
                        break;
                    case ItemType.TowerCrystals:
                    case ItemType.GanonCrystals:
                        {
                            largeItems.Add(GetCrystalRequirementLargeItemVM((ItemType)i));
                        }
                        break;
                    case ItemType.SmallKey:
                        {
                            largeItems.Add(GetSmallKeyLargeItemVM((ItemType)i));
                        }
                        break;
                    case ItemType.Bow:
                    case ItemType.Boomerang:
                    case ItemType.Bomb:
                    case ItemType.Powder:
                    case ItemType.Bombos:
                    case ItemType.Ether:
                    case ItemType.Quake:
                    case ItemType.Flute:
                        {
                            largeItems.Add(GetPairLargeItemVM((ItemType)i));
                        }
                        break;
                }
            }

            return largeItems;
        }
    }
}
