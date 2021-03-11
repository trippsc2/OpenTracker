using System.ComponentModel;
using Avalonia.Threading;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Sections;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels.Tooltips
{
    /// <summary>
    /// This class contains the map location tooltip control ViewModel data.
    /// </summary>
    public class MapLocationToolTipVM : ViewModelBase, IMapLocationToolTipVM
    {
        private readonly ILayoutSettings _layoutSettings;
        private readonly ILocation _location;

        public double Scale => _layoutSettings.UIScale;
        public string Name => _location.Name;

        public IMapLocationToolTipMarkingVM? SectionMarking { get; }
        public IMapLocationToolTipNotes Notes { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="layoutSettings">
        /// The layout settings data.
        /// </param>
        /// <param name="markingFactory">
        /// An Autofac factory for creating marking controls.
        /// </param>
        /// <param name="notesFactory">
        /// An Autofac factory for creating tooltip notes controls.
        /// </param>
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
        private async void OnLayoutChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LayoutSettings.UIScale))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Scale)));
            }
        }
    }
}
