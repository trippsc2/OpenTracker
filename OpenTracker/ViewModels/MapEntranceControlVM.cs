using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Actions;
using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using OpenTracker.Models.Sections;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Reactive;

namespace OpenTracker.ViewModels
{
    public class MapEntranceControlVM : ViewModelBase, IChangeMarking, IClearAvailableSections, IConnectLocation,
        IOpenMarkingSelect, IPinLocation, IPointerOver
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly AppSettings _appSettings;
        private readonly Game _game;
        private readonly MainWindowVM _mainWindow;

        public MapLocation MapLocation { get; }

        public ObservableCollection<MarkingSelectControlVM> ItemSelect =>
            MainWindowVM.EntranceMarkingSelect;

        private bool _highlighted;
        public bool Highlighted
        {
            get => _highlighted;
            private set => this.RaiseAndSetIfChanged(ref _highlighted, value);
        }

        public double CanvasX
        {
            get
            {
                double x = ImageDock switch
                {
                    Dock.Left => MapLocation.X - 84,
                    Dock.Right => MapLocation.X,
                    _ => MapLocation.X - 28,
                };

                if (_mainWindow.MapPanelOrientation == Orientation.Vertical)
                {
                    return x + 23;
                }
                
                if (MapLocation.Map == MapID.DarkWorld)
                {
                    return x + 2046;
                }
                
                return x + 13;
            }
        }

        public double CanvasY
        {
            get
            {
                double y = ImageDock switch
                {
                    Dock.Bottom => MapLocation.Y,
                    Dock.Top => MapLocation.Y - 84,
                    _ => MapLocation.Y - 28,
                };

                if (_mainWindow.MapPanelOrientation == Orientation.Vertical)
                {
                    if (MapLocation.Map == MapID.DarkWorld)
                    {
                        return y + 2046;
                    }
                    
                    return y + 13;
                }
                
                return y + 23;
            }
        }

        public bool Visible
        {
            get
            {
                return _game.Mode.Validate(MapLocation.ModeRequirement) &&
                    (_appSettings.DisplayAllLocations ||
                    ((MapLocation.Location.Sections[0].Marking != null ||
                    MapLocation.Location.Accessibility != AccessibilityLevel.Cleared) &&
                    (MapLocation.Location.Sections[0] is EntranceSection ||
                    MapLocation.Location.Accessibility != AccessibilityLevel.None)));
            }
        }

        public Dock ImageDock { get; }

        public string ImageSource
        {
            get
            {
                ISection section = MapLocation.Location.Sections[0];

                if (section.Marking == null)
                {
                    return "avares://OpenTracker/Assets/Images/Items/unknown1.png";
                }
                else
                {
                    int itemNumber;

                    switch (section.Marking)
                    {
                        case MarkingType.Bow:
                        case MarkingType.SilverArrows:
                        case MarkingType.Boomerang:
                        case MarkingType.RedBoomerang:
                        case MarkingType.SmallKey:
                        case MarkingType.BigKey:
                            {
                                return $"avares://OpenTracker/Assets/Images/Items/visible-" +
                                    $"{ section.Marking.ToString().ToLowerInvariant() }.png";
                            }
                        case MarkingType.Hookshot:
                        case MarkingType.Bomb:
                        case MarkingType.Mushroom:
                        case MarkingType.Powder:
                        case MarkingType.FireRod:
                        case MarkingType.IceRod:
                        case MarkingType.Bombos:
                        case MarkingType.Ether:
                        case MarkingType.Quake:
                        case MarkingType.Shovel:
                        case MarkingType.Lamp:
                        case MarkingType.Hammer:
                        case MarkingType.Flute:
                        case MarkingType.Net:
                        case MarkingType.Book:
                        case MarkingType.MoonPearl:
                        case MarkingType.CaneOfSomaria:
                        case MarkingType.CaneOfByrna:
                        case MarkingType.Cape:
                        case MarkingType.Mirror:
                        case MarkingType.Boots:
                        case MarkingType.Flippers:
                        case MarkingType.HalfMagic:
                        case MarkingType.Aga:
                            {
                                return $"avares://OpenTracker/Assets/Images/Items/" +
                                    $"{ section.Marking.ToString().ToLowerInvariant() }1.png";
                            }
                        case MarkingType.Bottle:
                        case MarkingType.Gloves:
                        case MarkingType.Shield:
                        case MarkingType.Mail:
                            {
                                var item = _game.Items[Enum.Parse<ItemType>(section.Marking.ToString())];
                                itemNumber = Math.Min(item.Current + 1, item.Maximum);

                                return $"avares://OpenTracker/Assets/Images/Items/" +
                                    $"{ section.Marking.ToString().ToLowerInvariant() }" +
                                    $"{ itemNumber.ToString(CultureInfo.InvariantCulture) }.png";
                            }
                        case MarkingType.Sword:
                            {
                                var sword = _game.Items[ItemType.Sword];

                                if (sword.Current == 0)
                                {
                                    itemNumber = 0;
                                }
                                else
                                {
                                    itemNumber = Math.Min(sword.Current + 1, sword.Maximum);
                                }

                                return $"avares://OpenTracker/Assets/Images/Items/" +
                                    $"{ section.Marking.ToString().ToLowerInvariant() }" +
                                    $"{ itemNumber.ToString(CultureInfo.InvariantCulture) }.png";
                            }
                        case MarkingType.HCFront:
                        case MarkingType.HCLeft:
                        case MarkingType.HCRight:
                        case MarkingType.EP:
                        case MarkingType.SP:
                        case MarkingType.SW:
                        case MarkingType.DPFront:
                        case MarkingType.DPLeft:
                        case MarkingType.DPRight:
                        case MarkingType.DPBack:
                        case MarkingType.TT:
                        case MarkingType.IP:
                        case MarkingType.MM:
                        case MarkingType.TRFront:
                        case MarkingType.TRLeft:
                        case MarkingType.TRRight:
                        case MarkingType.TRBack:
                        case MarkingType.GT:
                            {
                                return $"avares://OpenTracker/Assets/Images/" +
                                    $"{ section.Marking.ToString().ToLowerInvariant() }.png";
                            }
                        case MarkingType.ToH:
                            {
                                return "avares://OpenTracker/Assets/Images/th.png";
                            }
                        case MarkingType.PoD:
                            {
                                return "avares://OpenTracker/Assets/Images/pd.png";
                            }
                        case MarkingType.Ganon:
                            {
                                return "avares://OpenTracker/Assets/Images/ganon.png";
                            }
                    }
                }

                return null;
            }
        }

