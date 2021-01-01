using Avalonia.Layout;
using OpenTracker.Models;
using OpenTracker.Models.Settings;
using OpenTracker.Models.Utils;
using ReactiveUI;
using System.ComponentModel;

namespace OpenTracker.ViewModels.PinnedLocations
{
    /// <summary>
    /// This is the ViewModel for the Locations panel control.
    /// </summary>
    public class PinnedLocationsPanelVM : ViewModelBase
    {
        public static Orientation Orientation =>
            AppSettings.Instance.Layout.CurrentLayoutOrientation;

        public IObservableCollection<PinnedLocationVM> Locations { get; } =
            PinnedLocationVMCollection.Instance;

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
