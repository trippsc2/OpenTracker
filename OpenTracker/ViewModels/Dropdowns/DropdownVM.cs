using OpenTracker.Interfaces;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels.Dropdowns
{
    /// <summary>
    /// This is the ViewModel class for a dropdown icon.
    /// </summary>
    public class DropdownVM : ViewModelBase, IClickHandler
    {
        private readonly IDropdown _dropdown;
        private readonly string _imageSourceBase;

        public bool Visible =>
            _dropdown.RequirementMet;
        public string ImageSource =>
            _imageSourceBase + (_dropdown.Checked ? "1" : "0") + ".png";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="imageSourceBase">
        /// A string representing the base image source.
        /// </param>
        /// <param name="dropdown">
        /// An item that is to be represented by this control.
        /// </param>
        public DropdownVM(string imageSourceBase, IDropdown dropdown)
        {
            _dropdown = dropdown ?? throw new ArgumentNullException(nameof(dropdown));
            _imageSourceBase = imageSourceBase ??
                throw new ArgumentNullException(nameof(imageSourceBase));

            _dropdown.PropertyChanged += OnDropdownChanged;
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
        private void OnDropdownChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IDropdown.RequirementMet))
            {
                this.RaisePropertyChanged(nameof(Visible));
            }

            if (e.PropertyName == nameof(IDropdown.Checked))
            {
                this.RaisePropertyChanged(nameof(ImageSource));
            }
        }

        /// <summary>
        /// Handles left click and adds an item.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force)
        {
            UndoRedoManager.Instance.Execute(new CheckDropdown(_dropdown));
        }

        /// <summary>
        /// Handles right clicks and removes an item.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force)
        {
            UndoRedoManager.Instance.Execute(new UncheckDropdown(_dropdown));
        }
    }
}
