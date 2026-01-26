using System.Collections.Generic;
using OpenTracker.ViewModels.UIPanels;

namespace OpenTracker.ViewModels.Items;

/// <summary>
/// This interface contains the large item panel body control ViewModel data.
/// </summary>
public interface ILargeItemPanelVM : IUIPanelBodyVMBase
{
    delegate ILargeItemPanelVM Factory(List<ILargeItemVM> items);
}