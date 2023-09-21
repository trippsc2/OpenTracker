using System.Diagnostics.CodeAnalysis;
using OpenTracker.Models.AutoTracking.Memory;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.UnitTests.Models.AutoTracking.Memory;

[ExcludeFromCodeCoverage]
public sealed class MockMemoryFlag : ReactiveObject, IMemoryFlag
{
    [Reactive]
    public bool? Status { get; set; }
}