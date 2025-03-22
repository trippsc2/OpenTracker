using System.ComponentModel;
using System.Reactive;
using Avalonia.Input;
using Avalonia.Threading;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Locations.Map;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels.MapLocations
{
    public class MapLocationColorProvider : ViewModelBase, IMapLocationColorProvider
    {
        private readonly IColorSettings _colorSettings;
        private readonly IMapLocation _mapLocation;

        private bool _highlighted;
        private bool Highlighted
        {
            get => _highlighted;
            set
            {
                if (_highlighted == value)
                {
                    return;
                }
                
                _highlighted = value;
                this.RaisePropertyChanged(nameof(BorderColor));
            }
        }

        public string BorderColor => Highlighted ? "#ffffffff" : "#ff000000";
        public string Color => _colorSettings.AccessibilityColors[_mapLocation.Location.Accessibility];
        
        public ReactiveCommand<PointerEventArgs, Unit> HandlePointerEnter { get; }
        public ReactiveCommand<PointerEventArgs, Unit> HandlePointerLeave { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="colorSettings">
        /// The color settings data.
        /// </param>
        /// <param name="mapLocation">
        /// The map location data.
        /// </param>
        public MapLocationColorProvider(IColorSettings colorSettings, IMapLocation mapLocation)
        {
            _colorSettings = colorSettings;
            _mapLocation = mapLocation;

            HandlePointerEnter = ReactiveCommand.Create<PointerEventArgs>(HandlePointerEnterImpl);
            HandlePointerLeave = ReactiveCommand.Create<PointerEventArgs>(HandlePointerLeaveImpl);

            _colorSettings.PropertyChanged += OnColorChanged;
            _mapLocation.Location.PropertyChanged += OnLocationChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ObservableCollection for the
        /// accessibility colors.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnColorChanged(object? sender, PropertyChangedEventArgs e)
        {
            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Color)));
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ILocation interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnLocationChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ILocation.Accessibility))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Color)));
            }
        }

        /// <summary>
        /// Highlights the control.
        /// </summary>
        private void Highlight()
        {
            Highlighted = true;
        }

        /// <summary>
        /// Un-highlights the control.
        /// </summary>
        private void Unhighlight()
        {
            Highlighted = false;
        }

        /// <summary>
        /// Handles pointer entering the control.
        /// </summary>
        /// <param name="e">
        /// The PointerEnter event args.
        /// </param>
        private void HandlePointerEnterImpl(PointerEventArgs e)
        {
            Highlight();
        }

        /// <summary>
        /// Handles pointer leaving the control.
        /// </summary>
        /// <param name="e">
        /// The PointerLeave event args.
        /// </param>
        private void HandlePointerLeaveImpl(PointerEventArgs e)
        {
            Unhighlight();
        }
    }
}