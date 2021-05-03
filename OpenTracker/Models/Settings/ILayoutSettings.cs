using Avalonia.Controls;
using Avalonia.Layout;
using ReactiveUI;

namespace OpenTracker.Models.Settings
{
    /// <summary>
    /// This interface contains GUI layout settings data.
    /// </summary>
    public interface ILayoutSettings : IReactiveObject
    {
        bool AlwaysDisplayDungeonItems { get; set; }
        Orientation CurrentDynamicOrientation { get; set; }
        Orientation CurrentLayoutOrientation { get; }
        Orientation CurrentMapOrientation { get; }
        bool DisplayMapsCompasses { get; set; }
        Dock HorizontalItemsPlacement { get; set; }
        Dock HorizontalUIPanelPlacement { get; set; }
        Orientation? LayoutOrientation { get; set; }
        Orientation? MapOrientation { get; set; }
        double UIScale { get; set; }
        Dock VerticalItemsPlacement { get; set; }
        Dock VerticalUIPanelPlacement { get; set; }
    }
}