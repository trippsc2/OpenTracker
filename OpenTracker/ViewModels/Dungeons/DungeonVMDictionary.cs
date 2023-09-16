using System.Collections.Generic;
using OpenTracker.Autofac;
using OpenTracker.Models.Locations;
using OpenTracker.Utils;

namespace OpenTracker.ViewModels.Dungeons;

[DependencyInjection(SingleInstance = true)]
public sealed class DungeonVMDictionary : LazyDictionary<LocationID, List<IDungeonItemVM>>, IDungeonVMDictionary
{
    private readonly IDungeonVMFactory _factory;
        
    public DungeonVMDictionary(IDungeonVMFactory factory) : base(new Dictionary<LocationID, List<IDungeonItemVM>>())
    {
        _factory = factory;
    }

    protected override List<IDungeonItemVM> Create(LocationID key)
    {
        return _factory.GetDungeonItemVMs(key);
    }
}