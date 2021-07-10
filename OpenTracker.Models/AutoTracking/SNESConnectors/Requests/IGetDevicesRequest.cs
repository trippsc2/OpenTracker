using System.Collections.Generic;

namespace OpenTracker.Models.AutoTracking.SNESConnectors.Requests
{
    /// <summary>
    /// This interface contains the request to get the list of devices. 
    /// </summary>
    public interface IGetDevicesRequest : IRequest<IEnumerable<string>>
    {
        /// <summary>
        /// A factory for creating new <see cref="IGetDevicesRequest"/> objects.
        /// </summary>
        /// <returns>
        ///     A new <see cref="IGetDevicesRequest"/> object.
        /// </returns>
        delegate IGetDevicesRequest Factory();
    }
}