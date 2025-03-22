using OpenTracker.Models.Sections;

namespace OpenTracker.Models.UndoRedo.Sections
{
    /// <summary>
    /// This interface contains the <see cref="IUndoable"/> action to collect a <see cref="ISection"/>.
    /// </summary>
    public interface ICollectSection : IUndoable
    {
        /// <summary>
        /// A factory for creating new <see cref="ICollectSection"/> objects.
        /// </summary>
        /// <param name="section">
        ///     The <see cref="ISection"/>.
        /// </param>
        /// <param name="force">
        ///     A <see cref="bool"/> representing whether the accessibility logic should be obeyed.
        /// </param>
        /// <returns>
        ///     A new <see cref="ICollectSection"/> object.
        /// </returns>
        delegate ICollectSection Factory(ISection section, bool force);
    }
}