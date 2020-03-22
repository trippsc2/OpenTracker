using Avalonia.Media;
using OpenTracker.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    public class AppSettingsVM : INotifyPropertyChanged
    {
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
            DisplayAllLocations = true;
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
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}