using Avalonia.Layout;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Settings;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This is the class for the requirement of a specified Items panel orientation.
    /// </summary>
    public class ItemsPanelOrientationRequirement : IRequirement
    {
        private readonly ILayoutSettings _layoutSettings;
        private readonly Orientation _orientation;

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

        public ItemsPanelOrientationRequirement(Orientation orientation)
            : this(AppSettings.Instance.Layout, orientation)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="orientation">
        /// The orientation requirement.
        /// </param>
        private ItemsPanelOrientationRequirement(
            ILayoutSettings layoutSettings, Orientation orientation)
        {
            _layoutSettings = layoutSettings;
            _orientation = orientation;

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
            if (e.PropertyName == nameof(ILayoutSettings.CurrentLayoutOrientation))
            {
                UpdateAccessibility();
            }
        }

        /// <summary>
        /// Updates the accessibility of this requirement.
        /// </summary>
        private void UpdateAccessibility()
        {
            Accessibility = _layoutSettings.CurrentLayoutOrientation == _orientation ?
                AccessibilityLevel.Normal : AccessibilityLevel.None;
        }
    }
}
