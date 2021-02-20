using Avalonia.Layout;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Items.Large;
using OpenTracker.ViewModels.Items.Small;
using ReactiveUI;
using System.ComponentModel;

namespace OpenTracker.ViewModels.Items
{
    /// <summary>
    /// This is the ViewModel class for the Items panel control.
    /// </summary>
    public class ItemsPanelVM : ViewModelBase
    {
        private readonly HorizontalSmallItemPanelVM _horizontalSmallItemPanel =
            new HorizontalSmallItemPanelVM();
        private readonly VerticalSmallItemPanelVM _verticalSmallItemPanel =
            new VerticalSmallItemPanelVM();

        public static double Scale =>
            AppSettings.Instance.Layout.UIScale;
        public static Orientation Orientation =>
            AppSettings.Instance.Layout.CurrentLayoutOrientation;

        public ModeSettingsVM ModeSettings { get; } =
            new ModeSettingsVM();
        public LargeItemPanelVM LargeItems { get; } =
            new LargeItemPanelVM();
        public SmallItemPanelVM SmallItems
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
        public ItemsPanelVM()
        {
            PropertyChanged += OnPropertyChanged;
            AppSettings.Instance.Layout.PropertyChanged += OnLayoutChanged;
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
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Orientation))
            {
                this.RaisePropertyChanged(nameof(SmallItems));
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
            if (e.PropertyName == nameof(LayoutSettings.CurrentLayoutOrientation))
            {
                this.RaisePropertyChanged(nameof(Orientation));
            }

            if (e.PropertyName == nameof(LayoutSettings.UIScale))
            {
                this.RaisePropertyChanged(nameof(Scale));
            }
        }
    }
}
