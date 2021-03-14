using System.ComponentModel;
using Avalonia.Layout;
using OpenTracker.Models.Settings;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This class contains map orientation setting requirement data.
    /// </summary>
    public class MapOrientationRequirement : BooleanRequirement
    {
        private readonly ILayoutSettings _layoutSettings;
        private readonly Orientation? _expectedValue;

        public delegate MapOrientationRequirement Factory(Orientation? expectedValue);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="layoutSettings">
        /// The layout settings.
        /// </param>
        /// <param name="expectedValue">
        /// The expected orientation value.
        /// </param>
        public MapOrientationRequirement(ILayoutSettings layoutSettings, Orientation? expectedValue)
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
            if (e.PropertyName == nameof(ILayoutSettings.MapOrientation))
            {
                UpdateValue();
            }
        }

        protected override bool ConditionMet()
        {
            return _layoutSettings.MapOrientation == _expectedValue;
        }
    }
}