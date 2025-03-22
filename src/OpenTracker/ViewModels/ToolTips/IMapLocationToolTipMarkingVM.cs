using OpenTracker.Models.Markings;
using OpenTracker.Utils;

namespace OpenTracker.ViewModels.ToolTips
{
    public interface IMapLocationToolTipMarkingVM : IModelWrapper
    {
        delegate IMapLocationToolTipMarkingVM Factory(IMarking marking);
    }
}