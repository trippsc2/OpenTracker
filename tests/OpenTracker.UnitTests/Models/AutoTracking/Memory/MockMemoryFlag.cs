using System.Diagnostics.CodeAnalysis;
using OpenTracker.Models.AutoTracking.Memory;
using ReactiveUI;

namespace OpenTracker.UnitTests.Models.AutoTracking.Memory;

[ExcludeFromCodeCoverage]
public sealed class MockMemoryFlag : ReactiveObject, IMemoryFlag
{
    private bool? _status;
    public bool? Status
    {
        get => _status;
        set => this.RaiseAndSetIfChanged(ref _status, value);
    }
}