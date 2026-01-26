using OpenTracker.Models.Locations;

namespace OpenTracker.Models.UndoRedo.Locations;

/// <summary>
/// This interface contains the <see cref="IUndoable"/> action to clear a <see cref="ILocation"/>.
/// </summary>
public interface IClearLocation : IUndoable
{
    /// <summary>
    /// A factory for creating new <see cref="IClearLocation"/> objects.
    /// </summary>
    /// <param name="location">
    ///     The <see cref="ILocation"/>.
    /// </param>
    /// <param name="force">
    ///     A <see cref="bool"/> representing whether the accessibility logic should be obeyed.
    /// </param>
    /// <returns>
    ///     A new <see cref="IClearLocation"/> object.
    /// </returns>
    delegate IClearLocation Factory(ILocation location, bool force = false);
}