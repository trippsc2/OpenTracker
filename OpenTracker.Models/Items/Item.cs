using System;
using System.ComponentModel;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Items;
using ReactiveUI;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This class contains item data.
    /// </summary>
    public class Item : ReactiveObject, IItem
    {
        private readonly ISaveLoadManager _saveLoadManager;

        private readonly IAddItem.Factory _addItemFactory;
        private readonly IRemoveItem.Factory _removeItemFactory;
        
        private readonly int _starting;
        private readonly IAutoTrackValue? _autoTrackValue;

        private int _current;
        public int Current
        {
            get => _current;
            set => this.RaiseAndSetIfChanged(ref _current, value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="saveLoadManager">
        /// The save/load manager.
        /// </param>
        /// <param name="addItemFactory">
        /// An Autofac factory for creating undoable actions to add items.
        /// </param>
        /// <param name="removeItemFactory">
        /// An Autofac factory for creating undoable actions to remove items.
        /// </param>
        /// <param name="starting">
        /// A 32-bit signed integer representing the starting value of the item.
        /// </param>
        /// <param name="autoTrackValue">
        /// The auto-track value.
        /// </param>
        public Item(
            ISaveLoadManager saveLoadManager, IAddItem.Factory addItemFactory, IRemoveItem.Factory removeItemFactory,
            int starting, IAutoTrackValue? autoTrackValue)
        {
            _saveLoadManager = saveLoadManager;
            
            _starting = starting;
            _autoTrackValue = autoTrackValue;
            _addItemFactory = addItemFactory;
            _removeItemFactory = removeItemFactory;

            Current = _starting;

            if (!(_autoTrackValue is null))
            {
                _autoTrackValue.PropertyChanged += OnAutoTrackChanged;
            }
        }
        
        /// <summary>
        /// Subscribes to the PropertyChanged event on the IAutoTrackValue interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnAutoTrackChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IAutoTrackValue.CurrentValue))
            {
                AutoTrackUpdate();
            }
        }

        /// <summary>
        /// Update the Current property to match the auto-tracking value.
        /// </summary>
        private void AutoTrackUpdate()
        {
            if (!_autoTrackValue!.CurrentValue.HasValue)
            {
                return;
            }

            if (Current == _autoTrackValue.CurrentValue.Value)
            {
                return;
            }
            
            Current = _autoTrackValue.CurrentValue.Value;
            _saveLoadManager.Unsaved = true;
        }

        /// <summary>
        /// Creates a new undoable action to add an item and sends it to the undo/redo manager.
        /// </summary>
        public IUndoable CreateAddItemAction()
        {
            return _addItemFactory(this);
        }

        /// <summary>
        /// Returns whether an item can be added.
        /// </summary>
        /// <returns>
        /// A boolean representing whether an item can be added.
        /// </returns>
        public virtual bool CanAdd()
        {
            return true;
        }

        /// <summary>
        /// Adds an item.
        /// </summary>
        public virtual void Add()
        {
            Current++;
        }

        /// <summary>
        /// Creates a new undoable action to remove an item and sends it to the undo/redo manager.
        /// </summary>
        public IUndoable CreateRemoveItemAction()
        {
            return _removeItemFactory(this);
        }

        /// <summary>
        /// Returns whether an item can be removed.
        /// </summary>
        /// <returns>
        /// A boolean representing whether an item can be removed.
        /// </returns>
        public virtual bool CanRemove()
        {
            return Current > 0;
        }

        /// <summary>
        /// Removes an item.
        /// </summary>
        public virtual void Remove()
        {
            if (Current <= 0)
            {
                throw new Exception("Item cannot be removed, because it is already 0.");
            }

            Current--;
        }

        /// <summary>
        /// Resets the item to its starting values.
        /// </summary>
        public virtual void Reset()
        {
            Current = _starting;
        }

        /// <summary>
        /// Returns a new item save data instance for this item.
        /// </summary>
        /// <returns>
        /// A new item save data instance.
        /// </returns>
        public virtual ItemSaveData Save()
        {
            return new ItemSaveData()
            {
                Current = Current
            };
        }

        /// <summary>
        /// Loads item save data.
        /// </summary>
        public virtual void Load(ItemSaveData? saveData)
        {
            if (saveData is null)
            {
                return;
            }

            Current = saveData.Current;
        }
    }
}
