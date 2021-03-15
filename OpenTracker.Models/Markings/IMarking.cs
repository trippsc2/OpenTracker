using ReactiveUI;

namespace OpenTracker.Models.Markings
{
    /// <summary>
    /// This interface contains marking data.
    /// </summary>
    public interface IMarking : IReactiveObject
    {
        MarkType Mark { get; set; }

        delegate IMarking Factory();
    }
}