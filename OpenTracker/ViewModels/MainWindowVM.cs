using Newtonsoft.Json;
using OpenTracker.Interfaces;
using OpenTracker.JsonConverters;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using OpenTracker.Utils;
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

        public ModeSettingsControlVM ModeSettings { get; }
        public AutoTrackerDialogVM AutoTracker { get; }
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

        private bool _dynamicLayoutOrientation;
        public bool DynamicLayoutOrientation
        {
            get => _dynamicLayoutOrientation;
            set => this.RaiseAndSetIfChanged(ref _dynamicLayoutOrientation, value);
        }

        private bool _horizontalLayoutOrientation;
        public bool HorizontalLayoutOrientation
        {
            get => _horizontalLayoutOrientation;
            set => this.RaiseAndSetIfChanged(ref _horizontalLayoutOrientation, value);
        }

        private bool _verticalLayoutOrientation;
        public bool VerticalLayoutOrientation
        {
            get => _verticalLayoutOrientation;
            set => this.RaiseAndSetIfChanged(ref _verticalLayoutOrientation, value);
        }

        private bool _dynamicMapOrientation;
        public bool DynamicMapOrientation
        {
            get => _dynamicMapOrientation;
            set => this.RaiseAndSetIfChanged(ref _dynamicMapOrientation, value);
        }

        private bool _horizontalMapOrientation;
        public bool HorizontalMapOrientation
        {
            get => _horizontalMapOrientation;
            set => this.RaiseAndSetIfChanged(ref _horizontalMapOrientation, value);
        }

        private bool _verticalMapOrientation;
        public bool VerticalMapOrientation
        {
            get => _verticalMapOrientation;
            set => this.RaiseAndSetIfChanged(ref _verticalMapOrientation, value);
        }

        private bool _topHorizontalUIPanelPlacement;
        public bool TopHorizontalUIPanelPlacement
        {
            get => _topHorizontalUIPanelPlacement;
            private set => this.RaiseAndSetIfChanged(ref _topHorizontalUIPanelPlacement, value);
        }

        private bool _bottomHorizontalUIPanelPlacement;
        public bool BottomHorizontalUIPanelPlacement
        {
            get => _bottomHorizontalUIPanelPlacement;
            private set => this.RaiseAndSetIfChanged(ref _bottomHorizontalUIPanelPlacement, value);
        }

        private bool _leftVerticalUIPanelPlacement;
        public bool LeftVerticalUIPanelPlacement
        {
            get => _leftVerticalUIPanelPlacement;
            private set => this.RaiseAndSetIfChanged(ref _leftVerticalUIPanelPlacement, value);
        }

        private bool _rightVerticalUIPanelPlacement;
        public bool RightVerticalUIPanelPlacement
        {
            get => _rightVerticalUIPanelPlacement;
            private set => this.RaiseAndSetIfChanged(ref _rightVerticalUIPanelPlacement, value);
        }

        private bool _leftHorizontalItemsPlacement;
        public bool LeftHorizontalItemsPlacement
        {
            get => _leftHorizontalItemsPlacement;
            set => this.RaiseAndSetIfChanged(ref _leftHorizontalItemsPlacement, value);
        }

        private bool _rightHorizontalItemsPlacement;
        public bool RightHorizontalItemsPlacement
        {
            get => _rightHorizontalItemsPlacement;
            set => this.RaiseAndSetIfChanged(ref _rightHorizontalItemsPlacement, value);
        }

        private bool _topVerticalItemsPlacement;
        public bool TopVerticalItemsPlacement
        {
            get => _topVerticalItemsPlacement;
            set => this.RaiseAndSetIfChanged(ref _topVerticalItemsPlacement, value);
        }

        private bool _bottomVerticalItemsPlacement;
        public bool BottomVerticalItemsPlacement
        {
            get => _bottomVerticalItemsPlacement;
            set => this.RaiseAndSetIfChanged(ref _bottomVerticalItemsPlacement, value);
        }

        private AppSettingsVM _appSettings;
        public AppSettingsVM AppSettings
        {
            get => _appSettings;
            set => this.RaiseAndSetIfChanged(ref _appSettings, value);
        }

        public IAppSettingsVM AppSettingsInterface => _appSettings as IAppSettingsVM;
        public IAutoTrackerDialogVM AutoTrackerInterface => AutoTracker as IAutoTrackerDialogVM;

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

                AppSettings = JsonConvert.DeserializeObject<AppSettingsVM>(jsonContent, new SolidColorBrushConverter());
            }
            else
                AppSettings = new AppSettingsVM();

            AppSettings.PropertyChanged += OnAppSettingsChanged;

            _game = new Game();

            ModeSettings = new ModeSettingsControlVM(_game.Mode, _undoRedoManager);
            AutoTracker = new AutoTrackerDialogVM(_game.AutoTracker);

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
                        Items.Add(new ItemControlVM(_undoRedoManager, _appSettings, new Item[1] { _game.Items[(ItemType)i] }));
                        break;
                    case ItemType.Bow:
                    case ItemType.Boomerang:
                    case ItemType.Bomb:
                    case ItemType.Powder:
                    case ItemType.Bombos:
                    case ItemType.Ether:
                    case ItemType.Flute:
                        Items.Add(new ItemControlVM(_undoRedoManager, _appSettings, new Item[2] {
                            _game.Items[(ItemType)i], _game.Items[(ItemType)(i + 1)]
                        }));
                        break;
                    case ItemType.Quake:
                        Items.Add(new ItemControlVM(_undoRedoManager, _appSettings, new Item[2] {
                            _game.Items[(ItemType)i], _game.Items[(ItemType)(i + 1)]
                        }));
                        Items.Add(new ItemControlVM(_undoRedoManager, _appSettings, null));
                        break;
                    case ItemType.Mail:
                        Items.Add(new ItemControlVM(_undoRedoManager, _appSettings, new Item[1] { _game.Items[(ItemType)i] }));
                        Items.Add(new ItemControlVM(_undoRedoManager, _appSettings, null));
                        break;
                    default:
                        break;
                }
            }

            UpdateLayoutOrientation();
            UpdateMapOrientation();
            UpdateHorizontalUIPanelPlacement();
            UpdateVerticalUIPanelPlacement();
            UpdateHorizontalItemsPlacement();
            UpdateVerticalItemsPlacement();
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
            if (e.PropertyName == nameof(AppSettingsVM.LayoutOrientation))
                UpdateLayoutOrientation();

            if (e.PropertyName == nameof(AppSettingsVM.MapOrientation))
                UpdateMapOrientation();

            if (e.PropertyName == nameof(AppSettingsVM.HorizontalUIPanelPlacement))
                UpdateHorizontalUIPanelPlacement();

            if (e.PropertyName == nameof(AppSettingsVM.VerticalUIPanelPlacement))
                UpdateVerticalUIPanelPlacement();

            if (e.PropertyName == nameof(AppSettingsVM.HorizontalItemsPlacement))
                UpdateHorizontalItemsPlacement();

            if (e.PropertyName == nameof(AppSettingsVM.VerticalItemsPlacement))
                UpdateVerticalItemsPlacement();
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
            switch (AppSettings.LayoutOrientation)
            {
                case LayoutOrientation.Dynamic:
                    DynamicLayoutOrientation = true;
                    HorizontalLayoutOrientation = false;
                    VerticalLayoutOrientation = false;
                    break;
                case LayoutOrientation.Horizontal:
                    DynamicLayoutOrientation = false;
                    HorizontalLayoutOrientation = true;
                    VerticalLayoutOrientation = false;
                    break;
                case LayoutOrientation.Vertical:
                    DynamicLayoutOrientation = false;
                    HorizontalLayoutOrientation = false;
                    VerticalLayoutOrientation = true;
                    break;
            }
        }

        private void UpdateMapOrientation()
        {
            switch (AppSettings.MapOrientation)
            {
                case MapOrientation.Dynamic:
                    DynamicMapOrientation = true;
                    HorizontalMapOrientation = false;
                    VerticalMapOrientation = false;
                    break;
                case MapOrientation.Horizontal:
                    DynamicMapOrientation = false;
                    HorizontalMapOrientation = true;
                    VerticalMapOrientation = false;
                    break;
                case MapOrientation.Vertical:
                    DynamicMapOrientation = false;
                    HorizontalMapOrientation = false;
                    VerticalMapOrientation = true;
                    break;
            }
        }

        private void UpdateHorizontalUIPanelPlacement()
        {
            switch (AppSettings.HorizontalUIPanelPlacement)
            {
                case VerticalAlignment.Top:
                    TopHorizontalUIPanelPlacement = true;
                    BottomHorizontalUIPanelPlacement = false;
                    break;
                case VerticalAlignment.Bottom:
                    TopHorizontalUIPanelPlacement = false;
                    BottomHorizontalUIPanelPlacement = true;
                    break;
            }
        }

        private void UpdateVerticalUIPanelPlacement()
        {
            switch (AppSettings.VerticalUIPanelPlacement)
            {
                case HorizontalAlignment.Left:
                    LeftVerticalUIPanelPlacement = true;
                    RightVerticalUIPanelPlacement = false;
                    break;
                case HorizontalAlignment.Right:
                    LeftVerticalUIPanelPlacement = false;
                    RightVerticalUIPanelPlacement = true;
                    break;
            }
        }

        private void UpdateHorizontalItemsPlacement()
        {
            switch (AppSettings.HorizontalItemsPlacement)
            {
                case HorizontalAlignment.Left:
                    LeftHorizontalItemsPlacement = true;
                    RightHorizontalItemsPlacement = false;
                    break;
                case HorizontalAlignment.Right:
                    LeftHorizontalItemsPlacement = false;
                    RightHorizontalItemsPlacement = true;
                    break;
            }
        }

        private void UpdateVerticalItemsPlacement()
        {
            switch (AppSettings.VerticalItemsPlacement)
            {
                case VerticalAlignment.Top:
                    TopVerticalItemsPlacement = true;
                    BottomVerticalItemsPlacement = false;
                    break;
                case VerticalAlignment.Bottom:
                    TopVerticalItemsPlacement = false;
                    BottomVerticalItemsPlacement = true;
                    break;
            }
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
                AppSettings.LayoutOrientation = orientation;
        }

        private void SetMapOrientation(string orientationString)
        {
            if (Enum.TryParse(orientationString, out MapOrientation orientation))
                AppSettings.MapOrientation = orientation;
        }

        private void SetHorizontalUIPanelPlacement(string orientationString)
        {
            if (Enum.TryParse(orientationString, out VerticalAlignment orientation))
                AppSettings.HorizontalUIPanelPlacement = orientation;
        }

        private void SetVerticalUIPanelPlacement(string orientationString)
        {
            if (Enum.TryParse(orientationString, out HorizontalAlignment orientation))
                AppSettings.VerticalUIPanelPlacement = orientation;
        }

        private void SetHorizontalItemsPlacement(string orientationString)
        {
            if (Enum.TryParse(orientationString, out HorizontalAlignment orientation))
                AppSettings.HorizontalItemsPlacement = orientation;
        }

        private void SetVerticalItemsPlacement(string orientationString)
        {
            if (Enum.TryParse(orientationString, out VerticalAlignment orientation))
                AppSettings.VerticalItemsPlacement = orientation;
        }

        public async Task Reset()
        {
            bool? result = await _dialogService.ShowDialog(
                new MessageBoxDialogVM("Warning",
                "Resetting the tracker will set all items and locations back to their starting values.  This cannot be undone.\nDo you wish to proceed?"));

            if (result.HasValue && result.Value)
                _game.Reset();
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
