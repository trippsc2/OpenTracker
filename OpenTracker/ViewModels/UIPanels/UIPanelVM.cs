using Avalonia.Controls;
using Avalonia.Layout;
using OpenTracker.Models;
using OpenTracker.ViewModels.UIPanels.ItemsPanel;
using OpenTracker.ViewModels.UIPanels.LocationsPanel;
using ReactiveUI;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels.UIPanels
{
    /// <summary>
    /// This is the ViewModel for the UI panels control.
    /// </summary>
    public class UIPanelVM : ViewModelBase
    {
        private readonly MainWindowVM _mainWindow;

        public Dock UIPanelOrientationDock
        {
            get
            {
                switch (_mainWindow.UIPanelDock)
                {
                    case Dock.Left:
                    case Dock.Right:
                        {
                            return AppSettings.Instance.VerticalItemsPlacement switch
                            {
                                VerticalAlignment.Top => Dock.Top,
                                _ => Dock.Bottom,
                            };
                        }
                    case Dock.Bottom:
                    case Dock.Top:
                        {
                            return AppSettings.Instance.HorizontalItemsPlacement switch
                            {
                                HorizontalAlignment.Left => Dock.Left,
                                _ => Dock.Right,
                            };
                        }
                }

                return Dock.Right;
            }
        }

        public ItemsPanelControlVM ItemsPanel { get; }
        public LocationsPanelControlVM LocationsPanel { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mainWindow">
        /// The main window ViewModel parent class.
        /// </param>
        public UIPanelVM(MainWindowVM mainWindow)
        {
            _mainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));
            ItemsPanel = new ItemsPanelControlVM(this, _mainWindow);
            LocationsPanel = new LocationsPanelControlVM(this, _mainWindow);

            AppSettings.Instance.PropertyChanged += OnAppSettingsChanged;
            _mainWindow.PropertyChanged += OnMainWindowChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the AppSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnAppSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AppSettings.HorizontalItemsPlacement) ||
                e.PropertyName == nameof(AppSettings.VerticalItemsPlacement))
            {
                UpdateUIPanelOrientationDock();
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
                UpdateUIPanelOrientationDock();
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event for the UIPanelOrientationDock property.
        /// </summary>
        private void UpdateUIPanelOrientationDock()
        {
            this.RaisePropertyChanged(nameof(UIPanelOrientationDock));
        }
    }
}
