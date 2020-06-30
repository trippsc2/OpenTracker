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
    /// This is the view-model of the small or big key controls in the item panel.
    /// </summary>
    public class KeyControlVM : ViewModelBase, IClickHandler
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly AppSettings _appSettings;
        private readonly Game _game;
        private readonly string _imageSourceBase;
        private readonly IItem _item;
        private readonly bool _smallKey;

        public bool SmallKeyShuffle =>
            _game.Mode.SmallKeyShuffle;

        public bool BigKeyShuffle =>
            _game.Mode.BigKeyShuffle;

        public string ImageSource
        {
            get
            {
                if (_item == null)
                {
                    return null;
                }

                StringBuilder sb = new StringBuilder();
                sb.Append(_imageSourceBase);
                sb.Append(_item.Current > 0 ? "1" : "0");
                sb.Append(".png");

                return sb.ToString();
            }
        }

        public string ImageNumber
        {
            get
            {
                if (_smallKey)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(_item.Current.ToString(CultureInfo.InvariantCulture));
                    sb.Append(_item.Current == _item.Maximum ? "*" : "");

                    return sb.ToString();
                }

                return null;
            }
        }

        public bool TextVisible =>
            _smallKey && _item.Current > 0;

        public string TextColor
        {
            get
            {
                if (_item == null)
                {
                    return "#ffffffff";
                }

                if (_item.Current == _item.Maximum)
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
        /// <param name="item">
        /// The item of the key to be represented.
        /// </param>
        public KeyControlVM(UndoRedoManager undoRedoManager, AppSettings appSettings,
            Game game, IItem item)
        {
            _undoRedoManager = undoRedoManager;
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _item = item;

            _appSettings.PropertyChanged += OnAppSettingsChanged;
            _game.Mode.PropertyChanged += OnModeChanged;

            if (_item != null)
            {
                switch (_item.Type)
                {
                    case ItemType.HCSmallKey:
                    case ItemType.DPSmallKey:
                    case ItemType.ToHSmallKey:
                    case ItemType.ATSmallKey:
                    case ItemType.PoDSmallKey:
                    case ItemType.SPSmallKey:
                    case ItemType.SWSmallKey:
                    case ItemType.TTSmallKey:
                    case ItemType.IPSmallKey:
                    case ItemType.MMSmallKey:
                    case ItemType.TRSmallKey:
                    case ItemType.GTSmallKey:
                        {
                            _imageSourceBase = "avares://OpenTracker/Assets/Images/Items/smallkey";
                            _smallKey = true;
                        }
                        break;
                    case ItemType.EPBigKey:
                    case ItemType.DPBigKey:
                    case ItemType.ToHBigKey:
                    case ItemType.PoDBigKey:
                    case ItemType.SPBigKey:
                    case ItemType.SWBigKey:
                    case ItemType.TTBigKey:
                    case ItemType.IPBigKey:
                    case ItemType.MMBigKey:
                    case ItemType.TRBigKey:
                    case ItemType.GTBigKey:
                        {
                            _imageSourceBase = "avares://OpenTracker/Assets/Images/Items/bigkey";
                        }
                        break;
                }

                item.PropertyChanged += OnItemChanged;
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
                this.RaisePropertyChanged(nameof(TextColor));
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
                this.RaisePropertyChanged(nameof(SmallKeyShuffle));
            }

            if (e.PropertyName == nameof(Mode.DungeonItemShuffle))
            {
                this.RaisePropertyChanged(nameof(SmallKeyShuffle));
                this.RaisePropertyChanged(nameof(BigKeyShuffle));
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
            this.RaisePropertyChanged(nameof(ImageSource));
            
            if (_smallKey)
            {
                UpdateText();
                this.RaisePropertyChanged(nameof(TextColor));
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event for the TextVisible and ImageNumber properties.
        /// </summary>
        private void UpdateText()
        {
            this.RaisePropertyChanged(nameof(TextVisible));
            this.RaisePropertyChanged(nameof(ImageNumber));
        }

        /// <summary>
        /// Adds 1 to the represented item.
        /// </summary>
        private void AddItem()
        {
            _undoRedoManager.Execute(new AddItem(_item));
        }

        /// <summary>
        /// Removes 1 from the represented item.
        /// </summary>
        private void RemoveItem()
        {
            _undoRedoManager.Execute(new RemoveItem(_item));
        }

        /// <summary>
        /// Click handler for left click.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force = false)
        {
            if (_item != null && _item.Current < _item.Maximum)
            {
                AddItem();
            }
        }

        /// <summary>
        /// Click handler for right click.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force = false)
        {
            if (_item != null && _item.Current > 0)
            {
                RemoveItem();
            }
        }
    }
}
