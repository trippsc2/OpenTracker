using OpenTracker.ViewModels.Capture;

namespace OpenTracker.ViewModels.Menus
{
    public interface ICaptureWindowMenuItemVM : IMenuItemVM
    {
         new delegate ICaptureWindowMenuItemVM Factory(ICaptureWindowVM captureWindow);
    }
}