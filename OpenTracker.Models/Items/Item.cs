using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.SaveLoad;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This class contains item data.
    /// </summary>
    public class Item : IItem
    {
        private readonly int _starting;
        private readonly IAutoTrackValue? _autoTrackValue;

        public event PropertyChangedEventHandler? PropertyChanged;

        private int _current;
        public int Current
        {
            get => _current;
            set
            {
                if (_current != value)
                {
                    _current = value;
                    OnPropertyChanged(nameof(Current));
                }
            }
        }

        public delegate Item Factory(ItemType type, int starting);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="autoTrackValueFactory">
        /// The item autotrack value factory.
        /// </param>
        /// <param name="type">
        /// The item type.
        /// </param>
        /// <param name="starting">
        /// A 32-bit signed integer representing the starting value of the item.
        /// </param>
        public Item(IItemAutoTrackValueFactory autoTrackValueFactory, ItemType type, int starting)
        {
            _starting = starting;

            Current = _starting;

            _autoTrackValue = autoTrackValueFactory.GetAutoTrackValue(type);

            if (_autoTrackValue != null)
            {
                _autoTrackValue.PropertyChanged += OnAutoTrackChanged;
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
        /// Update the Current property to match the autotracking value.
        /// </summary>
        private void AutoTrackUpdate()
        {
            if (_autoTrackValue!.CurrentValue.HasValue)
            {
                if (Current != _autoTrackValue.CurrentValue.Value)
                {
                    Current = _autoTrackValue.CurrentValue.Value;
                    //SaveLoadManager.Instance.Unsaved = true;
                }
            }
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
