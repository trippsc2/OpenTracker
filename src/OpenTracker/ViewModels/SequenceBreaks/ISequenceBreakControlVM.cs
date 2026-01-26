using OpenTracker.Models.SequenceBreaks;

namespace OpenTracker.ViewModels.SequenceBreaks;

public interface ISequenceBreakControlVM
{
    delegate ISequenceBreakControlVM Factory(
        ISequenceBreak sequenceBreak, string text, string toolTipText);
}