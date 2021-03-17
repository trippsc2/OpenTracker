using System.ComponentModel;
using Avalonia.Layout;
using Avalonia.Threading;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels.PinnedLocations
{
    /// <summary>
    /// This class contains the pinned location panel control ViewModel data.
    /// </summary>
    public class PinnedLocationsPanelVM : ViewModelBase, IPinnedLocationsPanelVM
    {
        private readonly ILayoutSettings _layoutSettings;

        public Orientation Orientation => _layoutSettings.CurrentLayoutOrientation;

        public IPinnedLocationVMCollection Locations { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="layoutSettings">
        /// The layout settings data.
        /// </param>
        /// <param name="locations">
        /// The pinned location control collection.
        /// </param>
        public PinnedLocationsPanelVM(ILayoutSettings layoutSettings, IPinnedLocationVMCollection locations)
        {
            _layoutSettings = layoutSettings;

            Locations = locations;

            _layoutSettings.PropertyChanged += OnLayoutChanged;
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
        private async void OnLayoutChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ILayoutSettings.CurrentLayoutOrientation))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Orientation)));
            }
        }
    }
}
