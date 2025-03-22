using OpenTracker.Models.Dungeons.KeyDoors;

namespace OpenTracker.Models.Requirements.KeyDoor
{
    /// <summary>
    /// This interface contains <see cref="IKeyDoor"/> <see cref="IRequirement"/> data.
    /// </summary>
    public interface IKeyDoorRequirement : IRequirement
    {
        /// <summary>
        /// A factory for creating new <see cref="IKeyDoorRequirement"/> objects.
        /// </summary>
        /// <param name="keyDoor">
        ///     The <see cref="IKeyDoor"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="IKeyDoorRequirement"/> object.
        /// </returns>
        delegate IKeyDoorRequirement Factory(IKeyDoor keyDoor);
    }
}