using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.Items
{
    public class AutoTrackedItem : IItem
    {
        private readonly IItem _item;

        private Func<int, int?> AutoTrackFunction { get; }

        public ItemType Type =>
            _item.Type;
        public int Maximum =>
            _item.Maximum;
        public int Current => 
            _item.Current;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">
        /// The base item to be wrapped in auto tracking.
        /// </param>
        /// <param name="autoTrackFunction">
        /// The action delegate to perform autotracking.
        /// </param>
        /// <param name="memoryAddresses">
        /// The list of memory addresses to which to subscribe to indicate a change in autotracking.
        /// </param>
        internal AutoTrackedItem(
            IItem item, Func<int, int?> autoTrackFunction,
            List<(MemorySegmentType, int)> memoryAddresses)
        {
            _item = item ?? throw new ArgumentNullException(nameof(item));
            AutoTrackFunction = autoTrackFunction ??
                throw new ArgumentNullException(nameof(autoTrackFunction));

            foreach ((MemorySegmentType, int) address in memoryAddresses)
            {
                SubscribeToMemoryAddress(address.Item1, address.Item2);
            }

            _item.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the MemoryAddress class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnMemoryChanged(object sender, PropertyChangedEventArgs e)
        {
            AutoTrack();
        }

        /// <summary>
        /// Autotrack the item.
        /// </summary>
        private void AutoTrack()
        {
            int? result = AutoTrackFunction(Current);

            if (result.HasValue)
            {
                SetCurrent(Math.Min(Maximum, result.Value));
            }
        }

        /// <summary>
        /// Creates subscription to the PropertyChanged event on the MemoryAddress class.
        /// </summary>
        /// <param name="segment">
        /// The memory segment to which to subscribe.
        /// </param>
        /// <param name="index">
        /// The index within the memory address list to which to subscribe.
        /// </param>
        internal void SubscribeToMemoryAddress(MemorySegmentType segment, int index)
        {
            List<MemoryAddress> memory = segment switch
            {
                MemorySegmentType.Room => AutoTracker.Instance.RoomMemory,
                MemorySegmentType.OverworldEvent => AutoTracker.Instance.OverworldEventMemory,
                MemorySegmentType.Item => AutoTracker.Instance.ItemMemory,
                MemorySegmentType.NPCItem => AutoTracker.Instance.NPCItemMemory,
                _ => throw new ArgumentOutOfRangeException(nameof(segment))
            };

            if (index >= memory.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            memory[index].PropertyChanged += OnMemoryChanged;
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
            _item.Change(delta, ignoreMaximum);
        }

        /// <summary>
        /// Sets the current value of the item.
        /// </summary>
        /// <param name="current">
        /// A 32-bit integer representing the new current value.
        /// </param>
        public void Reset()
        {
            _item.Reset();
        }

        /// <summary>
        /// Resets the item to its starting values.
        /// </summary>
        public void SetCurrent(int current = 0)
        {
            _item.SetCurrent(current);
        }
    }
}
