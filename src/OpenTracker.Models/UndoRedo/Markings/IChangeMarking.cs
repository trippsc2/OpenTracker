using OpenTracker.Models.Markings;

namespace OpenTracker.Models.UndoRedo.Markings;

/// <summary>
/// This interface contains undoable action data to set a marking.
/// </summary>
public interface IChangeMarking : IUndoable
{
    /// <summary>
    /// A factory for creating new <see cref="IChangeMarking"/> objects.
    /// </summary>
    /// <param name="marking">
    ///     The <see cref="IMarking"/>.
    /// </param>
    /// <param name="newValue">
    ///     The new <see cref="MarkType"/> value.
    /// </param>
    /// <returns>
    ///     A new <see cref="IChangeMarking"/> object.
    /// </returns>
    delegate IChangeMarking Factory(IMarking marking, MarkType newValue);
}