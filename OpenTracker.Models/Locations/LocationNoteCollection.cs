using System.Collections.ObjectModel;
using OpenTracker.Models.Markings;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Locations;

/// <summary>
/// This class contains the <see cref="ObservableCollection{T}"/> container of location notes.
/// </summary>
[DependencyInjection]
public sealed class LocationNoteCollection : ObservableCollection<IMarking>, ILocationNoteCollection
{
}