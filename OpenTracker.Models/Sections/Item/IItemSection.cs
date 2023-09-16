using OpenTracker.Models.Accessibility;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Sections.Item;

/// <summary>
/// This interface contains item section data.
/// </summary>
public interface IItemSection : ISection
{
    /// <summary>
    /// A <see cref="int"/> representing the number of accessible items.
    /// </summary>
    int Accessible { get; }

    /// <summary>
    /// A factory for creating new <see cref="IItemSection"/> objects.
    /// </summary>
    /// <param name="name">
    ///     A <see cref="string"/> representing the section name.
    /// </param>
    /// <param name="node">
    ///     The <see cref="INode"/> to which this section belongs.
    /// </param>
    /// <param name="total">
    ///     A <see cref="int"/> representing the total number of items.
    /// </param>
    /// <param name="autoTrackValue">
    ///     The nullable <see cref="IAutoTrackValue"/>.
    /// </param>
    /// <param name="marking">
    ///     The nullable <see cref="IMarking"/>.
    /// </param>
    /// <param name="requirement">
    ///     The <see cref="IRequirement"/> for the section to be visible.
    /// </param>
    /// <param name="visibleNode">
    ///     The nullable <see cref="INode"/> that provides Inspect <see cref="AccessibilityLevel"/> for the section.
    /// </param>
    /// <returns>
    ///     A new <see cref="IItemSection"/>.
    /// </returns>
    delegate IItemSection Factory(
        string name, INode node, int total, IAutoTrackValue? autoTrackValue = null, IMarking? marking = null,
        IRequirement? requirement = null, INode? visibleNode = null);
}