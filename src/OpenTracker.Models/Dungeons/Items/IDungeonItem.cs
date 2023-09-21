using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Nodes;

namespace OpenTracker.Models.Dungeons.Items;

/// <summary>
/// This interface contains mutable dungeon item data.
/// </summary>
public interface IDungeonItem : INotifyPropertyChanged
{
    /// <summary>
    /// The <see cref="AccessibilityLevel"/> of the dungeon item.
    /// </summary>
    AccessibilityLevel Accessibility { get; }

    /// <summary>
    /// A factory for creating new <see cref="IDungeonItem"/> objects.
    /// </summary>
    /// <param name="dungeonData">
    ///     The <see cref="IMutableDungeon"/> parent class.
    /// </param>
    /// <param name="node">
    ///     The <see cref="INode"/> to which this item belongs.
    /// </param>
    /// <returns>
    ///     A new <see cref="IDungeonItem"/> object.
    /// </returns>
    delegate IDungeonItem Factory(IMutableDungeon dungeonData, INode node);
}