using OpenTracker.Models.Sections;

namespace OpenTracker.Models.UndoRedo.Sections
{
    /// <summary>
    /// This interface contains undoable action to uncollect a section.
    /// </summary>
    public interface IUncollectSection : IUndoable
    {
        delegate IUncollectSection Factory(ISection section);
    }
}