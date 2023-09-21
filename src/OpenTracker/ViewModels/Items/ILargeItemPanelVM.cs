using System.Collections.Generic;
using OpenTracker.Utils;

namespace OpenTracker.ViewModels.Items;

/// <summary>
/// This interface contains the large item panel body control ViewModel data.
/// </summary>
public interface ILargeItemPanelVM : IViewModel
{
    delegate ILargeItemPanelVM Factory(List<ILargeItemVM> items);
}