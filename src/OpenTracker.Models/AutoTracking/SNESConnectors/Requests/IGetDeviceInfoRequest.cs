using System.Collections.Generic;

namespace OpenTracker.Models.AutoTracking.SNESConnectors.Requests;

/// <summary>
/// This interface contains the request to get device info. 
/// </summary>
public interface IGetDeviceInfoRequest : IRequest<IEnumerable<string>>
{
    /// <summary>
    /// A factory for creating new <see cref="IGetDeviceInfoRequest"/> objects.
    /// </summary>
    /// <returns>
    ///     A new <see cref="IGetDeviceInfoRequest"/> object.
    /// </returns>
    delegate IGetDeviceInfoRequest Factory();
}