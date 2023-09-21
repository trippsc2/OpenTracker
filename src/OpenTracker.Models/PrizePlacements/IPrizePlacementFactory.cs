namespace OpenTracker.Models.PrizePlacements;

/// <summary>
/// This interface contains the creation logic for <see cref="IPrizePlacement"/> objects.
/// </summary>
public interface IPrizePlacementFactory
{
    /// <summary>
    /// A factory for creating the <see cref="IPrizePlacementFactory"/> object.
    /// </summary>
    /// <returns>
    ///     The <see cref="IPrizePlacementFactory"/> object.
    /// </returns>
    delegate IPrizePlacementFactory Factory();

    /// <summary>
    /// Returns a new <see cref="IPrizePlacement"/> object for the specified <see cref="PrizePlacementID"/>.
    /// </summary>
    /// <param name="id">
    ///     The <see cref="PrizePlacementID"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="IPrizePlacement"/> object.
    /// </returns>
    IPrizePlacement GetPrizePlacement(PrizePlacementID id);
}