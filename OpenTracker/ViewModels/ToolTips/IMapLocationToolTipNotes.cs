using OpenTracker.Models.Locations;
using OpenTracker.Utils;

namespace OpenTracker.ViewModels.ToolTips;

public interface IMapLocationToolTipNotes : IObservableCollection<IMapLocationToolTipMarkingVM>
{
    delegate IMapLocationToolTipNotes Factory(ILocation location);
}