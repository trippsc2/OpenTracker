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

            mapLocation.Location.RequirementChanged += OnItemRequirementChanged;

            foreach (ISection section in mapLocation.Location.Sections)
                section.PropertyChanged += OnSectionChanged;

            SetSizeAndPosition();
            SetColor();
            SetVisibility();
            SetImageSource();
        }

        private void CheckLocationAvailability()
        {
            ObservableCollection<PinnedLocationControlVM> pinnedLocations = _mainWindow.PinnedLocations;
            PinnedLocationControlVM thisPinnedLocation = null;

            foreach (PinnedLocationControlVM pinnedLocation in pinnedLocations)
            {
                if (pinnedLocation.Location == _mapLocation.Location)
                    thisPinnedLocation = pinnedLocation;
            }

            if (thisPinnedLocation != null)
            {
                bool sectionAvailable = false;

                foreach (ISection section in _mapLocation.Location.Sections)
                {
                    if (section.IsAvailable())
                    {
                        sectionAvailable = true;
                        break;
                    }
                }

                if (!sectionAvailable)
                    thisPinnedLocation.Close();
            }
        }

        private void SetSizeAndPosition()
        {
            if (_game.Mode.EntranceShuffle.HasValue &&
                _game.Mode.EntranceShuffle.Value)
            {
                if (_mapLocation.Location.Sections[0] is EntranceSection entranceSection &&
                    entranceSection.Marking != null)
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

        private void SetColor()
        {
            Color = _appSettings.AccessibilityColors[_mapLocation.Location.GetAccessibility(_game.Mode, _game.Items)];
        }

        private void SetImageSource()
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
                        case MarkingType.Powder:
                        case MarkingType.FireRod:
                        case MarkingType.IceRod:
                        case MarkingType.Shovel:
                        case MarkingType.Lamp:
                        case MarkingType.Hammer:
                        case MarkingType.Flute:
                        case MarkingType.Net:
                        case MarkingType.Book:
                        case MarkingType.MoonPearl:
                        case MarkingType.Bottle:
                        case MarkingType.CaneOfSomaria:
                        case MarkingType.CaneOfByrna:
                        case MarkingType.Cape:
                        case MarkingType.Mirror:
                        case MarkingType.Gloves:
                        case MarkingType.Boots:
                        case MarkingType.Flippers:
                        case MarkingType.HalfMagic:
                        case MarkingType.Sword:
                        case MarkingType.Shield:
                        case MarkingType.Mail:
                        case MarkingType.Aga:
                            Item item = _game.Items[Enum.Parse<ItemType>(entranceSection.Marking.ToString())];
                            itemNumber = Math.Min(item.Current + 1, item.Maximum);
                            ImageSource = "avares://OpenTracker/Assets/Images/Items/" +
                                entranceSection.Marking.ToString().ToLower() + itemNumber.ToString() + ".png";
                            break;
                        case MarkingType.Mushroom:
                            ImageSource = "avares://OpenTracker/Assets/Images/Items/mushroom1.png";
                            break;
                        case MarkingType.Bombos:
                        case MarkingType.Ether:
                        case MarkingType.Quake:
                            Item medallionDungeons = _game.Items[Enum.Parse<ItemType>(entranceSection.Marking.ToString()) + 1];
                            itemNumber = 1 + (medallionDungeons.Current * 2);
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

        private void SetVisibility()
        {
            if (_game.Mode.Validate(_mapLocation.VisibilityMode))
            {
                if (_mapLocation.Location.Sections[0] is EntranceSection entranceSection &&
                    entranceSection.Marking != null)
                    ImageVisible = true;
                else
                    ImageVisible = false;

                BorderVisible = !ImageVisible && (_appSettings.DisplayAllLocations ||
                    (_mapLocation.Location.GetAccessibility(_game.Mode, _game.Items) != AccessibilityLevel.Cleared &&
                    _mapLocation.Location.GetAccessibility(_game.Mode, _game.Items) != AccessibilityLevel.None));
            }
            else
            {
                ImageVisible = false;
                BorderVisible = false;
            }

            Visible = ImageVisible || BorderVisible;
        }

        public void ClearAvailableSections()
        {
            bool canBeCleared = false;

            foreach (ISection section in _mapLocation.Location.Sections)
            {
                if (section.IsAvailable() &&
                    (section.Accessibility >= AccessibilityLevel.SequenceBreak ||
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
            ObservableCollection<PinnedLocationControlVM> pinnedLocations = _mainWindow.PinnedLocations;
            PinnedLocationControlVM existingPinnedLocation = null;

            foreach (PinnedLocationControlVM pinnedLocation in pinnedLocations)
            {
                if (pinnedLocation.Location == _mapLocation.Location)
                    existingPinnedLocation = pinnedLocation;
            }

            if (existingPinnedLocation == null)
            {
                _undoRedoManager.Execute(new PinLocation(pinnedLocations,
                    new PinnedLocationControlVM(_undoRedoManager, _appSettings, _game, _mainWindow,
                    _mapLocation.Location)));
            }
            else if (pinnedLocations[0] != existingPinnedLocation)
                _undoRedoManager.Execute(new PinLocation(pinnedLocations, existingPinnedLocation));
        }

        private void OnAppSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            SetSizeAndPosition();
            SetColor();
            SetVisibility();
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            SetSizeAndPosition();
            SetColor();
            SetVisibility();
        }

        private void OnItemRequirementChanged(object sender, EventArgs e)
        {
            SetColor();
            SetVisibility();
        }

        private void OnSectionChanged(object sender, PropertyChangedEventArgs e)
        {
            SetSizeAndPosition();
            CheckLocationAvailability();
            SetColor();
            SetVisibility();
            SetImageSource();
        }
    }
}
