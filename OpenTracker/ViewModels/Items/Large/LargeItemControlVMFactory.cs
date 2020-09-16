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
        private static LargeItemVMBase GetLargeItemControlVM(LargeItemType type)
        {
            return new LargeItemVM(
                $"avares://OpenTracker/Assets/Images/Items/{type.ToString().ToLowerInvariant()}",
                ItemDictionary.Instance[Enum.Parse<ItemType>(type.ToString())]);
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
        private static LargeItemVMBase GetPrizeLargeItemControlVM(LargeItemType type)
        {
            IPrizeSection section = type switch
            {
                LargeItemType.Aga1 => (IPrizeSection)LocationDictionary
                    .Instance[LocationID.AgahnimTower].Sections[1],
                LargeItemType.Aga2 => (IPrizeSection)LocationDictionary
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
        private static LargeItemVMBase GetCrystalRequirementLargeItemVM(LargeItemType type)
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
        private static LargeItemVMBase GetSmallKeyLargeItemVM(LargeItemType type)
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
        private static LargeItemVMBase GetPairLargeItemVM(LargeItemType type)
        {
            var itemType = Enum.Parse<ItemType>(type.ToString());

            return new PairLargeItemVM(
                $"avares://OpenTracker/Assets/Images/Items/{type.ToString().ToLowerInvariant()}",
                new IItem[]
                {
                    ItemDictionary.Instance[itemType],
                    ItemDictionary.Instance[itemType + 1]
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
