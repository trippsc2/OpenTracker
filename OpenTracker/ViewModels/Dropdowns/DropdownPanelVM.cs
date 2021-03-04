using OpenTracker.Models.Modes;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using ReactiveUI;
using System.Collections.Generic;
using System.ComponentModel;
using Avalonia.Threading;

namespace OpenTracker.ViewModels.Dropdowns
{
    /// <summary>
    /// This class contains the dropdown panel ViewModel data.
    /// </summary>
    public class DropdownPanelVM : ViewModelBase, IDropdownPanelVM
    {
        private readonly ILayoutSettings _layoutSettings;
        private readonly IMode _mode;

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
        public DropdownPanelVM(ILayoutSettings layoutSettings, IMode mode, IDropdownVMFactory factory)
        {
            _layoutSettings = layoutSettings;
            _mode = mode;

            Dropdowns = factory.GetDropdownVMs();

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
        private async void OnModeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMode.EntranceShuffle))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Visible))); 
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
        private async void OnLayoutChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ILayoutSettings.UIScale))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Scale)));
            }
        }
    }
}
