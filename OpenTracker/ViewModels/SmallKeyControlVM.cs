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
using System.Text;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This is the view-model of the small key controls in the Items panel.
    /// </summary>
    public class SmallKeyControlVM : SmallItemControlVMBase, IClickHandler
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly AppSettings _appSettings;
        private readonly IRequirement _requirement;
        private readonly IItem _item;

        public bool Visible =>
            _requirement.Met;

        public bool TextVisible =>
            _item.Current > 0;

        public string ItemNumber
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(_item.Current.ToString(CultureInfo.InvariantCulture));
                sb.Append(_item.Current == _item.Maximum ? "*" : "");

                return sb.ToString();
            }
        }

        public string TextColor
        {
            get
            {
                if (_item == null)
                {
                    return "#ffffff";
                }

                if (_item.Current == _item.Maximum)
                {
                    return _appSettings.EmphasisFontColor;
                }

                return "#ffffff";
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
        public SmallKeyControlVM(
            UndoRedoManager undoRedoManager, AppSettings appSettings, IItem item)
        {
            _undoRedoManager = undoRedoManager ?? throw new ArgumentNullException(nameof(undoRedoManager));
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            _item = item ?? throw new ArgumentNullException(nameof(item));
            _requirement = RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOn];

            _appSettings.PropertyChanged += OnAppSettingsChanged;
            _item.PropertyChanged += OnItemChanged;
            _requirement.PropertyChanged += OnRequirementChanged;
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
            UpdateText();
            UpdateTextColor();
        }

        /// <summary>
        /// Raises the PropertyChanged event for the TextVisible and ItemNumber properties.
        /// </summary>
        private void UpdateText()
        {
            this.RaisePropertyChanged(nameof(TextVisible));
            this.RaisePropertyChanged(nameof(ItemNumber));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the TextColor properties.
        /// </summary>
        private void UpdateTextColor()
        {
            this.RaisePropertyChanged(nameof(TextColor));
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
