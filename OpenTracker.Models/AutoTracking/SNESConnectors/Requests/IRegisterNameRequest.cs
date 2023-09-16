using System.Reactive;

namespace OpenTracker.Models.AutoTracking.SNESConnectors.Requests;

/// <summary>
/// This interface contains the request to register the app name. 
/// </summary>
public interface IRegisterNameRequest : IRequest<Unit>
{
    /// <summary>
    /// A factory for creating new <see cref="IRegisterNameRequest"/> objects.
    /// </summary>
    /// <returns>
    ///     A new <see cref="IRegisterNameRequest"/> object.
    /// </returns>
    delegate IRegisterNameRequest Factory();
}