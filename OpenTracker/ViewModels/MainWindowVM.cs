using Newtonsoft.Json;
using OpenTracker.Actions;
using OpenTracker.Interfaces;
using OpenTracker.JsonConverters;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Reactive;
using System.Threading.Tasks;

namespace OpenTracker.ViewModels
{
    public class MainWindowVM : ViewModelBase, IMainWindowVM
    {
        private readonly IDialogService _dialogService;
        private readonly UndoRedoManager _undoRedoManager;
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

        public ReactiveCommand<Unit, Unit> UndoCommand { get; }
        public ReactiveCommand<Unit, Unit> RedoCommand { get; }
        public ReactiveCommand<Unit, Unit> ToggleDisplayAllLocationsCommand { get; }
        public ReactiveCommand<string, Unit> ItemPlacementCommand { get; }
        public ReactiveCommand<string, Unit> DungeonItemShuffleCommand { get; }
        public ReactiveCommand<string, Unit> WorldStateCommand { get; }
        public ReactiveCommand<Unit, Unit> EntranceShuffleCommand { get; }
        public ReactiveCommand<Unit, Unit> BossShuffleCommand { get; }
        public ReactiveCommand<Unit, Unit> EnemyShuffleCommand { get; }

        private bool _canUndo;
        public bool CanUndo
        {
            get => _canUndo;
            private set => this.RaiseAndSetIfChanged(ref _canUndo, value);
        }

        private bool _canRedo;
        public bool CanRedo
        {
            get => _canRedo;
            private set => this.RaiseAndSetIfChanged(ref _canRedo, value);
        }

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
            _undoRedoManager = new UndoRedoManager();

            _undoRedoManager.UndoableActions.CollectionChanged += OnUndoChanged;
            _undoRedoManager.RedoableActions.CollectionChanged += OnRedoChanged;

