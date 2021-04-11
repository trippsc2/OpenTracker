using System.ComponentModel;
using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.ShopShuffle
{
    /// <summary>
    ///     This class contains shop shuffle requirement data.
    /// </summary>
    public class ShopShuffleRequirement : BooleanRequirement, IShopShuffleRequirement
    {
        private readonly IMode _mode;
        private readonly bool _expectedValue;
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="mode">
        ///     The mode settings.
        /// </param>
        /// <param name="expectedValue">
        ///     A boolean expected shop shuffle shuffle value.
        /// </param>
        public ShopShuffleRequirement(IMode mode, bool expectedValue)
        {
            _mode = mode;
            _expectedValue = expectedValue;

            _mode.PropertyChanged += OnModeChanged;

            UpdateValue();
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IMode interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMode.ShopShuffle))
            {
                UpdateValue();
            }
        }

        protected override bool ConditionMet()
        {
            return _mode.ShopShuffle == _expectedValue;
        }
    }
}
