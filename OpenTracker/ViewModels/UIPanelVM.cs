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
    /// This is the class for the UI panels ViewModel.
    /// </summary>
    public class UIPanelVM : ViewModelBase, IUIPanelVM
    {
        private readonly ILayoutSettings _layoutSettings;
        public Dock ItemsDock =>
            _layoutSettings.CurrentLayoutOrientation switch
            {
                Orientation.Horizontal => _layoutSettings.HorizontalItemsPlacement,
                _ => _layoutSettings.VerticalItemsPlacement
            };

        public IDropdownPanelVM Dropdowns { get; }
        public IItemsPanelVM Items { get; }
        public IPinnedLocationsPanelVM Locations { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="layoutSettings">
        /// The layout settings.
        /// </param>
        /// <param name="dropdowns">
        /// The dropdowns panel.
        /// </param>
        /// <param name="items">
        /// The items panel.
        /// </param>
        /// <param name="locations">
        /// The pinned locations panel.
        /// </param>
        public UIPanelVM(
            ILayoutSettings layoutSettings, IDropdownPanelVM dropdowns, IItemsPanelVM items,
            IPinnedLocationsPanelVM locations)
        {
            _layoutSettings = layoutSettings;

            Dropdowns = dropdowns;
            Items = items;
            Locations = locations;

            _layoutSettings.PropertyChanged += OnLayoutChanged;
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
        private void OnLayoutChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ILayoutSettings.CurrentLayoutOrientation) ||
                e.PropertyName == nameof(ILayoutSettings.HorizontalItemsPlacement) ||
                e.PropertyName == nameof(ILayoutSettings.VerticalItemsPlacement))
            {
                this.RaisePropertyChanged(nameof(ItemsDock));
            }
        }
    }
}
