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
        private readonly ILayoutSettings _layoutSettings;
        private readonly IMode _mode;

        public bool Visible =>
            _mode.EntranceShuffle >= EntranceShuffle.All;
        public double Scale =>
            _layoutSettings.UIScale;

        public ObservableCollection<DropdownVM> Dropdowns { get; } =
            DropdownVMFactory.GetDropdownVMs();

        public DropdownPanelVM()
            : this(AppSettings.Instance.Layout, Mode.Instance)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        private DropdownPanelVM(ILayoutSettings layoutSettings, IMode mode)
        {
            _layoutSettings = layoutSettings;
            _mode = mode;

            _mode.PropertyChanged += OnModeChanged;
            _layoutSettings.PropertyChanged += OnLayoutChanged;
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
            if (e.PropertyName == nameof(IMode.EntranceShuffle))
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
            if (e.PropertyName == nameof(ILayoutSettings.UIScale))
            {
                this.RaisePropertyChanged(nameof(Scale));
            }
        }
    }
}
