using System.Diagnostics.CodeAnalysis;
using OpenTracker.Models.AutoTracking.Values;
using ReactiveUI;

namespace OpenTracker.UnitTests.Models.AutoTracking.Values;

[ExcludeFromCodeCoverage]
public sealed class MockAutoTrackValue : ReactiveObject, IAutoTrackValue
{
    private int? _currentValue;
    public int? CurrentValue
    {
        get => _currentValue;
        set => this.RaiseAndSetIfChanged(ref _currentValue, value);
    }
}