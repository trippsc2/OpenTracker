using OpenTracker.Interfaces;
using OpenTracker.Models.Items;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Globalization;

namespace OpenTracker.ViewModels.Items.Large
{
    /// <summary>
    /// This is the ViewModel for the large Items panel control representing a basic item.
    /// </summary>
    public class LargeItemVM : LargeItemVMBase, IClickHandler
    {
        private readonly IItem _item;
        private readonly string _imageSourceBase;

        public string ImageSource =>
            $"{_imageSourceBase}{_item.Current.ToString(CultureInfo.InvariantCulture)}.png";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="imageSourceBase">
        /// A string representing the base image source.
        /// </param>
        /// <param name="item">
        /// An item that is to be represented by this control.
        /// </param>
        public LargeItemVM(string imageSourceBase, IItem item)
        {
            _item = item ?? throw new ArgumentNullException(nameof(item));
            _imageSourceBase = imageSourceBase ??
                throw new ArgumentNullException(nameof(imageSourceBase));

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
            if (e.PropertyName == nameof(IItem.Current))
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
