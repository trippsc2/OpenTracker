using OpenTracker.Models.Dungeons.Mutable;

namespace OpenTracker.Models.Dungeons.KeyDoors;

/// <summary>
/// This interface contains creation logic for <see cref="IKeyDoor"/> objects.
/// </summary>
public interface IKeyDoorFactory
{
    /// <summary>
    /// A factory for creating the <see cref="IKeyDoorFactory"/> object.
    /// </summary>
    /// <returns>
    ///     The <see cref="IKeyDoorFactory"/> object.
    /// </returns>
    delegate IKeyDoorFactory Factory();

    /// <summary>
    /// Returns a new <see cref="IKeyDoor"/> object for the specified <see cref="KeyDoorID"/>.
    /// </summary>
    /// <param name="id">
    ///     The <see cref="KeyDoorID"/>.
    /// </param>
    /// <param name="dungeonData">
    ///     The <see cref="IMutableDungeon"/> parent class.
    /// </param>
    /// <returns>
    ///     A new <see cref="IKeyDoor"/> object.
    /// </returns>
    IKeyDoor GetKeyDoor(KeyDoorID id, IMutableDungeon dungeonData);
}