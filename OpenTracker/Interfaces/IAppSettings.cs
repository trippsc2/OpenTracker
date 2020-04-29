using OpenTracker.Models.Enums;
using ReactiveUI;

namespace OpenTracker.Interfaces
{
    public interface IAppSettings : IReactiveObject
    {
        LayoutOrientation LayoutOrientation { get; }
        MapOrientation MapOrientation { get; }
        VerticalAlignment HorizontalUIPanelPlacement { get; }
        HorizontalAlignment VerticalUIPanelPlacement { get; }
        HorizontalAlignment HorizontalItemsPlacement { get; }
        VerticalAlignment VerticalItemsPlacement { get; }
    }
}
