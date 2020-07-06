using OpenTracker.Models;
using OpenTracker.Models.Enums;
using OpenTracker.Models.Items;
using OpenTracker.ViewModels.Bases;
using System;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels.Factories
{
    public static class LargeItemControlVMFactory
    {
        internal static void GetLargeItemControlVMs(
            UndoRedoManager undoRedoManager, AppSettings appSettings,
            ObservableCollection<LargeItemControlVMBase> largeItems)
        {
            if (undoRedoManager == null)
            {
                throw new ArgumentNullException(nameof(undoRedoManager));
            }

            if (appSettings == null)
            {
                throw new ArgumentNullException(nameof(appSettings));
            }

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
                    case ItemType.Aga:
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
                            largeItems.Add(new LargeItemControlVM(
                                undoRedoManager, ItemDictionary.Instance[(ItemType)i]));
                        }
                        break;
                    case ItemType.Mail:
                        {
                            largeItems.Add(new LargeItemControlVM(
                                undoRedoManager, ItemDictionary.Instance[(ItemType)i]));
                            largeItems.Add(new LargeItemSpacerControlVM());
                        }
                        break;
                    case ItemType.TowerCrystals:
                    case ItemType.GanonCrystals:
                        {
                            largeItems.Add(new CrystalRequirementControlVM(
                                undoRedoManager, appSettings, ItemDictionary.Instance[(ItemType)i]));
                        }
                        break;
                    case ItemType.SmallKey:
                        {
                            largeItems.Add(new GenericSmallKeyControlVM(
                                undoRedoManager, appSettings, ItemDictionary.Instance[(ItemType)i]));
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
                            largeItems.Add(new LargeItemPairControlVM(
                                undoRedoManager, new IItem[2]
                                {
                                    ItemDictionary.Instance[(ItemType)i],
                                    ItemDictionary.Instance[(ItemType)(i + 1)]
                                }));
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
