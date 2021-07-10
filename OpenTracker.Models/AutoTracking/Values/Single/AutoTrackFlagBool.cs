using System.ComponentModel;
using OpenTracker.Models.AutoTracking.Memory;

namespace OpenTracker.Models.AutoTracking.Values.Single
{
    /// <summary>
    /// This class contains the auto-tracking result value of a memory flag.
    /// </summary>
    public class AutoTrackFlagBool : AutoTrackValueBase, IAutoTrackFlagBool
    {
        private readonly IMemoryFlag _flag;
        private readonly int _trueValue;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="flag">
        ///     The <see cref="IMemoryFlag"/> to be checked.
        /// </param>
        /// <param name="trueValue">
        ///     A <see cref="int"/> representing the resultant value, if the flag is set.
        /// </param>
        public AutoTrackFlagBool(IMemoryFlag flag, int trueValue)
        {
            _flag = flag;
            _trueValue = trueValue;

            UpdateValue();

            _flag.PropertyChanged += OnMemoryChanged;
        }

        /// <summary>
        /// Subscribes to the <see cref="IMemoryFlag.PropertyChanged"/> event.
        /// </summary>
        /// <param name="sender">
        ///     The <see cref="object"/> from which the event was sent.
        /// </param>
        /// <param name="e">
        ///     The <see cref="PropertyChangedEventArgs"/>.
        /// </param>
        private void OnMemoryChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMemoryFlag.Status))
            {
                UpdateValue();
            }
        }

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
