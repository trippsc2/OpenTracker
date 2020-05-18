using OpenTracker.Models.Enums;
using OpenTracker.Utils;
using System;
using System.ComponentModel;

namespace OpenTracker.Models
{
    [Serializable()]
    public class AppSettings : INotifyPropertyChanged
    {
        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;

        public bool? Maximized { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }

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

        private string _emphasisFontColor;
        public string EmphasisFontColor
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

        private string _connectorColor;
        public string ConnectorColor
        {
            get => _connectorColor;
            set
            {
                if (_connectorColor != value)
                {
                    _connectorColor = value;
                    OnPropertyChanged(nameof(ConnectorColor));
                }
            }
        }

        public ObservableDictionary<AccessibilityLevel, string> AccessibilityColors { get; }

        public AppSettings()
        {
            DisplayAllLocations = false;
            ShowItemCountsOnMap = true;
            HorizontalUIPanelPlacement = VerticalAlignment.Bottom;
            VerticalUIPanelPlacement = HorizontalAlignment.Left;
            EmphasisFontColor = "#ff00ff00";
            ConnectorColor = "#ff40e0d0";

            AccessibilityColors = new ObservableDictionary<AccessibilityLevel, string>()
            {
                { AccessibilityLevel.None, "#ffff3030" },
                { AccessibilityLevel.Partial, "#ffff8c00" },
                { AccessibilityLevel.Inspect, "#ff6495ed" },
                { AccessibilityLevel.SequenceBreak, "#ffffff00" },
                { AccessibilityLevel.Normal, "#ff00ff00" },
                { AccessibilityLevel.Cleared, "#ff333333" }
            };
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
