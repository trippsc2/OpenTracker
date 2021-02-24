using OpenTracker.Models.Modes;

namespace OpenTracker.ViewModels.Items.Small
{
    /// <summary>
    /// This is the ViewModel for the vertical small item panel control.
    /// </summary>
    public class VerticalSmallItemPanelVM : SmallItemPanelVM, IVerticalSmallItemPanelVM
    {
        public VerticalSmallItemPanelVM(IMode mode, ISmallItemVMFactory factory) : base(mode, factory)
        {
        }
    }
}
