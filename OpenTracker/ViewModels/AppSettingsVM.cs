using Avalonia.Media;
using OpenTracker.Enums;
using System.Collections.Generic;

namespace OpenTracker.ViewModels
{
    public class AppSettingsVM
    {
        public Dictionary<Accessibility, IBrush> AccessibilityColors { get; }

        public bool DisplayAllLocations { get; private set; }

        public AppSettingsVM()
        {
            AccessibilityColors = new Dictionary<Accessibility, IBrush>()
            {
                { Accessibility.None, Brush.Parse("#ff3030") },
                { Accessibility.Partial, Brush.Parse("#ff8c00") },
                { Accessibility.Inspect, Brush.Parse("#6495ed") },
                { Accessibility.SequenceBreak, Brush.Parse("#ffff00") },
                { Accessibility.Normal, Brush.Parse("#00ff00") },
                { Accessibility.Cleared, Brush.Parse("#333333") }
            };

            DisplayAllLocations = true;
        }
    }
}