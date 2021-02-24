using OpenTracker.Interfaces;
using OpenTracker.Models.Connections;
using OpenTracker.Utils;

namespace OpenTracker.ViewModels.Maps.Connections
{
    public interface IMapConnectionVM : IModelWrapper, IClickHandler, IPointerOver
    {
        delegate IMapConnectionVM Factory(IConnection connection);
    }
}
