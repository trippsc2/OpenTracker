using OpenTracker.Models.Items;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking.Values
{
    /// <summary>
    /// This is the class for representing the autotracking result value of a hidden item count.
    /// </summary>
    public class AutoTrackItemValue : IAutoTrackValue
    {
        private readonly IItem _item;

        public int? CurrentValue => 
            _item.Current;

        public event PropertyChangedEventHandler? PropertyChanged;

        public delegate AutoTrackItemValue Factory(IItem item);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">
        /// The item count to present.
        /// </param>
        public AutoTrackItemValue(IItem item)
        {
            _item = item ?? throw new ArgumentNullException(nameof(item));

            _item.PropertyChanged += OnItemChanged;
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
        private void OnItemChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IItem.Current))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentValue)));
            }
        }
    }
}
