using OpenTracker.Models.Locations.Map.Connections;
using OpenTracker.Utils;

namespace OpenTracker.ViewModels.Maps
{
    public interface IMapConnectionVM : IModelWrapper
    {
        delegate IMapConnectionVM Factory(IMapConnection connection);
    }
}
