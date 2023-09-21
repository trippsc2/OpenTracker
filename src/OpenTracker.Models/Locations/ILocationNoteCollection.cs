using System.Collections.ObjectModel;
using OpenTracker.Models.Markings;
using OpenTracker.Utils;

namespace OpenTracker.Models.Locations;

/// <summary>
/// This interface contains the <see cref="ObservableCollection{T}"/> container of location notes.
/// </summary>
public interface ILocationNoteCollection : IObservableCollection<IMarking>
{
}