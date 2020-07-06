using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using OpenTracker.Models.Items;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Undoables;
using OpenTracker.ViewModels;
using OpenTracker.ViewModels.Bases;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Text;

namespace OpenTracker.Views
{
    /// <summary>
    /// This is the view-model of the big key controls in the Items panel.
    /// </summary>
    public class BigKeyControlVM : SmallItemControlVMBase, IClickHandler
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly IRequirement _requirement;
        private readonly IItem _item;

        public bool Visible =>
            _requirement.Met;

        public string ImageSource
        {
            get
            {
                if (_item == null)
                {
                    return null;
                }

                StringBuilder sb = new StringBuilder();
                sb.Append("avares://OpenTracker/Assets/Images/Items/bigkey");
                sb.Append(_item.Current > 0 ? "1" : "0");
                sb.Append(".png");

                return sb.ToString();
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="item">
        /// The item of the key to be represented.
        /// </param>
        public BigKeyControlVM(UndoRedoManager undoRedoManager, IItem item)
        {
            _undoRedoManager = undoRedoManager ?? throw new ArgumentNullException(nameof(undoRedoManager));
            _item = item ?? throw new ArgumentNullException(nameof(item));
            _requirement = RequirementDictionary.Instance[RequirementType.DungeonItemShuffleKeysanity];
            
            _item.PropertyChanged += OnItemChanged;
            _requirement.PropertyChanged += OnRequirementChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IRequirement internal.
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
            this.RaisePropertyChanged(nameof(ImageSource));
        }

        /// <summary>
        /// Adds 1 to the represented item.
        /// </summary>
        private void AddItem()
        {
            _undoRedoManager.Execute(new AddItem(_item));
        }

        /// <summary>
        /// Removes 1 from the represented item.
        /// </summary>
        private void RemoveItem()
        {
            _undoRedoManager.Execute(new RemoveItem(_item));
        }

        /// <summary>
        /// Click handler for left click.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force = false)
        {
            if (_item != null && _item.Current < _item.Maximum)
            {
                AddItem();
            }
        }

        /// <summary>
        /// Click handler for right click.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force = false)
        {
            if (_item != null && _item.Current > 0)
            {
                RemoveItem();
            }
        }
    }
}
