using OpenTracker.Models.AutoTracking.Values;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.UnitTests.Models.AutoTracking.Values;

public sealed class MockAutoTrackValue : ReactiveObject, IAutoTrackValue
{
    [Reactive]
    public int? CurrentValue { get; set; }
}