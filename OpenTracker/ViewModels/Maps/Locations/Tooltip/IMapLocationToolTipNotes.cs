using OpenTracker.Models.Locations;
using OpenTracker.Utils;

namespace OpenTracker.ViewModels.Maps.Locations.Tooltip
{
    public interface IMapLocationToolTipNotes : IObservableCollection<IMapLocationToolTipMarkingVM>
    {
        delegate IMapLocationToolTipNotes Factory(ILocation location);
    }
}