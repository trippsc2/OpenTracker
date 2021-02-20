using Avalonia.Controls;
using Avalonia.Layout;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Dropdowns;
using OpenTracker.ViewModels.Items;
using OpenTracker.ViewModels.PinnedLocations;
using ReactiveUI;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This is the ViewModel for the UI panels control.
    /// </summary>
    public class UIPanelVM : ViewModelBase
    {
        private readonly ILayoutSettings _layoutSettings;
        public Dock ItemsDock =>
            _layoutSettings.CurrentLayoutOrientation switch
            {
                Orientation.Horizontal => _layoutSettings.HorizontalItemsPlacement,
                _ => _layoutSettings.VerticalItemsPlacement
            };

        public DropdownPanelVM Dropdowns { get; } =
            new DropdownPanelVM();
        public ItemsPanelVM Items { get; } =
            new ItemsPanelVM();
        public PinnedLocationsPanelVM Locations { get; } =
            new PinnedLocationsPanelVM();

        public UIPanelVM() : this(AppSettings.Instance.Layout)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        private UIPanelVM(ILayoutSettings layoutSettings)
        {
            _layoutSettings = layoutSettings;

            _layoutSettings.PropertyChanged += OnLayoutChanged;
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
            if (e.PropertyName == nameof(LayoutSettings.CurrentLayoutOrientation) ||
                e.PropertyName == nameof(LayoutSettings.HorizontalItemsPlacement) ||
                e.PropertyName == nameof(LayoutSettings.VerticalItemsPlacement))
            {
                this.RaisePropertyChanged(nameof(ItemsDock));
            }
        }
    }
}
