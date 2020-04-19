using OpenTracker.Utils;
using System.ComponentModel;

namespace OpenTracker.Interfaces
{
    public interface IAppSettingsVM : INotifyPropertyChanged
    {
        LayoutOrientation LayoutOrientation { get; }
        MapOrientation MapOrientation { get; }
        HorizontalItemsPlacement HorizontalItemsPlacement { get; }
        VerticalItemsPlacement VerticalItemsPlacement { get; }
    }
}
