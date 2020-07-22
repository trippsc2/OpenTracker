using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Sections;
using System;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels.LargeItemControls
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
        private static LargeItemControlVMBase GetLargeItemControlVM(ItemType type)
        {
            return new LargeItemControlVM(ItemDictionary.Instance[type]);
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
        private static LargeItemControlVMBase GetPrizeLargeItemControlVM(ItemType type)
        {
            IPrizeSection section = type switch
            {
                ItemType.Aga1 => ((IDungeon)LocationDictionary.Instance[LocationID.AgahnimTower])
                    .GetPrizeSection(),
                ItemType.Aga2 => ((IDungeon)LocationDictionary.Instance[LocationID.GanonsTower])
                    .GetPrizeSection(),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };

            return new PrizeLargeItemControlVM(section);
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
        private static LargeItemControlVMBase GetCrystalRequirementLargeItemControlVM(ItemType type)
        {
            return new CrystalRequirementLargeItemControlVM(ItemDictionary.Instance[type]);
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
        private static LargeItemControlVMBase GetSmallKeyLargeItemControlVM(ItemType type)
        {
            return new SmallKeyLargeItemControlVM(ItemDictionary.Instance[type]);
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
        private static LargeItemControlVMBase GetPairLargeItemControlVM(ItemType type)
        {
            return new PairLargeItemControlVM(
                new IItem[]
                {
                    ItemDictionary.Instance[type],
                    ItemDictionary.Instance[type + 1]
                });
        }

        /// <summary>
        /// Populates the observable collection of large item control ViewModels.
        /// </summary>
        /// <param name="largeItems">
        /// The observable collection of large item control ViewModels to be populated.
        /// </param>
        internal static void GetLargeItemControlVMs(ObservableCollection<LargeItemControlVMBase> largeItems)
        {
            if (largeItems == null)
            {
                throw new ArgumentNullException(nameof(largeItems));
            }

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
                            largeItems.Add(GetCrystalRequirementLargeItemControlVM((ItemType)i));
                        }
                        break;
                    case ItemType.SmallKey:
                        {
                            largeItems.Add(GetSmallKeyLargeItemControlVM((ItemType)i));
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
                            largeItems.Add(GetPairLargeItemControlVM((ItemType)i));
                        }
                        break;
                }
            }
        }
    }
}
