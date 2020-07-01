using OpenTracker.Models.Enums;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This is the class for Item Placement requirements.
    /// </summary>
    internal class ItemPlacementRequirement : IRequirement
    {
        private readonly Mode _mode;
        private readonly ItemPlacement _itemPlacement;

        public event PropertyChangedEventHandler PropertyChanged;

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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode data class.
        /// </param>
        /// <param name="itemPlacement">
        /// The required item placement.
        /// </param>
        public ItemPlacementRequirement(Mode mode, ItemPlacement itemPlacement)
        {
            _mode = mode ?? throw new ArgumentNullException(nameof(mode));
            _itemPlacement = itemPlacement;

            _mode.PropertyChanged += OnModeChanged;
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
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Mode class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.ItemPlacement))
            {
                UpdateAccessibility();
            }
        }

        /// <summary>
        /// Updates the accessibility of this requirement.
        /// </summary>
        private void UpdateAccessibility()
        {
            Accessibility = _mode.ItemPlacement == _itemPlacement ?
                AccessibilityLevel.Normal : AccessibilityLevel.None;
        }
    }
}
