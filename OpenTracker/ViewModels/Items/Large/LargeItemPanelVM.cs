using OpenTracker.Utils;
using System.Collections.Generic;

namespace OpenTracker.ViewModels.Items.Large
{
    /// <summary>
    /// This is the ViewModel for the large item panel control.
    /// </summary>
    public class LargeItemPanelVM : ViewModelBase, ILargeItemPanelVM
    {
        public List<ILargeItemVMBase> Items { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public LargeItemPanelVM(ILargeItemVMFactory factory)
        {
            Items = factory.GetLargeItemControlVMs();
        }
    }
}
