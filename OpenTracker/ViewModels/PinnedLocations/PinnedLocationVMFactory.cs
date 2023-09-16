using System.Collections.Generic;
using OpenTracker.Models.Locations;
using OpenTracker.ViewModels.PinnedLocations.Notes;
using OpenTracker.ViewModels.PinnedLocations.Sections;

namespace OpenTracker.ViewModels.PinnedLocations;

/// <summary>
/// This is the class for creating pinned location control ViewModel classes.
/// </summary>
public class PinnedLocationVMFactory : IPinnedLocationVMFactory
{
    private readonly ISectionVMFactory _sectionFactory;
    private readonly IPinnedLocationNoteAreaVM.Factory _noteFactory;

    private readonly IPinnedLocationVM.Factory _factory;

    public PinnedLocationVMFactory(
        ISectionVMFactory sectionFactory, IPinnedLocationNoteAreaVM.Factory noteFactory,
        IPinnedLocationVM.Factory factory)
    {
        _sectionFactory = sectionFactory;
        _noteFactory = noteFactory;

        _factory = factory;
    }

    /// <summary>
    /// Returns an observable collection of section control ViewModel instances for the
    /// specified location.
    /// </summary>
    /// <param name="location">
    /// The location to be represented.
    /// </param>
    /// <returns>
    /// An observable collection of section control ViewModel instances.
    /// </returns>
    private List<ISectionVM> GetSections(ILocation location)
    {
        var sections = new List<ISectionVM>();

        foreach (var section in location.Sections)
        {
            sections.Add(_sectionFactory.GetSectionVM(section));
        }

        return sections;
    }

    /// <summary>
    /// Returns a new location control ViewModel instance.
    /// </summary>
    /// <param name="location">
    /// The location to be represented.
    /// </param>
    /// <returns>
    /// A new location control ViewModel instance.
    /// </returns>
    public IPinnedLocationVM GetLocationControlVM(ILocation location)
    {
        return _factory(location, GetSections(location), _noteFactory(location));
    }
}