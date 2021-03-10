using OpenTracker.Models.Connections;
using OpenTracker.Utils;

namespace OpenTracker.ViewModels.Maps
{
    public interface IMapConnectionVM : IModelWrapper
    {
        delegate IMapConnectionVM Factory(IConnection connection);
    }
}
