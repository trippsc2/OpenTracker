using OpenTracker.Models.Markings;
using OpenTracker.Utils;

namespace OpenTracker.ViewModels.Maps.Locations.Tooltip
{
    public interface IMapLocationToolTipMarkingVM : IModelWrapper
    {
        delegate IMapLocationToolTipMarkingVM Factory(IMarking marking);
    }
}