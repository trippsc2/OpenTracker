using OpenTracker.Models.Accessibility;
using ReactiveUI;

namespace OpenTracker.Models.Dungeons.AccessibilityProvider
{
    public class BossAccessibilityProvider : ReactiveObject, IBossAccessibilityProvider
    {
        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            set => this.RaiseAndSetIfChanged(ref _accessibility, value);
        }
    }
}