using OpenTracker.Models.Locations.Map.Connections;
using OpenTracker.Utils;

namespace OpenTracker.ViewModels.Maps;

public class MapConnectionCollection : ViewModelCollection<IMapConnectionVM, IMapConnection>,
    IMapConnectionCollection
{
    private readonly IMapConnectionVM.Factory _factory;

    public MapConnectionCollection(
        IMapConnectionVM.Factory factory, Models.Locations.Map.Connections.IMapConnectionCollection model) : base(model)
    {
        _factory = factory;
    }

    protected override IMapConnectionVM CreateViewModel(IMapConnection model)
    {
        return _factory(model);
    }
}