        public List<Point> Points
        {
            get
            {
                return ImageDock switch
                {
                    Dock.Left => new List<Point>()
                        {
                            new Point(0, 0),
                            new Point(0, 56),
                            new Point(28, 28)
                        },
                    Dock.Bottom => new List<Point>()
                        {
                            new Point(0, 28),
                            new Point(56, 28),
                            new Point(28, 0)
                        },
                    Dock.Right => new List<Point>()
                        {
                            new Point(28, 0),
                            new Point(28, 56),
                            new Point(0, 28)
                        },
                    _ => new List<Point>()
                        {
                            new Point(0, 0),
                            new Point(56, 0),
                            new Point(28, 28)
                        },
                };
            }
        }

        public string Color =>
            _appSettings.AccessibilityColors[MapLocation.Location.Accessibility];

        public string BorderColor
        {
            get
            {
                if (Highlighted)
                {
                    return "#FFFFFFFF";
                }
                
                return "#FF000000";
            }
        }

        private bool _markingPopupOpen;
        public bool MarkingPopupOpen
        {
            get => _markingPopupOpen;
            set => this.RaiseAndSetIfChanged(ref _markingPopupOpen, value);
        }

        public double MarkingPopupHeight
        {
            get
            {
                if (MapLocation.Location.Sections[0] is EntranceSection)
                {
                    return 280.0;
                }
                
                return 200.0;
            }
        }

        public double MarkingPopupWidth
        {
            get
            {
                if (MapLocation.Location.Sections[0] is EntranceSection)
                {
                    return 272.0;
                }
                
                return 238.0;
            }
        }

        public ReactiveCommand<MarkingType?, Unit> ChangeMarkingCommand { get; }
        public ReactiveCommand<Unit, Unit> ClearVisibleItemCommand { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="appSettings">
        /// The app settings.
        /// </param>
        /// <param name="game">
        /// The game data.
        /// </param>
        /// <param name="mainWindow">
        /// The view-model of the main window.
        /// </param>
        /// <param name="mapLocation">
        /// The map location to be represented.
        /// </param>
        public MapEntranceControlVM(UndoRedoManager undoRedoManager, AppSettings appSettings,
            Game game, MainWindowVM mainWindow, MapLocation mapLocation)
        {
            _undoRedoManager = undoRedoManager;
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _mainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));
            MapLocation = mapLocation ?? throw new ArgumentNullException(nameof(mapLocation));

            ChangeMarkingCommand = ReactiveCommand.Create<MarkingType?>(ChangeMarking);
            ClearVisibleItemCommand = ReactiveCommand.Create(ClearMarking);

