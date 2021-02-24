using OpenTracker.Models.Modes;

namespace OpenTracker.ViewModels.Items.Small
{
    /// <summary>
    /// This is the ViewModel for the horizontal small item panel control.
    /// </summary>
    public class HorizontalSmallItemPanelVM : SmallItemPanelVM, IHorizontalSmallItemPanelVM
    {
        public HorizontalSmallItemPanelVM(IMode mode, ISmallItemVMFactory factory) : base(mode, factory)
        {
        }
    }
}
