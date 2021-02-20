using OpenTracker.Models.Modes;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OpenTracker.ViewModels.Dropdowns
{
    public class DropdownPanelVM : ViewModelBase
    {
        public static bool Visible =>
            Mode.Instance.EntranceShuffle >= EntranceShuffle.All;
        public static double Scale =>
            AppSettings.Instance.Layout.UIScale;

        public ObservableCollection<DropdownVM> Dropdowns { get; } =
            DropdownVMFactory.GetDropdownVMs();

        /// <summary>
        /// Constructor
        /// </summary>
        public DropdownPanelVM()
        {
            Mode.Instance.PropertyChanged += OnModeChanged;
            AppSettings.Instance.Layout.PropertyChanged += OnLayoutChanged;
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
            if (e.PropertyName == nameof(Mode.EntranceShuffle))
            {
                this.RaisePropertyChanged(nameof(Visible));
            }
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
