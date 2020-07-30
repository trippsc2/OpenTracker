using Avalonia.Layout;
using Newtonsoft.Json;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Utils;
using System;
using System.ComponentModel;
using System.IO;

namespace OpenTracker.Models
{
    /// <summary>
    /// This is the class for application data to be saved to file.
    /// </summary>
    [Serializable()]
    public class AppSettings : INotifyPropertyChanged
    {
        [field: NonSerialized()]
        private static readonly object _syncLock = new object();
        [field: NonSerialized()]
        private static volatile AppSettings _instance = null;

        public static AppSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new AppSettings(true);
                        }
                    }
                }

                return _instance;
            }
        }

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

        private Orientation? _layoutOrientation;
        public Orientation? LayoutOrientation
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

        private Orientation? _mapOrientation;
        public Orientation? MapOrientation
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

        private double _uiScale = 1.0;
        public double UIScale
        {
            get => _uiScale;
            set
            {
                if (_uiScale != value)
                {
                    _uiScale = value;
                    OnPropertyChanged(nameof(UIScale));
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

        public ObservableDictionary<AccessibilityLevel, string> AccessibilityColors { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="first">
        /// A boolean representing whether or not the constructor is being called by the Singleton
        /// pattern.
        /// </param>
        public AppSettings(bool first = false)
        {
            if (first)
            {
                string appSettingsPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "OpenTracker", "OpenTracker.json");

                if (File.Exists(appSettingsPath))
                {
                    string jsonContent = File.ReadAllText(appSettingsPath);
                    AppSettings appSettings = JsonConvert.DeserializeObject<AppSettings>(jsonContent);
                    CopyFrom(appSettings);
                }
                else
                {
                    DisplayAllLocations = false;
                    ShowItemCountsOnMap = true;
                    LayoutOrientation = null;
                    MapOrientation = null;
                    HorizontalUIPanelPlacement = VerticalAlignment.Bottom;
                    VerticalUIPanelPlacement = HorizontalAlignment.Left;
                    HorizontalItemsPlacement = HorizontalAlignment.Left;
                    VerticalItemsPlacement = VerticalAlignment.Top;
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
            }
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
        /// Copies data from an existing instance of this class.
        /// </summary>
        /// <param name="appSettings">
        /// An existing instance of this class.
        /// </param>
        private void CopyFrom(AppSettings appSettings)
        {
            Maximized = appSettings.Maximized;
            X = appSettings.X;
            Y = appSettings.Y;
            Width = appSettings.Width;
            Height = appSettings.Height;
            DisplayAllLocations = appSettings.DisplayAllLocations;
            ShowItemCountsOnMap = appSettings.ShowItemCountsOnMap;
            LayoutOrientation = appSettings.LayoutOrientation;
            MapOrientation = appSettings.MapOrientation;
            HorizontalUIPanelPlacement = appSettings.HorizontalUIPanelPlacement;
            VerticalUIPanelPlacement = appSettings.VerticalUIPanelPlacement;
            HorizontalItemsPlacement = appSettings.HorizontalItemsPlacement;
            VerticalItemsPlacement = appSettings.VerticalItemsPlacement;
            EmphasisFontColor = appSettings.EmphasisFontColor;
            ConnectorColor = appSettings.ConnectorColor;

            AccessibilityColors = new ObservableDictionary<AccessibilityLevel, string>();

            foreach (var color in appSettings.AccessibilityColors)
            {
                AccessibilityColors.Add(color);
            }
        }
    }
}
