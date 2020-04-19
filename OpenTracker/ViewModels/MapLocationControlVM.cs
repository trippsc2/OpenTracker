using Avalonia;
using Avalonia.Media;
using OpenTracker.Actions;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    public class MapLocationControlVM : ViewModelBase, IMapLocationControlVM
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly AppSettingsVM _appSettings;
        private readonly Game _game;
        private readonly MainWindowVM _mainWindow;
        private readonly MapLocation _mapLocation;

        private double _canvasX;
        public double CanvasX
        {
            get => _canvasX;
            private set => this.RaiseAndSetIfChanged(ref _canvasX, value);
        }

        private double _canvasY;
        public double CanvasY
        {
            get => _canvasY;
            private set => this.RaiseAndSetIfChanged(ref _canvasY, value);
        }

        private double _size;
        public double Size
        {
            get => _size;
            private set => this.RaiseAndSetIfChanged(ref _size, value);
        }

        private Thickness _borderSize;
        public Thickness BorderSize
        {
            get => _borderSize;
            private set => this.RaiseAndSetIfChanged(ref _borderSize, value);
        }

        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            private set => this.RaiseAndSetIfChanged(ref _accessibility, value);
        }

        private IBrush _color;
        public IBrush Color
        {
            get => _color;
            set => this.RaiseAndSetIfChanged(ref _color, value);
        }

        private bool _visible;
        public bool Visible
        {
            get => _visible;
            private set => this.RaiseAndSetIfChanged(ref _visible, value);
        }

        private bool _imageVisible;
        public bool ImageVisible
        {
            get => _imageVisible;
            private set => this.RaiseAndSetIfChanged(ref _imageVisible, value);
        }

        private bool _borderVisible;
        public bool BorderVisible
        {
            get => _borderVisible;
            set => this.RaiseAndSetIfChanged(ref _borderVisible, value);
        }

        private string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            private set => this.RaiseAndSetIfChanged(ref _imageSource, value);
        }

        public MapLocationControlVM(UndoRedoManager undoRedoManager, AppSettingsVM appSettings,
            Game game, MainWindowVM mainWindow, MapLocation mapLocation)
        {
            _undoRedoManager = undoRedoManager;
            _appSettings = appSettings;
            _game = game;
            _mainWindow = mainWindow;
            _mapLocation = mapLocation;

            appSettings.PropertyChanged += OnAppSettingsChanged;
            game.Mode.PropertyChanged += OnModeChanged;
            mapLocation.Location.PropertyChanged += OnLocationChanged;

            foreach (ISection section in mapLocation.Location.Sections)
            {
                if (section is EntranceSection entranceSection)
                {
                    entranceSection.PropertyChanging += OnSectionChanging;
                    section.PropertyChanged += OnSectionChanged;
                }
            }

            UpdateSizeAndPosition();
            UpdateColor();
            UpdateImage();
            UpdateVisibility();
        }

        private void OnAppSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AppSettingsVM.DisplayAllLocations))
                UpdateVisibility();
            else if (e.PropertyName != nameof(AppSettingsVM.EmphasisFontColor))
                UpdateColor();
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.EntranceShuffle))
                UpdateSizeAndPosition();

            UpdateVisibility();
        }

        private void OnLocationChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Location.Accessibility))
            {
                UpdateColor();
                UpdateVisibility();
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
            if (_game.Mode.EntranceShuffle.HasValue &&
                _game.Mode.EntranceShuffle.Value)
            {
                if (_mapLocation.Location.Sections[0] is EntranceSection &&
                    _mapLocation.Location.Sections[0].Marking != null)
                    Size = 55.0;
                else
                    Size = 40.0;

                BorderSize = new Thickness(5);
            }
            else
            {
                Size = 70.0;
                BorderSize = new Thickness(9);
            }

            CanvasX = _mapLocation.X - (Size / 2);
            CanvasY = _mapLocation.Y - (Size / 2);
        }

        private void UpdateColor()
        {
            Color = _appSettings.AccessibilityColors[_mapLocation.Location.Accessibility];
        }

        private void UpdateImage()
        {
            if (_mapLocation.Location.Sections[0] is EntranceSection entranceSection)
            {
                if (entranceSection.Marking == null)
                    ImageSource = "avares://OpenTracker/Assets/Images/Items/visible-empty.png";
                else
                {
                    int itemNumber = 0;
                    switch (entranceSection.Marking)
                    {
                        case MarkingType.Bow:
                        case MarkingType.SilverArrows:
                        case MarkingType.Boomerang:
                        case MarkingType.RedBoomerang:
                        case MarkingType.SmallKey:
                        case MarkingType.BigKey:
                            ImageSource = "avares://OpenTracker/Assets/Images/Items/visible-" +
                                entranceSection.Marking.ToString().ToLower() + ".png";
                            break;
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
                            ImageSource = "avares://OpenTracker/Assets/Images/Items/" +
                                entranceSection.Marking.ToString().ToLower() + "1.png";
                            break;
                        case MarkingType.Bottle:
                        case MarkingType.Gloves:
                        case MarkingType.Shield:
                        case MarkingType.Mail:
                            Item item = _game.Items[Enum.Parse<ItemType>(entranceSection.Marking.ToString())];
                            itemNumber = Math.Min(item.Current + 1, item.Maximum);
                            ImageSource = "avares://OpenTracker/Assets/Images/Items/" +
                                entranceSection.Marking.ToString().ToLower() + itemNumber.ToString() + ".png";
                            break;
                        case MarkingType.Sword:

                            Item sword = _game.Items[ItemType.Sword];

                            if (sword.Current == 0)
                                itemNumber = 0;
                            else
                                itemNumber = Math.Min(sword.Current + 1, sword.Maximum);

                            ImageSource = "avares://OpenTracker/Assets/Images/Items/" +
                                entranceSection.Marking.ToString().ToLower() + itemNumber.ToString() + ".png";

                            break;
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
                            ImageSource = "avares://OpenTracker/Assets/Images/" +
                                entranceSection.Marking.ToString().ToLower() + ".png";
                            break;
                        case MarkingType.ToH:
                            ImageSource = "avares://OpenTracker/Assets/Images/th.png";
                            break;
                        case MarkingType.PoD:
                            ImageSource = "avares://OpenTracker/Assets/Images/pd.png";
                            break;
                        case MarkingType.Ganon:
                            ImageSource = "avares://OpenTracker/Assets/Images/ganon.png";
                            break;
                    }
                }
            }
        }

        private void UpdateVisibility()
        {
            if (_game.Mode.Validate(_mapLocation.VisibilityMode))
            {
                if (_mapLocation.Location.Sections[0] is EntranceSection entranceSection &&
                    entranceSection.Marking != null)
                    ImageVisible = true;
                else
                    ImageVisible = false;

                BorderVisible = !ImageVisible && (_appSettings.DisplayAllLocations ||
                    (_mapLocation.Location.Accessibility != AccessibilityLevel.Cleared &&
                    _mapLocation.Location.Accessibility != AccessibilityLevel.None));
            }
            else
            {
                ImageVisible = false;
                BorderVisible = false;
            }

            Visible = ImageVisible || BorderVisible;
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
    }
}
