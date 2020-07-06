using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Items;
using OpenTracker.Models.Undoables;
using OpenTracker.ViewModels.Bases;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Globalization;

namespace OpenTracker.ViewModels
{
    public class CrystalRequirementControlVM : LargeItemControlVMBase, IClickHandler
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly AppSettings _appSettings;
        private readonly IItem _item;

        public string ImageSource { get; }

        public string ImageCount
        {
            get
            {
                if (_item == null)
                {
                    return null;
                }
                
                return (7 - _item.Current).ToString(CultureInfo.InvariantCulture);
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

                if (_item.Current == 0)
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
        /// <param name="item">
        /// An item that is to be represented by this control.
        /// </param>
        public CrystalRequirementControlVM(
            UndoRedoManager undoRedoManager, AppSettings appSettings, IItem item)
        {
            _undoRedoManager = undoRedoManager ?? throw new ArgumentNullException(nameof(undoRedoManager));
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            _item = item ?? throw new ArgumentNullException(nameof(item));
            ImageSource = "avares://OpenTracker/Assets/Images/Items/" +
                $"{_item.Type.ToString().ToLowerInvariant()}.png";

            _item.PropertyChanged += OnItemChanged;
            _appSettings.PropertyChanged += OnAppSettingsChanged;
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
            this.RaisePropertyChanged(nameof(ImageCount));
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
