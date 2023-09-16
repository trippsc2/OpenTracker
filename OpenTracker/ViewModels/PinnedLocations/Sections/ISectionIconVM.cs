using OpenTracker.Models.Sections;

namespace OpenTracker.ViewModels.PinnedLocations.Sections;

/// <summary>
/// This is the base class to type restrict the section icon control to valid ViewModel
/// classes.
/// </summary>
public interface ISectionIconVM
{
    delegate ISectionIconVM Factory(ISectionIconImageProvider imageProvider, ISection section, bool labelVisible);
}