using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.ViewModels.ToolTips;

[DependencyInjection]
public sealed class MapLocationToolTipNotes :
    ViewModelCollection<IMapLocationToolTipMarkingVM, IMarking>, IMapLocationToolTipNotes
{
    private readonly IMapLocationToolTipMarkingVM.Factory _factory;

    public MapLocationToolTipNotes(IMapLocationToolTipMarkingVM.Factory factory, ILocation location)
        : base(location.Notes)
    {
        _factory = factory;
    }

    protected override IMapLocationToolTipMarkingVM CreateViewModel(IMarking model)
    {
        return _factory(model);
    }
}