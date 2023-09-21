using OpenTracker.Models.Accessibility;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.Dungeons.AccessibilityProvider;

/// <summary>
/// This class contains the boss accessibility provider data.
/// </summary>
public sealed class BossAccessibilityProvider : ReactiveObject
{
    [Reactive]
    public AccessibilityLevel Accessibility { get; set; }
}