using OpenTracker.Interfaces;
using OpenTracker.Models.Items;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Globalization;

namespace OpenTracker.ViewModels.Items.Large
{
    /// <summary>
    /// This is the ViewModel for the large Items panel control representing small keys.
    /// </summary>
    public class SmallKeyLargeItemVM : ViewModelBase, ILargeItemVMBase, IClickHandler
    {
        private readonly IUndoRedoManager _undoRedoManager;

        private readonly IItem _item;
        private readonly IRequirement _requirement;
        private readonly string _imageSourceBase;

        public bool Visible =>
            _requirement.Met;
        public string ImageSource =>
            $"{_imageSourceBase}{(_item.Current > 0 ? "1" : "0")}.png";
        public string ImageCount =>
            _item.Current.ToString(CultureInfo.InvariantCulture);

        public delegate SmallKeyLargeItemVM Factory(IItem item, string imageSourceBase);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="imageSourceBase">
        /// A string representing the image source base.
        /// </param>
        /// <param name="item">
        /// An item that is to be represented by this control.
        /// </param>
        public SmallKeyLargeItemVM(
            IRequirementDictionary requirements, IUndoRedoManager undoRedoManager, IItem item,
            string imageSourceBase)
        {
            _undoRedoManager = undoRedoManager;

            _item = item;
            _requirement = requirements[RequirementType.GenericKeys];
            _imageSourceBase = imageSourceBase;

            _item.PropertyChanged += OnItemChanged;
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
                this.RaisePropertyChanged(nameof(ImageSource));
                this.RaisePropertyChanged(nameof(ImageCount));
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
        /// Handles left clicks and adds an item.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force)
        {
            _undoRedoManager.Execute(new AddItem(_item));
        }

        /// <summary>
        /// Handles right clicks and removes an item.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force)
        {
            _undoRedoManager.Execute(new RemoveItem(_item));
        }
    }
}
