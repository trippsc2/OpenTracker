using OpenTracker.Models.Accessibility;
using ReactiveUI;

namespace OpenTracker.Models.Dungeons.AccessibilityProvider
{
    public interface IBossAccessibilityProvider : IReactiveObject
    {
        AccessibilityLevel Accessibility { get; set; }

        delegate IBossAccessibilityProvider Factory();
    }
}