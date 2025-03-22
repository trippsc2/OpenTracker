using OpenTracker.Models.Sections;
using OpenTracker.Models.Sections.Boss;

namespace OpenTracker.Models.UndoRedo.Sections
{
    /// <summary>
    /// This interface contains the <see cref="IUndoable"/> action to toggle the <see cref="IPrizeSection"/>.
    /// </summary>
    public interface ITogglePrizeSection : IUndoable
    {
        /// <summary>
        /// A factory for creating new <see cref="ITogglePrizeSection"/> objects.
        /// </summary>
        /// <param name="section">
        ///     The <see cref="ISection"/>.
        /// </param>
        /// <param name="force">
        ///     A <see cref="bool"/> representing whether the accessibility logic should be obeyed.
        /// </param>
        /// <returns>
        ///     A new <see cref="ITogglePrizeSection"/> object.
        /// </returns>
        delegate ITogglePrizeSection Factory(ISection section, bool force);
    }
}