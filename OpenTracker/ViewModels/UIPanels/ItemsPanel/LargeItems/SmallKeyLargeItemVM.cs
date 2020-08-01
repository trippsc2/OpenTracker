using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Items;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Globalization;

namespace OpenTracker.ViewModels.UIPanels.ItemsPanel.LargeItems
{
    /// <summary>
    /// This is the ViewModel for the large Items panel control representing small keys.
    /// </summary>
    public class SmallKeyLargeItemVM : LargeItemVMBase, IClickHandler
    {
        private readonly IItem _item;
        private readonly string _imageSourceBase;
        private readonly IRequirement _requirement;

        public bool Visible =>
            _requirement.Met;
        public string ImageSource =>
            $"{_imageSourceBase}{(_item.Current > 0 ? "1" : "0")}.png";
        public string ImageCount =>
            _item.Current.ToString(CultureInfo.InvariantCulture);
        public string TextColor =>
            _item.Current == _item.Maximum ?
            AppSettings.Instance.Colors.EmphasisFontColor : "#ffffffff";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">
        /// An item that is to be represented by this control.
        /// </param>
        public SmallKeyLargeItemVM(IItem item)
        {
            _item = item ?? throw new ArgumentNullException(nameof(item));
            _imageSourceBase = "avares://OpenTracker/Assets/Images/Items/" +
                _item.Type.ToString().ToLowerInvariant();
            _requirement = RequirementDictionary.Instance[RequirementType.WorldStateRetro];

            _item.PropertyChanged += OnItemChanged;
            AppSettings.Instance.Colors.PropertyChanged += OnColorsChanged;
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
            if (e.PropertyName == nameof(IItem.Current))
            {
                UpdateTextColor();
                this.RaisePropertyChanged(nameof(ImageSource));
                this.RaisePropertyChanged(nameof(ImageCount));
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ColorSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnColorsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ColorSettings.EmphasisFontColor))
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
        /// Raises the PropertyChanged event for the TextColor property.
        /// </summary>
        private void UpdateTextColor()
        {
            this.RaisePropertyChanged(nameof(TextColor));
        }

        /// <summary>
        /// Handles left clicks and adds an item.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force)
        {
            UndoRedoManager.Instance.Execute(new AddItem(_item));
        }

        /// <summary>
        /// Handles right clicks and removes an item.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force)
        {
            UndoRedoManager.Instance.Execute(new RemoveItem(_item));
        }
    }
}
