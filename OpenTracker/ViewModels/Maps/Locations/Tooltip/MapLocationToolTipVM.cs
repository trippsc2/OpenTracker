using OpenTracker.Models.Locations;
using OpenTracker.Models.Sections;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using ReactiveUI;
using System.ComponentModel;

namespace OpenTracker.ViewModels.Maps.Locations.Tooltip
{
    /// <summary>
    /// This is the ViewModel class for map location tooltips.
    /// </summary>
    public class MapLocationToolTipVM : ViewModelBase, IMapLocationToolTipVM
    {
        private readonly ILayoutSettings _layoutSettings;
        private readonly ILocation _location;

        public double Scale =>
            _layoutSettings.UIScale;
        public string Name =>
            _location.Name;

        public IMapLocationToolTipMarkingVM? SectionMarking { get; }
        public IMapLocationToolTipNotes Notes { get; }

        public delegate IMapLocationToolTipVM Factory(ILocation location);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="location">
        /// The map location.
        /// </param>
        public MapLocationToolTipVM(
            ILayoutSettings layoutSettings, IMapLocationToolTipMarkingVM.Factory markingFactory,
            IMapLocationToolTipNotes.Factory notesFactory, ILocation location)
        {
            _layoutSettings = layoutSettings;
            _location = location;

            if (_location.Sections[0] is IMarkableSection markableSection)
            {
                SectionMarking = markingFactory(markableSection.Marking);
            }

            Notes = notesFactory(_location);

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
            if (e.PropertyName == nameof(LayoutSettings.UIScale))
            {
                this.RaisePropertyChanged(nameof(Scale));
            }
        }
    }
}
