using System.ComponentModel;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Threading;
using OpenTracker.Models.Locations.Map;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Tooltips;
using ReactiveUI;

namespace OpenTracker.ViewModels.MapLocations
{
    /// <summary>
    /// This class contains the map location control ViewModel data.
    /// </summary>
    public class MapLocationVM : ViewModelBase, IMapLocationVM
    {
        private readonly IAppSettings _appSettings;
        private readonly IMapLocation _mapLocation;
        private readonly IRequirement? _dockRequirement;

        private readonly Dock _metDock;
        private readonly Dock _unmetDock;

        public bool Visible =>
            _mapLocation.RequirementMet && (_appSettings.Tracker.DisplayAllLocations || _mapLocation.Visible);
        
        public IMapLocationMarkingVM? Marking { get; }
        public IShapedMapLocationVMBase Location { get; }
        public IMapLocationToolTipVM ToolTip { get; }

        public Dock MarkingDock => _dockRequirement is null || !_dockRequirement.Met ? _unmetDock : _metDock;

        private double BaseX
        {
            get
            {
                var x = _mapLocation.X;

                if (_appSettings.Layout.CurrentMapOrientation == Orientation.Vertical)
                {
                    return x + 23.0;
                }

                return _mapLocation.Map == MapID.DarkWorld ? x + 2046.0 : x + 13.0;
            }
        }

        private double BaseY
        {
            get
            {
                var y = _mapLocation.Y;

                if (_appSettings.Layout.CurrentMapOrientation == Orientation.Horizontal)
                {
                    return y + 23.0;
                }

                return _mapLocation.Map == MapID.DarkWorld ? y + 2046.0 : y + 13.0;
            }
        }

        private double MarkingOffsetX
        {
            get
            {
                if (Marking is null || MarkingDock == Dock.Right)
                {
                    return 0;
                }

                if (MarkingDock == Dock.Left)
                {
                    return -56.0;
                }

                var offsetDifference = -28.0 - Location.OffsetX;

                return offsetDifference >= 0 ? 0.0 : offsetDifference;
            }
        }

        private double MarkingOffsetY
        {
            get
            {
                if (Marking is null || MarkingDock == Dock.Bottom)
                {
                    return 0.0;
                }

                if (MarkingDock == Dock.Top)
                {
                    return -56.0;
                }
                
                var offsetDifference = -28.0 - Location.OffsetX;

                return offsetDifference >= 0 ? 0.0 : offsetDifference;
            }
        }

        public double CanvasX => BaseX + Location.OffsetX + MarkingOffsetX;
        public double CanvasY => BaseY + Location.OffsetY + MarkingOffsetY;
        
        public ReactiveCommand<PointerEventArgs, Unit> HandlePointerEnter { get; }
        public ReactiveCommand<PointerEventArgs, Unit> HandlePointerLeave { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appSettings">
        /// The app settings data.
        /// </param>
        /// <param name="mapLocation">
        /// The map location data to be represented.
        /// </param>
        /// <param name="dockRequirement">
        /// The requirement to be met to change the dock direction.
        /// </param>
        /// <param name="metDock">
        /// The marking dock direction when the requirement is met.
        /// </param>
        /// <param name="unmetDock">
        /// The default marking dock direction.
        /// </param>
        /// <param name="marking">
        /// The map location marking control.
        /// </param>
        /// <param name="location">
        /// The location shape control.
        /// </param>
        /// <param name="toolTip">
        /// The tooltip control.
        /// </param>
        public MapLocationVM(
            IAppSettings appSettings, IMapLocation mapLocation, IRequirement? dockRequirement, Dock metDock,
            Dock unmetDock, IMapLocationMarkingVM? marking, IShapedMapLocationVMBase location,
            IMapLocationToolTipVM toolTip)
        {
            _appSettings = appSettings;
            _mapLocation = mapLocation;
            _dockRequirement = dockRequirement;
            
            _metDock = metDock;
            _unmetDock = unmetDock;

            Marking = marking;
            Location = location;
            ToolTip = toolTip;

            HandlePointerEnter = Location.HandlePointerEnter;
            HandlePointerLeave = Location.HandlePointerLeave;

            PropertyChanged += OnPropertyChanged;
            _appSettings.Layout.PropertyChanged += OnLayoutChanged;
            _appSettings.Tracker.PropertyChanged += OnTrackerChanged;
            _mapLocation.PropertyChanged += OnMapLocationChanged;
            Location.PropertyChanged += OnLocationChanged;

            if (_dockRequirement is null)
            {
                return;
            }

            _dockRequirement.PropertyChanged += OnRequirementChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on this object.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MarkingDock))
            {
                await UpdateCoordinates();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ILayoutSettings interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnLayoutChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ILayoutSettings.CurrentMapOrientation))
            {
                await UpdateCoordinates();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ITrackerSettings interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnTrackerChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ITrackerSettings.DisplayAllLocations))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Visible)));
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IMapLocation interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnMapLocationChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMapLocation.RequirementMet) ||
                e.PropertyName == nameof(IMapLocation.Visible))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Visible)));
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IShapedMapLocationVMBase interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnLocationChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IShapedMapLocationVMBase.OffsetX) ||
                e.PropertyName == nameof(IShapedMapLocationVMBase.OffsetY))
            {
                await UpdateCoordinates();
            }
        }
        
        /// <summary>
        /// Subscribes to the PropertyChanged event on the IRequirement interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnRequirementChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirement.Met))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(MarkingDock)));
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event for the CanvasX and CanvasY properties.
        /// </summary>
        private async Task UpdateCoordinates()
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                this.RaisePropertyChanged(nameof(CanvasX));
                this.RaisePropertyChanged(nameof(CanvasY));
            });
        }
    }
}