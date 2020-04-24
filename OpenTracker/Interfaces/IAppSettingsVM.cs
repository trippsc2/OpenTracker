using OpenTracker.Utils;
using System.ComponentModel;

namespace OpenTracker.Interfaces
{
    public interface IAppSettingsVM : INotifyPropertyChanged
    {
        LayoutOrientation LayoutOrientation { get; }
        MapOrientation MapOrientation { get; }
        VerticalAlignment HorizontalUIPanelPlacement { get; }
        HorizontalAlignment VerticalUIPanelPlacement { get; }
        HorizontalAlignment HorizontalItemsPlacement { get; }
        VerticalAlignment VerticalItemsPlacement { get; }
    }
}
