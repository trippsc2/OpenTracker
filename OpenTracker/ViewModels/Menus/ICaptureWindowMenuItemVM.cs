using OpenTracker.ViewModels.Capture;

namespace OpenTracker.ViewModels.Menus
{
    public interface ICaptureWindowMenuItemVM : IMenuItemVM
    {
         delegate ICaptureWindowMenuItemVM Factory(ICaptureWindowVM captureWindow);
    }
}