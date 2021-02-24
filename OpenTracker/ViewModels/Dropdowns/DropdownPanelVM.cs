using OpenTracker.Models.Modes;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using ReactiveUI;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.ViewModels.Dropdowns
{
    /// <summary>
    /// This is the class for the dropdown panel ViewModel.
    /// </summary>
    public class DropdownPanelVM : ViewModelBase, IDropdownPanelVM
    {
        private readonly ILayoutSettings _layoutSettings;
        private readonly IMode _mode;
        private readonly IDropdownVMFactory _factory;

        public bool Visible =>
            _mode.EntranceShuffle >= EntranceShuffle.All;
        public double Scale =>
            _layoutSettings.UIScale;

        public List<IDropdownVM> Dropdowns { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="layoutSettings">
        /// The layout settings.
        /// </param>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="factory">
        /// The factory for creating new dropdown controls.
        /// </param>
        public DropdownPanelVM(
            ILayoutSettings layoutSettings, IMode mode, IDropdownVMFactory factory)
        {
            _layoutSettings = layoutSettings;
            _mode = mode;
            _factory = factory;

            Dropdowns = _factory.GetDropdownVMs();

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
