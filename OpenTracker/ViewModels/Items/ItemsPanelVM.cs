using Avalonia.Layout;
using Avalonia.Threading;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Items.Large;
using OpenTracker.ViewModels.Items.Small;
using OpenTracker.ViewModels.UIPanels;
using ReactiveUI;
using System.ComponentModel;

namespace OpenTracker.ViewModels.Items
{
    /// <summary>
    /// This class contains the items panel body control ViewModel data.
    /// </summary>
    public class ItemsPanelVM : ViewModelBase, IItemsPanelVM
    {
        private readonly ILayoutSettings _layoutSettings;

        private readonly IHorizontalSmallItemPanelVM _horizontalSmallItemPanel;
        private readonly IVerticalSmallItemPanelVM _verticalSmallItemPanel;

        public Orientation Orientation => _layoutSettings.CurrentLayoutOrientation;

        public ILargeItemPanelVM LargeItems { get; }
        public ISmallItemPanelVM SmallItems =>
            Orientation == Orientation.Horizontal ?
                (ISmallItemPanelVM) _horizontalSmallItemPanel : _verticalSmallItemPanel;

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
        /// <param name="largeItems">
        /// The large item panel control.
        /// </param>
        public ItemsPanelVM(
            ILayoutSettings layoutSettings, IHorizontalSmallItemPanelVM horizontalSmallItemPanel,
            IVerticalSmallItemPanelVM verticalSmallItemPanel, ILargeItemPanelVM largeItems)
        {
            _layoutSettings = layoutSettings;

            _horizontalSmallItemPanel = horizontalSmallItemPanel;
            _verticalSmallItemPanel = verticalSmallItemPanel;

            LargeItems = largeItems;

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
        private async void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Orientation))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(SmallItems)));
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
        private async void OnLayoutChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ILayoutSettings.CurrentLayoutOrientation))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Orientation)));
            }
        }
    }
}
