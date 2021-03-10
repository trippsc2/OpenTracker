using OpenTracker.Models.Locations;
using OpenTracker.Utils;

namespace OpenTracker.ViewModels.Tooltips
{
    public interface IMapLocationToolTipNotes : IObservableCollection<IMapLocationToolTipMarkingVM>
    {
        delegate IMapLocationToolTipNotes Factory(ILocation location);
    }
}