using Avalonia.Layout;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Settings;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This is the class for the requirement of a specified items panel orientation.
    /// </summary>
    public class ItemsPanelOrientationRequirement : BooleanRequirement
    {
        private readonly ILayoutSettings _layoutSettings;
        private readonly Orientation _expectedValue;

        public delegate ItemsPanelOrientationRequirement Factory(Orientation expectedValue);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="layoutSettings">
        /// The layout settings.
        /// </param>
        /// <param name="expectedValue">
        /// The expected orientation value.
        /// </param>
        public ItemsPanelOrientationRequirement(
            ILayoutSettings layoutSettings, Orientation expectedValue)
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
            if (e.PropertyName == nameof(ILayoutSettings.CurrentLayoutOrientation))
            {
                UpdateValue();
            }
        }

        protected override bool ConditionMet()
        {
            return _layoutSettings.CurrentLayoutOrientation == _expectedValue;
        }
    }
}
