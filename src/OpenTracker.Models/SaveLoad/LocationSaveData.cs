using System.Collections.Generic;
using OpenTracker.Models.Markings;

namespace OpenTracker.Models.SaveLoad;

/// <summary>
/// This contains location save data.
/// </summary>
public class LocationSaveData
{
    public IList<SectionSaveData>? Sections { get; init; }
    public IList<MarkType?>? Markings { get; init; }
}