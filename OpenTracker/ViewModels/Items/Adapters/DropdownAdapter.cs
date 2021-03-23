using System.ComponentModel;
using System.Reactive;
using Avalonia.Input;
using Avalonia.Threading;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.ViewModels.BossSelect;
using ReactiveUI;

namespace OpenTracker.ViewModels.Items.Adapters
{
    public class DropdownAdapter : ViewModelBase, IItemAdapter
    {
        private readonly IUndoRedoManager _undoRedoManager;

        private readonly IDropdown _dropdown;
        private readonly string _imageSourceBase;

        public bool Visible => _dropdown.RequirementMet;
        public string ImageSource => _imageSourceBase + (_dropdown.Checked ? "1" : "0") + ".png";
        public string? Label { get; } = null;
        public string LabelColor { get; } = "#ffffffff";
        public IBossSelectPopupVM? BossSelect { get; } = null;

        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }

        public delegate DropdownAdapter Factory(IDropdown dropdown, string imageSourceBase);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="imageSourceBase">
        /// A string representing the base image source.
        /// </param>
        /// <param name="dropdown">
        /// An item that is to be represented by this control.
        /// </param>
        public DropdownAdapter(IUndoRedoManager undoRedoManager, IDropdown dropdown, string imageSourceBase)
        {
            _undoRedoManager = undoRedoManager;

            _dropdown = dropdown;
            _imageSourceBase = imageSourceBase;

            HandleClick = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClickImpl);

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
        private async void OnDropdownChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(IDropdown.RequirementMet):
                    await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Visible)));
                    break;
                case nameof(IDropdown.Checked):
                    await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(ImageSource)));
                    break;
            }
        }

        /// <summary>
        /// Creates a new undoable action to check the dropdown to the undo/redo manager.
        /// </summary>
        private void CheckDropdown()
        {
            _undoRedoManager.NewAction(_dropdown.CreateCheckDropdownAction());
        }

        /// <summary>
        /// Creates a new undoable action to uncheck the dropdown to the undo/redo manager.
        /// </summary>
        private void UncheckDropdown()
        {
            _undoRedoManager.NewAction(_dropdown.CreateUncheckDropdownAction());
        }

        /// <summary>
        /// Handles the dropdown being clicked.
        /// </summary>
        /// <param name="e">
        /// The pointer released event args.
        /// </param>
        private void HandleClickImpl(PointerReleasedEventArgs e)
        {
            switch (e.InitialPressMouseButton)
            {
                case MouseButton.Left:
                    CheckDropdown();
                    break;
                case MouseButton.Right:
                    UncheckDropdown();
                    break;
            }
        }
    }
}