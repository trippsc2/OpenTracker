using System;
using System.ComponentModel;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.SaveLoad;
using ReactiveUI;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This class contains item data.
    /// </summary>
    public class Item : ReactiveObject, IItem
    {
        private readonly ISaveLoadManager _saveLoadManager;
        
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
        /// <param name="starting">
        /// A 32-bit signed integer representing the starting value of the item.
        /// </param>
        /// <param name="autoTrackValue">
        /// The auto-track value.
        /// </param>
        public Item(ISaveLoadManager saveLoadManager, int starting, IAutoTrackValue? autoTrackValue)
        {
            _saveLoadManager = saveLoadManager;
            
            _starting = starting;
            _autoTrackValue = autoTrackValue;

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
        /// Returns whether an item can be removed.
        /// </summary>
        /// <returns>
        /// A boolean representing whether an item can be removed.
        /// </returns>
        public bool CanRemove()
        {
            return Current > 0;
        }

        /// <summary>
        /// Removes an item.
        /// </summary>
        public virtual void Remove()
        {
            if (Current == 0)
            {
                throw new Exception("Cannot be remove from item.");
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
        public ItemSaveData Save()
        {
            return new ItemSaveData()
            {
                Current = Current
            };
        }

        /// <summary>
        /// Loads item save data.
        /// </summary>
        public void Load(ItemSaveData saveData)
        {
            if (saveData == null)
            {
                throw new ArgumentNullException(nameof(saveData));
            }

            Current = saveData.Current;
        }
    }
}
