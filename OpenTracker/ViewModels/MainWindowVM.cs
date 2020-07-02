using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Threading;
using Newtonsoft.Json;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using OpenTracker.Models.Sections;
using OpenTracker.ViewModels.Bases;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This is the view-model for the main window.
    /// </summary>
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
                            {
                                return _appSettings.HorizontalUIPanelPlacement switch
                                {
                                    VerticalAlignment.Top => Dock.Top,
                                    _ => Dock.Bottom,
                                };
                            }
                        case Orientation.Vertical:
                            {
                                return _appSettings.VerticalUIPanelPlacement switch
                                {
                                    HorizontalAlignment.Left => Dock.Left,
                                    _ => Dock.Right,
                                };
                            }
                    }
                }
                else
                {
                    switch (Orientation)
                    {
                        case Orientation.Horizontal:
                            {
                                return _appSettings.HorizontalUIPanelPlacement switch
                                {
                                    VerticalAlignment.Top => Dock.Top,
                                    _ => Dock.Bottom,
                                };
                            }
                        case Orientation.Vertical:
                            {
                                return _appSettings.VerticalUIPanelPlacement switch
                                {
                                    HorizontalAlignment.Left => Dock.Left,
                                    _ => Dock.Right,
                                };
                            }
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
                        {
                            return _appSettings.VerticalItemsPlacement switch
                            {
                                VerticalAlignment.Top => Dock.Top,
                                _ => Dock.Bottom,
                            };
                        }
                    case Dock.Bottom:
                    case Dock.Top:
                        {
                            return _appSettings.HorizontalItemsPlacement switch
                            {
                                HorizontalAlignment.Left => Dock.Left,
                                _ => Dock.Right,
                            };
                        }
                }

                return Dock.Right;
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
                {
                    return _appSettings.MapOrientation.Value;
                }
                else
                {
                    return Orientation;
                }
            }
        }

        public TopMenuControlVM TopMenu { get; }
        public ItemsPanelControlVM ItemsPanel { get; }

        public ObservableCollection<LocationControlVM> Locations { get; }
        public ObservableCollection<MapControlVM> Maps { get; }
        public ObservableCollection<ConnectorControlVM> Connectors { get; }
        public ObservableCollection<MapEntranceControlVM> MapEntrances { get; }
        public ObservableCollection<MapLocationControlVM> MapLocations { get; }

        public ReactiveCommand<Unit, Unit> OpenResetDialogCommand { get; }
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

        private readonly ObservableAsPropertyHelper<bool> _isOpeningResetDialog;
        public bool IsOpeningResetDialog =>
            _isOpeningResetDialog.Value;

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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dialogService">
        /// The dialog service.
        /// </param>
        public MainWindowVM(IDialogService dialogService) : this()
        {
            _dialogService = dialogService;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindowVM()
        {
            _undoRedoManager = new UndoRedoManager();
            _game = new Game();

            _undoRedoManager.UndoableActions.CollectionChanged += OnUndoChanged;
            _undoRedoManager.RedoableActions.CollectionChanged += OnRedoChanged;

            OpenResetDialogCommand = ReactiveCommand.CreateFromObservable(OpenResetDialogAsync);
            OpenResetDialogCommand.IsExecuting.ToProperty(
                this, x => x.IsOpeningResetDialog, out _isOpeningResetDialog);

            UndoCommand = ReactiveCommand.Create(Undo, this.WhenAnyValue(x => x.CanUndo));
            RedoCommand = ReactiveCommand.Create(Redo, this.WhenAnyValue(x => x.CanRedo));
            ToggleDisplayAllLocationsCommand = ReactiveCommand.Create(ToggleDisplayAllLocations);
            PropertyChanged += OnPropertyChanged;

            string appSettingsPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "OpenTracker", "OpenTracker.json");

            if (File.Exists(appSettingsPath))
            {
                string jsonContent = File.ReadAllText(appSettingsPath);
                _appSettings = JsonConvert.DeserializeObject<AppSettings>(jsonContent);
            }
            else
            {
                _appSettings = new AppSettings();
            }

            TopMenu = new TopMenuControlVM(this, _appSettings);
            ItemsPanel = new ItemsPanelControlVM(this, _appSettings, _game, _undoRedoManager);

            _appSettings.PropertyChanged += OnAppSettingsChanged;
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
                            {
                                NonEntranceMarkingSelect.Add(new MarkingSelectControlVM(_game, (MarkingType)i));
                            }
                            break;
                        case MarkingType.Quake:
                            {
                                NonEntranceMarkingSelect.Add(new MarkingSelectControlVM(_game, (MarkingType)i));
                                NonEntranceMarkingSelect.Add(new MarkingSelectControlVM(_game, null));
                            }
                            break;
                    }
                }
            }

            if (EntranceMarkingSelect.Count == 0)
            {
                for (int i = 0; i < Enum.GetValues(typeof(MarkingType)).Length; i++)
                {
                    EntranceMarkingSelect.Add(new MarkingSelectControlVM(_game, (MarkingType)i));
                }
            }

            Maps = new ObservableCollection<MapControlVM>();
            Connectors = new ObservableCollection<ConnectorControlVM>();
            MapEntrances = new ObservableCollection<MapEntranceControlVM>();
            MapLocations = new ObservableCollection<MapLocationControlVM>();
            Locations = new ObservableCollection<LocationControlVM>();

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
            {
                Maps.Add(new MapControlVM(_game, this, (MapID)i));
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on itself.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Orientation))
            {
                UpdateUIPanelDock();
                UpdateMapOrientation();
            }

            if (e.PropertyName == nameof(UIPanelDock))
            {
                this.RaisePropertyChanged(nameof(UIPanelHorizontalAlignment));
                this.RaisePropertyChanged(nameof(UIPanelVerticalAlignment));
                this.RaisePropertyChanged(nameof(UIPanelOrientationDock));
                this.RaisePropertyChanged(nameof(LocationsPanelMargin));
            }
        }

        /// <summary>
        /// Subscribes to the CollectionChanged event on the ObservableStack of undoable actions.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnUndoChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateCanUndo();
        }

        /// <summary>
        /// Subscribes to the CollectionChanged event on the ObservableStack of redoable actions.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnRedoChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateCanRedo();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the AppSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnAppSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AppSettings.LayoutOrientation) &&
                e.PropertyName == nameof(AppSettings.HorizontalUIPanelPlacement) &&
                e.PropertyName == nameof(AppSettings.VerticalUIPanelPlacement))
            {
                UpdateUIPanelDock();
            }

            if (e.PropertyName == nameof(AppSettings.MapOrientation))
            {
                UpdateMapOrientation();
            }

            if (e.PropertyName == nameof(AppSettings.HorizontalItemsPlacement) &&
                e.PropertyName == nameof(AppSettings.VerticalItemsPlacement))
            {
                UpdateUIPanelOrientationDock();
            }
        }

        /// <summary>
        /// Subscribes to the CollectionChanged event on the ObservableCollection of map entrance
        /// connections.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
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

                foreach ((MapLocation, MapLocation) connection in
                    (ObservableCollection<(MapLocation, MapLocation)>)sender)
                {
                    Connectors.Add(new ConnectorControlVM(_undoRedoManager, _game, this,
                        _appSettings, connection));
                }
            }
        }

        /// <summary>
        /// Updates the CanUndo property.
        /// </summary>
        private void UpdateCanUndo()
        {
            CanUndo = _undoRedoManager.CanUndo();
        }

        /// <summary>
        /// Undoes the last action.
        /// </summary>
        private void Undo()
        {
            _undoRedoManager.Undo();
        }

        /// <summary>
        /// Updates the CanRedo property.
        /// </summary>
        private void UpdateCanRedo()
        {
            CanRedo = _undoRedoManager.CanRedo();
        }

        /// <summary>
        /// Redoes the last action.
        /// </summary>
        private void Redo()
        {
            _undoRedoManager.Redo();
        }

        /// <summary>
        /// Raises the PropertyChanged event for the UIPanelDock property.
        /// </summary>
        private void UpdateUIPanelDock()
        {
            this.RaisePropertyChanged(nameof(UIPanelDock));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the MapPanelOrientation property.
        /// </summary>
        private void UpdateMapOrientation()
        {
            this.RaisePropertyChanged(nameof(MapPanelOrientation));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the UIPanelOrientationDock property.
        /// </summary>
        private void UpdateUIPanelOrientationDock()
        {
            this.RaisePropertyChanged(nameof(UIPanelOrientationDock));
        }

        /// <summary>
        /// Changes the expected orientation layout, if dynamic orientation is enabled,
        /// to the specified orientation.
        /// </summary>
        /// <param name="orientation">
        /// The new expected orientation layout.
        /// </param>
        public void ChangeLayout(Orientation orientation)
        {
            Orientation = orientation;
        }

        /// <summary>
        /// Saves the tracker data to a file at the specified path.
        /// </summary>
        /// <param name="path">
        /// The file path to which the tracker data is to be saved.
        /// </param>
        public void Save(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            SaveData saveData = new SaveData(_game);

            string json = JsonConvert.SerializeObject(saveData);

            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Opens and reads the tracker data from a file at the specified path.
        /// </summary>
        /// <param name="path">
        /// The file path to which the tracker data is to be opened.
        /// </param>
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
            {
                _game.Items[item].SetCurrent(saveData.ItemCounts[item]);
            }

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

            foreach ((LocationID, int) locationIndex in saveData.PrizePlacements.Keys)
            {
                if (saveData.PrizePlacements[locationIndex] == null)
                {
                    _game.Locations[locationIndex.Item1].BossSections[locationIndex.Item2].Prize = null;
                }
                else
                {
                    _game.Locations[locationIndex.Item1].BossSections[locationIndex.Item2].Prize =
                        _game.Items[saveData.PrizePlacements[locationIndex].Value];
                }
            }

            foreach ((LocationID, int) locationIndex in saveData.BossPlacements.Keys)
            {
                if (saveData.BossPlacements[locationIndex] == null)
                {
                    _game.Locations[locationIndex.Item1].BossSections[locationIndex.Item2].BossPlacement.Boss = null;
                }
                else
                {
                    _game.Locations[locationIndex.Item1].BossSections[locationIndex.Item2].BossPlacement.Boss =
                        saveData.BossPlacements[locationIndex].Value;
                }
            }

            foreach ((LocationID, int, LocationID, int) connection in saveData.Connections)
            {
                MapLocation location1 = _game.Locations[connection.Item1].MapLocations[connection.Item2];
                MapLocation location2 = _game.Locations[connection.Item3].MapLocations[connection.Item4];

                _game.Connections.Add((location1, location2));
            }
        }

        /// <summary>
        /// Toggles whether to display all locations on the map.
        /// </summary>
        private void ToggleDisplayAllLocations()
        {
            _appSettings.DisplayAllLocations = !_appSettings.DisplayAllLocations;
        }

        /// <summary>
        /// Resets the undo/redo manager, pinned locations, and game data to their starting values.
        /// </summary>
        private void Reset()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                _undoRedoManager.Reset();
                Locations.Clear();
                _game.Reset();
            });
        }

        /// <summary>
        /// Opens a reset dialog window.
        /// </summary>
        private void OpenResetDialog()
        {
            Dispatcher.UIThread.InvokeAsync(async () =>
            {
                bool? result = await _dialogService.ShowDialog(
                    new MessageBoxDialogVM("Warning",
                    "Resetting the tracker will set all items and locations back to their starting values." +
                    "  This cannot be undone.\n\nDo you wish to proceed?")).ConfigureAwait(false);

                if (result.HasValue && result.Value)
                {
                    Reset();
                }
            });
        }

        /// <summary>
        /// Returns the observable result of the OpenResetDialog method.
        /// </summary>
        /// <returns>
        /// The observable result of the OpenResetDialog method.
        /// </returns>
        private IObservable<Unit> OpenResetDialogAsync()
        {
            return Observable.Start(() => { OpenResetDialog(); });
        }

        /// <summary>
        /// Saves the app settings to a file.
        /// </summary>
        /// <param name="maximized">
        /// A boolean representing whether the window is maximized.
        /// </param>
        /// <param name="bounds">
        /// The boundaries of the window.
        /// </param>
        public void SaveAppSettings(bool maximized, Rect bounds)
        {
            _appSettings.Maximized = maximized;

            _appSettings.X = bounds.X;
            _appSettings.Y = bounds.Y;
            _appSettings.Width = bounds.Width;
            _appSettings.Height = bounds.Height;

            string appSettingsPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "OpenTracker", "OpenTracker.json");

            if (File.Exists(appSettingsPath))
            {
                File.Delete(appSettingsPath);
            }

            string json = JsonConvert.SerializeObject(_appSettings, Formatting.Indented);

            File.WriteAllText(appSettingsPath, json);
        }

        /// <summary>
        /// Returns a new autotracker view-model.
        /// </summary>
        /// <returns>
        /// A new autotracker view-model.
        /// </returns>
        public object GetAutoTrackerViewModel()
        {
            return new AutoTrackerDialogVM(_game.AutoTracker);
        }

        /// <summary>
        /// Returns a new color select view-model.
        /// </summary>
        /// <returns>
        /// A new color select view-model.
        /// </returns>
        public object GetColorSelectViewModel()
        {
            return new ColorSelectDialogVM(_appSettings);
        }
    }
}
