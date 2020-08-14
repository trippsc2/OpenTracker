using Avalonia.Layout;
using OpenTracker.Models;
using OpenTracker.Models.Settings;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OpenTracker.ViewModels.PinnedLocations
{
    /// <summary>
    /// This is the ViewModel for the Locations panel control.
    /// </summary>
    public class PinnedLocationsPanelVM : ViewModelBase
    {
        public Orientation Orientation =>
            AppSettings.Instance.Layout.CurrentLayoutOrientation;

        public ObservableCollection<PinnedLocationVM> Locations { get; } =
            PinnedLocationCollection.Instance;

        /// <summary>
        /// Constructor
        /// </summary>
        public PinnedLocationsPanelVM()
        {
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
            if (e.PropertyName == nameof(LayoutSettings.CurrentLayoutOrientation))
            {
                this.RaisePropertyChanged(nameof(Orientation));
            }
        }
    }
}
