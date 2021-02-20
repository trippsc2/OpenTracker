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
        public static Dock ItemsDock =>
            AppSettings.Instance.Layout.CurrentLayoutOrientation switch
            {
                Orientation.Horizontal => AppSettings.Instance.Layout.HorizontalItemsPlacement,
                _ => AppSettings.Instance.Layout.VerticalItemsPlacement
            };

        public DropdownPanelVM Dropdowns { get; } =
            new DropdownPanelVM();
        public ItemsPanelVM Items { get; } =
            new ItemsPanelVM();
        public PinnedLocationsPanelVM Locations { get; } =
            new PinnedLocationsPanelVM();

        /// <summary>
        /// Constructor
        /// </summary>
        public UIPanelVM()
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
            if (e.PropertyName == nameof(LayoutSettings.CurrentLayoutOrientation) ||
                e.PropertyName == nameof(LayoutSettings.HorizontalItemsPlacement) ||
                e.PropertyName == nameof(LayoutSettings.VerticalItemsPlacement))
            {
                this.RaisePropertyChanged(nameof(ItemsDock));
            }
        }
    }
}
