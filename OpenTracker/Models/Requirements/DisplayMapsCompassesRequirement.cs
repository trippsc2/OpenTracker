using OpenTracker.Models.Settings;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This class contains display maps/compasses setting requirement data.
    /// </summary>
    public class DisplayMapsCompassesRequirement : BooleanRequirement
    {
        private readonly ILayoutSettings _layoutSettings;
        private readonly bool _expectedValue;

        public delegate DisplayMapsCompassesRequirement Factory(bool expectedValue);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="layoutSettings">
        /// The layout settings.
        /// </param>
        /// <param name="expectedValue">
        /// A boolean representing the expected value.
        /// </param>
        public DisplayMapsCompassesRequirement(
            ILayoutSettings layoutSettings, bool expectedValue)
        {
            _layoutSettings = layoutSettings;
            _expectedValue = expectedValue;

            _layoutSettings.PropertyChanged += OnLayoutChanged;

            UpdateValue();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ILayoutSettings interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnLayoutChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ILayoutSettings.DisplayMapsCompasses))
            {
                UpdateValue();
            }
        }

        protected override bool ConditionMet()
        {
            return _layoutSettings.DisplayMapsCompasses == _expectedValue;
        }
    }
}
