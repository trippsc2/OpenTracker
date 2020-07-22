using Avalonia.Layout;
using OpenTracker.Models;
using OpenTracker.Models.Locations;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace OpenTracker.ViewModels.MapAreaControls
{
    /// <summary>
    /// This is the ViewModel of the map area control.
    /// </summary>
    public class MapAreaControlVM : ViewModelBase
    {
        private readonly MainWindowVM _mainWindow;

        public Orientation MapPanelOrientation => 
            AppSettings.Instance.MapOrientation ?? _mainWindow.Orientation;

        public ObservableCollection<MapControlVM> Maps { get; }
        public ObservableCollection<ConnectionControlVM> Connectors { get; } =
            new ObservableCollection<ConnectionControlVM>();
        public ObservableCollection<MapLocationControlVMBase> MapLocations { get; }

        public MapAreaControlVM(MainWindowVM mainWindow)
        {
            _mainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));
            Maps = MapAreaControlVMFactory.GetMapControlVMs(this);
            MapLocations = MapAreaControlVMFactory.GetMapLocationControlVMs(
                this, mainWindow.LocationsPanel.Locations);

            AppSettings.Instance.PropertyChanged += OnAppSettingsChanged;
            _mainWindow.PropertyChanged += OnMainWindowChanged;
            ConnectionCollection.Instance.CollectionChanged += OnConnectionsChanged;
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
            if (e.PropertyName == nameof(AppSettings.MapOrientation))
            {
                UpdateMapOrientation();
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
            if (e.PropertyName == nameof(MainWindowVM.Orientation))
            {
                UpdateMapOrientation();
            }
        }

        /// <summary>
        /// Subscribes to the CollectionChanged event on the ObservableCollection of map entrance
        /// connections.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnConnectionsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (object item in e.NewItems)
                {
                    Connectors.Add(new ConnectionControlVM(((MapLocation, MapLocation))item, this));
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (object item in e.OldItems)
                {
                    (MapLocation, MapLocation) connection = ((MapLocation, MapLocation))item;

                    foreach (ConnectionControlVM connector in Connectors)
                    {
                        if (connector.Connection == connection)
                        {
                            Connectors.Remove(connector);
                            break;
                        }
                    }
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                Connectors.Clear();

                foreach ((MapLocation, MapLocation) connection in
                    (ObservableCollection<(MapLocation, MapLocation)>)sender)
                {
                    Connectors.Add(new ConnectionControlVM(connection, this));
                }
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event for the MapPanelOrientation property.
        /// </summary>
        private void UpdateMapOrientation()
        {
            this.RaisePropertyChanged(nameof(MapPanelOrientation));
        }
    }
}
