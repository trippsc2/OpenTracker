using Avalonia;
using Avalonia.Layout;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using ReactiveUI;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels.MapAreaControls
{
    /// <summary>
    /// This is the ViewModel of the map control.
    /// </summary>
    public class MapControlVM : ViewModelBase
    {
        private readonly MapAreaControlVM _mapArea;
        private readonly MapID _id;

        public Thickness MapMargin =>
            _mapArea.MapPanelOrientation switch
            {
                Orientation.Horizontal => new Thickness(10, 20),
                _ => new Thickness(20, 10)
            };

        public string ImageSource 
        {
            get
            {
                WorldState worldState = Mode.Instance.WorldState == WorldState.Inverted ?
                    WorldState.Inverted : WorldState.StandardOpen;

                return $"avares://OpenTracker/Assets/Images/Maps/" +
                    $"{ worldState.ToString().ToLowerInvariant() }" +
                    $"_{ _id.ToString().ToLowerInvariant() }.png";
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">
        /// The map identity.
        /// </param>
        /// <param name="mapArea">
        /// The view-model of the main window.
        /// </param>
        public MapControlVM(MapID id, MapAreaControlVM mapArea)
        {
            _id = id;
            _mapArea = mapArea ?? throw new ArgumentNullException(nameof(mapArea));

            Mode.Instance.PropertyChanged += OnModeChanged;
            _mapArea.PropertyChanged += OnMapAreaChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the MapAreaControlVM class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnMapAreaChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MapAreaControlVM.MapPanelOrientation))
            {
                this.RaisePropertyChanged(nameof(MapMargin));
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Mode class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.WorldState))
            {
                this.RaisePropertyChanged(nameof(ImageSource));
            }
        }
    }
}
