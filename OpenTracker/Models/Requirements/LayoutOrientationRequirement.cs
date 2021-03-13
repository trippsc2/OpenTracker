using System.ComponentModel;
using Avalonia.Layout;
using OpenTracker.Models.Settings;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This class contains layout orientation setting requirement data.
    /// </summary>
    public class LayoutOrientationRequirement : BooleanRequirement
    {
        private readonly ILayoutSettings _layoutSettings;
        private readonly Orientation? _expectedValue;

        public delegate LayoutOrientationRequirement Factory(Orientation? expectedValue);
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="layoutSettings">
        /// The layout settings data.
        /// </param>
        /// <param name="expectedValue">
        /// The expected orientation value.
        /// </param>
        public LayoutOrientationRequirement(ILayoutSettings layoutSettings, Orientation? expectedValue)
        {
            _layoutSettings = layoutSettings;
            _expectedValue = expectedValue;

            _layoutSettings.PropertyChanged += OnLayoutSettingsChanged;
            
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
        private void OnLayoutSettingsChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ILayoutSettings.LayoutOrientation))
            {
                UpdateValue();
            }
        }
        
        protected override bool ConditionMet()
        {
            return _layoutSettings.LayoutOrientation == _expectedValue;
        }
    }
}