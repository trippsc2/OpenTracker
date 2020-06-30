using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Undoables;
using OpenTracker.Models.Enums;
using OpenTracker.Models.Items;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This is the view-model for the item controls in the items panel.
    /// </summary>
    public class ItemControlVM : ViewModelBase, IClickHandler
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly AppSettings _appSettings;
        private readonly string _imageSourceBase;
        private readonly Game _game;
        private readonly IItem[] _items;

        public string ImageSource
        {
            get
            {
                if (_items == null || _items[0] == null)
                {
                    return null;
                }

                StringBuilder sb = new StringBuilder();

                if (_items[0].Type == ItemType.TowerCrystals || _items[0].Type == ItemType.GanonCrystals)
                {
                    sb.Append(_imageSourceBase);
                }
                else if (_items[0].Type == ItemType.SmallKey)
                {
                    if (_game.Mode.WorldState != WorldState.Retro)
                    {
                        return null;
                    }
                    
                    sb.Append(_imageSourceBase);
                }
                else
                {
                    int imageNumber = _items[0].Current;

                    if (_items.Length == 2)
                    {
                        imageNumber += _items[1].Current * (_items[0].Maximum + 1);
                    }

                    sb.Append(_imageSourceBase);
                    sb.Append(imageNumber.ToString(CultureInfo.InvariantCulture));
                }

                sb.Append(".png");

                return sb.ToString();
            }
        }

        public string ImageNumber
        {
            get
            {
                if (_items == null || _items[0] == null)
                {
                    return null;
                }

                if (_items[0].Type == ItemType.TowerCrystals || _items[0].Type == ItemType.GanonCrystals)
                {
                    return (7 - _items[0].Current).ToString(CultureInfo.InvariantCulture);
                }

                if (_items[0].Type == ItemType.SmallKey && _game.Mode.WorldState == WorldState.Retro)
                {
                    return _items[0].Current.ToString(CultureInfo.InvariantCulture);
                }

                return null;
            }
        }
        
        public string TextColor
        {
            get
            {
                if (_items == null || _items[0] == null)
                {
                    return "#ffffffff";
                }

                if ((_items[0].Type == ItemType.TowerCrystals || _items[0].Type == ItemType.GanonCrystals) &&
                    _items[0].Current == 0)
                {
                    return _appSettings.EmphasisFontColor;
                }
                
                if (_items[0].Type == ItemType.SmallKey && _game.Mode.WorldState == WorldState.Retro &&
                    _items[0].Current == _items[0].Maximum)
                {
                    return _appSettings.EmphasisFontColor;
                }
                
                return "#ffffffff";
            }
        }

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
        /// <param name="items">
        /// An array of items that are to be represented by this control.
        /// </param>
        public ItemControlVM(UndoRedoManager undoRedoManager, AppSettings appSettings,
            Game game, IItem[] items)
        {
            _undoRedoManager = undoRedoManager;
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _items = items;

            _appSettings.PropertyChanged += OnAppSettingsChanged;

            if (_items != null)
            {
                if (_items[0].Type == ItemType.SmallKey)
                {
                    _game.Mode.PropertyChanged += OnModeChanged;
                    _imageSourceBase = "avares://OpenTracker/Assets/Images/Items/visible-smallkey";
                }
                else
                {
                    _imageSourceBase = $"avares://OpenTracker/Assets/Images/Items/" +
                        $"{_items[0].Type.ToString().ToLowerInvariant()}";
                }

                foreach (var item in _items)
                {
                    item.PropertyChanged += OnItemChanged;
                }
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
            if (e.PropertyName == nameof(AppSettings.EmphasisFontColor))
            {
                UpdateTextColor();
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
            UpdateImageSource();

            if (_items[0].Type == ItemType.TowerCrystals || _items[0].Type == ItemType.GanonCrystals ||
                _items[0].Type == ItemType.SmallKey)
            {
                UpdateText();
                UpdateTextColor();
            }
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
            if (e.PropertyName == nameof(Mode.WorldState))
            {
                UpdateImageSource();
                UpdateText();
                UpdateTextColor();
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event for the ImageSource property.
        /// </summary>
        private void UpdateImageSource()
        {
            this.RaisePropertyChanged(nameof(ImageSource));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the ImageNumber property.
        /// </summary>
        private void UpdateText()
        {
            this.RaisePropertyChanged(nameof(ImageNumber));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the TextColor property.
        /// </summary>
        private void UpdateTextColor()
        {
            this.RaisePropertyChanged(nameof(TextColor));
        }

        /// <summary>
        /// Adds 1 for the item on the specified index of the Items array.
        /// </summary>
        /// <param name="index">
        /// A 32-bit integer representing the index of the Items array to be manipulated.
        /// </param>
        private void AddItem(int index)
        {
            _undoRedoManager.Execute(new AddItem(_items[index]));
        }

        /// <summary>
        /// Removes 1 for the item on the specified index of the Items array.
        /// </summary>
        /// <param name="index">
        /// A 32-bit integer representing the index of the Items array to be manipulated.
        /// </param>
        private void RemoveItem(int index)
        {
            _undoRedoManager.Execute(new RemoveItem(_items[index]));
        }

        /// <summary>
        /// Adds 1 for the item on the specified index of the Items array, if it is not at maximum.
        /// If it is at maximum, sets the item to 0.
        /// </summary>
        /// <param name="index">
        /// A 32-bit integer representing the index of the Items array to be manipulated.
        /// </param>
        private void CycleItem(int index)
        {
            _undoRedoManager.Execute(new CycleItem(_items[index]));
        }

        /// <summary>
        /// Click handler for left click.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force)
        {
            if (_items != null)
            {
                if (_items.Length > 1)
                {
                    CycleItem(0);
                }
                else if ((_items[0].Type != ItemType.SmallKey ||
                    _game.Mode.WorldState == WorldState.Retro) &&
                    _items[0].Current < _items[0].Maximum)
                {
                    AddItem(0);
                }
            }
        }

        /// <summary>
        /// Click handler for right click.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force)
        {
            if (_items != null)
            {
                if (_items.Length > 1)
                {
                    CycleItem(1);
                }
                else if ((_items[0].Type != ItemType.SmallKey ||
                    _game.Mode.WorldState == WorldState.Retro) &&
                    _items[0].Current > 0)
                {
                    RemoveItem(0);
                }
            }
        }
    }
}