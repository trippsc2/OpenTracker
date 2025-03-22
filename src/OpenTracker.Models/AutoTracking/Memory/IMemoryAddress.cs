using ReactiveUI;

namespace OpenTracker.Models.AutoTracking.Memory
{
    /// <summary>
    /// This interface contains SNES memory address data.
    /// </summary>
    public interface IMemoryAddress : IReactiveObject
    {
        /// <summary>
        /// A nullable <see cref="byte"/> representing the value of the SNES memory address.
        /// </summary>
        byte? Value { get; set; }

        /// <summary>
        /// A factory for creating new <see cref="IMemoryAddress"/> objects.
        /// </summary>
        /// <returns>
        ///     A new <see cref="IMemoryAddress"/> object.
        /// </returns>
        delegate IMemoryAddress Factory();

        /// <summary>
        /// Resets the object to its starting value.
        /// </summary>
        void Reset();
    }
}