using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Actions;
using OpenTracker.Models.Enums;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Globalization;

namespace OpenTracker.ViewModels
{
    public class ItemControlVM : ViewModelBase, IClickHandler
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly AppSettings _appSettings;
        private readonly string _imageSourceBase;
        private readonly Game _game;
        private readonly Item[] _items;

        public string ImageSource
        {
            get
            {
                if (_items == null || _items[0] == null)
                    return null;

                if (_items[0].Type == ItemType.TowerCrystals || _items[0].Type == ItemType.GanonCrystals)
                    return _imageSourceBase + ".png";
                else if (_items[0].Type == ItemType.SmallKey)
                {
                    if (_game.Mode.WorldState == WorldState.Retro)
                        return _imageSourceBase + ".png";
                    else
                        return null;
                }
                else
                {
                    int imageNumber = _items[0].Current;

                    if (_items.Length == 2)
                        imageNumber += _items[1].Current * (_items[0].Maximum + 1);

                    return _imageSourceBase + imageNumber.ToString(CultureInfo.InvariantCulture) + ".png";
                }
            }
        }

        public string ImageNumber
        {
            get
            {
                if (_items == null || _items[0] == null)
                    return null;

                if (_items[0].Type == ItemType.TowerCrystals || _items[0].Type == ItemType.GanonCrystals)
                    return (7 - _items[0].Current).ToString(CultureInfo.InvariantCulture);

                if (_items[0].Type == ItemType.SmallKey && _game.Mode.WorldState == WorldState.Retro)
                    return _items[0].Current.ToString(CultureInfo.InvariantCulture);

                return null;
            }
        }
        
        public string TextColor
        {
            get
            {
                if (_items == null || _items[0] == null)
                    return "#ffffffff";

                if ((_items[0].Type == ItemType.TowerCrystals || _items[0].Type == ItemType.GanonCrystals) &&
                    _items[0].Current == 0)
                    return _appSettings.EmphasisFontColor;
                else if (_items[0].Type == ItemType.SmallKey && _game.Mode.WorldState == WorldState.Retro &&
                    _items[0].Current == _items[0].Maximum)
                    return _appSettings.EmphasisFontColor;
                else
                    return "#ffffffff";
            }
        }

        public ItemControlVM(UndoRedoManager undoRedoManager, AppSettings appSettings,
            Game game, Item[] items)
        {
            _undoRedoManager = undoRedoManager;
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _items = items;

            _appSettings.PropertyChanged += OnAppSettingsChanged;

            if (_items != null)
            {
                _imageSourceBase = "avares://OpenTracker/Assets/Images/Items/" + _items[0].Type.ToString().ToLowerInvariant();

                foreach (Item item in _items)
                    item.PropertyChanged += OnItemChanged;

                if (_items[0].Type == ItemType.SmallKey)
                {
                    _game.Mode.PropertyChanged += OnModeChanged;
                    _imageSourceBase = "avares://OpenTracker/Assets/Images/Items/visible-smallkey";
                }
            }
        }

        private void OnAppSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AppSettings.EmphasisFontColor))
                UpdateTextColor();
        }

        private void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateImageSource();

            if (_items[0].Type == ItemType.TowerCrystals || _items[0].Type == ItemType.GanonCrystals ||
                _items[0].Type == ItemType.SmallKey)
            {
                UpdateText();
                UpdateTextColor();
            }
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.WorldState))
            {
                UpdateImageSource();
                UpdateText();
                UpdateTextColor();
            }
        }

        private void UpdateImageSource()
        {
            this.RaisePropertyChanged(nameof(ImageSource));
        }

        private void UpdateText()
        {
            this.RaisePropertyChanged(nameof(ImageNumber));
        }

        private void UpdateTextColor()
        {
            this.RaisePropertyChanged(nameof(TextColor));
        }

        private void AddItem(int index)
        {
            _undoRedoManager.Execute(new AddItem(_items[index]));
        }

        private void RemoveItem(int index)
        {
            _undoRedoManager.Execute(new RemoveItem(_items[index]));
        }

        private void CycleItem(int index)
        {
            _undoRedoManager.Execute(new CycleItem(_items[index]));
        }

        public void OnLeftClick(bool force)
        {
            if (_items != null)
            {
                if (_items.Length > 1)
                    CycleItem(0);
                else if ((_items[0].Type != ItemType.SmallKey || _game.Mode.WorldState == WorldState.Retro) &&
                    _items[0].Current < _items[0].Maximum)
                    AddItem(0);
            }
        }

        public void OnRightClick(bool force)
        {
            if (_items != null)
            {
                if (_items.Length > 1)
                    CycleItem(1);
                else if ((_items[0].Type != ItemType.SmallKey || _game.Mode.WorldState == WorldState.Retro) && 
                    _items[0].Current > 0)
                    RemoveItem(0);
            }
        }
    }
}