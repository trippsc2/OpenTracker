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

        public double CanvasX => _mapLocation.X - (Size / 2);
        public double CanvasY => _mapLocation.Y - (Size / 2);

        public double Size
        {
            get
            {
                if (_game.Mode.EntranceShuffle.HasValue &&
                    _game.Mode.EntranceShuffle.Value)
                {
                    if (_mapLocation.Location.Sections[0] is EntranceSection &&
                        _mapLocation.Location.Sections[0].Marking != null)
                        return 55.0;
                    else
                        return 40.0;
                }
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

        public Thickness BorderSize
        {
            get
            {
                if (_game.Mode.EntranceShuffle.HasValue &&
                    _game.Mode.EntranceShuffle.Value)
                    return new Thickness(5);
                else
                    return new Thickness(9);
            }
        }

        public IBrush Color => 
            _appSettings.AccessibilityColors[_mapLocation.Location.Accessibility];

        public bool ImageVisible
        {
            get
            {
                if (_game.Mode.Validate(_mapLocation.VisibilityMode))
                {
                    if (_mapLocation.Location.Sections[0] is EntranceSection entranceSection &&
                        entranceSection.Marking != null)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
        }

        public bool BorderVisible
        {
            get
            {
                if (_game.Mode.Validate(_mapLocation.VisibilityMode))
                    return !ImageVisible && (_appSettings.DisplayAllLocations ||
                        (_mapLocation.Location.Accessibility != AccessibilityLevel.Cleared &&
                        _mapLocation.Location.Accessibility != AccessibilityLevel.None));
                else
                    return false;
            }
        }

        public bool Visible => ImageVisible || BorderVisible;

        public string ImageSource
        {
            get
            {
                if (_mapLocation.Location.Sections[0] is EntranceSection entranceSection)
                {
                    if (entranceSection.Marking == null)
                        return "avares://OpenTracker/Assets/Images/Items/visible-empty.png";
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
                                return "avares://OpenTracker/Assets/Images/Items/visible-" +
                                    entranceSection.Marking.ToString().ToLower() + ".png";
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
                                    entranceSection.Marking.ToString().ToLower() + "1.png";
                            case MarkingType.Bottle:
                            case MarkingType.Gloves:
                            case MarkingType.Shield:
                            case MarkingType.Mail:
                                Item item = _game.Items[Enum.Parse<ItemType>(entranceSection.Marking.ToString())];
                                itemNumber = Math.Min(item.Current + 1, item.Maximum);
                                return "avares://OpenTracker/Assets/Images/Items/" +
                                    entranceSection.Marking.ToString().ToLower() + itemNumber.ToString() + ".png";
                            case MarkingType.Sword:

                                Item sword = _game.Items[ItemType.Sword];

                                if (sword.Current == 0)
                                    itemNumber = 0;
                                else
                                    itemNumber = Math.Min(sword.Current + 1, sword.Maximum);

                                return "avares://OpenTracker/Assets/Images/Items/" +
                                    entranceSection.Marking.ToString().ToLower() + itemNumber.ToString() + ".png";

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
                                    entranceSection.Marking.ToString().ToLower() + ".png";
                            case MarkingType.ToH:
                                return "avares://OpenTracker/Assets/Images/th.png";
                            case MarkingType.PoD:
                                return "avares://OpenTracker/Assets/Images/pd.png";
                            case MarkingType.Ganon:
                                return "avares://OpenTracker/Assets/Images/ganon.png";
                        }
                    }
                }

                return null;
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
                    return _mapLocation.Location.Available.ToString();
                else if (_mapLocation.Location.Accessible == 0)
                    return _mapLocation.Location.Available.ToString();
                else
                    return _mapLocation.Location.Accessible.ToString() + "/" +
                        _mapLocation.Location.Available.ToString();
            }
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
        }

        private void OnAppSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AppSettingsVM.DisplayAllLocations))
                UpdateVisibility();

            if (e.PropertyName == nameof(AppSettingsVM.ShowItemCountsOnMap))
                UpdateText();

            UpdateColor();
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateSizeAndPosition();
            UpdateVisibility();
            UpdateText();
        }

        private void OnLocationChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Location.Accessibility))
            {
                UpdateColor();
                UpdateVisibility();
            }

            if (e.PropertyName == nameof(Location.Accessible))
                UpdateText();

            if (e.PropertyName == nameof(Location.Available))
                UpdateText();

            if (e.PropertyName == nameof(Location.Total))
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
            this.RaisePropertyChanged(nameof(BorderVisible));
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
