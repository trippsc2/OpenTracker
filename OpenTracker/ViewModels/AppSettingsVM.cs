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

        private IBrush _emphasisFontColor;
        public IBrush EmphasisFontColor
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

        public IBrush AccessibilityNoneColor
        {
            get => AccessibilityColors[Accessibility.None];
            set
            {
                if (AccessibilityColors[Accessibility.None] != value)
                {
                    AccessibilityColors[Accessibility.None] = value;
                    OnPropertyChanged(nameof(AccessibilityNoneColor));
                }
            }
        }

        public IBrush AccessibilityPartialColor
        {
            get => AccessibilityColors[Accessibility.Partial];
            set
            {
                if (AccessibilityColors[Accessibility.Partial] != value)
                {
                    AccessibilityColors[Accessibility.Partial] = value;
                    OnPropertyChanged(nameof(AccessibilityPartialColor));
                }
            }
        }

        public IBrush AccessibilityInspectColor
        {
            get => AccessibilityColors[Accessibility.Inspect];
            set
            {
                if (AccessibilityColors[Accessibility.Inspect] != value)
                {
                    AccessibilityColors[Accessibility.Inspect] = value;
                    OnPropertyChanged(nameof(AccessibilityInspectColor));
                }
            }
        }

        public IBrush AccessibilitySequenceBreakColor
        {
            get => AccessibilityColors[Accessibility.SequenceBreak];
            set
            {
                if (AccessibilityColors[Accessibility.SequenceBreak] != value)
                {
                    AccessibilityColors[Accessibility.SequenceBreak] = value;
                    OnPropertyChanged(nameof(AccessibilitySequenceBreakColor));
                }
            }
        }

        public IBrush AccessibilityNormalColor
        {
            get => AccessibilityColors[Accessibility.Normal];
            set
            {
                if (AccessibilityColors[Accessibility.Normal] != value)
                {
                    AccessibilityColors[Accessibility.Normal] = value;
                    OnPropertyChanged(nameof(AccessibilityNormalColor));
                }
            }
        }

        public Dictionary<Accessibility, IBrush> AccessibilityColors { get; }

        public AppSettingsVM()
        {
            DisplayAllLocations = true;
            EmphasisFontColor = Brush.Parse("#00ff00");

            AccessibilityColors = new Dictionary<Accessibility, IBrush>()
            {
                { Accessibility.None, Brush.Parse("#ff3030") },
                { Accessibility.Partial, Brush.Parse("#ff8c00") },
                { Accessibility.Inspect, Brush.Parse("#6495ed") },
                { Accessibility.SequenceBreak, Brush.Parse("#ffff00") },
                { Accessibility.Normal, Brush.Parse("#00ff00") },
                { Accessibility.Cleared, Brush.Parse("#333333") }
            };
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}