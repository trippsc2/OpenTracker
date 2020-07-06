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
    /// <summary>
    /// This is the view-model for the large item controls in the items panel.
    /// </summary>
    public class LargeItemControlVM : LargeItemControlVMBase, IClickHandler
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly IItem _item;
        private readonly string _imageSourceBase;

        public string ImageSource
        {
            get
            {
                if (_item == null)
                {
                    return null;
                }

                return $"{_imageSourceBase}{_item.Current.ToString(CultureInfo.InvariantCulture)}.png";
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="item">
        /// An item that is to be represented by this control.
        /// </param>
        public LargeItemControlVM(UndoRedoManager undoRedoManager, IItem item)
        {
            _undoRedoManager = undoRedoManager ?? throw new ArgumentNullException(nameof(undoRedoManager));
            _item = item ?? throw new ArgumentNullException(nameof(item));
            _imageSourceBase = "avares://OpenTracker/Assets/Images/Items/" +
                _item.Type.ToString().ToLowerInvariant();

            _item.PropertyChanged += OnItemChanged;
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
            if (_item.Current < _item.Maximum)
            {
                _undoRedoManager.Execute(new AddItem(_item));
            }
        }

        /// <summary>
        /// Click handler for right click.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force)
        {
            if (_item.Current > 0)
            {
                _undoRedoManager.Execute(new RemoveItem(_item));
            }
        }
    }
}
