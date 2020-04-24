using Avalonia.Media;
using OpenTracker.Interfaces;
using OpenTracker.Models.Enums;
using OpenTracker.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    [Serializable()]
    public class AppSettingsVM : IAppSettingsVM
    {
        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _displayAllLocations;
        public bool DisplayAllLocations
        {
            get => _displayAllLocations;
            set
            {
                if (_displayAllLocations != value)
                {
                    _displayAllLocations = value;
                    OnPropertyChanged(nameof(DisplayAllLocations));
                }
            }
        }

        private bool _showItemCountsOnMap;
        public bool ShowItemCountsOnMap
        {
            get => _showItemCountsOnMap;
            set
            {
                if (_showItemCountsOnMap != value)
                {
                    _showItemCountsOnMap = value;
                    OnPropertyChanged(nameof(ShowItemCountsOnMap));
                }
            }
        }

        private LayoutOrientation _layoutOrientation;
        public LayoutOrientation LayoutOrientation
        {
            get => _layoutOrientation;
            set
            {
                if (_layoutOrientation != value)
                {
                    _layoutOrientation = value;
                    OnPropertyChanged(nameof(LayoutOrientation));
                }
            }
        }

        private MapOrientation _mapOrientation;
        public MapOrientation MapOrientation
        {
            get => _mapOrientation;
            set
            {
                if (_mapOrientation != value)
                {
                    _mapOrientation = value;
                    OnPropertyChanged(nameof(MapOrientation));
                }
            }
        }

        private VerticalAlignment _horizontalUIPanelPlacement;
        public VerticalAlignment HorizontalUIPanelPlacement
        {
            get => _horizontalUIPanelPlacement;
            set
            {
                if (_horizontalUIPanelPlacement != value)
                {
                    _horizontalUIPanelPlacement = value;
                    OnPropertyChanged(nameof(HorizontalUIPanelPlacement));
                }
            }
        }

        private HorizontalAlignment _verticalUIPanelPlacement;
        public HorizontalAlignment VerticalUIPanelPlacement
        {
            get => _verticalUIPanelPlacement;
            set
            {
                if (_verticalUIPanelPlacement != value)
                {
                    _verticalUIPanelPlacement = value;
                    OnPropertyChanged(nameof(VerticalUIPanelPlacement));
                }
            }
        }

        private HorizontalAlignment _horizontalItemsPlacement;
        public HorizontalAlignment HorizontalItemsPlacement
        {
            get => _horizontalItemsPlacement;
            set
            {
                if (_horizontalItemsPlacement != value)
                {
                    _horizontalItemsPlacement = value;
                    OnPropertyChanged(nameof(HorizontalItemsPlacement));
                }
            }
        }

        private VerticalAlignment _verticalItemsPlacement;
        public VerticalAlignment VerticalItemsPlacement
        {
            get => _verticalItemsPlacement;
            set
            {
                if (_verticalItemsPlacement != value)
                {
                    _verticalItemsPlacement = value;
                    OnPropertyChanged(nameof(VerticalItemsPlacement));
                }
            }
        }

        private SolidColorBrush _emphasisFontColor;
        public SolidColorBrush EmphasisFontColor
        {
            get => _emphasisFontColor;
            set
            {
                if (_emphasisFontColor != value)
                {
                    _emphasisFontColor = value;
                    OnPropertyChanged(nameof(EmphasisFontColor));
                }
            }
        }

        public SolidColorBrush AccessibilityNoneColor
        {
            get => AccessibilityColors[AccessibilityLevel.None];
            set
            {
                if (AccessibilityColors[AccessibilityLevel.None] != value)
                {
                    AccessibilityColors[AccessibilityLevel.None] = value;
                    OnPropertyChanged(nameof(AccessibilityNoneColor));
                }
            }
        }

        public SolidColorBrush AccessibilityPartialColor
        {
            get => AccessibilityColors[AccessibilityLevel.Partial];
            set
            {
                if (AccessibilityColors[AccessibilityLevel.Partial] != value)
                {
                    AccessibilityColors[AccessibilityLevel.Partial] = value;
                    OnPropertyChanged(nameof(AccessibilityPartialColor));
                }
            }
        }

        public SolidColorBrush AccessibilityInspectColor
        {
            get => AccessibilityColors[AccessibilityLevel.Inspect];
            set
            {
                if (AccessibilityColors[AccessibilityLevel.Inspect] != value)
                {
                    AccessibilityColors[AccessibilityLevel.Inspect] = value;
                    OnPropertyChanged(nameof(AccessibilityInspectColor));
                }
            }
        }

        public SolidColorBrush AccessibilitySequenceBreakColor
        {
            get => AccessibilityColors[AccessibilityLevel.SequenceBreak];
            set
            {
                if (AccessibilityColors[AccessibilityLevel.SequenceBreak] != value)
                {
                    AccessibilityColors[AccessibilityLevel.SequenceBreak] = value;
                    OnPropertyChanged(nameof(AccessibilitySequenceBreakColor));
                }
            }
        }

        public SolidColorBrush AccessibilityNormalColor
        {
            get => AccessibilityColors[AccessibilityLevel.Normal];
            set
            {
                if (AccessibilityColors[AccessibilityLevel.Normal] != value)
                {
                    AccessibilityColors[AccessibilityLevel.Normal] = value;
                    OnPropertyChanged(nameof(AccessibilityNormalColor));
                }
            }
        }

        public Dictionary<AccessibilityLevel, SolidColorBrush> AccessibilityColors { get; }

        public AppSettingsVM()
        {
            DisplayAllLocations = false;
            ShowItemCountsOnMap = true;
            HorizontalUIPanelPlacement = VerticalAlignment.Bottom;
            VerticalUIPanelPlacement = HorizontalAlignment.Left;
            EmphasisFontColor = new SolidColorBrush(Color.Parse("#00ff00"));

            AccessibilityColors = new Dictionary<AccessibilityLevel, SolidColorBrush>()
            {
                { AccessibilityLevel.None, new SolidColorBrush(Color.Parse("#ff3030")) },
                { AccessibilityLevel.Partial, new SolidColorBrush(Color.Parse("#ff8c00")) },
                { AccessibilityLevel.Inspect, new SolidColorBrush(Color.Parse("#6495ed")) },
                { AccessibilityLevel.SequenceBreak, new SolidColorBrush(Color.Parse("#ffff00")) },
                { AccessibilityLevel.Normal, new SolidColorBrush(Color.Parse("#00ff00")) },
                { AccessibilityLevel.Cleared, new SolidColorBrush(Color.Parse("#333333")) }
            };
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}