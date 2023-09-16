using OpenTracker.Autofac;
using OpenTracker.Models.Modes;

namespace OpenTracker.ViewModels.Dungeons;

/// <summary>
/// This class contains the horizontal small item panel control ViewModel data.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class HorizontalDungeonPanelVM : OrientedDungeonPanelVMBase, IHorizontalDungeonPanelVM
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mode">
    /// The mode settings data.
    /// </param>
    /// <param name="dungeonItems">
    /// The dungeon items control dictionary.
    /// </param>
    public HorizontalDungeonPanelVM(IMode mode, IDungeonVMDictionary dungeonItems) : base(mode, dungeonItems)
    {
    }
}