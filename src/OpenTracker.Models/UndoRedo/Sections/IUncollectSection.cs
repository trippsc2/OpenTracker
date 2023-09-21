using OpenTracker.Models.Sections;

namespace OpenTracker.Models.UndoRedo.Sections;

/// <summary>
/// This interface contains the <see cref="IUndoable"/> action to uncollect a <see cref="ISection"/>.
/// </summary>
public interface IUncollectSection : IUndoable
{
    /// <summary>
    /// A factory for creating new <see cref="IUncollectSection"/> objects.
    /// </summary>
    /// <param name="section">
    ///     The <see cref="ISection"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="IUncollectSection"/> object.
    /// </returns>
    delegate IUncollectSection Factory(ISection section);
}