using Avalonia;
using Avalonia.Layout;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using ReactiveUI;
using System.ComponentModel;

namespace OpenTracker.ViewModels.Maps
{
    /// <summary>
    /// This is the ViewModel of the map control.
    /// </summary>
    public class MapVM : ViewModelBase
    {
        private readonly ILayoutSettings _layoutSettings;
        private readonly IMode _mode;
        private readonly MapID _id;

        public Thickness Margin =>
            _layoutSettings.CurrentMapOrientation switch
            {
                Orientation.Horizontal => new Thickness(10, 20),
                _ => new Thickness(20, 10)
            };
        public string ImageSource 
        {
            get
            {
                var worldState = _mode.WorldState == WorldState.Inverted ?
                    WorldState.Inverted : WorldState.StandardOpen;

                return $"avares://OpenTracker/Assets/Images/Maps/" +
                    worldState.ToString().ToLowerInvariant() +
                    $"_{_id.ToString().ToLowerInvariant()}.png";
            }
        }

        public MapVM(MapID id) : this(AppSettings.Instance.Layout, Mode.Instance, id)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">
        /// The map identity.
        /// </param>
        private MapVM(ILayoutSettings layoutSettings, IMode mode, MapID id)
        {
            _layoutSettings = layoutSettings;
            _mode = mode;
            _id = id;

            _mode.PropertyChanged += OnModeChanged;
            _layoutSettings.PropertyChanged += OnLayoutChanged;
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
