using OpenTracker.Models;
using OpenTracker.Models.Enums;
using System;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        private readonly AppSettingsVM _appSettings;
        private readonly Game _game;

        public ObservableCollection<MapControlVM> Maps { get; }
        public ObservableCollection<ItemControlVM> Items { get; }
        public ObservableCollection<PinnedLocationControlVM> PinnedLocations { get; }

        public MainWindowVM()
        {
            _appSettings = new AppSettingsVM();
            _game = new Game();

            Maps = new ObservableCollection<MapControlVM>();
            PinnedLocations = new ObservableCollection<PinnedLocationControlVM>();

            for (int i = 0; i < Enum.GetValues(typeof(MapID)).Length; i++)
                Maps.Add(new MapControlVM(_appSettings, _game, this, (MapID)i));

            Items = new ObservableCollection<ItemControlVM>();

            for (int i = 0; i < Enum.GetValues(typeof(ItemType)).Length; i++)
            {
                switch ((ItemType)i)
                {
                    case ItemType.Bow:
                    case ItemType.Boomerang:
                    case ItemType.Bomb:
                    case ItemType.Powder:
                    case ItemType.Bombos:
                    case ItemType.Ether:
                    case ItemType.Quake:
                    case ItemType.Flute:
                        Items.Add(new ItemControlVM(new Item[2] {
                            _game.Items[(ItemType)i], _game.Items[(ItemType)(i + 1)] }));
                        break;
                    case ItemType.MoonPearl:
                        Items.Add(new ItemControlVM(new Item[1] { _game.Items[(ItemType)i] }));
                        Items.Add(new ItemControlVM(null));
                        break;
                    case ItemType.Hookshot:
                    case ItemType.Mushroom:
                    case ItemType.TowerCrystals:
                    case ItemType.FireRod:
                    case ItemType.IceRod:
                    case ItemType.Shovel:
                    case ItemType.GanonCrystals:
                    case ItemType.Lamp:
                    case ItemType.Hammer:
                    case ItemType.Net:
                    case ItemType.Book:
                    case ItemType.Bottle:
                    case ItemType.CaneOfSomaria:
                    case ItemType.CaneOfByrna:
                    case ItemType.Cape:
                    case ItemType.Mirror:
                    case ItemType.GoMode:
                    case ItemType.Aga:
                    case ItemType.Gloves:
                    case ItemType.Boots:
                    case ItemType.Flippers:
                    case ItemType.HalfMagic:
                    case ItemType.Sword:
                    case ItemType.Shield:
                    case ItemType.Mail:
                        Items.Add(new ItemControlVM(new Item[1] { _game.Items[(ItemType)i] }));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
