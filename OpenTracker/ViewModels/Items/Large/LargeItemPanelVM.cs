using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels.Items.Large
{
    /// <summary>
    /// This is the ViewModel for the large item panel control.
    /// </summary>
    public class LargeItemPanelVM : ViewModelBase
    {
        public ObservableCollection<LargeItemVMBase> Items { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public LargeItemPanelVM()
        {
            Items = LargeItemControlVMFactory.GetLargeItemControlVMs();
        }
    }
}
