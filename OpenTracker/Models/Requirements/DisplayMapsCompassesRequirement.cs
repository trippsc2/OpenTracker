using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Settings;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    public class DisplayMapsCompassesRequirement : IRequirement
    {
        private readonly ILayoutSettings _layoutSettings;
        private readonly bool _displayMapsCompasses;

        public bool Met =>
            Accessibility != AccessibilityLevel.None;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? ChangePropagated;

        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            private set
            {
                if (_accessibility != value)
                {
                    _accessibility = value;
                    OnPropertyChanged(nameof(Accessibility));
                }
            }
        }

        public DisplayMapsCompassesRequirement(bool displayMapsCompasses)
            : this(AppSettings.Instance.Layout, displayMapsCompasses)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="displayMapsCompasses">
        /// A boolean representing the display maps/compasses requirement.
        /// </param>
        private DisplayMapsCompassesRequirement(
            ILayoutSettings layoutSettings, bool displayMapsCompasses)
        {
            _layoutSettings = layoutSettings;
            _displayMapsCompasses = displayMapsCompasses;

            _layoutSettings.PropertyChanged += OnLayoutChanged;

            UpdateAccessibility();
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            ChangePropagated?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the LayoutSettings class.
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
                UpdateAccessibility();
            }
        }

        /// <summary>
        /// Updates the accessibility of this requirement.
        /// </summary>
        private void UpdateAccessibility()
        {
            Accessibility =
                _layoutSettings.DisplayMapsCompasses == _displayMapsCompasses ?
                AccessibilityLevel.Normal : AccessibilityLevel.None;
        }
    }
}
