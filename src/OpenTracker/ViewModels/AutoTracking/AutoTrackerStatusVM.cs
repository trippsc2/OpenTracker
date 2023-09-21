using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.AutoTracking.SNESConnectors;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.AutoTracking;

/// <summary>
/// This class contains the auto-tracker status text control ViewModel data.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class AutoTrackerStatusVM : ViewModel
{
    private static readonly Dictionary<ConnectionStatus, string> StatusTextColorValues = new()
    {
        { ConnectionStatus.NotConnected, "#ffffff" },
        { ConnectionStatus.SelectDevice, "#ffffff" },
        { ConnectionStatus.Connecting, "#ffff00" },
        { ConnectionStatus.Attaching, "#ffff00" },
        { ConnectionStatus.Connected, "#00ff00" },
        { ConnectionStatus.Error, "#ff3030" }
    };
    private static readonly Dictionary<(ConnectionStatus status, bool raceIllegal), string> StatusTextValues = new()
    {
        { (ConnectionStatus.NotConnected, false), "NOT CONNECTED" },
        { (ConnectionStatus.NotConnected, true), "NOT CONNECTED" },
        { (ConnectionStatus.SelectDevice, false), "SELECT DEVICE" },
        { (ConnectionStatus.SelectDevice, true), "SELECT DEVICE" },
        { (ConnectionStatus.Connecting, false), "CONNECTING" },
        { (ConnectionStatus.Connecting, true), "CONNECTING" },
        { (ConnectionStatus.Attaching, false), "ATTACHING" },
        { (ConnectionStatus.Attaching, true), "ATTACHING" },
        { (ConnectionStatus.Connected, false), "CONNECTED (RACE ILLEGAL)" },
        { (ConnectionStatus.Connected, true), "#CONNECTED (RACE LEGAL)" },
        { (ConnectionStatus.Error, false), "ERROR" },
        { (ConnectionStatus.Error, true), "ERROR" }
    };
    
    private IAutoTracker AutoTracker { get; }

    [ObservableAsProperty]
    public string StatusTextColor { get; } = "#ffffff";
    [ObservableAsProperty]
    public string StatusText { get; } = "NOT CONNECTED";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="autoTracker">
    /// The auto-tracker data.
    /// </param>
    public AutoTrackerStatusVM(IAutoTracker autoTracker)
    {
        AutoTracker = autoTracker;
        
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.AutoTracker.Status)
                .Select(x => StatusTextColorValues[x])
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.StatusTextColor)
                .DisposeWith(disposables);
            this.WhenAnyValue(
                    x => x.AutoTracker.Status,
                    x => x.AutoTracker.RaceIllegalTracking)
                .Select(x => StatusTextValues[x])
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.StatusText)
                .DisposeWith(disposables);
        });
    }
}