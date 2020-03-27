using Newtonsoft.Json;
using OpenTracker.Interfaces;
using OpenTracker.JsonConverters;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reactive;

namespace OpenTracker.ViewModels
{
    public class MainWindowVM : ViewModelBase, IMainWindowVM
    {
        private readonly IDialogService _dialogService;
        private readonly Game _game;

        public ObservableCollection<MapControlVM> Maps { get; }
        public ObservableCollection<ItemControlVM> Items { get; }
        public ObservableCollection<PinnedLocationControlVM> PinnedLocations { get; }
        public ObservableCollection<DungeonItemControlVM> HCItems { get; }
        public ObservableCollection<DungeonItemControlVM> ATItems { get; }
        public ObservableCollection<DungeonItemControlVM> SmallKeyItems { get; }
        public ObservableCollection<DungeonItemControlVM> BigKeyItems { get; }
        public ObservableCollection<DungeonPrizeControlVM> Prizes { get; }
        public ObservableCollection<BossControlVM> Bosses { get; }

        public ReactiveCommand<Unit, Unit> ToggleDisplayAllLocationsCommand { get; }

        private AppSettingsVM _appSettings;
        public AppSettingsVM AppSettings
        {
            get => _appSettings;
            set => this.RaiseAndSetIfChanged(ref _appSettings, value);
        }

        private bool _modeSettingsPopupOpen;
        public bool ModeSettingsPopupOpen
        {
            get => _modeSettingsPopupOpen;
            set => this.RaiseAndSetIfChanged(ref _modeSettingsPopupOpen, value);
        }

        private bool _itemPlacementBasic;
        public bool ItemPlacementBasic
        {
            get => _itemPlacementBasic;
            set => this.RaiseAndSetIfChanged(ref _itemPlacementBasic, value);
        }

        private bool _itemPlacementAdvanced;
        public bool ItemPlacementAdvanced
        {
            get => _itemPlacementAdvanced;
            set => this.RaiseAndSetIfChanged(ref _itemPlacementAdvanced, value);
        }

        private bool _dungeonItemShuffleStandard;
        public bool DungeonItemShuffleStandard
        {
            get => _dungeonItemShuffleStandard;
            set => this.RaiseAndSetIfChanged(ref _dungeonItemShuffleStandard, value);
        }

        private bool _dungeonItemShuffleMapsCompasses;
        public bool DungeonItemShuffleMapsCompasses
        {
            get => _dungeonItemShuffleMapsCompasses;
            set => this.RaiseAndSetIfChanged(ref _dungeonItemShuffleMapsCompasses, value);
        }

        private bool _dungeonItemShuffleMapsCompassesSmallKeys;
        public bool DungeonItemShuffleMapsCompassesSmallKeys
        {
            get => _dungeonItemShuffleMapsCompassesSmallKeys;
            set => this.RaiseAndSetIfChanged(ref _dungeonItemShuffleMapsCompassesSmallKeys, value);
        }

        private bool _dungeonItemShuffleKeysanity;
        public bool DungeonItemShuffleKeysanity
        {
            get => _dungeonItemShuffleKeysanity;
            set => this.RaiseAndSetIfChanged(ref _dungeonItemShuffleKeysanity, value);
        }

        private bool _worldStateStandardOpen;
        public bool WorldStateStandardOpen
        {
            get => _worldStateStandardOpen;
            set => this.RaiseAndSetIfChanged(ref _worldStateStandardOpen, value);
        }

        private bool _worldStateInverted;
        public bool WorldStateInverted
        {
            get => _worldStateInverted;
            set => this.RaiseAndSetIfChanged(ref _worldStateInverted, value);
        }

        private bool _entranceShuffle;
        public bool EntranceShuffle
        {
            get => _entranceShuffle;
            set => this.RaiseAndSetIfChanged(ref _entranceShuffle, value);
        }

        private bool _bossShuffle;
        public bool BossShuffle
        {
            get => _bossShuffle;
            set => this.RaiseAndSetIfChanged(ref _bossShuffle, value);
        }

