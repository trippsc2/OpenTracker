using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Actions;
using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Reactive;

namespace OpenTracker.ViewModels
{
    public class MapLocationControlVM : ViewModelBase, IChangeMarking, IClearAvailableSections, IOpenMarkingSelect,
        IPinLocation, IPointerOver
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly AppSettings _appSettings;
        private readonly Game _game;
        private readonly MainWindowVM _mainWindow;
        private readonly MapLocation _mapLocation;
        private readonly Dock _nonEntranceDock;
        private readonly Dock _entranceDock;

        public ObservableCollection<MarkingSelectControlVM> ItemSelect =>
            MainWindowVM.NonEntranceMarkingSelect;

        private bool _highlighted;
        public bool Highlighted
        {
            get => _highlighted;
            private set => this.RaiseAndSetIfChanged(ref _highlighted, value);
        }

        public string BorderColor
        {
            get
            {
                if (Highlighted)
                    return "#FFFFFFFF";
                else
                    return "#FF000000";
            }
        }

        public double CanvasX
        {
            get
            {
                double x = ImageDock switch
                {
                    Dock.Left => _mapLocation.X - (Size / 2) - 55,
                    Dock.Right => _mapLocation.X - (Size / 2),
                    _ => Math.Min(_mapLocation.X - (Size / 2), _mapLocation.X - 27.5),
                };

                if (_mainWindow.MapPanelOrientation == Orientation.Vertical)
                    return x + 23;
                else
                {
                    if (_mapLocation.Map == MapID.DarkWorld)
                        return x + 2046;
                    else
                        return x + 13;
                }
            }
        }

        public double CanvasY
        {
            get
            {
                double y = ImageDock switch
                {
                    Dock.Bottom => _mapLocation.Y - (Size / 2),
                    Dock.Top => _mapLocation.Y - (Size / 2) - 55,
                    _ => Math.Min(_mapLocation.Y - (Size / 2), _mapLocation.Y - 27.5),
                };

                if (_mainWindow.MapPanelOrientation == Orientation.Vertical)
                {
                    if (_mapLocation.Map == MapID.DarkWorld)
                        return y + 2046;
                    else
                        return y + 13;
                }
                else
                    return y + 23;
            }
        }

        public bool Visible
        {
            get
            {
                return _game.Mode.Validate(_mapLocation.VisibilityMode) &&
                    (_appSettings.DisplayAllLocations ||
                    ((_mapLocation.Location.Sections[0].Marking != null ||
                    _mapLocation.Location.Accessibility != AccessibilityLevel.Cleared) &&
                    (_mapLocation.Location.Sections[0] is EntranceSection ||
                    _mapLocation.Location.Accessibility != AccessibilityLevel.None)));
            }
        }

        private Dock _imageDock;
        public Dock ImageDock
        {
            get => _imageDock;
            private set => this.RaiseAndSetIfChanged(ref _imageDock, value);
        }

        public bool ImageVisible =>
            _mapLocation.Location.Sections[0].HasMarking;

        public string ImageSource
        {
            get
            {
                ISection section = _mapLocation.Location.Sections[0];

                if (section.Marking == null)
                    return "avares://OpenTracker/Assets/Images/Items/unknown1.png";
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
                            return "avares://OpenTracker/Assets/Images/Items/visible-" +
                                section.Marking.ToString().ToLowerInvariant() + ".png";
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
                            return "avares://OpenTracker/Assets/Images/Items/" +
                                section.Marking.ToString().ToLowerInvariant() + "1.png";
                        case MarkingType.Bottle:
                        case MarkingType.Gloves:
                        case MarkingType.Shield:
                        case MarkingType.Mail:
                            Item item = _game.Items[Enum.Parse<ItemType>(section.Marking.ToString())];
                            itemNumber = Math.Min(item.Current + 1, item.Maximum);
                            return "avares://OpenTracker/Assets/Images/Items/" +
                                section.Marking.ToString().ToLowerInvariant() +
                                itemNumber.ToString(CultureInfo.InvariantCulture) + ".png";
                        case MarkingType.Sword:

                            Item sword = _game.Items[ItemType.Sword];

                            if (sword.Current == 0)
                                itemNumber = 0;
                            else
                                itemNumber = Math.Min(sword.Current + 1, sword.Maximum);

                            return "avares://OpenTracker/Assets/Images/Items/" +
                                section.Marking.ToString().ToLowerInvariant() +
                                itemNumber.ToString(CultureInfo.InvariantCulture) + ".png";

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
                            return "avares://OpenTracker/Assets/Images/" +
                                section.Marking.ToString().ToLowerInvariant() + ".png";
                        case MarkingType.ToH:
                            return "avares://OpenTracker/Assets/Images/th.png";
                        case MarkingType.PoD:
                            return "avares://OpenTracker/Assets/Images/pd.png";
                        case MarkingType.Ganon:
                            return "avares://OpenTracker/Assets/Images/ganon.png";
                    }
                }

