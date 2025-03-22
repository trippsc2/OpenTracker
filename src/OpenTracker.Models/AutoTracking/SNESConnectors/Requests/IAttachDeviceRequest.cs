using System.Reactive;

namespace OpenTracker.Models.AutoTracking.SNESConnectors.Requests
{
    /// <summary>
    /// This interface contains the request to attach a device. 
    /// </summary>
    public interface IAttachDeviceRequest : IRequest<Unit>
    {
        /// <summary>
        /// A factory for creating new <see cref="IAttachDeviceRequest"/> objects.
        /// </summary>
        /// <returns>
        ///     A new <see cref="IAttachDeviceRequest"/> object.
        /// </returns>
        delegate IAttachDeviceRequest Factory(string device);
    }
}