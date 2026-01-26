using OpenTracker.Utils.Dialog;

namespace OpenTracker.ViewModels.Capture.Design;

public class CaptureDesignDialogVM : DialogViewModelBase, ICaptureDesignDialogVM
{
    public ICaptureWindowCollection Windows { get; }

    public CaptureDesignDialogVM(ICaptureWindowCollection windows)
    {
        Windows = windows;
    }
}