using OpenTracker.Models.Items;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Reactive;
using Avalonia.Input;

namespace OpenTracker.ViewModels.Items.Large
{
    /// <summary>
    /// This class contains item pair large items panel control ViewModel data.
    /// </summary>
    public class PairLargeItemVM : ViewModelBase, ILargeItemVMBase
    {
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;

        private readonly IItem[] _items;
        private readonly string _imageSourceBase;

        public string ImageSource =>
            _imageSourceBase + _items[0].Current.ToString(CultureInfo.InvariantCulture) +
            $"{_items[1].Current.ToString(CultureInfo.InvariantCulture)}.png";
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

        public delegate PairLargeItemVM Factory(IItem[] items, string imageSourceBase);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="undoableFactory">
        /// A factory for creating undoable actions.
        /// </param>
        /// <param name="imageSourceBase">
        /// A string representing the image source base.
        /// </param>
        /// <param name="items">
        /// An array of items that are to be represented by this control.
        /// </param>
        public PairLargeItemVM(
            IUndoRedoManager undoRedoManager, IUndoableFactory undoableFactory, IItem[] items,
            string imageSourceBase)
        {
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            _items = items;
            _imageSourceBase = imageSourceBase;

            if (_items.Length != 2)
            {
                throw new ArgumentOutOfRangeException(nameof(items));
            }
            
            HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);

            foreach (var item in _items)
            {
                item.PropertyChanged += OnItemChanged;
            }
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
            this.RaisePropertyChanged(nameof(ImageSource));
        }

        /// <summary>
        /// Creates an undoable action to add an item to the first item in the pair and sends it to the undo/redo
        /// manager.
        /// </summary>
        private void AddFirstItem()
        {
            _undoRedoManager.NewAction(_undoableFactory.GetCycleItem(_items[0]));
        }

        /// <summary>
        /// Creates an undoable action to add an item to the second item in the pair and sends it to the undo/redo
        /// manager.
        /// </summary>
        private void AddSecondItem()
        {
            _undoRedoManager.NewAction(_undoableFactory.GetCycleItem(_items[1]));
        }

        /// <summary>
        /// Handles clicking the control.
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
                    AddFirstItem();
                }
                    break;
                case MouseButton.Right:
                {
                    AddSecondItem();
                }
                    break;
            }
        }
    }
}