                return null;
            }
        }

        public double Size
        {
            get
            {
                if ((_game.Mode.EntranceShuffle.HasValue &&
                    _game.Mode.EntranceShuffle.Value) ||
                    _mapLocation.Location.Sections[0] is TakeAnySection)
                    return 40.0;
                else
                {
                    if (_mapLocation.Location.Total > 1)
                    {
                        switch (_mapLocation.Location.ID)
                        {
                            case LocationID.EasternPalace:
                            case LocationID.DesertPalace:
                            case LocationID.TowerOfHera:
                            case LocationID.PalaceOfDarkness:
                            case LocationID.SwampPalace:
                            case LocationID.SkullWoods:
                            case LocationID.ThievesTown:
                            case LocationID.IcePalace:
                            case LocationID.MiseryMire:
                            case LocationID.TurtleRock:
                            case LocationID.GanonsTower:
                                return 130.0;
                            default:
                                return 90.0;
                        }
                    }
                    else
                        return 70.0;
                }
            }
        }

        public string Color =>
            _appSettings.AccessibilityColors[_mapLocation.Location.Accessibility];

        public Thickness BorderSize
        {
            get
            {
                if ((_game.Mode.EntranceShuffle.HasValue &&
                    _game.Mode.EntranceShuffle.Value) ||
                    _mapLocation.Location.Sections[0] is TakeAnySection)
                    return new Thickness(5);
                else
                    return new Thickness(9);
            }
        }

        public bool TextVisible
        {
            get
            {
                if (_game.Mode.EntranceShuffle.Value)
                    return false;

                if (!_appSettings.ShowItemCountsOnMap)
                    return false;

                if (_mapLocation.Location.Available == 0)
                    return false;

                if (_mapLocation.Location.Total <= 1)
                    return false;

                return true;
            }
        }

        public string Text
        {
            get
            {
                if (_game.Mode.EntranceShuffle.Value)
                    return null;

                if (!_appSettings.ShowItemCountsOnMap)
                    return null;

                if (_mapLocation.Location.Available == 0)
                    return null;

                if (_mapLocation.Location.Total <= 1)
                    return null;

                if (_mapLocation.Location.Available == _mapLocation.Location.Accessible)
                    return _mapLocation.Location.Available.ToString(CultureInfo.InvariantCulture);
                else if (_mapLocation.Location.Accessible == 0)
                    return _mapLocation.Location.Available.ToString(CultureInfo.InvariantCulture);
                else
                    return _mapLocation.Location.Accessible.ToString(CultureInfo.InvariantCulture) + "/" +
                        _mapLocation.Location.Available.ToString(CultureInfo.InvariantCulture);
            }
        }

        public CornerRadius CornerRadius
        {
            get
            {
                if (_mapLocation.Location.Sections[0] is TakeAnySection)
                    return new CornerRadius(40);
                else
                    return new CornerRadius(0);
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
                if (_mapLocation.Location.Sections[0] is EntranceSection)
                    return 280.0;
                else
                    return 200.0;
            }
        }

        public double MarkingPopupWidth
        {
            get
            {
                if (_mapLocation.Location.Sections[0] is EntranceSection)
                    return 272.0;
                else
                    return 238.0;
            }
        }

        public ReactiveCommand<MarkingType?, Unit> ChangeMarkingCommand { get; }
        public ReactiveCommand<Unit, Unit> ClearVisibleItemCommand { get; }

        public MapLocationControlVM(UndoRedoManager undoRedoManager, AppSettings appSettings,
            Game game, MainWindowVM mainWindow, MapLocation mapLocation)
        {
            _undoRedoManager = undoRedoManager;
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _mainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));
            _mapLocation = mapLocation ?? throw new ArgumentNullException(nameof(mapLocation));

            ChangeMarkingCommand = ReactiveCommand.Create<MarkingType?>(ChangeMarking);
            ClearVisibleItemCommand = ReactiveCommand.Create(ClearMarking);

            switch (_mapLocation.Location.ID)
            {
                case LocationID.EtherTablet:
                case LocationID.FloatingIsland:
                case LocationID.GanonsTower:
                    _nonEntranceDock = Dock.Left;
                    _entranceDock = Dock.Left;
                    break;
                case LocationID.BumperCave:
                    _nonEntranceDock = Dock.Top;
                    _entranceDock = Dock.Left;
                    break;
                case LocationID.DesertPalace:
                    _nonEntranceDock = Dock.Top;
                    _entranceDock = Dock.Bottom;
                    break;
                default:
                    _nonEntranceDock = Dock.Top;
                    _entranceDock = Dock.Top;
                    break;
            }

            UpdateImageDock();

            PropertyChanged += OnPropertyChanged;

            _appSettings.PropertyChanged += OnAppSettingsChanged;
            _appSettings.AccessibilityColors.PropertyChanged += OnColorChanged;
            _game.Mode.PropertyChanged += OnModeChanged;
            _mainWindow.PropertyChanged += OnMainWindowChanged;
            _mapLocation.Location.PropertyChanged += OnLocationChanged;

            foreach (ISection section in mapLocation.Location.Sections)
            {
                section.PropertyChanging += OnSectionChanging;
                section.PropertyChanged += OnSectionChanged;
            }

        }

        private void UpdateBorderColor()
        {
            this.RaisePropertyChanged(nameof(BorderColor));
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Highlighted))
                UpdateBorderColor();

            if (e.PropertyName == nameof(ImageDock))
                UpdateSizeAndPosition();
        }

        private void OnAppSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AppSettings.DisplayAllLocations))
                UpdateVisibility();

            if (e.PropertyName == nameof(AppSettings.ShowItemCountsOnMap))
                UpdateText();
        }

        private void OnColorChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateColor();
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateSizeAndPosition();
            UpdateVisibility();
            UpdateText();

            if (e.PropertyName == nameof(Mode.EntranceShuffle))
                UpdateImageDock();
        }

        private void OnMainWindowChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainWindowVM.MapPanelOrientation))
                UpdateSizeAndPosition();
        }

        private void OnLocationChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Models.Location.Accessibility))
            {
                UpdateColor();
                UpdateVisibility();
            }

            if (e.PropertyName == nameof(Models.Location.Accessible))
                UpdateText();

            if (e.PropertyName == nameof(Models.Location.Available))
                UpdateText();

            if (e.PropertyName == nameof(Models.Location.Total))
            {
                UpdateSizeAndPosition();
                UpdateText();
            }
        }

        private void OnSectionChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == nameof(ISection.Marking))
                UnsubscribeFromMarkingItem((ISection)sender);
        }

        private void OnSectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ISection.Marking))
            {
                UpdateSizeAndPosition();
                UpdateImage();
                UpdateVisibility();
                SubscribeToMarkingItem((ISection)sender);
            }
        }

        private void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateImage();
        }

        private void UpdateSizeAndPosition()
        {
            this.RaisePropertyChanged(nameof(BorderSize));
            this.RaisePropertyChanged(nameof(Size));
            this.RaisePropertyChanged(nameof(CanvasX));
            this.RaisePropertyChanged(nameof(CanvasY));
        }

        private void UpdateColor()
        {
            this.RaisePropertyChanged(nameof(Color));
        }

        private void UpdateVisibility()
        {
            this.RaisePropertyChanged(nameof(ImageVisible));
            this.RaisePropertyChanged(nameof(Visible));
        }

        private void UpdateImage()
        {
            this.RaisePropertyChanged(nameof(ImageSource));
        }

        private void UpdateText()
        {
            this.RaisePropertyChanged(nameof(TextVisible));
            this.RaisePropertyChanged(nameof(Text));
        }

        private void UpdateImageDock()
        {
            if (_game.Mode.EntranceShuffle.Value)
                ImageDock = _entranceDock;
            else
                ImageDock = _nonEntranceDock;
        }

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
                        ItemType itemType = Enum.Parse<ItemType>(section.Marking.Value.ToString());
                        _game.Items[itemType].PropertyChanged -= OnItemChanged;
                        break;
                }
            }
        }

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
                        ItemType itemType = Enum.Parse<ItemType>(section.Marking.Value.ToString());
                        _game.Items[itemType].PropertyChanged += OnItemChanged;
                        break;
                }
            }
        }

        private void ClearMarking()
        {
            _undoRedoManager.Execute(new MarkSection(_mapLocation.Location.Sections[0], null));
            MarkingPopupOpen = false;
        }

        public void ChangeMarking(MarkingType? marking)
        {
            if (marking != null)
            {
                _undoRedoManager.Execute(new MarkSection(_mapLocation.Location.Sections[0], marking));
                MarkingPopupOpen = false;
            }
        }

        public void OpenMarkingSelect()
        {
            MarkingPopupOpen = true;
        }

        public void ClearAvailableSections()
        {
            bool canBeCleared = false;

            foreach (ISection section in _mapLocation.Location.Sections)
            {
                if (section.IsAvailable() &&
                    (section.Accessibility >= AccessibilityLevel.Partial ||
                    (section.Accessibility == AccessibilityLevel.Inspect &&
                    section.Marking == null) ||
                    section is EntranceSection))
                    canBeCleared = true;
            }

            if (canBeCleared)
                _undoRedoManager.Execute(new ClearLocation(_game, _mapLocation.Location));
        }

        public void PinLocation()
        {
            ObservableCollection<LocationControlVM> pinnedLocations = _mainWindow.Locations;
            LocationControlVM existingPinnedLocation = null;

            foreach (LocationControlVM pinnedLocation in pinnedLocations)
            {
                if (pinnedLocation.Location == _mapLocation.Location)
                    existingPinnedLocation = pinnedLocation;
            }

            if (existingPinnedLocation == null)
            {
                _undoRedoManager.Execute(new PinLocation(pinnedLocations,
                    new LocationControlVM(_undoRedoManager, _appSettings, _game, _mainWindow,
                    _mapLocation.Location)));
            }
            else if (pinnedLocations[0] != existingPinnedLocation)
                _undoRedoManager.Execute(new PinLocation(pinnedLocations, existingPinnedLocation));
        }

        public void OnPointerEnter()
        {
            Highlighted = true;
        }

        public void OnPointerLeave()
        {
            Highlighted = false;
        }
    }
}
