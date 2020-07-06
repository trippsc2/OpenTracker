using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using OpenTracker.Models.Items;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Undoables;
using OpenTracker.ViewModels.Bases;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Globalization;

namespace OpenTracker.ViewModels
{
    public class GenericSmallKeyControlVM : LargeItemControlVMBase, IClickHandler
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly AppSettings _appSettings;
        private readonly IItem _item;
        private readonly string _imageSourceBase;
        private readonly IRequirement _requirement;

        public bool Visible =>
            _requirement.Met;

        public string ImageSource
        {
            get
            {
                if (_item == null)
                {
                    return null;
                }

                return $"{_imageSourceBase}{(_item.Current > 0 ? "1" : "0")}.png";
            }
        }

        public string ImageCount
        {
            get
            {
                if (_item == null)
                {
                    return null;
                }

                return _item.Current.ToString(CultureInfo.InvariantCulture);
            }
        }

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
        /// An item that is to be represented by this control.
        /// </param>
        public GenericSmallKeyControlVM(
            UndoRedoManager undoRedoManager, AppSettings appSettings, IItem item)
        {
            _undoRedoManager = undoRedoManager ?? throw new ArgumentNullException(nameof(undoRedoManager));
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            _item = item ?? throw new ArgumentNullException(nameof(item));
            _imageSourceBase = "avares://OpenTracker/Assets/Images/Items/" +
                $"{_item.Type.ToString().ToLowerInvariant()}";
            _requirement = RequirementDictionary.Instance[RequirementType.WorldStateRetro];

            _item.PropertyChanged += OnItemChanged;
            _appSettings.PropertyChanged += OnAppSettingsChanged;
            Mode.Instance.PropertyChanged += OnModeChanged;
            _requirement.PropertyChanged += OnRequirementChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IItem interface.
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
            UpdateText();
            UpdateTextColor();
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
        /// Subscribes to the PropertyChanged event on the IRequirement interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(Visible));
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
            this.RaisePropertyChanged(nameof(ImageCount));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the TextColor property.
        /// </summary>
        private void UpdateTextColor()
        {
            this.RaisePropertyChanged(nameof(TextColor));
        }

        /// <summary>
        /// Click handler for left click.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force)
        {
            if (_item.Current < _item.Maximum)
            {
                _undoRedoManager.Execute(new AddItem(_item));
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
            if (_item.Current > 0)
            {
                _undoRedoManager.Execute(new RemoveItem(_item));
            }
        }
    }
}
