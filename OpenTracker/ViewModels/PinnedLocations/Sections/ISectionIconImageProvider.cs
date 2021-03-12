using System.ComponentModel;
using OpenTracker.Models.Sections;

namespace OpenTracker.ViewModels.PinnedLocations.Sections
{
    /// <summary>
    /// This interface contains the logic for providing the section icon image.
    /// </summary>
    public interface ISectionIconImageProvider : INotifyPropertyChanged
    {
        string ImageSource { get; }

        delegate ISectionIconImageProvider Factory(ISection section, string imageSourceBase);
    }
}