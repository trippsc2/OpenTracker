using OpenTracker.Interfaces;
using OpenTracker.Models.Items;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels.Items.Small
{
    /// <summary>
    /// This is the ViewModel of the small Items panel control representing big keys, compasses,
    /// and maps.
    /// </summary>
    public class SmallItemVM : ViewModelBase, ISmallItemVMBase, IClickHandler
    {
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;

        private readonly IItem _item;
        private readonly IRequirement _requirement;
        private readonly string _imageSourceBase;

        public bool Visible =>
            _requirement.Met;
        public string ImageSource =>
            _imageSourceBase + (_item.Current > 0 ? "1" : "0") + ".png";

        public delegate SmallItemVM Factory(
            IItem item, IRequirement requirement, string imageSourceBase);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="imageSourceBase">
        /// A string representing the base image source.
        /// </param>
        /// <param name="item">
        /// The item of the key to be represented.
        /// </param>
        /// <param name="requirement">
        /// The requirement for displaying the control.
        /// </param>
        public SmallItemVM(
            IUndoRedoManager undoRedoManager, IUndoableFactory undoableFactory,
            IItem item, IRequirement requirement, string imageSourceBase)
        {
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            _imageSourceBase = imageSourceBase;
            _item = item;
            _requirement = requirement;
            
            _item.PropertyChanged += OnItemChanged;
            _requirement.PropertyChanged += OnRequirementChanged;
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
            }
        }

        /// <summary>
        /// Handles left clicks and adds an item.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force = false)
        {
            _undoRedoManager.Execute(_undoableFactory.GetAddItem(_item));
        }

        /// <summary>
        /// Handles right clicks and removes an item.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force = false)
        {
            _undoRedoManager.Execute(_undoableFactory.GetRemoveItem(_item));
        }
    }
}
