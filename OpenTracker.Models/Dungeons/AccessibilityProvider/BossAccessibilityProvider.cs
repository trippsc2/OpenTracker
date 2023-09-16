using OpenTracker.Models.Accessibility;
using ReactiveUI;

namespace OpenTracker.Models.Dungeons.AccessibilityProvider;

/// <summary>
/// This class contains the boss accessibility provider data.
/// </summary>
public class BossAccessibilityProvider : ReactiveObject, IBossAccessibilityProvider
{
    private AccessibilityLevel _accessibility;
    public AccessibilityLevel Accessibility
    {
        get => _accessibility;
        set => this.RaiseAndSetIfChanged(ref _accessibility, value);
    }
}