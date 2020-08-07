using Avalonia;
using Avalonia.Layout;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Settings;
using ReactiveUI;
using System.ComponentModel;

namespace OpenTracker.ViewModels.Maps
{
    /// <summary>
    /// This is the ViewModel of the map control.
    /// </summary>
    public class MapVM : ViewModelBase
    {
        private readonly MapID _id;

        public Thickness Margin =>
            AppSettings.Instance.Layout.CurrentMapOrientation switch
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
                    worldState.ToString().ToLowerInvariant() +
                    $"_{_id.ToString().ToLowerInvariant()}.png";
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">
        /// The map identity.
        /// </param>
        public MapVM(MapID id)
        {
            _id = id;

            Mode.Instance.PropertyChanged += OnModeChanged;
            AppSettings.Instance.Layout.PropertyChanged += OnLayoutChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the LayoutSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnLayoutChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LayoutSettings.CurrentMapOrientation))
            {
                this.RaisePropertyChanged(nameof(Margin));
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
