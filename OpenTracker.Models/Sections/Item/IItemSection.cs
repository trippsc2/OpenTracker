using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Sections.Item
{
    /// <summary>
    ///     This interface contains item section data.
    /// </summary>
    public interface IItemSection : ISection
    {
        /// <summary>
        ///     A 32-bit signed integer representing the number of accessible items.
        /// </summary>
        int Accessible { get; }

        /// <summary>
        ///     A factory for creating new item sections.
        /// </summary>
        /// <param name="name">
        ///     A string representing the name of the section.
        /// </param>
        /// <param name="node">
        ///     The requirement node to which this section belongs.
        /// </param>
        /// <param name="total">
        ///     A 32-bit signed integer representing the total number of items.
        /// </param>
        /// <param name="autoTrackValue">
        ///     The auto-tracking value for this section.
        /// </param>
        /// <param name="marking">
        ///     The section marking.
        /// </param>
        /// <param name="requirement">
        ///     The requirement for the section to be visible.
        /// </param>
        /// <param name="visibleNode">
        ///     The node that provides Inspect accessibility for this section.
        /// </param>
        /// <returns>
        ///     A new item section.
        /// </returns>
        delegate IItemSection Factory(
            string name, INode node, int total, IAutoTrackValue? autoTrackValue = null, IMarking? marking = null,
            IRequirement? requirement = null, INode? visibleNode = null);
    }
}
