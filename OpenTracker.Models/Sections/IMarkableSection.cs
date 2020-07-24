using OpenTracker.Models.Markings;

namespace OpenTracker.Models.Sections
{
    public interface IMarkableSection : ISection
    {
        IMarking Marking { get; }
    }
}
