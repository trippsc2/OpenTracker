using System.Windows.Input;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Capture;

namespace OpenTracker.ViewModels.Menus
{
    public interface ICaptureWindowMenuItemVM : IMenuItemVM
    {
        new delegate ICaptureWindowMenuItemVM Factory(ICaptureWindowVM captureWindow);
    }
}