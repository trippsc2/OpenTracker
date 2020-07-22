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
        /// Changes the current value by the specified delta and specifying whether the obey
        /// or ignore the maximum value.
        /// </summary>
        /// <param name="delta">
        /// A 32-bit integer representing the delta value of the change.
        /// </param>
        /// <param name="ignoreMaximum">
        /// A boolean representing whether the maximum is ignored.
        /// </param>
        public void Change(int delta, bool ignoreMaximum = false)
        {
            if (ignoreMaximum)
            {
                Current += delta;
            }
            else
            {
                Current = Math.Min(Maximum, Current + delta);
            }
        }

        /// <summary>
        /// Sets the current value of the item.
        /// </summary>
        /// <param name="current">
        /// A 32-bit integer representing the new current value.
        /// </param>
        public void SetCurrent(int current = 0)
        {
            Current = current;
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
