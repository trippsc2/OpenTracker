using OpenTracker.Models.SaveLoad;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This is the item data class.
    /// </summary>
    public class Item : IItem
    {
        private readonly int _starting;

        public ItemType Type { get; }
        public int Maximum { get; }

        public event PropertyChangedEventHandler PropertyChanged;

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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="itemType">
        /// The item type.
        /// </param>
        /// <param name="starting">
        /// A 32-bit signed integer representing the starting value of the item.
        /// </param>
        /// <param name="maximum">
        /// A 32-bit signed integer representing the maximum value of the item.
        /// </param>
        public Item(ItemType itemType, int starting = 0, int maximum = 1)
        {
            if (starting > maximum)
            {
                throw new ArgumentOutOfRangeException(nameof(starting));
            }

            Type = itemType;
            _starting = starting;
            Current = _starting;
            Maximum = maximum;
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Returns whether an item can be added.
        /// </summary>
        /// <returns>
        /// A boolean representing whether an item can be added.
        /// </returns>
        public bool CanAdd()
        {
            return Current < Maximum;
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
        /// Resets the item to its starting values.
        /// </summary>
        public void Reset()
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
