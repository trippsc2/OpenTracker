using System;
using Avalonia.Controls;
using Avalonia.Layout;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;

namespace OpenTracker.ViewModels.MapLocations
{
    public class MapLocationVM : ViewModelBase, IMapLocationVM
    {
        private readonly IAppSettings _appSettings;
        private readonly IMapLocation _mapLocation;
        private readonly IRequirement? _dockRequirement;

        private readonly Dock _requirementMetDock;
        private readonly Dock _requirementUnmetDock;

        public bool Visible =>
            _mapLocation.Requirement.Met && (_appSettings.Tracker.DisplayAllLocations ||
            _mapLocation.Location.Accessibility != AccessibilityLevel.Cleared &&
            _mapLocation.Location.Accessibility != AccessibilityLevel.None);
        
        public IMapLocationMarkingVM? Marking { get; }
        public IShapedMapLocationVMBase Location { get; }

        public Dock MarkingDock =>
            _dockRequirement is null || !_dockRequirement.Met ? _requirementUnmetDock : _requirementMetDock;

        private double BaseX
        {
            get
            {
                var x = _mapLocation.X;

                if (_appSettings.Layout.CurrentMapOrientation == Orientation.Vertical)
                {
                    return x + 23;
                }

                return _mapLocation.Map == MapID.DarkWorld ? x + 2046 : x + 13;
            }
        }

        private double BaseY
        {
            get
            {
                var y = _mapLocation.Y;

                if (_appSettings.Layout.CurrentMapOrientation == Orientation.Horizontal)
                {
                    return y + 2046;
                }

                return _mapLocation.Map == MapID.DarkWorld ? y + 2046 : y + 13;
            }
        }

        private double MarkingOffsetX => MarkingDock switch
        {
            Dock.Left => -55,
            Dock.Right => 0,
            _ => -27.5
        };

        private double MarkingOffsetY => MarkingDock switch
        {
            Dock.Bottom => 0,
            Dock.Top => -55,
            _ => -27.5
        };

        public double CanvasX => BaseX + Location.OffsetX + MarkingOffsetX;
        public double CanvasY => BaseY + Location.OffsetY + MarkingOffsetY;
    }
}