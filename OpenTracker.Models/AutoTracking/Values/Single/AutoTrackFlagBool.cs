using System.ComponentModel;
using OpenTracker.Models.AutoTracking.Memory;

namespace OpenTracker.Models.AutoTracking.Values.Single
{
    /// <summary>
    ///     This class contains the auto-tracking result value of a memory flag.
    /// </summary>
    public class AutoTrackFlagBool : AutoTrackValueBase, IAutoTrackFlagBool
    {
        private readonly IMemoryFlag _flag;
        private readonly int _trueValue;
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="flag">
        ///     The memory flag to be checked.
        /// </param>
        /// <param name="trueValue">
        ///     A 32-bit signed integer representing the resultant value, if the flag is set.
        /// </param>
        public AutoTrackFlagBool(IMemoryFlag flag, int trueValue)
        {
            _flag = flag;
            _trueValue = trueValue;

            UpdateValue();

            _flag.PropertyChanged += OnMemoryChanged;
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IMemoryFlag interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnMemoryChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMemoryFlag.Status))
            {
                UpdateValue();
            }
        }

        /// <summary>
        ///     Updates the current value of this value.
        /// </summary>
        protected override int? GetNewValue()
        {
            if (_flag.Status is null)
            {
                return null;
            }
            
            return _flag.Status.Value ? _trueValue : 0;
        }
    }
}
