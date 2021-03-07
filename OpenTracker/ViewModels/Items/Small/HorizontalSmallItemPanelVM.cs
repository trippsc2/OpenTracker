using OpenTracker.Models.Modes;

namespace OpenTracker.ViewModels.Items.Small
{
    /// <summary>
    /// This class contains the horizontal small item panel control ViewModel data.
    /// </summary>
    public class HorizontalSmallItemPanelVM : SmallItemPanelVM, IHorizontalSmallItemPanelVM
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings data.
        /// </param>
        /// <param name="factory">
        /// The factory for creating small item controls.
        /// </param>
        public HorizontalSmallItemPanelVM(IMode mode, ISmallItemVMFactory factory) : base(mode, factory)
        {
        }
    }
}