            switch (MapLocation.Location.ID)
            {
                case LocationID.WomanLeftDoor:
                case LocationID.TavernFront:
                case LocationID.ForestChestGameEntrance:
                case LocationID.CastleMainEntrance:
                case LocationID.CastleTowerEntrance:
                case LocationID.EasternPalaceEntrance:
                case LocationID.DesertFrontEntrance:
                case LocationID.SkullWoodsBack:
                case LocationID.ThievesTownEntrance:
                case LocationID.BumperCaveEntrance:
                case LocationID.SwampPalaceEntrance:
                case LocationID.PalaceOfDarknessEntrance:
                case LocationID.DarkIceRodRockEntrance:
                case LocationID.IceFairyCaveEntrance:
                case LocationID.IcePalaceEntrance:
                case LocationID.MiseryMireEntrance:
                case LocationID.TowerOfHeraEntrance:
                case LocationID.ParadoxCaveMiddle:
                case LocationID.ParadoxCaveBottom:
                case LocationID.EDMConnectorBottom:
                case LocationID.TurtleRockEntrance:
                case LocationID.GanonsTowerEntrance:
                    {
                        ImageDock = Dock.Bottom;
                    }
                    break;
                case LocationID.LibraryEntrance:
                case LocationID.DeathMountainEntryCave:
                case LocationID.DarkIceRodCaveEntrance:
                case LocationID.IceRodCaveEntrance:
                case LocationID.SpiralCaveBottom:
                case LocationID.SpiralCaveTop:
                case LocationID.HookshotCaveTop:
                    {
                        ImageDock = Dock.Left;
                    }
                    break;
                case LocationID.MimicCaveEntrance:
                case LocationID.DeathMountainShop:
                case LocationID.TRLedgeRight:
                    {
                        ImageDock = Dock.Right;
                    }
                    break;
                default:
                    {
                        ImageDock = Dock.Top;
                    }
                    break;
            }

            PropertyChanged += OnPropertyChanged;

            _appSettings.PropertyChanged += OnAppSettingsChanged;
            _appSettings.AccessibilityColors.PropertyChanged += OnColorChanged;
            _game.Mode.PropertyChanged += OnModeChanged;
            _mainWindow.PropertyChanged += OnMainWindowChanged;
            MapLocation.Location.PropertyChanged += OnLocationChanged;

            foreach (ISection section in mapLocation.Location.Sections)
            {
                section.PropertyChanging += OnSectionChanging;
                section.PropertyChanged += OnSectionChanged;
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
            if (e.PropertyName == nameof(Highlighted))
            {
                UpdateBorderColor();
            }

            if (e.PropertyName == nameof(ImageDock))
            {
                UpdatePosition();
            }
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
            if (e.PropertyName == nameof(AppSettings.DisplayAllLocations))
            {
                UpdateVisibility();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ObservableCollection for the
        /// accessibility colors.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnColorChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateColor();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Mode class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdatePosition();

            if (e.PropertyName == nameof(Mode.WorldState) &&
                MapLocation.ModeRequirement.WorldState.HasValue)
            {
                UpdateVisibility();
            }

            if (e.PropertyName == nameof(Mode.EntranceShuffle))
            {
                UpdateVisibility();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the MainWindowVM class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnMainWindowChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainWindowVM.MapPanelOrientation))
            {
                UpdatePosition();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Location class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnLocationChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Models.Location.Accessibility))
            {
                UpdateColor();
                UpdateVisibility();
            }

