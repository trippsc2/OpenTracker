using System.ComponentModel;
using Avalonia.Layout;
using Avalonia.Threading;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels.Dungeons
{
    public class DungeonPanelVM : ViewModelBase, IDungeonPanelVM
    {
        private readonly ILayoutSettings _layoutSettings;

        private readonly IHorizontalDungeonPanelVM _horizontalSmallItemPanel;
        private readonly IVerticalDungeonPanelVM _verticalSmallItemPanel;

        public Orientation Orientation => _layoutSettings.CurrentLayoutOrientation;

        public IOrientedDungeonPanelVMBase Items =>
            Orientation == Orientation.Horizontal ?
                (IOrientedDungeonPanelVMBase) _horizontalSmallItemPanel : _verticalSmallItemPanel;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="layoutSettings">
        /// The layout settings data.
        /// </param>
        /// <param name="horizontalSmallItemPanel">
        /// The horizontal small item panel control.
        /// </param>
        /// <param name="verticalSmallItemPanel">
        /// The vertical small item panel control.
        /// </param>
        public DungeonPanelVM(
            ILayoutSettings layoutSettings, IHorizontalDungeonPanelVM horizontalSmallItemPanel,
            IVerticalDungeonPanelVM verticalSmallItemPanel)
        {
            _layoutSettings = layoutSettings;

            _horizontalSmallItemPanel = horizontalSmallItemPanel;
            _verticalSmallItemPanel = verticalSmallItemPanel;

            PropertyChanged += OnPropertyChanged;
            _layoutSettings.PropertyChanged += OnLayoutChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on this class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Orientation))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Items)));
            }
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
        private async void OnLayoutChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ILayoutSettings.CurrentLayoutOrientation))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Orientation)));
            }
        }
    }
}