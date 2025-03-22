using OpenTracker.Models.Modes;

namespace OpenTracker.ViewModels.Dungeons
{
    /// <summary>
    /// This class contains the vertical small items panel control ViewModel data.
    /// </summary>
    public class VerticalDungeonPanelVM : OrientedDungeonPanelVMBase, IVerticalDungeonPanelVM
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
}