        private bool _enemyShuffle;
        public bool EnemyShuffle
        {
            get => _enemyShuffle;
            set => this.RaiseAndSetIfChanged(ref _enemyShuffle, value);
        }

        private bool _smallKeyShuffle;
        public bool SmallKeyShuffle
        {
            get => _smallKeyShuffle;
            set => this.RaiseAndSetIfChanged(ref _smallKeyShuffle, value);
        }

        private bool _bigKeyShuffle;
        public bool BigKeyShuffle
        {
            get => _bigKeyShuffle;
            set => this.RaiseAndSetIfChanged(ref _bigKeyShuffle, value);
        }

        public MainWindowVM(IDialogService dialogService) : this()
        {
            _dialogService = dialogService;
        }

        public MainWindowVM()
        {
            ToggleDisplayAllLocationsCommand = ReactiveCommand.Create(ToggleDisplayAllLocations);

            string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            if (!Directory.Exists(appData))
                Directory.CreateDirectory(appData);

            string appSettingsPath = appData + Path.DirectorySeparatorChar + "opentracker.json";

            if (File.Exists(appSettingsPath))
            {
                string jsonContent = File.ReadAllText(appSettingsPath);

                _appSettings = JsonConvert.DeserializeObject<AppSettingsVM>(jsonContent, new SolidColorBrushConverter());
            }
            else
                _appSettings = new AppSettingsVM();

            _game = new Game();

            RefreshItemPlacement();
            RefreshDungeonItemShuffle();
            RefreshWorldState();
            RefreshEntranceShuffle();
            RefreshBossShuffle();
            RefreshEnemyShuffle();

            Maps = new ObservableCollection<MapControlVM>();
            PinnedLocations = new ObservableCollection<PinnedLocationControlVM>();
            HCItems = new ObservableCollection<DungeonItemControlVM>()
            {
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.HCSmallKey])
            };
            ATItems = new ObservableCollection<DungeonItemControlVM>()
            {
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.ATSmallKey])
            };
            SmallKeyItems = new ObservableCollection<DungeonItemControlVM>()
            {
                new DungeonItemControlVM(_appSettings, _game.Mode, null),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.DPSmallKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.ToHSmallKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.PoDSmallKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.SPSmallKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.SWSmallKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.TTSmallKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.IPSmallKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.MMSmallKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.TRSmallKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.GTSmallKey])
            };
            BigKeyItems = new ObservableCollection<DungeonItemControlVM>()
            {
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.EPBigKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.DPBigKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.ToHBigKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.PoDBigKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.SPBigKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.SWBigKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.TTBigKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.IPBigKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.MMBigKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.TRBigKey]),
                new DungeonItemControlVM(_appSettings, _game.Mode, _game.Items[ItemType.GTBigKey])
            };
            Prizes = new ObservableCollection<DungeonPrizeControlVM>()
            {
                new DungeonPrizeControlVM(_game, _game.Locations[LocationID.EasternPalace].BossSection),
                new DungeonPrizeControlVM(_game, _game.Locations[LocationID.DesertPalace].BossSection),
                new DungeonPrizeControlVM(_game, _game.Locations[LocationID.TowerOfHera].BossSection),
                new DungeonPrizeControlVM(_game, _game.Locations[LocationID.PalaceOfDarkness].BossSection),
                new DungeonPrizeControlVM(_game, _game.Locations[LocationID.SwampPalace].BossSection),
                new DungeonPrizeControlVM(_game, _game.Locations[LocationID.SkullWoods].BossSection),
                new DungeonPrizeControlVM(_game, _game.Locations[LocationID.ThievesTown].BossSection),
                new DungeonPrizeControlVM(_game, _game.Locations[LocationID.IcePalace].BossSection),
                new DungeonPrizeControlVM(_game, _game.Locations[LocationID.MiseryMire].BossSection),
                new DungeonPrizeControlVM(_game, _game.Locations[LocationID.TurtleRock].BossSection)
            };
            Bosses = new ObservableCollection<BossControlVM>()
            {
                new BossControlVM(_game, _game.Locations[LocationID.EasternPalace].BossSection),
                new BossControlVM(_game, _game.Locations[LocationID.DesertPalace].BossSection),
                new BossControlVM(_game, _game.Locations[LocationID.TowerOfHera].BossSection),
                new BossControlVM(_game, _game.Locations[LocationID.PalaceOfDarkness].BossSection),
                new BossControlVM(_game, _game.Locations[LocationID.SwampPalace].BossSection),
                new BossControlVM(_game, _game.Locations[LocationID.SkullWoods].BossSection),
                new BossControlVM(_game, _game.Locations[LocationID.ThievesTown].BossSection),
                new BossControlVM(_game, _game.Locations[LocationID.IcePalace].BossSection),
                new BossControlVM(_game, _game.Locations[LocationID.MiseryMire].BossSection),
                new BossControlVM(_game, _game.Locations[LocationID.TurtleRock].BossSection)
            };

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

            PropertyChanged += OnPropertyChanged;
        }

        private void ToggleDisplayAllLocations()
        {
            _appSettings.DisplayAllLocations = !_appSettings.DisplayAllLocations;
        }

        private async void Reset()
        {
            bool? result = await _dialogService.ShowDialog(
                new MessageBoxVM("Warning",
                "Resetting the tracker will set all items and locations back to their starting values.  This cannot be undone.\nDo you wish to proceed?"));

            if (result.HasValue && result.Value)
                _game.Reset();
        }

        private void RefreshItemPlacement()
        {
            switch (_game.Mode.ItemPlacement)
            {
                case null:
                    ItemPlacementBasic = false;
                    ItemPlacementAdvanced = false;
                    break;
                case ItemPlacement.Basic:
                    ItemPlacementBasic = true;
                    ItemPlacementAdvanced = false;
                    break;
                case ItemPlacement.Advanced:
                    ItemPlacementBasic = false;
                    ItemPlacementAdvanced = true;
                    break;
            }
        }

        private void RefreshDungeonItemShuffle()
        {
            switch (_game.Mode.DungeonItemShuffle)
            {
                case null:
                    DungeonItemShuffleStandard = false;
                    DungeonItemShuffleMapsCompasses = false;
                    DungeonItemShuffleMapsCompassesSmallKeys = false;
                    DungeonItemShuffleKeysanity = false;
                    break;
                case DungeonItemShuffle.Standard:
                    DungeonItemShuffleStandard = true;
                    DungeonItemShuffleMapsCompasses = false;
                    DungeonItemShuffleMapsCompassesSmallKeys = false;
                    DungeonItemShuffleKeysanity = false;
                    break;
                case DungeonItemShuffle.MapsCompasses:
                    DungeonItemShuffleStandard = false;
                    DungeonItemShuffleMapsCompasses = true;
                    DungeonItemShuffleMapsCompassesSmallKeys = false;
                    DungeonItemShuffleKeysanity = false;
                    break;
                case DungeonItemShuffle.MapsCompassesSmallKeys:
                    DungeonItemShuffleStandard = false;
                    DungeonItemShuffleMapsCompasses = false;
                    DungeonItemShuffleMapsCompassesSmallKeys = true;
                    DungeonItemShuffleKeysanity = false;
                    break;
                case DungeonItemShuffle.Keysanity:
                    DungeonItemShuffleStandard = false;
                    DungeonItemShuffleMapsCompasses = false;
                    DungeonItemShuffleMapsCompassesSmallKeys = false;
                    DungeonItemShuffleKeysanity = true;
                    break;
            }
        }

        private void RefreshWorldState()
        {
            switch (_game.Mode.WorldState)
            {
                case null:
                    WorldStateStandardOpen = false;
                    WorldStateInverted = false;
                    break;
                case WorldState.StandardOpen:
                    WorldStateStandardOpen = true;
                    WorldStateInverted = false;
                    break;
                case WorldState.Inverted:
                    WorldStateStandardOpen = false;
                    WorldStateInverted = true;
                    break;
            }
        }

        private void RefreshEntranceShuffle()
        {
            if (_game.Mode.EntranceShuffle.HasValue && _game.Mode.EntranceShuffle.Value)
                EntranceShuffle = true;
            else
                EntranceShuffle = false;
        }

        private void RefreshBossShuffle()
        {
            if (_game.Mode.BossShuffle.HasValue && _game.Mode.BossShuffle.Value)
                BossShuffle = true;
            else
                BossShuffle = false;
        }

        private void RefreshEnemyShuffle()
        {
            if (_game.Mode.EnemyShuffle.HasValue && _game.Mode.EnemyShuffle.Value)
                EnemyShuffle = true;
            else
                EnemyShuffle = false;
        }

        private void SetItemPlacement(ItemPlacement itemPlacement)
        {
            _game.Mode.ItemPlacement = itemPlacement;
        }

        private void SetDungeonItemShuffle(DungeonItemShuffle dungeonItemShuffle)
        {
            _game.Mode.DungeonItemShuffle = dungeonItemShuffle;

            if (dungeonItemShuffle >= DungeonItemShuffle.MapsCompassesSmallKeys)
                SmallKeyShuffle = true;
            else
                SmallKeyShuffle = false;

            if (dungeonItemShuffle == DungeonItemShuffle.Keysanity)
                BigKeyShuffle = true;
            else
                BigKeyShuffle = false;
        }

        private void SetWorldState(WorldState worldState)
        {
            _game.Mode.WorldState = worldState;
        }

        private void SetEntranceShuffle(bool entranceShuffle)
        {
            _game.Mode.EntranceShuffle = entranceShuffle;
        }

        private void SetBossShuffle(bool bossShuffle)
        {
            _game.Mode.BossShuffle = bossShuffle;
        }

        private void SetEnemyShuffle(bool enemyShuffle)
        {
            _game.Mode.EnemyShuffle = enemyShuffle;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ItemPlacementBasic) && ItemPlacementBasic)
                SetItemPlacement(ItemPlacement.Basic);

            if (e.PropertyName == nameof(ItemPlacementAdvanced) && ItemPlacementAdvanced)
                SetItemPlacement(ItemPlacement.Advanced);

            if (e.PropertyName == nameof(DungeonItemShuffleStandard) && DungeonItemShuffleStandard)
                SetDungeonItemShuffle(DungeonItemShuffle.Standard);

            if (e.PropertyName == nameof(DungeonItemShuffleMapsCompasses) && DungeonItemShuffleMapsCompasses)
                SetDungeonItemShuffle(DungeonItemShuffle.MapsCompasses);

            if (e.PropertyName == nameof(DungeonItemShuffleMapsCompassesSmallKeys) && DungeonItemShuffleMapsCompassesSmallKeys)
                SetDungeonItemShuffle(DungeonItemShuffle.MapsCompassesSmallKeys);

            if (e.PropertyName == nameof(DungeonItemShuffleKeysanity) && DungeonItemShuffleKeysanity)
                SetDungeonItemShuffle(DungeonItemShuffle.Keysanity);

            if (e.PropertyName == nameof(WorldStateStandardOpen) && WorldStateStandardOpen)
                SetWorldState(WorldState.StandardOpen);

            if (e.PropertyName == nameof(WorldStateInverted) && WorldStateInverted)
                SetWorldState(WorldState.Inverted);

            if (e.PropertyName == nameof(EntranceShuffle))
                SetEntranceShuffle(EntranceShuffle);

            if (e.PropertyName == nameof(BossShuffle))
                SetBossShuffle(BossShuffle);

            if (e.PropertyName == nameof(EnemyShuffle))
                SetEnemyShuffle(EnemyShuffle);
        }

        public void SaveAppSettings()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            if (!Directory.Exists(appData))
                Directory.CreateDirectory(appData);

            string appSettingsPath = appData + Path.DirectorySeparatorChar + "opentracker.json";

            if (File.Exists(appSettingsPath))
                File.Delete(appSettingsPath);

            string json = JsonConvert.SerializeObject(_appSettings, Formatting.Indented, new SolidColorBrushConverter());

            File.WriteAllText(appSettingsPath, json);
        }
    }
}
