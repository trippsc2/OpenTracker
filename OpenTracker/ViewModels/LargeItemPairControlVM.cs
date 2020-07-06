using OpenTracker.Interfaces;
using OpenTracker.Models.Items;
using OpenTracker.Models.Undoables;
using OpenTracker.ViewModels.Bases;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Globalization;

namespace OpenTracker.ViewModels
{
    public class LargeItemPairControlVM : LargeItemControlVMBase, IClickHandler
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly IItem[] _items;
        private readonly string _imageSourceBase;

        public string ImageSource
        {
            get
            {
                if (_items == null || _items[0] == null || _items[1] == null)
                {
                    return null;
                }

                return _imageSourceBase +
                    ((_items[1].Current * (_items[0].Maximum + 1)) +
                    _items[0].Current).ToString(CultureInfo.InvariantCulture) + ".png";
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="items">
        /// An array of items that are to be represented by this control.
        /// </param>
        public LargeItemPairControlVM(UndoRedoManager undoRedoManager, IItem[] items)
        {
            _undoRedoManager = undoRedoManager ?? throw new ArgumentNullException(nameof(undoRedoManager));
            _items = items ?? throw new ArgumentNullException(nameof(items));
            _imageSourceBase = "avares://OpenTracker/Assets/Images/Items/" +
                _items[0].Type.ToString().ToLowerInvariant();

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
        /// Click handler for left click.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force)
        {
            _undoRedoManager.Execute(new CycleItem(_items[0]));
        }

        /// <summary>
        /// Click handler for right click.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force)
        {
            _undoRedoManager.Execute(new CycleItem(_items[1]));
        }
    }
}
