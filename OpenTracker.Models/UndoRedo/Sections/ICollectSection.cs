using OpenTracker.Models.Sections;

namespace OpenTracker.Models.UndoRedo.Sections
{
    /// <summary>
    /// This interface contains undoable action data to collect an item/entrance from a location section.
    /// </summary>
    public interface ICollectSection : IUndoable
    {
        delegate ICollectSection Factory(ISection section, bool force);
    }
}