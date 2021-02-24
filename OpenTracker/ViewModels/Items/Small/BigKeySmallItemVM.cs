using OpenTracker.Interfaces;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Items;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels.Items.Small
{
    public class BigKeySmallItemVM : ViewModelBase, ISmallItemVMBase, IClickHandler
    {
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;

        private readonly IRequirement _requirement;
        private readonly IRequirement _spacerRequirement;
        private readonly IItem _item;

        public bool SpacerVisible =>
            _spacerRequirement.Met;
        public bool Visible =>
            _requirement.Met;
        public string ImageSource =>
            "avares://OpenTracker/Assets/Images/Items/bigkey" +
            (_item.Current > 0 ? "1" : "0") + ".png";

        public delegate BigKeySmallItemVM Factory(
            IDungeon dungeon, IRequirement requirement, IRequirement spacerRequirement);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dungeon">
        /// The dungeon whose big keys are to be represented.
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
        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(SpacerVisible));
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
            this.RaisePropertyChanged(nameof(ImageSource));
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
