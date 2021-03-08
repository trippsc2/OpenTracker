using OpenTracker.Utils;
using System.Collections.Generic;

namespace OpenTracker.ViewModels.Dropdowns
{
    /// <summary>
    /// This class contains the dropdown panel control ViewModel data.
    /// </summary>
    public class DropdownPanelVM : ViewModelBase, IDropdownPanelVM
    {
        public List<IDropdownVM> Dropdowns { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        /// The factory for creating new dropdown controls.
        /// </param>
        public DropdownPanelVM(IDropdownVMFactory factory)
        {
            Dropdowns = factory.GetDropdownVMs();
        }
    }
}
