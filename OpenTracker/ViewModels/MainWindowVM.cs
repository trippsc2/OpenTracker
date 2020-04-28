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

        public AppSettingsVM AppSettings { get; }
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

        public bool DynamicLayoutOrientation => 
            AppSettings.LayoutOrientation == LayoutOrientation.Dynamic;
        public bool HorizontalLayoutOrientation =>
            AppSettings.LayoutOrientation == LayoutOrientation.Horizontal;
        public bool VerticalLayoutOrientation =>
            AppSettings.LayoutOrientation == LayoutOrientation.Vertical;

        public bool DynamicMapOrientation =>
            AppSettings.MapOrientation == MapOrientation.Dynamic;
        public bool HorizontalMapOrientation =>
            AppSettings.MapOrientation == MapOrientation.Horizontal;
        public bool VerticalMapOrientation => 
            AppSettings.MapOrientation == MapOrientation.Vertical;

        public bool TopHorizontalUIPanelPlacement =>
            AppSettings.HorizontalUIPanelPlacement == VerticalAlignment.Top;
        public bool BottomHorizontalUIPanelPlacement =>
            AppSettings.HorizontalUIPanelPlacement == VerticalAlignment.Bottom;

        public bool LeftVerticalUIPanelPlacement => 
            AppSettings.VerticalUIPanelPlacement == HorizontalAlignment.Left;
        public bool RightVerticalUIPanelPlacement =>
            AppSettings.VerticalUIPanelPlacement == HorizontalAlignment.Right;

        public bool LeftHorizontalItemsPlacement =>
            AppSettings.HorizontalItemsPlacement == HorizontalAlignment.Left;
        public bool RightHorizontalItemsPlacement =>
            AppSettings.HorizontalItemsPlacement == HorizontalAlignment.Right;

        public bool TopVerticalItemsPlacement =>
            AppSettings.VerticalItemsPlacement == VerticalAlignment.Top;
        public bool BottomVerticalItemsPlacement =>
            AppSettings.VerticalItemsPlacement == VerticalAlignment.Bottom;

        public IAppSettingsVM AppSettingsInterface => AppSettings as IAppSettingsVM;
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
                new DungeonItemControlVM(_undoRedoManager, AppSettings, _game.Items[ItemType.HCSmallKey])
            };
            ATItems = new ObservableCollection<DungeonItemControlVM>()
            {
                new DungeonItemControlVM(_undoRedoManager, AppSettings, _game.Items[ItemType.ATSmallKey])
            };
            SmallKeyItems = new ObservableCollection<DungeonItemControlVM>()
            {
                new DungeonItemControlVM(_undoRedoManager, AppSettings, null),
                new DungeonItemControlVM(_undoRedoManager, AppSettings, _game.Items[ItemType.DPSmallKey]),
                new DungeonItemControlVM(_undoRedoManager, AppSettings, _game.Items[ItemType.ToHSmallKey]),
                new DungeonItemControlVM(_undoRedoManager, AppSettings, _game.Items[ItemType.PoDSmallKey]),
                new DungeonItemControlVM(_undoRedoManager, AppSettings, _game.Items[ItemType.SPSmallKey]),
                new DungeonItemControlVM(_undoRedoManager, AppSettings, _game.Items[ItemType.SWSmallKey]),
                new DungeonItemControlVM(_undoRedoManager, AppSettings, _game.Items[ItemType.TTSmallKey]),
                new DungeonItemControlVM(_undoRedoManager, AppSettings, _game.Items[ItemType.IPSmallKey]),
                new DungeonItemControlVM(_undoRedoManager, AppSettings, _game.Items[ItemType.MMSmallKey]),
                new DungeonItemControlVM(_undoRedoManager, AppSettings, _game.Items[ItemType.TRSmallKey]),
                new DungeonItemControlVM(_undoRedoManager, AppSettings, _game.Items[ItemType.GTSmallKey])
            };
            BigKeyItems = new ObservableCollection<DungeonItemControlVM>()
            {
                new DungeonItemControlVM(_undoRedoManager, AppSettings, _game.Items[ItemType.EPBigKey]),
                new DungeonItemControlVM(_undoRedoManager, AppSettings, _game.Items[ItemType.DPBigKey]),
                new DungeonItemControlVM(_undoRedoManager, AppSettings, _game.Items[ItemType.ToHBigKey]),
                new DungeonItemControlVM(_undoRedoManager, AppSettings, _game.Items[ItemType.PoDBigKey]),
                new DungeonItemControlVM(_undoRedoManager, AppSettings, _game.Items[ItemType.SPBigKey]),
                new DungeonItemControlVM(_undoRedoManager, AppSettings, _game.Items[ItemType.SWBigKey]),
                new DungeonItemControlVM(_undoRedoManager, AppSettings, _game.Items[ItemType.TTBigKey]),
                new DungeonItemControlVM(_undoRedoManager, AppSettings, _game.Items[ItemType.IPBigKey]),
                new DungeonItemControlVM(_undoRedoManager, AppSettings, _game.Items[ItemType.MMBigKey]),
                new DungeonItemControlVM(_undoRedoManager, AppSettings, _game.Items[ItemType.TRBigKey]),
                new DungeonItemControlVM(_undoRedoManager, AppSettings, _game.Items[ItemType.GTBigKey])
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
                Maps.Add(new MapControlVM(_undoRedoManager, AppSettings, _game, this, (MapID)i));

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
                        Items.Add(new ItemControlVM(_undoRedoManager, AppSettings, new Item[1] { _game.Items[(ItemType)i] }));
                        break;
                    case ItemType.Bow:
                    case ItemType.Boomerang:
                    case ItemType.Bomb:
                    case ItemType.Powder:
                    case ItemType.Bombos:
                    case ItemType.Ether:
                    case ItemType.Flute:
                        Items.Add(new ItemControlVM(_undoRedoManager, AppSettings, new Item[2] {
                            _game.Items[(ItemType)i], _game.Items[(ItemType)(i + 1)]
                        }));
                        break;
                    case ItemType.Quake:
                        Items.Add(new ItemControlVM(_undoRedoManager, AppSettings, new Item[2] {
                            _game.Items[(ItemType)i], _game.Items[(ItemType)(i + 1)]
                        }));
                        Items.Add(new ItemControlVM(_undoRedoManager, AppSettings, null));
                        break;
                    case ItemType.Mail:
                        Items.Add(new ItemControlVM(_undoRedoManager, AppSettings, new Item[1] { _game.Items[(ItemType)i] }));
                        Items.Add(new ItemControlVM(_undoRedoManager, AppSettings, null));
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
            this.RaisePropertyChanged(nameof(DynamicLayoutOrientation));
            this.RaisePropertyChanged(nameof(HorizontalLayoutOrientation));
            this.RaisePropertyChanged(nameof(VerticalLayoutOrientation));
        }

        private void UpdateMapOrientation()
        {
            this.RaisePropertyChanged(nameof(DynamicMapOrientation));
            this.RaisePropertyChanged(nameof(HorizontalMapOrientation));
            this.RaisePropertyChanged(nameof(VerticalMapOrientation));
        }

        private void UpdateHorizontalUIPanelPlacement()
        {
            this.RaisePropertyChanged(nameof(TopHorizontalUIPanelPlacement));
            this.RaisePropertyChanged(nameof(BottomHorizontalUIPanelPlacement));
        }

        private void UpdateVerticalUIPanelPlacement()
        {
            this.RaisePropertyChanged(nameof(LeftVerticalUIPanelPlacement));
            this.RaisePropertyChanged(nameof(RightVerticalUIPanelPlacement));
        }

        private void UpdateHorizontalItemsPlacement()
        {
            this.RaisePropertyChanged(nameof(LeftHorizontalItemsPlacement));
            this.RaisePropertyChanged(nameof(RightHorizontalItemsPlacement));
        }

        private void UpdateVerticalItemsPlacement()
        {
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
            AppSettings.DisplayAllLocations = !AppSettings.DisplayAllLocations;
        }

        private void ToggleShowItemCountsOnMap()
        {
            AppSettings.ShowItemCountsOnMap = !AppSettings.ShowItemCountsOnMap;
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

        private void Reset()
        {
            _undoRedoManager.Reset();
            Locations.Clear();
            _game.Reset();
        }

        public async Task OpenResetDialog()
        {
            bool? result = await _dialogService.ShowDialog(
                new MessageBoxDialogVM("Warning",
                "Resetting the tracker will set all items and locations back to their starting values.  This cannot be undone.\nDo you wish to proceed?"));

            if (result.HasValue && result.Value)
                Reset();
        }

        public void SaveAppSettings()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            if (!Directory.Exists(appData))
                Directory.CreateDirectory(appData);

            string appSettingsPath = appData + Path.DirectorySeparatorChar + "opentracker.json";

            if (File.Exists(appSettingsPath))
                File.Delete(appSettingsPath);

            string json = JsonConvert.SerializeObject(AppSettings, Formatting.Indented, new SolidColorBrushConverter());

            File.WriteAllText(appSettingsPath, json);
        }
    }
}
