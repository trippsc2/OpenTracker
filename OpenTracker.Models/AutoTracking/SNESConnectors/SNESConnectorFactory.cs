using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking.SNESConnectors
{
    /// <summary>
    /// This class contains creation logic for the SNES connector.
    /// </summary>
    public class SNESConnectorFactory : ISNESConnectorFactory
    {
        private readonly SNESConnector.Factory _factory;
        private readonly NewSNESConnector.Factory _experimentalFactory;

        public event PropertyChangedEventHandler? PropertyChanged;

        private bool _experimental;
        public bool Experimental
        {
            get => _experimental;
            set
            {
                if (_experimental != value)
                {
                    _experimental = value;
                    OnPropertyChanged(nameof(Experimental));
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        /// An Autofac factory that creates SNES connectors.
        /// </param>
        /// <param name="experimentalFactory">
        /// An Autofac factory that creates experimental SNES connectors.
        /// </param>
        public SNESConnectorFactory(
            SNESConnector.Factory factory, NewSNESConnector.Factory experimentalFactory)
        {
            _factory = factory;
            _experimentalFactory = experimentalFactory;
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Returns a new SNES connector instance.
        /// </summary>
        /// <returns>
        /// A new SNES connector instance.
        /// </returns>
        public ISNESConnector GetConnector()
        {
            if (Experimental)
            {
                return _experimentalFactory();
            }

            return _factory();
        }
    }
}
