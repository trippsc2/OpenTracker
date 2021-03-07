using OpenTracker.Models.Connections;
using OpenTracker.Utils;

namespace OpenTracker.ViewModels.Maps.Connections
{
    public interface IMapConnectionVM : IModelWrapper
    {
        delegate IMapConnectionVM Factory(IConnection connection);
    }
}
