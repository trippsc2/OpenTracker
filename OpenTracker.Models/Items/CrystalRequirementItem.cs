using System.ComponentModel;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Items;
using ReactiveUI;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This class contains crystal requirement data.
    /// </summary>
    public class CrystalRequirementItem : CappedItem, ICrystalRequirementItem
    {
        private bool _known;
        public bool Known
        {
            get => _known;
            set => this.RaiseAndSetIfChanged(ref _known, value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="saveLoadManager">
        /// The save/load manager.
        /// </param>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="addItemFactory">
        /// An Autofac factory for creating undoable actions to add items.
        /// </param>
        /// <param name="removeItemFactory">
        /// An Autofac factory for creating undoable actions to remove items.
        /// </param>
        public CrystalRequirementItem(
            ISaveLoadManager saveLoadManager, IUndoRedoManager undoRedoManager, IAddItem.Factory addItemFactory,
            IRemoveItem.Factory removeItemFactory)
            : base(saveLoadManager, undoRedoManager, addItemFactory, removeItemFactory, 0, 7,
                null)
        {
            PropertyChanged += OnPropertyChanged;
        }
        
        /// <summary>
        /// Subscribes to the PropertyChanged event on this object.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Known))
            {
                this.RaisePropertyChanged(nameof(Current));
            }
        }

        public override bool CanAdd()
        {
            return Known && base.CanAdd();
        }

        public override void Add()
        {
            if (!Known)
            {
                Known = true;
                return;
            }

            if (Current < Maximum)
            {
                Current++;
                return;
            }

            Current = 0;
            Known = false;
        }
        
        public override bool CanRemove()
        {
            return Known || Current > 0;
        }

        public override void Remove()
        {
            if (Current > 0)
            {
                Current--;
                return;
            }
            
            if (Known)
            {
                Known = false;
                return;
            }

            Known = true;
            Current = Maximum;

            Current--;
        }

        /// <summary>
        /// Resets the item to its starting value.
        /// </summary>
        public override void Reset()
        {
            Known = false;
            base.Reset();
        }
    }
}
