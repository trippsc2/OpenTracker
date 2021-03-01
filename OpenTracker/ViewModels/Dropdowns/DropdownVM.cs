using System;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System.ComponentModel;
using System.Reactive;
using Avalonia.Input;

namespace OpenTracker.ViewModels.Dropdowns
{
    /// <summary>
    /// This is the ViewModel class for a dropdown icon.
    /// </summary>
    public class DropdownVM : ViewModelBase, IDropdownVM
    {
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;

        private readonly IDropdown _dropdown;
        private readonly string _imageSourceBase;

        public bool Visible =>
            _dropdown.RequirementMet;
        public string ImageSource =>
            _imageSourceBase + (_dropdown.Checked ? "1" : "0") + ".png";
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

        public delegate IDropdownVM Factory(IDropdown dropdown, string imageSourceBase);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="undoableFactory">
        /// The factory for creating undoable actions.
        /// </param>
        /// <param name="imageSourceBase">
        /// A string representing the base image source.
        /// </param>
        /// <param name="dropdown">
        /// An item that is to be represented by this control.
        /// </param>
        public DropdownVM(
            IUndoRedoManager undoRedoManager, IUndoableFactory undoableFactory, IDropdown dropdown,
            string imageSourceBase)
        {
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            _dropdown = dropdown;
            _imageSourceBase = imageSourceBase;

            HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);

            _dropdown.PropertyChanged += OnDropdownChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IDropdown interface.
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
        /// Creates a new undoable action to check the dropdown to the undo/redo manager.
        /// </summary>
        private void CheckDropdown()
        {
            _undoRedoManager.Execute(_undoableFactory.GetCheckDropdown(_dropdown));
        }

        /// <summary>
        /// Creates a new undoable action to uncheck the dropdown to the undo/redo manager.
        /// </summary>
        private void UncheckDropdown()
        {
            _undoRedoManager.Execute(_undoableFactory.GetUncheckDropdown(_dropdown));
        }

        /// <summary>
        /// Handles the dropdown being clicked.
        /// </summary>
        /// <param name="e">
        /// The pointer released event args.
        /// </param>
        private void HandleClick(PointerReleasedEventArgs e)
        {
            switch (e.InitialPressMouseButton)
            {
                case MouseButton.Left:
                {
                    CheckDropdown();
                }
                    break;
                case MouseButton.Right:
                {
                    UncheckDropdown();
                }
                    break;
            }
        }
    }
}
