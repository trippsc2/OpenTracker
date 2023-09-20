using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using OpenTracker.ViewModels.AutoTracking;

namespace OpenTracker.ViewModels;

/// <summary>
/// This is the class for the status bar ViewModel.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class StatusBarVM : ViewModel
{
    public AutoTrackerStatusVM AutoTrackerStatus { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="autoTrackerStatus">
    /// The auto-tracker status.
    /// </param>
    public StatusBarVM(AutoTrackerStatusVM autoTrackerStatus)
    {
        AutoTrackerStatus = autoTrackerStatus;
    }
}