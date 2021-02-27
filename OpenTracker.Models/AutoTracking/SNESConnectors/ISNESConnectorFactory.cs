using System.ComponentModel;

namespace OpenTracker.Models.AutoTracking.SNESConnectors
{
    /// <summary>
    /// This interface contains creation logic for the SNES connector.
    /// </summary>
    public interface ISNESConnectorFactory : INotifyPropertyChanged
    {
        bool Experimental { get; set; }

        ISNESConnector GetConnector();
    }
}