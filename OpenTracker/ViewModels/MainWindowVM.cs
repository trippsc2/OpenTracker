using Avalonia;
using Avalonia.ThemeManager;
using Avalonia.Threading;
using Newtonsoft.Json;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
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
    public class MainWindowVM : ViewModelBase, IAutoTrackerAccess, IColorSelectAccess,
        IOpen, ISave, ISaveAppSettings, IAppSettings, IBounds
    {
        private readonly IDialogService _dialogService;
        private readonly UndoRedoManager _undoRedoManager;
        private readonly AppSettings _appSettings;
        private readonly Game _game;

        public bool? Maximized
        {
            get => _appSettings.Maximized;
            set => _appSettings.Maximized = value;
        }

        public double? X
        {
            get => _appSettings.X;
            set => _appSettings.X = value;
        }

        public double? Y
        {
            get => _appSettings.Y;
            set => _appSettings.Y = value;
        }

        public double? Width
        {
            get => _appSettings.Width;
            set => _appSettings.Width = value;
        }

        public double? Height
        {
            get => _appSettings.Height;
            set => _appSettings.Height = value;
        }

        public ModeSettingsControlVM ModeSettings { get; }
        public ThemeSelector Selector { get; }

        public ObservableCollection<MapControlVM> Maps { get; }
        public ObservableCollection<ItemControlVM> Items { get; }
        public ObservableCollection<LocationControlVM> Locations { get; }
        public ObservableCollection<DungeonItemControlVM> HCItems { get; }
        public ObservableCollection<DungeonItemControlVM> ATItems { get; }
        public ObservableCollection<DungeonItemControlVM> SmallKeyItems { get; }
        public ObservableCollection<DungeonItemControlVM> BigKeyItems { get; }
        public ObservableCollection<DungeonPrizeControlVM> Prizes { get; }
        public ObservableCollection<BossControlVM> Bosses { get; }

        public ReactiveCommand<Unit, Unit> UndoCommand { get; }
        public ReactiveCommand<Unit, Unit> RedoCommand { get; }
        public ReactiveCommand<Unit, Unit> ToggleDisplayAllLocationsCommand { get; }
        public ReactiveCommand<Unit, Unit> ToggleShowItemCountsOnMapCommand { get; }
        public ReactiveCommand<string, Unit> SetLayoutOrientationCommand { get; }
        public ReactiveCommand<string, Unit> SetMapOrientationCommand { get; }
        public ReactiveCommand<string, Unit> SetHorizontalUIPanelPlacementCommand { get; }
        public ReactiveCommand<string, Unit> SetVerticalUIPanelPlacementCommand { get; }
        public ReactiveCommand<string, Unit> SetHorizontalItemsPlacementCommand { get; }
        public ReactiveCommand<string, Unit> SetVerticalItemsPlacementCommand { get; }

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

        public bool DisplayAllLocations =>
            _appSettings.DisplayAllLocations;
        public bool ShowItemCountsOnMap =>
            _appSettings.ShowItemCountsOnMap;

        public LayoutOrientation LayoutOrientation =>
            _appSettings.LayoutOrientation;
        public MapOrientation MapOrientation =>
            _appSettings.MapOrientation;
        public VerticalAlignment HorizontalUIPanelPlacement =>
            _appSettings.HorizontalUIPanelPlacement;
        public HorizontalAlignment VerticalUIPanelPlacement =>
            _appSettings.VerticalUIPanelPlacement;
        public HorizontalAlignment HorizontalItemsPlacement =>
            _appSettings.HorizontalItemsPlacement;
        public VerticalAlignment VerticalItemsPlacement =>
            _appSettings.VerticalItemsPlacement;

        public bool DynamicLayoutOrientation => 
            _appSettings.LayoutOrientation == LayoutOrientation.Dynamic;
        public bool HorizontalLayoutOrientation =>
            _appSettings.LayoutOrientation == LayoutOrientation.Horizontal;
        public bool VerticalLayoutOrientation =>
            _appSettings.LayoutOrientation == LayoutOrientation.Vertical;

        public bool DynamicMapOrientation =>
            _appSettings.MapOrientation == MapOrientation.Dynamic;
        public bool HorizontalMapOrientation =>
            _appSettings.MapOrientation == MapOrientation.Horizontal;
        public bool VerticalMapOrientation => 
            _appSettings.MapOrientation == MapOrientation.Vertical;

        public bool TopHorizontalUIPanelPlacement =>
            _appSettings.HorizontalUIPanelPlacement == VerticalAlignment.Top;
        public bool BottomHorizontalUIPanelPlacement =>
            _appSettings.HorizontalUIPanelPlacement == VerticalAlignment.Bottom;

        public bool LeftVerticalUIPanelPlacement => 
            _appSettings.VerticalUIPanelPlacement == HorizontalAlignment.Left;
        public bool RightVerticalUIPanelPlacement =>
            _appSettings.VerticalUIPanelPlacement == HorizontalAlignment.Right;

        public bool LeftHorizontalItemsPlacement =>
            _appSettings.HorizontalItemsPlacement == HorizontalAlignment.Left;
        public bool RightHorizontalItemsPlacement =>
            _appSettings.HorizontalItemsPlacement == HorizontalAlignment.Right;

        public bool TopVerticalItemsPlacement =>
            _appSettings.VerticalItemsPlacement == VerticalAlignment.Top;
        public bool BottomVerticalItemsPlacement =>
            _appSettings.VerticalItemsPlacement == VerticalAlignment.Bottom;

        public bool SmallKeyShuffle => _game.Mode.SmallKeyShuffle;
        public bool BigKeyShuffle => _game.Mode.BigKeyShuffle;

        public bool BossShuffle => _game.Mode.BossShuffle.Value;

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
            ToggleShowItemCountsOnMapCommand = ReactiveCommand.Create(ToggleShowItemCountsOnMap);
            SetLayoutOrientationCommand = ReactiveCommand.Create<string>(SetLayoutOrientation);
            SetMapOrientationCommand = ReactiveCommand.Create<string>(SetMapOrientation);
            SetHorizontalUIPanelPlacementCommand = ReactiveCommand.Create<string>(SetHorizontalUIPanelPlacement);
            SetVerticalUIPanelPlacementCommand = ReactiveCommand.Create<string>(SetVerticalUIPanelPlacement);
            SetHorizontalItemsPlacementCommand = ReactiveCommand.Create<string>(SetHorizontalItemsPlacement);
            SetVerticalItemsPlacementCommand = ReactiveCommand.Create<string>(SetVerticalItemsPlacement);

            string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            if (!Directory.Exists(appData))
                Directory.CreateDirectory(appData);

            string appSettingsPath = appData + Path.DirectorySeparatorChar + "opentracker.json";

            if (File.Exists(appSettingsPath))
            {
                string jsonContent = File.ReadAllText(appSettingsPath);

                _appSettings = JsonConvert.DeserializeObject<AppSettings>(jsonContent);
            }
            else
                _appSettings = new AppSettings();

            _appSettings.PropertyChanged += OnAppSettingsChanged;

            _game = new Game();

            _game.Mode.PropertyChanged += OnModeChanged;

            ModeSettings = new ModeSettingsControlVM(_game.Mode, _undoRedoManager);

            Maps = new ObservableCollection<MapControlVM>();
            Locations = new ObservableCollection<LocationControlVM>();
            HCItems = new ObservableCollection<DungeonItemControlVM>()
            {
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Items[ItemType.HCSmallKey])
            };
            ATItems = new ObservableCollection<DungeonItemControlVM>()
            {
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Items[ItemType.ATSmallKey])
            };
            SmallKeyItems = new ObservableCollection<DungeonItemControlVM>()
            {
                new DungeonItemControlVM(_undoRedoManager, _appSettings, null),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Items[ItemType.DPSmallKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Items[ItemType.ToHSmallKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Items[ItemType.PoDSmallKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Items[ItemType.SPSmallKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Items[ItemType.SWSmallKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Items[ItemType.TTSmallKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Items[ItemType.IPSmallKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Items[ItemType.MMSmallKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Items[ItemType.TRSmallKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Items[ItemType.GTSmallKey])
            };
            BigKeyItems = new ObservableCollection<DungeonItemControlVM>()
            {
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Items[ItemType.EPBigKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Items[ItemType.DPBigKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Items[ItemType.ToHBigKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Items[ItemType.PoDBigKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Items[ItemType.SPBigKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Items[ItemType.SWBigKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Items[ItemType.TTBigKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Items[ItemType.IPBigKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Items[ItemType.MMBigKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Items[ItemType.TRBigKey]),
                new DungeonItemControlVM(_undoRedoManager, _appSettings, _game.Items[ItemType.GTBigKey])
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
                    case ItemType.Sword:
                    case ItemType.Shield:
                    case ItemType.Aga:
                    case ItemType.TowerCrystals:
                    case ItemType.GanonCrystals:
                    case ItemType.Hookshot:
                    case ItemType.Mushroom:
                    case ItemType.Boots:
                    case ItemType.FireRod:
                    case ItemType.IceRod:
                    case ItemType.SmallKey:
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
                        Items.Add(new ItemControlVM(_undoRedoManager, _appSettings, _game,
                            new Item[1] { _game.Items[(ItemType)i] }));
                        break;
                    case ItemType.Bow:
                    case ItemType.Boomerang:
                    case ItemType.Bomb:
                    case ItemType.Powder:
                    case ItemType.Bombos:
                    case ItemType.Ether:
                    case ItemType.Quake:
                    case ItemType.Flute:
                        Items.Add(new ItemControlVM(_undoRedoManager, _appSettings, _game,
                            new Item[2] {
                            _game.Items[(ItemType)i], _game.Items[(ItemType)(i + 1)]
                        }));
                        break;
                    case ItemType.Mail:
                        Items.Add(new ItemControlVM(_undoRedoManager, _appSettings, _game,
                            new Item[1] { _game.Items[(ItemType)i] }));
                        Items.Add(new ItemControlVM(_undoRedoManager, _appSettings, _game, null));
                        break;
                    default:
                        break;
                }
            }
        }

        private void OnUndoChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateCanUndo();
        }

        private void OnRedoChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateCanRedo();
        }

        private void OnAppSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AppSettings.DisplayAllLocations))
                this.RaisePropertyChanged(nameof(DisplayAllLocations));

            if (e.PropertyName == nameof(AppSettings.ShowItemCountsOnMap))
                this.RaisePropertyChanged(nameof(ShowItemCountsOnMap));

            if (e.PropertyName == nameof(AppSettings.LayoutOrientation))
                UpdateLayoutOrientation();

            if (e.PropertyName == nameof(AppSettings.MapOrientation))
                UpdateMapOrientation();

            if (e.PropertyName == nameof(AppSettings.HorizontalUIPanelPlacement))
                UpdateHorizontalUIPanelPlacement();

            if (e.PropertyName == nameof(AppSettings.VerticalUIPanelPlacement))
                UpdateVerticalUIPanelPlacement();

            if (e.PropertyName == nameof(AppSettings.HorizontalItemsPlacement))
                UpdateHorizontalItemsPlacement();

            if (e.PropertyName == nameof(AppSettings.VerticalItemsPlacement))
                UpdateVerticalItemsPlacement();
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.DungeonItemShuffle))
            {
                this.RaisePropertyChanged(nameof(SmallKeyShuffle));
                this.RaisePropertyChanged(nameof(BigKeyShuffle));
            }

            if (e.PropertyName == nameof(Mode.WorldState))
                this.RaisePropertyChanged(nameof(SmallKeyShuffle));

            if (e.PropertyName == nameof(Mode.BossShuffle))
                this.RaisePropertyChanged(nameof(BossShuffle));
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

        private void UpdateLayoutOrientation()
        {
            this.RaisePropertyChanged(nameof(LayoutOrientation));
            this.RaisePropertyChanged(nameof(DynamicLayoutOrientation));
            this.RaisePropertyChanged(nameof(HorizontalLayoutOrientation));
            this.RaisePropertyChanged(nameof(VerticalLayoutOrientation));
        }

        private void UpdateMapOrientation()
        {
            this.RaisePropertyChanged(nameof(MapOrientation));
            this.RaisePropertyChanged(nameof(DynamicMapOrientation));
            this.RaisePropertyChanged(nameof(HorizontalMapOrientation));
            this.RaisePropertyChanged(nameof(VerticalMapOrientation));
        }

        private void UpdateHorizontalUIPanelPlacement()
        {
            this.RaisePropertyChanged(nameof(HorizontalUIPanelPlacement));
            this.RaisePropertyChanged(nameof(TopHorizontalUIPanelPlacement));
            this.RaisePropertyChanged(nameof(BottomHorizontalUIPanelPlacement));
        }

        private void UpdateVerticalUIPanelPlacement()
        {
            this.RaisePropertyChanged(nameof(VerticalUIPanelPlacement));
            this.RaisePropertyChanged(nameof(LeftVerticalUIPanelPlacement));
            this.RaisePropertyChanged(nameof(RightVerticalUIPanelPlacement));
        }

        private void UpdateHorizontalItemsPlacement()
        {
            this.RaisePropertyChanged(nameof(HorizontalItemsPlacement));
            this.RaisePropertyChanged(nameof(LeftHorizontalItemsPlacement));
            this.RaisePropertyChanged(nameof(RightHorizontalItemsPlacement));
        }

        private void UpdateVerticalItemsPlacement()
        {
            this.RaisePropertyChanged(nameof(VerticalItemsPlacement));
            this.RaisePropertyChanged(nameof(TopVerticalItemsPlacement));
            this.RaisePropertyChanged(nameof(BottomVerticalItemsPlacement));
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
                    _game.Locations[location].Sections[i].Available =
                        saveData.LocationSectionCounts[location][i];
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

        private void ToggleShowItemCountsOnMap()
        {
            _appSettings.ShowItemCountsOnMap = !_appSettings.ShowItemCountsOnMap;
        }

        private void SetLayoutOrientation(string orientationString)
        {
            if (Enum.TryParse(orientationString, out LayoutOrientation orientation))
                _appSettings.LayoutOrientation = orientation;
        }

        private void SetMapOrientation(string orientationString)
        {
            if (Enum.TryParse(orientationString, out MapOrientation orientation))
                _appSettings.MapOrientation = orientation;
        }

        private void SetHorizontalUIPanelPlacement(string orientationString)
        {
            if (Enum.TryParse(orientationString, out VerticalAlignment orientation))
                _appSettings.HorizontalUIPanelPlacement = orientation;
        }

        private void SetVerticalUIPanelPlacement(string orientationString)
        {
            if (Enum.TryParse(orientationString, out HorizontalAlignment orientation))
                _appSettings.VerticalUIPanelPlacement = orientation;
        }

        private void SetHorizontalItemsPlacement(string orientationString)
        {
            if (Enum.TryParse(orientationString, out HorizontalAlignment orientation))
                _appSettings.HorizontalItemsPlacement = orientation;
        }

        private void SetVerticalItemsPlacement(string orientationString)
        {
            if (Enum.TryParse(orientationString, out VerticalAlignment orientation))
                _appSettings.VerticalItemsPlacement = orientation;
        }

        private void Reset()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                _undoRedoManager.Reset();
                Locations.Clear();
                _game.Reset();
            });
        }

        public async Task OpenResetDialog()
        {
            bool? result = await _dialogService.ShowDialog(
                new MessageBoxDialogVM("Warning",
                "Resetting the tracker will set all items and locations back to their starting values.  This cannot be undone.\n\nDo you wish to proceed?"))
                .ConfigureAwait(false);

            if (result.HasValue && result.Value)
                Reset();
        }

        public void SaveAppSettings(bool maximized, Rect bounds)
        {
            _appSettings.Maximized = maximized;

            _appSettings.X = bounds.X;
            _appSettings.Y = bounds.Y;
            _appSettings.Width = bounds.Width;
            _appSettings.Height = bounds.Height;

            string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            if (!Directory.Exists(appData))
                Directory.CreateDirectory(appData);

            string appSettingsPath = appData + Path.DirectorySeparatorChar + "opentracker.json";

            if (File.Exists(appSettingsPath))
                File.Delete(appSettingsPath);

            string json = JsonConvert.SerializeObject(_appSettings, Formatting.Indented);

            File.WriteAllText(appSettingsPath, json);
        }

        public object GetAutoTrackerViewModel()
        {
            return new AutoTrackerDialogVM(_game.AutoTracker);
        }

        public object GetColorSelectViewModel()
        {
            return new ColorSelectDialogVM(_appSettings);
        }
    }
}
