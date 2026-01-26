using OpenTracker.Models.Markings;

namespace OpenTracker.ViewModels.Markings.Images;

public interface IMarkingImageFactory
{
    IMarkingImageVMBase GetMarkingImageVM(MarkType type);
}