            if (e.PropertyName == nameof(Models.Location.Total))
            {
                UpdatePosition();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanging event on the ISection-implementing classes.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanging event.
        /// </param>
        private void OnSectionChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == nameof(ISection.Marking))
            {
                UnsubscribeFromMarkingItem((ISection)sender);
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ISection-implementing classes.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnSectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ISection.Marking))
            {
                UpdatePosition();
                UpdateImage();
                UpdateVisibility();
                SubscribeToMarkingItem((ISection)sender);
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Item class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateImage();
        }

        /// <summary>
        /// Raises the PropertyChanged event for the BorderColor property.
        /// </summary>
        private void UpdateBorderColor()
        {
            this.RaisePropertyChanged(nameof(BorderColor));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the CanvasX and CanvasY property.
        /// </summary>
        private void UpdatePosition()
        {
            this.RaisePropertyChanged(nameof(CanvasX));
            this.RaisePropertyChanged(nameof(CanvasY));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the Color property.
        /// </summary>
        private void UpdateColor()
        {
            this.RaisePropertyChanged(nameof(Color));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the Visible property.
        /// </summary>
        private void UpdateVisibility()
        {
            this.RaisePropertyChanged(nameof(Visible));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the ImageSource property.
        /// </summary>
        private void UpdateImage()
        {
            this.RaisePropertyChanged(nameof(ImageSource));
        }

        /// <summary>
        /// Unsubscribes from the marking item of the specified section.
        /// </summary>
        /// <param name="section">
        /// The section marking from which to unsubscribe.
        /// </param>
        private void UnsubscribeFromMarkingItem(ISection section)
        {
            if (section.Marking.HasValue)
            {
                switch (section.Marking.Value)
                {
                    case MarkingType.Bottle:
                    case MarkingType.Gloves:
                    case MarkingType.Sword:
                    case MarkingType.Shield:
                    case MarkingType.Mail:
                        {
                            ItemType itemType = Enum.Parse<ItemType>(section.Marking.Value.ToString());
                            _game.Items[itemType].PropertyChanged -= OnItemChanged;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Subscribes to the marking item of the specified section.
        /// </summary>
        /// <param name="section">
        /// The section marking to which to subscribe.
        /// </param>
        private void SubscribeToMarkingItem(ISection section)
        {
            if (section.Marking.HasValue)
            {
                switch (section.Marking.Value)
                {
                    case MarkingType.Bottle:
                    case MarkingType.Gloves:
                    case MarkingType.Sword:
                    case MarkingType.Shield:
                    case MarkingType.Mail:
                        {
                            ItemType itemType = Enum.Parse<ItemType>(section.Marking.Value.ToString());
                            _game.Items[itemType].PropertyChanged += OnItemChanged;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Clears the marking of the first section of the location.
        /// </summary>
        private void ClearMarking()
        {
            _undoRedoManager.Execute(new MarkSection(MapLocation.Location.Sections[0], null));
            MarkingPopupOpen = false;
        }

        /// <summary>
        /// Changes the marking of the first section of the location to the specified
        /// marking.
        /// </summary>
        /// <param name="marking">
        /// The marking to be set.
        /// </param>
        public void ChangeMarking(MarkingType? marking)
        {
            if (marking != null)
            {
                _undoRedoManager.Execute(new MarkSection(MapLocation.Location.Sections[0], marking));
                MarkingPopupOpen = false;
            }
        }

        /// <summary>
        /// Opens the Marking Select popup.
        /// </summary>
        public void OpenMarkingSelect()
        {
            MarkingPopupOpen = true;
        }

        /// <summary>
        /// Clears available sections.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether to ignore logic for clearing the sections.
        /// </param>
        public void ClearAvailableSections(bool force = false)
        {
            bool canBeCleared = false;

            foreach (ISection section in MapLocation.Location.Sections)
            {
                if (section.IsAvailable() && (force || section is EntranceSection ||
                    section.Accessibility >= AccessibilityLevel.Partial ||
                    (section.Accessibility == AccessibilityLevel.Inspect &&
                    section.Marking == null)))
                {
                    canBeCleared = true;
                }
            }

            if (canBeCleared)
            {
                _undoRedoManager.Execute(new ClearLocation(_game, MapLocation.Location, force));
            }
        }

        /// <summary>
        /// Pins the location.
        /// </summary>
        public void PinLocation()
        {
            ObservableCollection<LocationControlVM> pinnedLocations = _mainWindow.Locations;
            LocationControlVM existingPinnedLocation = null;

            foreach (LocationControlVM pinnedLocation in pinnedLocations)
            {
                if (pinnedLocation.Location == MapLocation.Location)
                {
                    existingPinnedLocation = pinnedLocation;
                }
            }

            if (existingPinnedLocation == null)
            {
                _undoRedoManager.Execute(new PinLocation(pinnedLocations,
                    new LocationControlVM(_undoRedoManager, _appSettings, _game, _mainWindow,
                    MapLocation.Location)));
            }
            else if (pinnedLocations[0] != existingPinnedLocation)
            {
                _undoRedoManager.Execute(new PinLocation(pinnedLocations, existingPinnedLocation));
            }
        }

        /// <summary>
        /// Handler for when the pointer enters the control.
        /// </summary>
        public void OnPointerEnter()
        {
            Highlighted = true;
        }

        /// <summary>
        /// Handler for when the pointer exits the control.
        /// </summary>
        public void OnPointerLeave()
        {
            Highlighted = false;
        }

        /// <summary>
        /// Connect this entrance location to the specified location.
        /// </summary>
        /// <param name="location">
        /// The location to which this location is connected.
        /// </param>
        public void ConnectLocation(IConnectLocation location)
        {
            if (location == null)
            {
                return;
            }

            if (location is MapEntranceControlVM entrance)
            {
                _undoRedoManager.Execute(new AddConnection(_game,
                    (entrance.MapLocation, MapLocation)));
            }
        }
    }
}