            UndoCommand = ReactiveCommand.Create(Undo, this.WhenAnyValue(x => x.CanUndo));
            RedoCommand = ReactiveCommand.Create(Redo, this.WhenAnyValue(x => x.CanRedo));
            ToggleDisplayAllLocationsCommand = ReactiveCommand.Create(ToggleDisplayAllLocations);
            ItemPlacementCommand = ReactiveCommand.Create<string>(SetItemPlacement);
            DungeonItemShuffleCommand = ReactiveCommand.Create<string>(SetDungeonItemShuffle);
            WorldStateCommand = ReactiveCommand.Create<string>(SetWorldState);
            EntranceShuffleCommand = ReactiveCommand.Create(ToggleEntranceShuffle);
            BossShuffleCommand = ReactiveCommand.Create(ToggleBossShuffle);
            EnemyShuffleCommand = ReactiveCommand.Create(ToggleEnemyShuffle);

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
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Mode, _game.Items[ItemType.HCSmallKey])
            };
            ATItems = new ObservableCollection<DungeonItemControlVM>()
            {
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Mode, _game.Items[ItemType.ATSmallKey])
            };
            SmallKeyItems = new ObservableCollection<DungeonItemControlVM>()
            {
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Mode, null),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Mode, _game.Items[ItemType.DPSmallKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Mode, _game.Items[ItemType.ToHSmallKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Mode, _game.Items[ItemType.PoDSmallKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Mode, _game.Items[ItemType.SPSmallKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Mode, _game.Items[ItemType.SWSmallKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Mode, _game.Items[ItemType.TTSmallKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Mode, _game.Items[ItemType.IPSmallKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Mode, _game.Items[ItemType.MMSmallKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Mode, _game.Items[ItemType.TRSmallKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Mode, _game.Items[ItemType.GTSmallKey])
            };
            BigKeyItems = new ObservableCollection<DungeonItemControlVM>()
            {
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Mode, _game.Items[ItemType.EPBigKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Mode, _game.Items[ItemType.DPBigKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Mode, _game.Items[ItemType.ToHBigKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Mode, _game.Items[ItemType.PoDBigKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Mode, _game.Items[ItemType.SPBigKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Mode, _game.Items[ItemType.SWBigKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Mode, _game.Items[ItemType.TTBigKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Mode, _game.Items[ItemType.IPBigKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Mode, _game.Items[ItemType.MMBigKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Mode, _game.Items[ItemType.TRBigKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Mode, _game.Items[ItemType.GTBigKey])
            };
            Prizes = new ObservableCollection<DungeonPrizeControlVM>()
            {
                new DungeonPrizeControlVM(_undoRedoManager, _game, _game.Locations[LocationID.EasternPalace].BossSection),
                new DungeonPrizeControlVM(_undoRedoManager, _game, _game.Locations[LocationID.DesertPalace].BossSection),
                new DungeonPrizeControlVM(_undoRedoManager, _game, _game.Locations[LocationID.TowerOfHera].BossSection),
                new DungeonPrizeControlVM(_undoRedoManager, _game, _game.Locations[LocationID.PalaceOfDarkness].BossSection),
                new DungeonPrizeControlVM(_undoRedoManager, _game, _game.Locations[LocationID.SwampPalace].BossSection),
                new DungeonPrizeControlVM(_undoRedoManager, _game, _game.Locations[LocationID.SkullWoods].BossSection),
                new DungeonPrizeControlVM(_undoRedoManager, _game, _game.Locations[LocationID.ThievesTown].BossSection),
                new DungeonPrizeControlVM(_undoRedoManager, _game, _game.Locations[LocationID.IcePalace].BossSection),
                new DungeonPrizeControlVM(_undoRedoManager, _game, _game.Locations[LocationID.MiseryMire].BossSection),
                new DungeonPrizeControlVM(_undoRedoManager, _game, _game.Locations[LocationID.TurtleRock].BossSection)
            };
            Bosses = new ObservableCollection<BossControlVM>()
            {
                new BossControlVM(_undoRedoManager, _game, _game.Locations[LocationID.EasternPalace].BossSection),
                new BossControlVM(_undoRedoManager, _game, _game.Locations[LocationID.DesertPalace].BossSection),
                new BossControlVM(_undoRedoManager, _game, _game.Locations[LocationID.TowerOfHera].BossSection),
                new BossControlVM(_undoRedoManager, _game, _game.Locations[LocationID.PalaceOfDarkness].BossSection),
                new BossControlVM(_undoRedoManager, _game, _game.Locations[LocationID.SwampPalace].BossSection),
                new BossControlVM(_undoRedoManager, _game, _game.Locations[LocationID.SkullWoods].BossSection),
                new BossControlVM(_undoRedoManager, _game, _game.Locations[LocationID.ThievesTown].BossSection),
                new BossControlVM(_undoRedoManager, _game, _game.Locations[LocationID.IcePalace].BossSection),
                new BossControlVM(_undoRedoManager, _game, _game.Locations[LocationID.MiseryMire].BossSection),
                new BossControlVM(_undoRedoManager, _game, _game.Locations[LocationID.TurtleRock].BossSection)
            };

            for (int i = 0; i < Enum.GetValues(typeof(MapID)).Length; i++)
                Maps.Add(new MapControlVM(_undoRedoManager, _appSettings, _game, this, (MapID)i));

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
                        Items.Add(new ItemControlVM(_undoRedoManager, new Item[2] {
                            _game.Items[(ItemType)i], _game.Items[(ItemType)(i + 1)]
                        }));
                        break;
                    case ItemType.MoonPearl:
                        Items.Add(new ItemControlVM(_undoRedoManager, new Item[1] { _game.Items[(ItemType)i] }));
                        Items.Add(new ItemControlVM(_undoRedoManager, null));
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
                        Items.Add(new ItemControlVM(_undoRedoManager, new Item[1] { _game.Items[(ItemType)i] }));
                        break;
                    default:
                        break;
                }
            }

            _game.Mode.PropertyChanged += OnModeChanged;
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
                    SmallKeyShuffle = false;
                    BigKeyShuffle = false;
                    break;
                case DungeonItemShuffle.Standard:
                    DungeonItemShuffleStandard = true;
                    DungeonItemShuffleMapsCompasses = false;
                    DungeonItemShuffleMapsCompassesSmallKeys = false;
                    DungeonItemShuffleKeysanity = false;
                    SmallKeyShuffle = false;
                    BigKeyShuffle = false;
                    break;
                case DungeonItemShuffle.MapsCompasses:
                    DungeonItemShuffleStandard = false;
                    DungeonItemShuffleMapsCompasses = true;
                    DungeonItemShuffleMapsCompassesSmallKeys = false;
                    DungeonItemShuffleKeysanity = false;
                    SmallKeyShuffle = false;
                    BigKeyShuffle = false;
                    break;
                case DungeonItemShuffle.MapsCompassesSmallKeys:
                    DungeonItemShuffleStandard = false;
                    DungeonItemShuffleMapsCompasses = false;
                    DungeonItemShuffleMapsCompassesSmallKeys = true;
                    DungeonItemShuffleKeysanity = false;
                    SmallKeyShuffle = true;
                    BigKeyShuffle = false;
                    break;
                case DungeonItemShuffle.Keysanity:
                    DungeonItemShuffleStandard = false;
                    DungeonItemShuffleMapsCompasses = false;
                    DungeonItemShuffleMapsCompassesSmallKeys = false;
                    DungeonItemShuffleKeysanity = true;
                    SmallKeyShuffle = true;
                    BigKeyShuffle = true;
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

        private void SetItemPlacement(string itemPlacementString)
        {
            if (Enum.TryParse(itemPlacementString, out ItemPlacement itemPlacement))
                _undoRedoManager.Execute(new ChangeItemPlacement(_game.Mode, itemPlacement));
        }

        private void SetDungeonItemShuffle(string dungeonItemShuffleString)
        {
            if (Enum.TryParse(dungeonItemShuffleString, out DungeonItemShuffle dungeonItemShuffle))
                _undoRedoManager.Execute(new ChangeDungeonItemShuffle(_game.Mode, dungeonItemShuffle));
        }

        private void SetWorldState(string worldStateString)
        {
            if (Enum.TryParse(worldStateString, out WorldState worldState))
                _undoRedoManager.Execute(new ChangeWorldState(_game.Mode, worldState));
        }

        private void ToggleEntranceShuffle()
        {
            _undoRedoManager.Execute(new ChangeEntranceShuffle(_game.Mode, !_game.Mode.EntranceShuffle.Value));
        }

        private void ToggleBossShuffle()
        {
            _undoRedoManager.Execute(new ChangeBossShuffle(_game.Mode, !_game.Mode.BossShuffle.Value));
        }

        private void ToggleEnemyShuffle()
        {
            _undoRedoManager.Execute(new ChangeEnemyShuffle(_game.Mode, !_game.Mode.EnemyShuffle.Value));
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_game.Mode.ItemPlacement))
                RefreshItemPlacement();

            if (e.PropertyName == nameof(_game.Mode.DungeonItemShuffle))
                RefreshDungeonItemShuffle();

            if (e.PropertyName == nameof(_game.Mode.WorldState))
                RefreshWorldState();

            if (e.PropertyName == nameof(_game.Mode.EntranceShuffle))
                RefreshEntranceShuffle();

            if (e.PropertyName == nameof(_game.Mode.BossShuffle))
                RefreshBossShuffle();

            if (e.PropertyName == nameof(_game.Mode.EnemyShuffle))
                RefreshEnemyShuffle();
        }

        private void OnUndoChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateCanUndo();
        }

        private void OnRedoChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateCanRedo();
        }

        private void UpdateCanUndo()
        {
            CanUndo = _undoRedoManager.CanUndo();
        }

        private void Undo()
        {
            _undoRedoManager.Undo();
        }

        private void UpdateCanRedo()
        {
            CanRedo = _undoRedoManager.CanRedo();
        }

        private void Redo()
        {
            _undoRedoManager.Redo();
        }

        public void Save(string path)
        {
            if (File.Exists(path))
                File.Delete(path);

            SaveData saveData = new SaveData(_game);

            string json = JsonConvert.SerializeObject(saveData);

            File.WriteAllText(path, json);
        }

        public void Open(string path)
        {
            string jsonContent = File.ReadAllText(path);

            SaveData saveData = JsonConvert.DeserializeObject<SaveData>(jsonContent);

            _game.Mode.ItemPlacement = saveData.Mode.ItemPlacement;
            _game.Mode.DungeonItemShuffle = saveData.Mode.DungeonItemShuffle;
            _game.Mode.WorldState = saveData.Mode.WorldState;
            _game.Mode.EntranceShuffle = saveData.Mode.EntranceShuffle;
            _game.Mode.BossShuffle = saveData.Mode.BossShuffle;
            _game.Mode.EnemyShuffle = saveData.Mode.EnemyShuffle;

            foreach (ItemType item in saveData.ItemCounts.Keys)
                _game.Items[item].SetCurrent(saveData.ItemCounts[item]);

            foreach (LocationID location in saveData.LocationSectionCounts.Keys)
            {
                foreach (int i in saveData.LocationSectionCounts[location].Keys)
                {
                    switch (_game.Locations[location].Sections[i])
                    {
                        case BossSection bossSection:

                            bossSection.Available = saveData.LocationSectionCounts[location][i] == 1;

                            break;
                        case EntranceSection entranceSection:

                            entranceSection.Available = saveData.LocationSectionCounts[location][i] == 1;

                            break;
                        case ItemSection itemSection:

                            itemSection.Available = saveData.LocationSectionCounts[location][i];

                            break;
                    }
                }
            }

            foreach (LocationID location in saveData.LocationSectionMarkings.Keys)
            {
                foreach (int i in saveData.LocationSectionMarkings[location].Keys)
                {
                    _game.Locations[location].Sections[i].Marking =
                        saveData.LocationSectionMarkings[location][i];
                }
            }

            foreach (LocationID location in saveData.PrizePlacements.Keys)
            {
                if (saveData.PrizePlacements[location] == null)
                    _game.Locations[location].BossSection.Prize = null;
                else
                {
                    _game.Locations[location].BossSection.Prize =
                        _game.Items[saveData.PrizePlacements[location].Value];
                }
            }

            foreach (LocationID location in saveData.BossPlacements.Keys)
            {
                if (saveData.BossPlacements[location] == null)
                    _game.Locations[location].BossSection.Boss = null;
                else
                {
                    _game.Locations[location].BossSection.Boss =
                        _game.Bosses[saveData.BossPlacements[location].Value];
                }
            }
        }

        private void ToggleDisplayAllLocations()
        {
            _appSettings.DisplayAllLocations = !_appSettings.DisplayAllLocations;
        }

        public async Task Reset()
        {
            bool? result = await _dialogService.ShowDialog(
                new MessageBoxDialogVM("Warning",
                "Resetting the tracker will set all items and locations back to their starting values.  This cannot be undone.\nDo you wish to proceed?"));

            if (result.HasValue && result.Value)
                _game.Reset();
        }

        public async Task ColorSelect()
        {
            bool? result = await _dialogService.ShowDialog(_appSettings);
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
