using OpenTracker.Models.UndoRedo;
using ReactiveUI;

namespace OpenTracker.Models.Markings
{
    /// <summary>
    ///     This interface contains marking data.
    /// </summary>
    public interface IMarking : IReactiveObject
    {
        /// <summary>
        ///     The current mark type of this marking.
        /// </summary>
        MarkType Mark { get; set; }

        /// <summary>
        ///     A factory for creating new markings.
        /// </summary>
        /// <returns>
        ///     A new marking.
        /// </returns>
        delegate IMarking Factory();

        /// <summary>
        ///     Returns a new undoable action to change the marking.
        /// </summary>
        /// <param name="newMarking">
        ///     The new marking value.
        /// </param>
        /// <returns>
        ///     A new undoable action to change the marking.
        /// </returns>
        IUndoable CreateChangeMarkingAction(MarkType newMarking);
    }
}