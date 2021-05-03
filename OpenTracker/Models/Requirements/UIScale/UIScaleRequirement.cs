using System;
using System.ComponentModel;
using OpenTracker.Models.Settings;

namespace OpenTracker.Models.Requirements.UIScale
{
    /// <summary>
    ///     This class contains UI scale requirement data.
    /// </summary>
    public class UIScaleRequirement : BooleanRequirement, IUIScaleRequirement
    {
        private readonly ILayoutSettings _layoutSettings;
        private readonly double _expectedValue;
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="layoutSettings">
        ///     The layout settings data.
        /// </param>
        /// <param name="expectedValue">
        ///     The expected dock value.
        /// </param>
        public UIScaleRequirement(ILayoutSettings layoutSettings, double expectedValue)
        {
            _layoutSettings = layoutSettings;
            _expectedValue = expectedValue;

            _layoutSettings.PropertyChanged += OnLayoutSettingsChanged;
            
            UpdateValue();
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the ILayoutSettings interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnLayoutSettingsChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ILayoutSettings.UIScale))
            {
                UpdateValue();
            }
        }
        
        protected override bool ConditionMet()
        {
            return Math.Abs(_layoutSettings.UIScale - _expectedValue) < 0.01;
        }
    }
}