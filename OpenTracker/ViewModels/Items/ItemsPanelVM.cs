using Avalonia.Layout;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Items.Large;
using OpenTracker.ViewModels.Items.Small;
using ReactiveUI;
using System.ComponentModel;
using Avalonia.Threading;

namespace OpenTracker.ViewModels.Items
{
    /// <summary>
    /// This is the ViewModel class for the Items panel control.
    /// </summary>
    public class ItemsPanelVM : ViewModelBase, IItemsPanelVM
    {
        private readonly ILayoutSettings _layoutSettings;

        private readonly IHorizontalSmallItemPanelVM _horizontalSmallItemPanel;
        private readonly IVerticalSmallItemPanelVM _verticalSmallItemPanel;

        public double Scale =>
            _layoutSettings.UIScale;
        public Orientation Orientation =>
            _layoutSettings.CurrentLayoutOrientation;

        public IModeSettingsVM ModeSettings { get; }
        public ILargeItemPanelVM LargeItems { get; }
        public ISmallItemPanelVM SmallItems
        {
            get
            {
                if (Orientation == Orientation.Horizontal)
                {
                    return _horizontalSmallItemPanel;
                }

                return _verticalSmallItemPanel;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemsPanelVM(
            ILayoutSettings layoutSettings, IHorizontalSmallItemPanelVM horizontalSmallItemPanel,
            IVerticalSmallItemPanelVM verticalSmallItemPanel, IModeSettingsVM modeSettings,
            ILargeItemPanelVM largeItems)
        {
            _layoutSettings = layoutSettings;

            _horizontalSmallItemPanel = horizontalSmallItemPanel;
            _verticalSmallItemPanel = verticalSmallItemPanel;

            ModeSettings = modeSettings;
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
            if (e.PropertyName == nameof(ILayoutSettings.CurrentLayoutOrientation))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Orientation)));
            }

            if (e.PropertyName == nameof(ILayoutSettings.UIScale))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Scale)));
            }
        }
    }
}
