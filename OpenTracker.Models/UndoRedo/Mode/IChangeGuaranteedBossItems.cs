using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This interface contains the <see cref="IUndoable"/> action to change the <see cref="IMode.GuaranteedBossItems"/>
    /// property.
    /// </summary>
    public interface IChangeGuaranteedBossItems : IUndoable
    {
        /// <summary>
        /// A factory for creating new <see cref="IChangeGuaranteedBossItems"/> objects.
        /// </summary>
        /// <param name="newValue">
        ///     A <see cref="bool"/> representing the new <see cref="IMode.GuaranteedBossItems"/> value.
        /// </param>
        /// <returns>
        ///     A new <see cref="IChangeGuaranteedBossItems"/> object.
        /// </returns>
        delegate IChangeGuaranteedBossItems Factory(bool newValue);
    }
}