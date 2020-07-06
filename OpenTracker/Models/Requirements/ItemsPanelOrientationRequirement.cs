using Avalonia.Layout;
using OpenTracker.Models.Enums;
using OpenTracker.ViewModels;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
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

        public ItemsPanelOrientationRequirement(ItemsPanelControlVM itemsPanel, Orientation orientation)
        {
            _itemsPanel = itemsPanel ?? throw new ArgumentNullException(nameof(itemsPanel));
            _orientation = orientation;

            itemsPanel.PropertyChanged += OnItemsPanelChanged;

            UpdateAccessibility();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnItemsPanelChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ItemsPanelControlVM.ItemsPanelOrientation))
            {
                UpdateAccessibility();
            }
        }

        private void UpdateAccessibility()
        {
            Accessibility = _itemsPanel.ItemsPanelOrientation == _orientation ?
                AccessibilityLevel.Normal : AccessibilityLevel.None;
        }
    }
}
