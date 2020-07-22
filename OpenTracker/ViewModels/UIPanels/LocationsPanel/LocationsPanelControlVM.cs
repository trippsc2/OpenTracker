using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using OpenTracker.ViewModels.UIPanels.LocationsPanel.PinnedLocations;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OpenTracker.ViewModels.UIPanels.LocationsPanel
{
    /// <summary>
    /// This is the ViewModel for the Locations panel control.
    /// </summary>
    public class LocationsPanelControlVM : ViewModelBase
    {
        private readonly UIPanelVM _uiPanel;
        private readonly MainWindowVM _mainWindow;

        public Thickness LocationsPanelMargin =>
            _uiPanel.UIPanelOrientationDock switch
            {
                Dock.Left => new Thickness(1, 0, 2, 2),
                Dock.Bottom => new Thickness(2, 2, 0, 1),
                Dock.Right => new Thickness(2, 0, 1, 2),
                _ => new Thickness(2, 1, 0, 2),
            };
        public Orientation LocationsPanelOrientation =>
            _mainWindow.UIPanelDock switch
            {
                Dock.Left => Orientation.Vertical,
                Dock.Right => Orientation.Vertical,
                _ => Orientation.Horizontal,
            };

        public ObservableCollection<PinnedLocationVM> Locations { get; } =
            new ObservableCollection<PinnedLocationVM>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uiPanel">
        /// The UI panels ViewModel parent class.
        /// </param>
        /// <param name="mainWindow">
        /// The main window ViewModel parent class.
        /// </param>
        public LocationsPanelControlVM(UIPanelVM uiPanel, MainWindowVM mainWindow)
        {
            _uiPanel = uiPanel ?? throw new ArgumentNullException(nameof(uiPanel));
            _mainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));

            _uiPanel.PropertyChanged += OnUIPanelChanged;
            _mainWindow.PropertyChanged += OnMainWindowChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the UIPanel class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnUIPanelChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(UIPanelVM.UIPanelOrientationDock))
            {
                this.RaisePropertyChanged(nameof(LocationsPanelMargin));
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the MainWindowVM class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnMainWindowChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainWindowVM.UIPanelDock))
            {
                this.RaisePropertyChanged(nameof(LocationsPanelOrientation));
            }
        }

        /// <summary>
        /// Resets this control to its starting values.
        /// </summary>
        public void Reset()
        {
            Locations.Clear();
        }
    }
}
