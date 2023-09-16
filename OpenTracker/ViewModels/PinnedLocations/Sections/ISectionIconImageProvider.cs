using OpenTracker.Models.Sections;
using ReactiveUI;

namespace OpenTracker.ViewModels.PinnedLocations.Sections;

/// <summary>
/// This interface contains the logic for providing the section icon image.
/// </summary>
public interface ISectionIconImageProvider : IReactiveObject
{
    string ImageSource { get; }

    delegate ISectionIconImageProvider Factory(ISection section, string imageSourceBase);
}