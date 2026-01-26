using System;

namespace OpenTracker.ViewModels.Capture.Design;

public class CaptureDesignFactory
{
    private readonly CaptureWindowDesignVM.Factory _windowFactory;

    public CaptureDesignFactory(CaptureWindowDesignVM.Factory windowFactory)
    {
        _windowFactory = windowFactory;
    }
        
    public ICaptureDesignVM GetDesignVM(ICaptureControlVM control)
    {
        return control switch
        {
            ICaptureWindowVM captureWindow => _windowFactory(captureWindow),
            _ => throw new ArgumentOutOfRangeException(nameof(control))
        };
    }
}