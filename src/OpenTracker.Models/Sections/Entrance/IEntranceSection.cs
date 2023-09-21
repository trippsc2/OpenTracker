using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;

namespace OpenTracker.Models.Sections.Entrance;

/// <summary>
/// This interface contains entrance section data.
/// </summary>
public interface IEntranceSection : ISection
{
    /// <summary>
    /// A factory for creating new <see cref="IEntranceSection"/> objects.
    /// </summary>
    /// <param name="name">
    ///     A <see cref="string"/> representing the section name.
    /// </param>
    /// <param name="entranceShuffleLevel">
    ///     The minimum <see cref="EntranceShuffle"/> level.
    /// </param>
    /// <param name="requirement">
    ///     The <see cref="IRequirement"/> for the section to be active.
    /// </param>
    /// <param name="node">
    ///     The <see cref="INode"/> to which this section belongs.
    /// </param>
    /// <param name="exitProvided">
    ///     The <see cref="IOverworldNode"/> exit that this section provides.
    /// </param>
    /// <returns>
    ///     A new <see cref="IEntranceSection"/> object.
    /// </returns>
    delegate IEntranceSection Factory(
        string name, EntranceShuffle entranceShuffleLevel, IRequirement requirement, INode node,
        IOverworldNode? exitProvided = null);
}