using OpenTracker.Models.Enums;

namespace OpenTracker.Interfaces
{
    public interface IChangeMarking
    {
        void ChangeMarking(MarkingType? marking);
    }
}
