using OpenTracker.Models.Modes;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.ViewModels.Dungeons;

/// <summary>
/// This class contains the vertical small items panel control ViewModel data.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class VerticalDungeonPanelVM : OrientedDungeonPanelVMBase, IVerticalDungeonPanelVM
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
    public VerticalDungeonPanelVM(IMode mode, IDungeonVMDictionary dungeonItems) : base(mode, dungeonItems)
    {
    }
}