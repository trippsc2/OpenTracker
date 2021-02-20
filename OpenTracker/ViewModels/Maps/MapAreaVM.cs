using Avalonia.Layout;
using OpenTracker.Models.Connections;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Maps.MapLocations;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace OpenTracker.ViewModels.Maps
{
    /// <summary>
    /// This is the ViewModel of the map area control.
    /// </summary>
    public class MapAreaVM : ViewModelBase
    {
        public static Orientation Orientation => 
            AppSettings.Instance.Layout.CurrentMapOrientation;

        public ObservableCollection<MapVM> Maps { get; }
        public ObservableCollection<MapConnectionVM> Connectors { get; } =
            new ObservableCollection<MapConnectionVM>();
        public ObservableCollection<MapLocationVMBase> MapLocations { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MapAreaVM()
        {
            Maps = MapAreaVMFactory.GetMapControlVMs();
            MapLocations = MapAreaVMFactory.GetMapLocationControlVMs();

            AppSettings.Instance.Layout.PropertyChanged += OnLayoutChanged;
            ConnectionCollection.Instance.CollectionChanged += OnConnectionsChanged;
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
            if (e.PropertyName == nameof(LayoutSettings.CurrentMapOrientation))
            {
                this.RaisePropertyChanged(nameof(Orientation));
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
                    Connectors.Add(new MapConnectionVM((Connection)item, this));
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (object item in e.OldItems)
                {
                    Connection connection = (Connection)item;

                    foreach (MapConnectionVM connector in Connectors)
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

                foreach (Connection connection in (ObservableCollection<Connection>)sender)
                {
                    Connectors.Add(new MapConnectionVM(connection, this));
                }
            }
        }
    }
}
