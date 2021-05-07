namespace OpenTracker.Models.AutoTracking.SNESConnectors.Requests
{
    /// <summary>
    /// This interface contains the request to read a sequence of memory addresses. 
    /// </summary>
    public interface IReadMemoryRequest : IRequest<byte[]>
    {
        /// <summary>
        /// A factory for creating new <see cref="IReadMemoryRequest"/> objects.
        /// </summary>
        /// <returns>
        ///     A new <see cref="IReadMemoryRequest"/> object.
        /// </returns>
        delegate IReadMemoryRequest Factory(ulong address, int bytesToRead);
    }
}