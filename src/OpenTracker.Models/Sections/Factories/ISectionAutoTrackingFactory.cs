using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Locations;

namespace OpenTracker.Models.Sections.Factories;

/// <summary>
/// This interface containing creation logic for section <see cref="IAutoTrackValue"/> objects.
/// </summary>
public interface ISectionAutoTrackingFactory
{
    /// <summary>
    /// Returns the <see cref="IAutoTrackValue"/> for the specified <see cref="LocationID"/> and section index.
    /// </summary>
    /// <param name="id">
    ///     The <see cref="LocationID"/>.
    /// </param>
    /// <param name="sectionIndex">
    ///     A <see cref="int"/> representing the section index.
    /// </param>
    /// <returns>
    ///     The nullable <see cref="IAutoTrackValue"/>.
    /// </returns>
    IAutoTrackValue? GetAutoTrackValue(LocationID id, int sectionIndex = 0);
}