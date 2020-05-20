﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Threading;
using Newtonsoft.Json;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Actions;
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
        IDynamicLayout, IOpen, ISave, ISaveAppSettings, IBounds
    {
        private readonly IDialogService _dialogService;
        private readonly UndoRedoManager _undoRedoManager;
        private readonly AppSettings _appSettings;
        private readonly Game _game;

        public static ObservableCollection<MarkingSelectControlVM> NonEntranceMarkingSelect { get; } = 
            new ObservableCollection<MarkingSelectControlVM>();
        public static ObservableCollection<MarkingSelectControlVM> EntranceMarkingSelect { get; } = 
            new ObservableCollection<MarkingSelectControlVM>();

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

        public bool DisplayAllLocations =>
            _appSettings.DisplayAllLocations;
        public bool ShowItemCountsOnMap =>
            _appSettings.ShowItemCountsOnMap;

        public bool DynamicLayoutOrientation =>
            _appSettings.LayoutOrientation == null;
        public bool HorizontalLayoutOrientation =>
            _appSettings.LayoutOrientation == Orientation.Horizontal;
        public bool VerticalLayoutOrientation =>
            _appSettings.LayoutOrientation == Orientation.Vertical;

        public bool DynamicMapOrientation =>
            _appSettings.MapOrientation == null;
        public bool HorizontalMapOrientation =>
            _appSettings.MapOrientation == Orientation.Horizontal;
        public bool VerticalMapOrientation =>
            _appSettings.MapOrientation == Orientation.Vertical;

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

        public bool SmallKeyShuffle =>
            _game.Mode.SmallKeyShuffle;
        public bool BigKeyShuffle =>
            _game.Mode.BigKeyShuffle;
        public bool BossShuffle =>
            _game.Mode.BossShuffle.Value;

        private Orientation _orientation;
        public Orientation Orientation
        {
            get => _orientation;
            private set => this.RaiseAndSetIfChanged(ref _orientation, value);
        }

        public Dock UIPanelDock
        {
            get
            {
                if (_appSettings.LayoutOrientation.HasValue)
                {
                    switch (_appSettings.LayoutOrientation.Value)
                    {
                        case Orientation.Horizontal:
                            return _appSettings.HorizontalUIPanelPlacement switch
                            {
                                VerticalAlignment.Top => Dock.Top,
                                _ => Dock.Bottom,
                            };
                        case Orientation.Vertical:
                            return _appSettings.VerticalUIPanelPlacement switch
                            {
                                HorizontalAlignment.Left => Dock.Left,
                                _ => Dock.Right,
                            };
                    }
                }
                else
                {
                    switch (Orientation)
                    {
                        case Orientation.Horizontal:
                            return _appSettings.HorizontalUIPanelPlacement switch
                            {
                                VerticalAlignment.Top => Dock.Top,
                                _ => Dock.Bottom,
                            };
                        case Orientation.Vertical:
                            return _appSettings.VerticalUIPanelPlacement switch
                            {
                                HorizontalAlignment.Left => Dock.Left,
                                _ => Dock.Right,
                            };
                    }
                }

                return Dock.Right;
            }
        }

        public HorizontalAlignment UIPanelHorizontalAlignment
        {
            get
            {
                return UIPanelDock switch
                {
                    Dock.Left => HorizontalAlignment.Left,
                    Dock.Right => HorizontalAlignment.Right,
                    _ => HorizontalAlignment.Stretch,
                };
            }
        }

        public VerticalAlignment UIPanelVerticalAlignment
        {
            get
            {
                return UIPanelDock switch
                {
                    Dock.Bottom => VerticalAlignment.Bottom,
                    Dock.Top => VerticalAlignment.Top,
                    _ => VerticalAlignment.Stretch,
                };
            }
        }

        public Dock UIPanelOrientationDock
        {
            get
            {
                switch (UIPanelDock)
                {
                    case Dock.Left:
                    case Dock.Right:
                        return _appSettings.VerticalItemsPlacement switch
                        {
                            VerticalAlignment.Top => Dock.Top,
                            _ => Dock.Bottom,
                        };
                    case Dock.Bottom:
                    case Dock.Top:
                        return _appSettings.HorizontalItemsPlacement switch
                        {
                            HorizontalAlignment.Left => Dock.Left,
                            _ => Dock.Right,
                        };
                }

                return Dock.Right;
            }
        }

        public Thickness ItemsPanelMargin
        {
            get
            {
                return UIPanelOrientationDock switch
                {
                    Dock.Left => new Thickness(2, 0, 1, 2),
                    Dock.Bottom => new Thickness(2, 1, 0, 2),
                    Dock.Right => new Thickness(1, 0, 2, 2),
                    _ => new Thickness(2, 2, 0, 1),
                };
            }
        }

        public Thickness LocationsPanelMargin
        {
            get
            {
                return UIPanelOrientationDock switch
                {
                    Dock.Left => new Thickness(1, 0, 2, 2),
                    Dock.Bottom => new Thickness(2, 2, 0, 1),
                    Dock.Right => new Thickness(2, 0, 1, 2),
                    _ => new Thickness(2, 1, 0, 2),
                };
            }
        }

        public Orientation LocationsPanelOrientation
        {
            get
            {
                return UIPanelDock switch
                {
                    Dock.Left => Orientation.Vertical,
                    Dock.Right => Orientation.Vertical,
                    _ => Orientation.Horizontal,
                };
            }
        }

        public Orientation MapPanelOrientation
        {
            get
            {
                if (_appSettings.MapOrientation.HasValue)
                    return _appSettings.MapOrientation.Value;
                else
                    return Orientation;
            }
        }

        public ModeSettingsControlVM ModeSettings { get; }

        public ObservableCollection<ItemControlVM> Items { get; }
        public ObservableCollection<DungeonItemControlVM> HCItems { get; }
        public ObservableCollection<DungeonItemControlVM> ATItems { get; }
        public ObservableCollection<DungeonItemControlVM> SmallKeyItems { get; }
        public ObservableCollection<DungeonItemControlVM> BigKeyItems { get; }
        public ObservableCollection<DungeonPrizeControlVM> Prizes { get; }
        public ObservableCollection<BossControlVM> Bosses { get; }
        public ObservableCollection<LocationControlVM> Locations { get; }
        public ObservableCollection<MapControlVM> Maps { get; }
        public ObservableCollection<ConnectorControlVM> Connectors { get; }
        public ObservableCollection<MapEntranceControlVM> MapEntrances { get; }
        public ObservableCollection<MapLocationControlVM> MapLocations { get; }

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

            PropertyChanged += OnPropertyChanged;

            string appSettingsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                Path.DirectorySeparatorChar + "OpenTracker" + Path.DirectorySeparatorChar + "OpenTracker.json";

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
            _game.Connections.CollectionChanged += OnConnectionsChanged;

            if (NonEntranceMarkingSelect.Count == 0)
            {
                for (int i = 0; i < Enum.GetValues(typeof(MarkingType)).Length; i++)
                {
                    switch ((MarkingType)i)
                    {
                        case MarkingType.Sword:
                        case MarkingType.Shield:
                        case MarkingType.Mail:
                        case MarkingType.Boots:
                        case MarkingType.Gloves:
                        case MarkingType.Flippers:
                        case MarkingType.MoonPearl:
                        case MarkingType.Bow:
                        case MarkingType.SilverArrows:
                        case MarkingType.Boomerang:
                        case MarkingType.RedBoomerang:
                        case MarkingType.Hookshot:
                        case MarkingType.Bomb:
                        case MarkingType.Mushroom:
                        case MarkingType.FireRod:
                        case MarkingType.IceRod:
                        case MarkingType.Bombos:
                        case MarkingType.Ether:
                        case MarkingType.Powder:
                        case MarkingType.Lamp:
                        case MarkingType.Hammer:
                        case MarkingType.Flute:
                        case MarkingType.Net:
                        case MarkingType.Book:
                        case MarkingType.Shovel:
                        case MarkingType.SmallKey:
                        case MarkingType.Bottle:
                        case MarkingType.CaneOfSomaria:
                        case MarkingType.CaneOfByrna:
                        case MarkingType.Cape:
                        case MarkingType.Mirror:
                        case MarkingType.HalfMagic:
                        case MarkingType.BigKey:
                            NonEntranceMarkingSelect.Add(new MarkingSelectControlVM(_game, (MarkingType)i));
                            break;
                        case MarkingType.Quake:
                            NonEntranceMarkingSelect.Add(new MarkingSelectControlVM(_game, (MarkingType)i));
                            NonEntranceMarkingSelect.Add(new MarkingSelectControlVM(_game, null));
                            break;
                        default:
                            break;
                    }
                }
            }

            if (EntranceMarkingSelect.Count == 0)
            {
                for (int i = 0; i < Enum.GetValues(typeof(MarkingType)).Length; i++)
                    EntranceMarkingSelect.Add(new MarkingSelectControlVM(_game, (MarkingType)i));
            }

            ModeSettings = new ModeSettingsControlVM(_game.Mode, _undoRedoManager);

            Maps = new ObservableCollection<MapControlVM>();
            Connectors = new ObservableCollection<ConnectorControlVM>();
            MapEntrances = new ObservableCollection<MapEntranceControlVM>();
            MapLocations = new ObservableCollection<MapLocationControlVM>();
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

            foreach (Models.Location location in _game.Locations.Values)
            {
                foreach (MapLocation mapLocation in location.MapLocations)
                {
                    if (location.Sections[0] is EntranceSection)
                    {
                        MapEntrances.Add(new MapEntranceControlVM(_undoRedoManager, _appSettings,
                            _game, this, mapLocation));
                    }
                    else
                    {
                        MapLocations.Add(new MapLocationControlVM(_undoRedoManager, _appSettings,
                            _game, this, mapLocation));
                    }
                }
            }

            for (int i = 0; i < Enum.GetValues(typeof(MapID)).Length; i++)
                Maps.Add(new MapControlVM(_game, this, (MapID)i));

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

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Orientation))
            {
                UpdateLayoutOrientation();
                UpdateMapOrientation();
            }

            if (e.PropertyName == nameof(UIPanelDock))
            {
                this.RaisePropertyChanged(nameof(UIPanelHorizontalAlignment));
                this.RaisePropertyChanged(nameof(UIPanelVerticalAlignment));
                this.RaisePropertyChanged(nameof(UIPanelOrientationDock));
                this.RaisePropertyChanged(nameof(ItemsPanelMargin));
                this.RaisePropertyChanged(nameof(LocationsPanelMargin));
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

        private void OnConnectionsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (object item in e.NewItems)
                {
                    (MapLocation, MapLocation) connection = ((MapLocation, MapLocation))item;
                    Connectors.Add(new ConnectorControlVM(_undoRedoManager, _game, this,
                        _appSettings, connection));
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (object item in e.OldItems)
                {
                    (MapLocation, MapLocation) connection = ((MapLocation, MapLocation))item;

                    foreach (ConnectorControlVM connector in Connectors)
                    {
                        if (connector.Connection == connection)
                        {
                            Connectors.Remove(connector);
                            break;
                        }
                    }
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                Connectors.Clear();

                foreach ((MapLocation, MapLocation) connection in (ObservableCollection<(MapLocation, MapLocation)>)sender)
                    Connectors.Add(new ConnectorControlVM(_undoRedoManager, _game, this,
                        _appSettings, connection));
            }
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
            this.RaisePropertyChanged(nameof(UIPanelDock));
            this.RaisePropertyChanged(nameof(DynamicLayoutOrientation));
            this.RaisePropertyChanged(nameof(HorizontalLayoutOrientation));
            this.RaisePropertyChanged(nameof(VerticalLayoutOrientation));
        }

        private void UpdateMapOrientation()
        {
            this.RaisePropertyChanged(nameof(MapPanelOrientation));
            this.RaisePropertyChanged(nameof(DynamicMapOrientation));
            this.RaisePropertyChanged(nameof(HorizontalMapOrientation));
            this.RaisePropertyChanged(nameof(VerticalMapOrientation));
        }

        private void UpdateHorizontalUIPanelPlacement()
        {
            this.RaisePropertyChanged(nameof(UIPanelDock));
            this.RaisePropertyChanged(nameof(TopHorizontalUIPanelPlacement));
            this.RaisePropertyChanged(nameof(BottomHorizontalUIPanelPlacement));
        }

        private void UpdateVerticalUIPanelPlacement()
        {
            this.RaisePropertyChanged(nameof(UIPanelDock));
            this.RaisePropertyChanged(nameof(LeftVerticalUIPanelPlacement));
            this.RaisePropertyChanged(nameof(RightVerticalUIPanelPlacement));
        }

        private void UpdateHorizontalItemsPlacement()
        {
            this.RaisePropertyChanged(nameof(UIPanelOrientationDock));
            this.RaisePropertyChanged(nameof(ItemsPanelMargin));
            this.RaisePropertyChanged(nameof(LeftHorizontalItemsPlacement));
            this.RaisePropertyChanged(nameof(RightHorizontalItemsPlacement));
        }

        private void UpdateVerticalItemsPlacement()
        {
            this.RaisePropertyChanged(nameof(UIPanelOrientationDock));
            this.RaisePropertyChanged(nameof(ItemsPanelMargin));
            this.RaisePropertyChanged(nameof(TopVerticalItemsPlacement));
            this.RaisePropertyChanged(nameof(BottomVerticalItemsPlacement));
        }

        public void ChangeLayout(Orientation orientation)
        {
            Orientation = orientation;
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

            foreach ((LocationID, int, LocationID, int) connection in saveData.Connections)
            {
                MapLocation location1 = _game.Locations[connection.Item1].MapLocations[connection.Item2];
                MapLocation location2 = _game.Locations[connection.Item3].MapLocations[connection.Item4];

                _game.Connections.Add((location1, location2));
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
            if (orientationString == "Dynamic")
                _appSettings.LayoutOrientation = null;
            else if (Enum.TryParse(orientationString, out Orientation orientation))
                _appSettings.LayoutOrientation = orientation;
        }

        private void SetMapOrientation(string orientationString)
        {
            if (orientationString == "Dynamic")
                _appSettings.MapOrientation = null;
            else if (Enum.TryParse(orientationString, out Orientation orientation))
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

            string appSettingsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                Path.DirectorySeparatorChar + "OpenTracker" + Path.DirectorySeparatorChar + "OpenTracker.json";

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
