using OpenTracker.Models.Enums;

namespace OpenTracker.Interfaces
{
    /// <summary>
    /// This is the interface to allow changing the marking of a section.
    /// </summary>
    public interface IChangeMarking
    {
        void ChangeMarking(MarkingType? marking);
    }
}
