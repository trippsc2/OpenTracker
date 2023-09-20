using OpenTracker.Models.Sections;
using OpenTracker.Utils;

namespace OpenTracker.ViewModels.PinnedLocations.Sections;

/// <summary>
/// This interface contains the logic for providing the section icon image.
/// </summary>
public interface ISectionIconImageProvider : IViewModel
{
    string ImageSource { get; }

    delegate ISectionIconImageProvider Factory(ISection section, string imageSourceBase);
}