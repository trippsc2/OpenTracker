using Avalonia.Input;
using Avalonia.Threading;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Items;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Reactive;

namespace OpenTracker.ViewModels.Items.Small
{
    /// <summary>
    /// This class contains big key small item panel control ViewModel data.
    /// </summary>
    public class BigKeySmallItemVM : ViewModelBase, ISmallItemVMBase
    {
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;

        private readonly IRequirement _requirement;
        private readonly IRequirement _spacerRequirement;
        private readonly IItem _item;

        public bool SpacerVisible => _spacerRequirement.Met;
        public bool Visible => _requirement.Met;
        public string ImageSource =>
            "avares://OpenTracker/Assets/Images/Items/bigkey" + (_item.Current > 0 ? "1" : "0") + ".png";
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }

        public delegate BigKeySmallItemVM Factory(
            IDungeon dungeon, IRequirement requirement, IRequirement spacerRequirement);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="undoableFactory">
        /// A factory for creating undoable actions.
        /// </param>
        /// <param name="dungeon">
        /// The dungeon whose big keys are to be represented.
        /// </param>
        /// <param name="requirement">
        /// The requirement for the control to be visible.
        /// </param>
        /// <param name="spacerRequirement">
        /// The requirement for the control to take reserve space.
        /// </param>
        public BigKeySmallItemVM(
            IUndoRedoManager undoRedoManager, IUndoableFactory undoableFactory, IDungeon dungeon,
            IRequirement requirement, IRequirement spacerRequirement)
        {
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            _item = dungeon.BigKeyItem ??
                throw new ArgumentOutOfRangeException(nameof(dungeon));
            _requirement = requirement;
            _spacerRequirement = spacerRequirement;
            
            HandleClick = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClickImpl);

            _item.PropertyChanged += OnItemChanged;
            _requirement.PropertyChanged += OnRequirementChanged;
            _spacerRequirement.PropertyChanged += OnRequirementChanged;
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
        private async void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                this.RaisePropertyChanged(nameof(SpacerVisible));
                this.RaisePropertyChanged(nameof(Visible));
            });
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
        private async void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(ImageSource)));
        }

        /// <summary>
        /// Creates an undoable action to add an item and sends it to the undo/redo manager.
        /// </summary>
        private void AddItem()
        {
            _undoRedoManager.NewAction(_undoableFactory.GetAddItem(_item));
        }

        /// <summary>
        /// Creates an undoable action to remove an item and sends it to the undo/redo manager.
        /// </summary>
        private void RemoveItem()
        {
            _undoRedoManager.NewAction(_undoableFactory.GetRemoveItem(_item));
        }

        /// <summary>
        /// Handles clicking the control.
        /// </summary>
        /// <param name="e">
        /// The pointer released event args.
        /// </param>
        private void HandleClickImpl(PointerReleasedEventArgs e)
        {
            switch (e.InitialPressMouseButton)
            {
                case MouseButton.Left:
                    AddItem();
                    break;
                case MouseButton.Right:
                    RemoveItem();
                    break;
            }
        }
    }
}
