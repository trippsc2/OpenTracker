using Avalonia.Layout;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.ViewModels.UIPanels.ItemsPanel;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This is the class for the requirement of a specified Items panel orientation.
    /// </summary>
    public class ItemsPanelOrientationRequirement : IRequirement
    {
        private readonly ItemsPanelControlVM _itemsPanel;
        private readonly Orientation _orientation;

        public bool Met =>
            Accessibility != AccessibilityLevel.None;

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
        /// <param name="itemsPanel">
        /// The Items panel control ViewModel instance.
        /// </param>
        /// <param name="orientation">
        /// The orientation requirement.
        /// </param>
        public ItemsPanelOrientationRequirement(ItemsPanelControlVM itemsPanel, Orientation orientation)
        {
            _itemsPanel = itemsPanel ?? throw new ArgumentNullException(nameof(itemsPanel));
            _orientation = orientation;

            itemsPanel.PropertyChanged += OnItemsPanelChanged;

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
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ItemsPanelControlVM class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnItemsPanelChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ItemsPanelControlVM.ItemsPanelOrientation))
            {
                UpdateAccessibility();
            }
        }

        /// <summary>
        /// Updates the accessibility of this requirement.
        /// </summary>
        private void UpdateAccessibility()
        {
            Accessibility = _itemsPanel.ItemsPanelOrientation == _orientation ?
                AccessibilityLevel.Normal : AccessibilityLevel.None;
        }
    }
}
