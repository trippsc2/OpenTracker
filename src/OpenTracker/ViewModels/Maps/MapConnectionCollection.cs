using OpenTracker.Models.Locations.Map.Connections;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.ViewModels.Maps;

[DependencyInjection]
public sealed class MapConnectionCollection : ViewModelCollection<IMapConnectionVM, IMapConnection>,
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