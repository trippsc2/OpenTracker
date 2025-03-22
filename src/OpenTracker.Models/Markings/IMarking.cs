using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Markings;
using ReactiveUI;

namespace OpenTracker.Models.Markings
{
    /// <summary>
    /// This interface contains marking data.
    /// </summary>
    public interface IMarking : IReactiveObject
    {
        /// <summary>
        /// The current <see cref="MarkType"/> of the marking.
        /// </summary>
        MarkType Mark { get; set; }

        /// <summary>
        /// A factory for creating new <see cref="IMarking"/> objects.
        /// </summary>
        /// <returns>
        ///     A new <see cref="IMarking"/> object.
        /// </returns>
        delegate IMarking Factory();

        /// <summary>
        /// Returns a new <see cref="IChangeMarking"/> object.
        /// </summary>
        /// <param name="newMarking">
        ///     The new <see cref="MarkType"/> value.
        /// </param>
        /// <returns>
        ///     A new <see cref="IChangeMarking"/> object.
        /// </returns>
        IUndoable CreateChangeMarkingAction(MarkType newMarking);
    }
}