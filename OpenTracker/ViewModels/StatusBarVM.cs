using OpenTracker.Autofac;
using OpenTracker.Utils;
using OpenTracker.ViewModels.AutoTracking;

namespace OpenTracker.ViewModels;

/// <summary>
/// This is the class for the status bar ViewModel.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class StatusBarVM : ViewModel, IStatusBarVM
{
    public IAutoTrackerStatusVM AutoTrackerStatus { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="autoTrackerStatus">
    /// The auto-tracker status.
    /// </param>
    public StatusBarVM(IAutoTrackerStatusVM autoTrackerStatus)
    {
        AutoTrackerStatus = autoTrackerStatus;
    }
}