using OpenTracker.Models.Modes;

namespace OpenTracker.ViewModels.Items.Small
{
    /// <summary>
    /// This class contains the vertical small items panel control ViewModel data.
    /// </summary>
    public class VerticalSmallItemPanelVM : SmallItemPanelVM, IVerticalSmallItemPanelVM
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings data.
        /// </param>
        /// <param name="factory">
        /// A factory for creating small items controls.
        /// </param>
        public VerticalSmallItemPanelVM(IMode mode, ISmallItemVMFactory factory) : base(mode, factory)
        {
        }
    }
}
