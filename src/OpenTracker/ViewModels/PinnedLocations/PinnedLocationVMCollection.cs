using OpenTracker.Models.Locations;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.ViewModels.PinnedLocations;

/// <summary>
/// This is the class for the collection of pinned location ViewModels.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class PinnedLocationVMCollection : ViewModelCollection<IPinnedLocationVM, ILocation>,
    IPinnedLocationVMCollection
{
    private readonly IPinnedLocationDictionary _pinnedLocations;

    public PinnedLocationVMCollection(
        IPinnedLocationDictionary pinnedLocations, IPinnedLocationCollection model) : base(model)
    {
        _pinnedLocations = pinnedLocations;
    }

    protected override IPinnedLocationVM CreateViewModel(ILocation model)
    {
        return _pinnedLocations[model.ID];
    }
}