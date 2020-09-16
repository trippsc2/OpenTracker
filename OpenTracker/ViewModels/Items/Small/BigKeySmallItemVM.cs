using OpenTracker.Interfaces;
using OpenTracker.Models.Items;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels.Items.Small
{
    /// <summary>
    /// This is the ViewModel of the small Items panel control representing big keys.
    /// </summary>
    public class BigKeySmallItemVM : SmallItemVMBase, IClickHandler
    {
        private readonly IRequirement _requirement;
        private readonly IItem _item;

        public bool Visible =>
            _requirement.Met;
        public string ImageSource =>
            _item.Current > 0 ? "avares://OpenTracker/Assets/Images/Items/bigkey1.png" :
            "avares://OpenTracker/Assets/Images/Items/bigkey0.png";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">
        /// The item of the key to be represented.
        /// </param>
        public BigKeySmallItemVM(IItem item)
        {
            _item = item ?? throw new ArgumentNullException(nameof(item));
            _requirement = RequirementDictionary.Instance[RequirementType.BigKeyShuffleOn];
            
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
            UndoRedoManager.Instance.Execute(new AddItem(_item));
        }

        /// <summary>
        /// Handles right clicks and removes an item.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force = false)
        {
            UndoRedoManager.Instance.Execute(new RemoveItem(_item));
        }
    }
}
